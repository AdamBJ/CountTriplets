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
    private struct Info
    {
        public List<long>occurenceIdxs;

        public Info(int idx)
        {
            this.occurenceIdxs = new List<long>();
            this.occurenceIdxs.Add(idx);
        }
    }

    // Complete the countTriplets function below.
    static long countTriplets(List<long> arr, long r) {
        Dictionary<long, Info> dict = new Dictionary<long, Info>();
        for (int i=0; i<arr.Count; i++)
        {
            long current = arr[i];
            if (dict.ContainsKey(current))
            {
                Info currInfo = dict[current];
                currInfo.occurenceIdxs.Add(i);
                dict[current] = currInfo;
            }
            else
            {
                dict.Add(current, new Info(i));
            }
        }

        return 1;

    }



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
