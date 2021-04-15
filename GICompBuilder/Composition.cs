using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICompBuilder
{
    public class Composition : IComparable
    {
        public const int SIZE = 4;

        public Character[] Characters { get; private set; }
        public double Rating { get; private set; }

        public Composition(Character[] composition, RatingDB rater)
        {
            // Validation
            if (composition.Length != SIZE)
            {
                Console.WriteLine($"[Composition] Error - Invalid size of comp. Expected: {SIZE} Actual: {composition.Length}");
            }

            Characters = composition;

            Rating = rater.RateComp(composition);
        }

        public int CompareTo(object obj)
        {
            return Rating.CompareTo(((Composition)obj).Rating);
        }

        public override string ToString()
        {
            return Utility.CharacterListToString(Characters) + $"({Rating})";
        }
    }
}
