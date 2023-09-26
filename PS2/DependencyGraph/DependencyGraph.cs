// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{

    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an element to a 
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        //Dictionaries to represent a graph without nodes or edges
        Dictionary<string, HashSet<string>> dependentsGraph; //One routing to cells that are dependent on your current cell
        Dictionary<string, HashSet<string>> dependeesGraph; //One routing to cells that your current cell is dependent on
        private int size;


        /// <summary>
        /// Creates an empty DependencyGraph.
        /// </summary>
        public DependencyGraph()
        {
            // Create the empty graphs and keep track of the size
            dependentsGraph = new Dictionary<string, HashSet<string>>();
            dependeesGraph = new Dictionary<string, HashSet<string>>();
            size = 0;
        }


        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return size; }
        }


        /// <summary>
        /// The size of dependees(s).
        /// This property is an example of an indexer.  If dg is a DependencyGraph, you would
        /// invoke it like this:
        /// dg["a"]
        /// It should return the size of dependees("a")
        /// </summary>
        public int this[string s]
        {
            get
            {
                if (dependeesGraph.TryGetValue(s, out HashSet<string> dependees))
                {
                    return dependees.Count;
                }
                return 0;
            }
        }


        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            //if the dpendents graph contains the key that means it has dependents
            return dependentsGraph.ContainsKey(s);
        }


        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            //if the dpendents graph contains the key that means it has dependees
            return dependeesGraph.ContainsKey(s);
        }


        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            //Checks if the key is even in the graph
            if (dependentsGraph.ContainsKey(s))
            {
                //If so pulls out the list of dependents and returns it
                dependentsGraph.TryGetValue(s, out HashSet<string> dependents);
                return dependents;
            }
            //Else returns an empty set
            return new HashSet<string>();
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            //Checks if the key is even in the graph
            if (dependeesGraph.ContainsKey(s))
            {
                //If so pulls out the list of dependeees and returns it
                dependeesGraph.TryGetValue(s, out HashSet<string> dependees);
                return dependees;
            }
            //Else returns an empty set
            return new HashSet<string>();
        }


        /// <summary>
        /// <para>Adds the ordered pair (s,t), if it doesn't exist</para>
        /// 
        /// <para>This should be thought of as:</para>   
        /// 
        ///   t depends on s
        ///
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is</param>        /// 
        public void AddDependency(string s, string t)
        {
            //Adds new key and hashset if the key is not already in the set for dependents.
            if (!dependentsGraph.ContainsKey(s))
            {
                HashSet<string> dependents = new HashSet<string>();
                dependents.Add(t);
                dependentsGraph.Add(s, dependents);
                size++;
            }
            //Adds the value to the hashset if the key is already in the set but doesn't have the given value for dependents
            else if (!GetDependents(s).Contains(t))
            {
                dependentsGraph.TryGetValue(s, out HashSet<string> dependents);
                dependents.Add(t);
                size++;
            }
            //Adds new key and hashset if the key is not already in the set for dependees.
            if (!dependeesGraph.ContainsKey(t))
            {
                HashSet<string> dependees = new HashSet<string>();
                dependees.Add(s);
                dependeesGraph.Add(t, dependees);
            }
            //Adds the value to the hashset if the key is already in the set but doesn't have the given value for dependees
            else if (!GetDependees(t).Contains(s))
            {
                dependeesGraph.TryGetValue(t, out HashSet<string> dependees);
                dependees.Add(s);
            }



        }


        /// <summary>
        /// Removes the ordered pair (s,t), if it exists
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public void RemoveDependency(string s, string t)
        {
            //Check if values are inside graph, otherwise do nothing
            if (dependentsGraph.ContainsKey(s))
            {
                //Pull out hashsets
                dependentsGraph.TryGetValue(s, out HashSet<string> dependents);
                dependeesGraph.TryGetValue(t, out HashSet<string> dependees);
                //Remove key and set it's the only value associated with the key
                if (dependents.Count == 1)
                {
                    //If the list size is only one, removes the key and values
                    dependentsGraph.Remove(s);
                }
                else
                {
                    //Else just removes it from the list
                    dependents.Remove(t);
                }
                if (dependees.Count == 1)
                {
                    //If the list size is only one, removes the key and values
                    dependeesGraph.Remove(t);
                }
                else
                {
                    //Else just removes it from the list
                    dependees.Remove(s);
                }
                //Decrease size
                size--;
            }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            //Checks if the graph even has the given key
            if (dependentsGraph.ContainsKey(s))
            {
                //Pull out the dependents list
                dependentsGraph.TryGetValue(s, out HashSet<string> dependents);
                //Copy to an array so you can iterate while moving through
                string[] list = new string[dependents.Count + 1];
                list = dependents.ToArray<string>();
                //Remove every pair
                for (int i = 0; i < list.Length; i++)
                {
                    RemoveDependency(s, list[i]);
                }
            }
                string[] list2 = new string[newDependents.Count() + 1];
                list2 = newDependents.ToArray<string>();
                //Add in every pair
                for (int j = 0; j < list2.Length; j++)
                {
                    AddDependency(s, list2[j]);
                }
        }


        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            //Checks if the graph contains the given key 
            if (dependeesGraph.ContainsKey(s))
            {
                //Pull out the list of dependees
                dependeesGraph.TryGetValue(s, out HashSet<string> dependees);
                //Copy to an array so we can iterate while removing
                string[] list = new string[dependees.Count + 1];
                list = dependees.ToArray<string>();
                for (int i = 0; i < list.Length; i++)
                {
                    //Remove every pair
                    RemoveDependency(list[i], s);
                }
            }
            string[] list2 = new string[newDependees.Count() + 1];
            list2 = newDependees.ToArray<string>();
            //Add in every pair
            for (int j = 0; j < list2.Length; j++)
            {
                AddDependency(list2[j], s);
            }
        }

    }

}

