using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collection_Game_Tool.Services
{
    public class FileGenerationService
    {
        //Creates the text file containing game information
        public void buildGameData(
            Divisions.DivisionsModel divisions, 
            PrizeLevels.PrizeLevels prizeLevels,
            GameSetup.GameSetupModel gameInfo)
        {
            List<List<int[]>> divisionLevles = new List<List<int[]>>();
            int numberOfDivisions = divisions.getNumberOfDivisions();
            for (int i = 0; i < numberOfDivisions; i++)
            {
                divisionLevles.Add(getDivisionWinningPermutations(i, gameInfo.picks, gameInfo.maxPermutations, divisions.getDivision(i), prizeLevels));
            }
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\tlousignont\Documents\PayOutTest.txt");
            List<string> lines = new List<string>();
            foreach (List<int[]> li in divisionLevles)
            {
                StringBuilder sb = new StringBuilder();
                foreach (int[] i in li)
                {
                    for (int j = 0; j < i.Length; j++)
                    {
                        sb.Append("" + i[j] + ", ");
                    }
                    lines.Add(sb.ToString());
                }
            }
            foreach (string s in lines)
            {
                file.WriteLine(s);
            }
        }

        //Creates the collection of win permutations
        private List<int[]> getDivisionWinningPermutations(
            int divisionIndicator,
            int totalNumberOfPicks,
            int numberOfPermuitations,
            Divisions.DivisionModel division,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int[]> divisionWinCombinations = new List<int[]>();
            List<int> startingPermuitation = getBaseWinCombinaiton(totalNumberOfPicks,division,prizeLevels);
            int[] permuitationArray = startingPermuitation.ToArray();
            divisionWinCombinations.Add(permuitationArray);
            bool ableToFindNextdivision = true;
            for (int i = 0; i < numberOfPermuitations && ableToFindNextdivision; i++)
            {
                ableToFindNextdivision = findNextPermutation(permuitationArray);
                int[] newPermuitation = permuitationArray;
                divisionWinCombinations.Add(newPermuitation);
            }
            return divisionWinCombinations;
        }

        //Creates a base win combination for a given division
        private List<int> getBaseWinCombinaiton(
            int totalNumberOfPicks,
            Divisions.DivisionModel division,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int> neededPicksForDivisionWin = division.getNeededPicks();
            for (int i = neededPicksForDivisionWin.Count; i < totalNumberOfPicks; i++)
            {
                neededPicksForDivisionWin.Add(0);
            }
            neededPicksForDivisionWin.Sort();
            return neededPicksForDivisionWin;
        }

        private bool findNextPermutation(int[] values)
        {
            int i = values.Length - 1;
            while (i > 0 && values[i - 1] >= values[i])
            {
                i--;
            }

            if (i <= 0)
            {
                return false;
            }

            int j = values.Length - 1;
            while (values[j] <= values[i - 1])
            {
                j--;
            }

            int temp = values[i - 1];
            values[i - 1] = values[j];
            values[j] = temp;

            j = values.Length - 1;
            while (i < j)
            {
                temp = values[i];
                values[i] = values[j];
                values[j] = temp;
                i++;
                j--;
            }

            return true;
        }
    }
}
