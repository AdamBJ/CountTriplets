using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class CountTriplets {
    static long countTriplets(List<long> arr, long r) {
        // Precompute occurences of all integers in arr
        Dictionary<long, List<long>> dict = new Dictionary<long, List<long>>();
        for (int i=0; i<arr.Count; i++)
        {
            long current = arr[i];
            if (dict.ContainsKey(current))
            {
                List<long> currOccurenceIdxs = dict[current];
                currOccurenceIdxs.Add(i);
                dict[current] = currOccurenceIdxs;
            }
            else
            {
                dict.Add(current, new List<long>(){i});
            }
        }

        long numTriplets = 0;
        bool shouldContinue = false;
        foreach (KeyValuePair<long, List<long>> entry in dict)
        {
            if (shouldContinue)
            {
                shouldContinue = false;
                continue;
            }
            long first = entry.Key;
            List<long> firstOccurenceIdxs = entry.Value;
            foreach(long firstOccurenceIdx in firstOccurenceIdxs)
            {
                long second = first*r;
                if (!dict.ContainsKey(second))
                {
                    // No way to continue to triple
                    break;
                }
                else
                {
                    List<long> secondOccurenceIdxs = dict[second];
                    for(int i=secondOccurenceIdxs.Count-1; i>=0; i--)
                    {   
                        long secondOccurenceIdx = secondOccurenceIdxs[i];
                        if (secondOccurenceIdx <= firstOccurenceIdx)
                        {
                            // To the left, not valid triple
                            break;
                        }

                        long third = second*r;
                        if (!dict.ContainsKey(third))
                        {
                            // No way to continue to triple
                            shouldContinue = true;
                            break;
                        } 
                        else
                        {
                            List<long> thirdOccurenceIdxs = dict[third];
                            for(int j=thirdOccurenceIdxs.Count-1; j>=0; j--)
                            {
                                long thirdOccurenceIdx = thirdOccurenceIdxs[j];
                                if (thirdOccurenceIdx <= secondOccurenceIdx)
                                {
                                    // To the left, not valid triple
                                    break;
                                }
                                // Found valid triple
                                numTriplets++;
                            }
                        }
                    }
                }
            }
        }

        return numTriplets;
    }


        // for(int i=0; i<arr.Count; i++)
        // {
        //     long first = arr[i];
        //     long second = first * r;
        //     if (!dict.ContainsKey(second))
        //     {
        //         continue;
        //     } 
        //     else
        //     {
        //         long third = second * r;
        //         if (!dict.ContainsKey(third))
        //         {
        //             continue;
        //         }
        //         else
        //         {
        //             // We found a triplet pattern. Count number occurences.
        //             List<long> firstOccurenceIdxs = dict[first];
        //             List<long> secondOccurenceIdxs = dict[second];
        //             List<long> thirdOccurenceIdxs = dict[third];
        //             // Fundamental principle of counting, x ways to pick first item, y ways to pick second item, z way to pick third -> x*y*z ways to pick three items.
        //             numTriplets = 
        //                 getNumPickable(i, firstOccurenceIdxs) * getNumPickable(i, secondOccurenceIdxs) * getNumPickable(i, thirdOccurenceIdxs);
        //         }
        //     }
        // }
        

    // private static long getNumPickable(int i, List<long> occurenceIdxs)
    // {
    //     // TODO go over this backwards. That way we can short-circuit execution once we find an index < i
    //     long numPickable = 0;
    //     foreach(long occurenceIdx in occurenceIdxs)
    //     {
    //         if (occurenceIdx < i)
    //         {
    //             continue;
    //         }
    //         numPickable++;
    //     }

    //     return numPickable;
    // }

    static void Main(string[] args) {
        //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nr = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(nr[0]);

        long r = Convert.ToInt64(nr[1]);

        List<long> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt64(arrTemp)).ToList();

        long ans = countTriplets(arr, r);

        //textWriter.WriteLine(ans);

        //textWriter.Flush();
        //textWriter.Close();
    }
}
