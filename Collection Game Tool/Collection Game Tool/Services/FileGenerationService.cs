using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Collection_Game_Tool.Services
{
    public class FileGenerationService
    {

        public FileGenerationService() {}

        public void buildGameData(
            Divisions.DivisionsModel divisions, 
            PrizeLevels.PrizeLevels prizeLevels,
            GameSetup.GameSetupModel gameInfo,
            string fileName)
        {
            int numberOfDivisions = divisions.getNumberOfDivisions();
            List<int[]>[] divisionLevles = new List<int[]>[numberOfDivisions];
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < numberOfDivisions; i++)
            {
                int divisionIndex = i; 
                Thread t = new Thread(() => divisionLevles[divisionIndex] = getDivisionWinningPermutations(divisionIndex, gameInfo.totalPicks, gameInfo.maxPermutations, divisions.getDivision(divisionIndex), prizeLevels).OrderBy(a => Guid.NewGuid()).ToList());
                t.Start();
                threads.Add(t);
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }
            writeFile(fileName, divisionLevles);
        }

        private void writeFile(string fileName, List<int[]>[] divisionLevles)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\GTech\" + fileName + ".txt"))
            {
                List<string> lines = new List<string>();
                int divisionIndicator = 0;
                foreach (List<int[]> li in divisionLevles)
                {
                    foreach (int[] i in li)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(divisionIndicator + " |");

                        for (int j = 0; j < i.Length; j++)
                        {
                            if (j != 0)
                            {
                                sb.Append(", " + i[j]);
                            }
                            else
                            {
                                sb.Append(i[j]);
                            }

                        }
                        lines.Add(sb.ToString());
                    }
                    divisionIndicator++;
                }
                foreach (string s in lines)
                {
                    file.WriteLine(s);
                }
            }
        }

        //Creates the collection of win permutations
        private List<int[]> getDivisionWinningPermutations(
            int divisionIndicator,
            short totalNumberOfPicks,
            uint numberOfPermuitations,
            Divisions.DivisionModel division,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int[]> divisionIncompleteWinPermiutations = new List<int[]>();
            int[] permuitationArray = getBaseWinCombinaiton(totalNumberOfPicks,division,prizeLevels).ToArray();
            int[] firstPermuitation = new int[totalNumberOfPicks];
            permuitationArray.CopyTo(firstPermuitation, 0);
            bool ableToFindNextdivision = true;
            int[] nonWinningPicks = getExtraPicksForWinCombination(division, prizeLevels);
            for (int i = 0; i < numberOfPermuitations && ableToFindNextdivision; i++)
            {
                int[] newPermuitation = new int[totalNumberOfPicks];
                permuitationArray.CopyTo(newPermuitation, 0);
                if (ableToFindNextdivision)
                {
                    divisionIncompleteWinPermiutations.Add(fillPermiutation(newPermuitation,nonWinningPicks));
                }
                permuitationArray = findNextPermutation(permuitationArray);
                ableToFindNextdivision = !(permuitationArray[0] == -1);
            }
            
            return fillBlankDivisionPermiutationsWithNonWinningData(
                divisionIncompleteWinPermiutations,
                nonWinningPicks,
                division,
                prizeLevels,
                numberOfPermuitations);
        }

        private List<int[]> fillBlankDivisionPermiutationsWithNonWinningData(
            List<int[]> nonWinningPermutations,
            int[] extraPicks,
            Divisions.DivisionModel div,
            PrizeLevels.PrizeLevels prizeLevels,
            uint maxNumberOfPermiutations)
        {
            List<int[]> filledPermiutations = new List<int[]>();

            if (nonWinningPermutations.Count < maxNumberOfPermiutations)
            {
                filledPermiutations = createExtraPermiutations(nonWinningPermutations, extraPicks, div, prizeLevels,maxNumberOfPermiutations);
            }
            else
            {
                filledPermiutations = nonWinningPermutations;
            }
            return filledPermiutations;
        }

        private List<int[]> createExtraPermiutations(
            List<int[]> nonWinningPermutations,
            int[] extraValues,
            Divisions.DivisionModel div,
            PrizeLevels.PrizeLevels prizeLevels,
            uint maxNumberOfPermiutations)
        {
            int numberOfAddedPermiutations = nonWinningPermutations.Count;
            List<int[]> expandedPermiutationList = new List<int[]>(nonWinningPermutations);
            List<int> winningIndexes = new List<int>();
            foreach (PrizeLevels.PrizeLevel p in div.getPrizeLevelsAtDivision())
            {
                winningIndexes.Add(prizeLevels.getLevelOfPrize(p) + 1);
            }
            for (int i = 0; i < nonWinningPermutations.Count && numberOfAddedPermiutations <= maxNumberOfPermiutations; i++)
            {
                int[] permiutation = nonWinningPermutations.ElementAt(i);
                List<int> extraValuesCopy = extraValues.ToList<int>();
                for (int j = 0; j < permiutation.Length; j++)
                {
                    if (extraValuesCopy.Contains(permiutation[j]))
                    {
                        extraValuesCopy.Remove(permiutation[j]);
                    }
                }
                for (int j = 0; j < permiutation.Length; j++)
                {
                    if (!winningIndexes.Contains(permiutation[j]))
                    {
                        for (int k = 0; k < extraValuesCopy.Count; k++)
                        {
                            if (!(permiutation[j] == extraValuesCopy.ElementAt(k)))
                            {
                                int[] tempPermuitation = new int[permiutation.Length];
                                permiutation.CopyTo(tempPermuitation, 0);
                                tempPermuitation[j] = extraValuesCopy.ElementAt(k);
                                if (!expandedPermiutationList.Contains(tempPermuitation))
                                {
                                    expandedPermiutationList.Add(tempPermuitation);
                                    numberOfAddedPermiutations++;
                                }
                            }
                        }
                    }
                }
            }
            return expandedPermiutationList;
        }

        private int[] fillPermiutation(
            int[] blankPermiutation,
            int[] extraValues)
        {
            int[] permiutationCopy = new int[blankPermiutation.Length];
            blankPermiutation.CopyTo(permiutationCopy, 0);
            List<int> extraValuesCopy = extraValues.ToList<int>();
            Random rand = new Random();
            for (int i = 0; i < permiutationCopy.Length; i++)
            {
                if (permiutationCopy[i] == 0)
                {
                    if (!(extraValuesCopy.Count == 0))
                    {
                        extraValuesCopy.OrderBy(a => Guid.NewGuid());
                        int selection = rand.Next(0, extraValuesCopy.Count - 1);
                        permiutationCopy[i] = extraValuesCopy.ElementAt(selection);
                        extraValuesCopy.RemoveAt(selection);
                    } 
                }
            }
            return permiutationCopy;
        }

        private int[] getExtraPicksForWinCombination(
            Divisions.DivisionModel division,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int> winningDivisionIndexs = new List<int>();
            List<int> extraPicks = new List<int>();
            foreach(PrizeLevels.PrizeLevel pl in division.getPrizeLevelsAtDivision())
            {
                winningDivisionIndexs.Add(prizeLevels.getLevelOfPrize(pl));
            }
            int numberOfPrizeLevels = prizeLevels.getNumPrizeLevels();
            for (int i = 0; i < numberOfPrizeLevels; i++)
            {
                if (!winningDivisionIndexs.Contains(i))
                {
                    int numberOfCollections = prizeLevels.getPrizeLevel(i).numCollections - 1;
                    for (int j = 0; j < numberOfCollections; j++)
                    {
                        extraPicks.Add(i + 1);
                    }
                }
            }
            return extraPicks.ToArray();
        }

        //Creates a base win combination for a given division
        private List<int> getBaseWinCombinaiton(
            int totalNumberOfPicks,
            Divisions.DivisionModel division,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int> neededPicksForDivisionWin = getNeededPicksForDivision(division, prizeLevels);
            for (int i = neededPicksForDivisionWin.Count; i < totalNumberOfPicks; i++)
            {
                neededPicksForDivisionWin.Add(0);
            }
            neededPicksForDivisionWin.Sort();
            return neededPicksForDivisionWin;
        }

        private List<int> getNeededPicksForDivision(
            Divisions.DivisionModel division,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int> neededPicks = new List<int>();
            List<PrizeLevels.PrizeLevel> pls = division.getPrizeLevelsAtDivision();
            foreach (PrizeLevels.PrizeLevel pl in pls)
            {
                int numberToCollect = pl.numCollections;
                int indexinPrizeLevels = prizeLevels.getLevelOfPrize(pl) + 1;
                for (int i = 0; i < numberToCollect; i++)
                {
                    neededPicks.Add(indexinPrizeLevels);
                }
            }
            return neededPicks;
        }

        private int[] findNextPermutation(int[] values)
        {
            
            int i = values.Length - 1;
            while (i > 0 && values[i - 1] >= values[i])
            {
                i--;
            }

            if (i <= 0)
            {
                int[] fail = {-1};
                return fail;
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

            return values;
        }
    }
}
