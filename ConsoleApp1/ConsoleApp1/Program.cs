using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Fun with LINQ to objects ***");
            Console.WriteLine();
            //define an array of strings
            string[] currentVideoGames = {"Morrowind", "Uncharted 2",
            "Fallout 3", "Dexter", "System Shock 2"};

            //desired query: Games that have a space in the title  

            #region Firts let's try the olde fashioned way
            string[] result = QueryOverStringsLongHand(currentVideoGames);

            Console.WriteLine("Return results from longhaand version");
            foreach(string s in result)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion

            #region using LINQ
            List<string> listResult = QueryOverStrings(currentVideoGames);

            Console.WriteLine("Return results from query method");
            foreach(string s in listResult)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion
        }

        #region olde fashioned way
        static string[] QueryOverStringsLongHand(string[] s)
        {
            string[] resultsWithSpaces = new string[s.Length];

            //find the results
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(" "))
                    resultsWithSpaces[i] = s[i];
            }

            //sort results
            Array.Sort(resultsWithSpaces);

            //print results 
            Console.WriteLine("Immediate results from long hand version.");
            foreach(string s1 in resultsWithSpaces)
            {
                if(s1 != null)
                    Console.WriteLine("Item: {0}", s1);
            }
            Console.WriteLine();
            //return array
            //figure out size
            int count = 0;
            foreach(string s2 in resultsWithSpaces)
            {
                if (s2 != null) count++;
            }

            //create output array
            string[] outputArray = new string[count];

            //populate output array
            count = 0;
            foreach(string s1 in resultsWithSpaces)
            {
                if(s1 != null)
                {
                    outputArray[count] = s1;
                    count++;
                }
            }

            return outputArray;
        }
        #endregion

        #region
        static List<string> QueryOverStrings(string[] inputArray)
        {
            //build Query
            ///IEnumerable<string> subset = from...
            var subset = from game in inputArray
                         where game.Contains(" ")
                         orderby game
                         select game;

            //print result
            ReflectOverQueryResults(subset, "Query Expression");

            //print the results
            Console.WriteLine("  Immediate results using LINQ Query");
            foreach(var s in subset)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            // demonstrate reuse of query
            inputArray[0] = "some string";
            Console.WriteLine("  Immediate results using LINQ Query after change to data");
            foreach (var s in subset)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            //demonstrate returning results - immediate execution
            List<string> outputList = (from game in inputArray)
                                        where game.contains(" ")
                                        order game
                                        select game).ToList<string>();


        }

        static void ReflectOverQueryResults(object resultSet, string queryType)
        {
            Console.WriteLine("*** query type: {0} ***", queryType);
            Console.WriteLine("*** resultSet is of type: {0} ***", resultSet.GetType().Name);
            Console.WriteLine("*** resultSet location: {0} ***", resultSet.GetType().Assembly.GetName().Name);
        }
        #endregion
    }
}
