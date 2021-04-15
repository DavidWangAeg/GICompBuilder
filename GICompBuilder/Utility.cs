using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GICompBuilder
{
    public static class Utility
    {
        private static Character[] _allChars;

        static Utility()
        {
            var vals = Enum.GetValues(typeof(Character));
            _allChars = new Character[vals.Length];
            vals.CopyTo(_allChars, 0);
        }

        public static string CharacterListToString(Character[] comp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < comp.Length - 1; ++i)
            {
                sb.Append(comp[i] + ", ");
            }

            sb.Append(comp[comp.Length - 1] + "]");

            return sb.ToString();
        }

        public static bool IsTraveller(this Character c)
        {
            return c == Character.Traveller_Anemo || c == Character.Traveller_Geo;
        }

        public static List<Character[]> GenerateAllPermutations(Character[] characterList, int groupSize)
        {
            List<Character[]> results = new List<Character[]>();
            List<Character> current = new List<Character>(new Character[groupSize]);

            GenerateAllPermutationsRecursive(results, characterList, 0, current, 0, groupSize - 1, false);

            return results;
        }

        private static void GenerateAllPermutationsRecursive(List<Character[]> results, Character[] characterList, int listIndex, List<Character> current, int depth, int maxDepth, bool hasTraveller)
        {
            // Add remaining chars
            for (; listIndex < characterList.Length; ++listIndex)
            {
                Character currChar = characterList[listIndex];
                bool isTraveller = currChar.IsTraveller();
                if (isTraveller && hasTraveller)
                {
                    continue;
                }

                current[depth] = characterList[listIndex];                

                if (depth == maxDepth)
                {
                    results.Add(current.ToArray());
                }
                else
                {
                    GenerateAllPermutationsRecursive(results, characterList, listIndex + 1, current, depth + 1, maxDepth, isTraveller);
                }
            }
        }

        public static Character[] GenerateListExcluding(Character[] toExclude)
        {
            return _allChars.Where(c => !toExclude.Contains(c)).ToArray();
        }
    }
}
