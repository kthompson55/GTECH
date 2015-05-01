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
            int numberOfDivisions = divisions.getNumberOfDivisions() + 1;
            List<int[]>[] divisionLevles = new List<int[]>[numberOfDivisions];
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < numberOfDivisions; i++)
            {
                int divisionIndex = i;
                Thread t;
                if (divisionIndex == numberOfDivisions - 1)
                {
                    t = new Thread(() => divisionLevles[divisionIndex] = getDivisionLossingPermutations(gameInfo, prizeLevels).OrderBy(a => Guid.NewGuid()).ToList());
                }
                else
                {
                    t = new Thread(() => divisionLevles[divisionIndex] = getDivisionWinningPermutations(divisionIndex, gameInfo.totalPicks, gameInfo.maxPermutations, divisions.getDivision(divisionIndex), prizeLevels).OrderBy(a => Guid.NewGuid()).ToList());
                }
                t.Start();
                threads.Add(t);
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }
            writeFile(fileName, divisionLevles);
        }

        private List<int[]> getDivisionLossingPermutations(
            GameSetup.GameSetupModel gameInfo,
            PrizeLevels.PrizeLevels prizeLevels
            )
        {
            List<int[]> lossPermutations = new List<int[]>();
            List<int> extraPicks = getExtraPicksForWinCombination(new Divisions.DivisionModel(), prizeLevels).ToList();
            List<int[]> baseLossconditions = new List<int[]>();
            int numberOfPermutationsForNearWinAmount = (int)gameInfo.maxPermutations;
            if (gameInfo.isNearWin)
            {
                numberOfPermutationsForNearWinAmount = (int)(gameInfo.maxPermutations / gameInfo.nearWins);
               baseLossconditions.AddRange(getBaseNearWinLossPermiutations(gameInfo.nearWins, gameInfo.totalPicks, prizeLevels));
            }
            else
            {
                int[] baseLoss = new int[gameInfo.totalPicks];
                baseLossconditions.Add(baseLoss);
            }
            lossPermutations = getAllLosePermiutations(baseLossconditions, prizeLevels, gameInfo.nearWins, numberOfPermutationsForNearWinAmount);
            return lossPermutations;
        }

        private List<int[]> getAllLosePermiutations(
            List<int[]> baseLossconditions,
            PrizeLevels.PrizeLevels prizeLevels,
            int numberOfNearWins,
            int maxNumberOfPermiutationsPerNearWin)
        {
            List<int[]> lossPermituations = new List<int[]>();
            int[] nearWinCounts = new int[numberOfNearWins + 1];

            foreach (int[] lossCondition in baseLossconditions)
            {
                int nearWinType = 0;
                for (int i = 1; i < lossCondition.Length; i++)
                {
                    if (lossCondition[i] != lossCondition[i - 1])
                    {
                        nearWinType++;
                    }
                }

                int[] bsaePermuitation = new int[lossCondition.Length];
                lossCondition.CopyTo(bsaePermuitation, 0);
                bool ableToFindNextdivision = true;
                for (int i = nearWinCounts[nearWinType]; i < maxNumberOfPermiutationsPerNearWin && ableToFindNextdivision; i++)
                {
                    int[] newPermuitation = new int[lossCondition.Length];
                    lossCondition.CopyTo(newPermuitation, 0);
                    if (ableToFindNextdivision)
                    {
                        lossPermituations.Add(newPermuitation);
                        nearWinCounts[nearWinType]++;
                    }
                    bsaePermuitation = findNextPermutation(lossCondition);
                    ableToFindNextdivision = !(bsaePermuitation[0] == -1);
                }
            }
            return createExtraPermiutations(lossPermituations, maxNumberOfPermiutationsPerNearWin, prizeLevels); ;
        }

        /// <summary>
        /// Fills in the zero values of a permutation with desired extra pick values
        /// </summary>
        /// <param name="extraPicks"> List of extra picks that will be used to fill in zero values</param>
        /// <param name="permutation"> Base permutation to be filled in </param>
        /// <returns> Returns a filled in permutation with no zero values</returns>
        private int[] fillPermiutation(
            List<int> extraPicks,
            int[] permutation)
        {
            List<int> extraPicksCopy = new List<int>(extraPicks);
            int[] filledPermiutation = new int[permutation.Length];
            permutation.CopyTo(filledPermiutation, 0);
            Random rand = new Random();
            for (int i = 0; i < filledPermiutation.Length; i++)
            {
                if (filledPermiutation[i] == 0)
                {
                    int extraPickIndex = rand.Next(0, extraPicksCopy.Count);
                    filledPermiutation[i] = extraPicks.ElementAt(extraPickIndex);
                    extraPicks.RemoveAt(extraPickIndex);
                }
            }
            return filledPermiutation;
        }

        private List<int[]> createExtraPermiutations(
            List<int[]> permutations,
            int desiredAmountOfPermiutations,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            HashSet<int[]> permiutationList = new HashSet<int[]>();
            for (int i = 0; i < permutations.Count; i++)
            {
                HashSet<int[]> extrasForPerm = createExtraPermiutationsFormBase(permutations[i], getExtraPicks(permutations[i], prizeLevels), (int)desiredAmountOfPermiutations/permutations.Count);
                permiutationList.UnionWith(extrasForPerm);
            }
            return permiutationList.ToList();
        }

        private HashSet<int[]> createExtraPermiutationsFormBase(
            int[] basePermiutation,
            int[] extraPicks,
            int maxNumberOfPermiutaitons)
        {
            HashSet<int[]> extraPermiutations = new HashSet<int[]>();
            int[] copyOfExtraPicks = new int[extraPicks.Length];
            extraPicks.CopyTo(copyOfExtraPicks, 0);
            int[] copyOfBase = new int[basePermiutation.Length];
            basePermiutation.CopyTo(copyOfBase, 0);
            int numberOfFailuers = 0;
            while (extraPermiutations.Count < maxNumberOfPermiutaitons && numberOfFailuers < maxNumberOfPermiutaitons)
            {
                if (!extraPermiutations.Add(fillPermiutation(copyOfBase, copyOfExtraPicks.ToArray())))
                {
                    numberOfFailuers++;
                }
            }
            return extraPermiutations;
        }


        private List<int[]> getBaseNearWinLossPermiutations(
            int nearWinPrizeLevels,
            int totalNumberOfPicks,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int[]> nearWinBasePermiutations = new List<int[]>();
            List<int[]> prizeLevelCombinations = getPrizeLevelCombinationsForNearWins(nearWinPrizeLevels, totalNumberOfPicks, prizeLevels);
            foreach (int[] combo in prizeLevelCombinations)
            {
                List<int> neededPicks = new List<int>();
                for (int i = 0; i < combo.Length; i++)
                {
                    int prizeLevelToSelect = combo[i];
                    for (int k = 0; k < prizeLevels.getPrizeLevel(prizeLevelToSelect).numCollections - 1; k++)
                    {
                        neededPicks.Add(combo[i ] + 1);
                    }
                }
                nearWinBasePermiutations.Add(getBaseCombinaiton(totalNumberOfPicks, neededPicks.ToArray()).ToArray());
            }
            return nearWinBasePermiutations;
        }

        private List<int[]> getPrizeLevelCombinationsForNearWins(
            int nearWinPrizeLevels,
            int totalNumberOfPicks,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            int numberOfPrizeLevels = prizeLevels.getNumPrizeLevels();
            List<int[]> prizeLevelCombinations = new List<int[]>();
            int numberOfMaximumCombinations = prizeLevels.getNumPrizeLevels();

            List<int> prizeLevelsIndexes = new List<int>();
            for(int i = 0; i < numberOfPrizeLevels; i++){
                prizeLevelsIndexes.Add(i);
            }
            // Max possible Base
            for (int i = 1; i <= nearWinPrizeLevels; i++)
            {
                for (int j= 0; j < numberOfMaximumCombinations; j++)
                {
                    Random rand = new Random();
                    int[] tempPrizeLevelCombinations = new int[i];
                    int numberOfPicksForPrizeLevelCombintation = 0;
                    bool newBaseComboAdded = false;
                    List<int> tempPrizeLevelIndexes = new List<int>(prizeLevelsIndexes);
                    do
                    {
                        for (int k = 0; k < i; k++)
                        {
                            int randomPrizeLevelSelection = rand.Next(0, tempPrizeLevelIndexes.Count);
                            numberOfPicksForPrizeLevelCombintation += prizeLevels.getPrizeLevel(randomPrizeLevelSelection).numCollections - 1;
                            tempPrizeLevelCombinations[k] = randomPrizeLevelSelection;
                            tempPrizeLevelIndexes.RemoveAt(randomPrizeLevelSelection);
                        }
                        if (!prizeLevelCombinations.Any(tempPrizeLevelCombinations.SequenceEqual) && numberOfPicksForPrizeLevelCombintation <= totalNumberOfPicks)
                        {
                            prizeLevelCombinations.Add(tempPrizeLevelCombinations);
                            newBaseComboAdded = true;
                        }
                        else if (numberOfPicksForPrizeLevelCombintation > totalNumberOfPicks || tempPrizeLevelIndexes.Count == 0)
                        {
                            tempPrizeLevelCombinations = new int[i];
                            numberOfPicksForPrizeLevelCombintation = 0;
                        }
                        tempPrizeLevelIndexes = new List<int>(prizeLevelsIndexes);
                    } while (!newBaseComboAdded);
                }  
            }
            return prizeLevelCombinations;
        }

        private void writeFile(string fileName, List<int[]>[] divisionLevles)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
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
                                if (i[j] > 0)
                                {
                                    sb.Append(", " + i[j]);
                                }
                                else
                                {
                                    sb.Append("," + "W:" + (i[j] * -1));
                                }
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

        /// <summary>
        /// Gets all non winning picks for a given permutation
        /// </summary>
        /// <param name="permutation"> The base permutation</param>
        /// <param name="prizeLevels"> Used to find collection amount for unused pic<s/param>
        /// <returns> Returns an array of non winning picks</returns>
        private int[] getExtraPicks(
            int[] permutation,
            PrizeLevels.PrizeLevels prizeLevels)
        {
            List<int> usedIndexs = new List<int>();
            List<int> extraPicks = new List<int>();
            foreach (int i in permutation)
            {
                if (i - 1 != -1 && !usedIndexs.Contains((i-1)))
                {
                     usedIndexs.Add(i -1);
                }
            }
            int numberOfPrizeLevels = prizeLevels.getNumPrizeLevels();
            for (int i = 0; i < numberOfPrizeLevels; i++)
            {
                if (!usedIndexs.Contains(i))
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

        private List<int> getBaseCombinaiton(
            int totalNumberOfPicks,
            int[] picks)
        {
            List<int> neededPicksForComination = picks.ToList();
            for (int i = neededPicksForComination.Count; i < totalNumberOfPicks; i++)
            {
                neededPicksForComination.Add(0);
            }
            neededPicksForComination.Sort();
            return neededPicksForComination;
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
                if (pl.isInstantWin)
                {
                    numberToCollect = 1;
                    neededPicks.Add(-indexinPrizeLevels);
                }
                else
                {
                    numberToCollect = pl.numCollections;
                    for (int i = 0; i < numberToCollect; i++)
                    {
                        neededPicks.Add(indexinPrizeLevels);
                    }
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
