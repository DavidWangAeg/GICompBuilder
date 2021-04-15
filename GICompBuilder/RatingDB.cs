using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICompBuilder
{
    public class RatingDB
    {
        private const int COMP_SIZE = 4;
        private const double RATING_WEIGHT = 1.0;
        private const double SYNERGY_WEIGHT = 1.0;

        private double[] _charRating;
        private double[,] _synergyMatrix;

        private double[] _charRatingNorm;
        private double[,] _synergyMatrixNorm;

        private int _n;

        public RatingDB(double[] charRating, double ratingMin, double ratingMax, double[,] synergyMatrix, double synergyMin, double synergyMax)
        {
            _charRating = charRating;
            _synergyMatrix = synergyMatrix;

            _n = charRating.Length;            
            if (synergyMatrix.GetLength(0) != _n || synergyMatrix.GetLength(1) != _n)
            {
                Console.WriteLine($"[RatingDB] Error - Invalid size of matrix. Expected: {_n} Actual: {synergyMatrix.GetLength(0)}x{synergyMatrix.GetLength(1)}");
                return;
            }

            // Normalize ratings
            double diff = ratingMax - ratingMin;

            _charRatingNorm = _charRating.Select(s => (s - ratingMin) / diff).ToArray();

            // Normalize synergy matrix
            diff = synergyMax - synergyMin;
            _synergyMatrixNorm = new double[_n, _n];

            for (int i = 0; i < _n; ++i)
            {
                for (int j = 0; j < _n; ++j)
                {
                    if (i == j)
                    {
                        _synergyMatrixNorm[i, j] = -1;
                    }
                    else if (j > i)
                    {
                        double value = -1;

                        // Normalize valid entries
                        if (synergyMatrix[i, j] >= 0)
                        {
                            value = (synergyMatrix[i, j] - synergyMin) / diff;
                        }

                        _synergyMatrixNorm[i, j] = value;
                        _synergyMatrixNorm[j, i] = value;
                    }
                }
            }
        }

        public double RateComp(Character[] comp)
        {
            if (comp.Length != COMP_SIZE)
            {
                Console.WriteLine($"[RatingDB.RateComp] Error - Invalid size of comp. Expected: {COMP_SIZE} Actual: {comp.Length}");
                return -1;
            }

            // Add individual chars
            double rating = 0;
            for (int i = 0; i < COMP_SIZE; ++i)
            {
                rating += _charRating[(int)comp[i]];
            }

            rating *= RATING_WEIGHT;

            double synergy = 0;
            List<Character[]> pairs = Utility.GenerateAllPermutations(comp, 2);

            for (int i = 0; i < pairs.Count; ++i)
            {
                synergy += GetSynergy(pairs[i][0], pairs[i][1]);
            }

            synergy *= SYNERGY_WEIGHT;

            // Console.WriteLine($"[RatingDB.GetSynergy] Info - {Utility.CharacterListToString(comp)} - Rating: {rating} Synergy: {synergy} Total: {rating + synergy}");

            return rating + synergy;
        }

        public double GetSynergy(Character a, Character b)
        {
            int i = (int)a, j = (int)b;

            if (i >= _n || j >= _n)
            {
                Console.WriteLine($"[RatingDB.GetSynergy] Error - Character is out of range ({_n}). Received: {a} ({(int)a}), {b} ({(int)b})");
                return -1;
            }

            double val = _synergyMatrixNorm[i, j];
            if (val < 0)
            {
                Console.WriteLine($"[RatingDB.GetSynergy] Error - Invalid synergy value {val} found between {a} and {b}.");
                return -1;
            }

            return val;
        }

        public string DumpRatings()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[Ratings]");

            for (int i = 0; i < _n; ++i)
            {
                sb.AppendLine($"{(Character)i} - {_charRatingNorm[i]}");
            }

            sb.AppendLine("[Synergy]");
            
            for (int i = 0; i < _n; ++i)
            {
                StringBuilder line = new StringBuilder();
                line.Append($"[{(Character)i}]");

                for (int j = 0; j < _n; ++j)
                {
                    line.Append(String.Format("{0,4:0.0}", _synergyMatrixNorm[i, j]));
                }

                sb.AppendLine(line.ToString());
            }

            return sb.ToString();
        }
    }
}
