using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICompBuilder
{
    class Program
    {
        private static double[] CharacterRating =
        {
            3,
            9.6,
            7,
            7.8,
            7.2,
            7.6,
            4.1,
            4.7,
            7.8,
            6.7,
            9.4,
            5.4,
            7.2,
            8.8,
            6.1,
            3.8,
            5.2,
            5.6,
            7.2,
            5.3,
            3.2,
            5.2,
            8.3,
            8.3,
            4.7,
            9.3,
            7.5,
            7.7,
            6.7,
            5.5,
            4.7,
            9.6
        };

        public static double[,] SynergyMatrix = new double[,]
{
            {-1,2,2,2,2,2,2,1,1,1,1,0,1,1,1,1,1,1,1,1,1,1,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,3,2,3,2,2,0,2,2,2,3,0,1,1,0,1,2,2,2,1,1,2,2,2,1,1,1,2,1,1,1},
            {-1,-1,-1,0,0,2,2,2,3,0,3,2,2,2,2,2,2,2,2,0,2,0,2,3,2,2,0,2,1,1,1,1},
            {-1,-1,-1,-1,0,2,2,1,3,0,3,0,2,2,2,0,2,2,2,0,2,0,2,3,2,1,0,2,0,2,1,2},
            {-1,-1,-1,-1,-1,2,2,1,3,0,3,0,2,2,1,1,2,1,2,0,2,0,2,3,2,3,0,2,0,1,1,1},
            {-1,-1,-1,-1,-1,-1,2,1,2,3,3,3,2,3,3,1,2,2,2,2,1,2,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,1,1,1,1,0,1,1,1,1,1,2,2,2,2,3,1,1,1,1,2,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,1,1,1,1,0,1,1,0,1,1,1,1,1,2,0,1,1,1,1,1,1,0,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,1,2,1,1,3,2,1,1,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,3,1,2,3,3,2,2,3,3,0,2,0,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,3,2,2,2,1,2,3,3,3,2,3,2,3,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,1,3,1,3,1,1,1,1,0,2,2,2,2,0,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2,0,2,1,1,2,1,3,1,1,1,1,2,1,1,0,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2,2,1,1,1,1,3,2,2,2,3,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,1,1,1,1,1,3,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,1,1,2,1,3,0,1,1,1,2,1,1,0,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,1,1,1,3,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,1,1,1,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2,2,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,0,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,2,2,2,2,1,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,1,1,0,0,2,0,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2,3,3,1,1,0,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,1,2,3,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2,1,1,1,-1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,1,1,1,1,1},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,0,2,0,2},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,3,2,2,2},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,3,3},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2,2},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,2},
            {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1}
        };

        static void Main(string[] args)
        {
            RatingDB db = new RatingDB(CharacterRating, 1, 10, SynergyMatrix, 0, 3);

            // Console.WriteLine(db.DumpRatings());

            //Character[] test = { Character.Diluc, Character.Xingqiu, Character.Traveller_Anemo, Character.Traveller_Geo };

            //List<Character[]> pairs = Utility.GenerateAllPermutations(test, 2);

            //foreach (Character[] pair in pairs)
            //{
            //    Console.WriteLine(Utility.CompToString(pair));
            //}

            Character[] seedChars = { Character.Diluc, Character.HuTao};


            Character[] otherChars = Utility.GenerateListExcluding(seedChars);
            Console.WriteLine(Utility.CharacterListToString(otherChars));

            List<Character[]> pairs = Utility.GenerateAllPermutations(otherChars, 2);

            List<Composition> allComps = new List<Composition>();

            for (int i = 0; i < pairs.Count; ++i)
            {
                Character[] comp = new Character[4];
                comp[0] = seedChars[0];
                comp[1] = seedChars[1];
                comp[2] = pairs[i][0];
                comp[3] = pairs[i][1];

                allComps.Add(new Composition(comp, db));
            }

            allComps.Sort();

            for (int i = 0; i < pairs.Count; ++i)
            {
                Console.WriteLine(allComps[i]);
            }

            return;
        }

    }
}
