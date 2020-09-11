using System;
using System.Collections.Generic;
using System.IO;

namespace MidleTierExercise
{
    /// <summary>
    /// Pyramide challenge by Dan Hoang Vu 11-09-2020
    /// Most of the logic is in main.cs, since I want to 
    /// make it easier to read
    /// </summary>
    class Program
    {

        static string numbers =
            @"215
            192 124
            117 269 442
            218 836 347 235
            320 805 522 417 345
            229 601 728 835 133 124
            248 202 277 433 207 263 257
            359 464 504 528 516 716 871 182
            461 441 426 656 863 560 380 171 923
            381 348 573 533 448 632 387 176 975 449
            223 711 445 645 245 543 931 532 937 541 444
            330 131 333 928 376 733 017 778 839 168 197 197
            131 171 522 137 217 224 291 413 528 520 227 229 928
            223 626 034 683 839 052 627 310 713 999 629 817 410 121
            924 622 911 233 325 139 721 218 253 223 107 233 230 124 233";

        static string sampleInput = @"1
                                    8 9
                                    1 5 9
                                    4 5 2 3";

        //using a dictionary to add to organise the layers
        private static Dictionary<int, List<Node>> layers = new Dictionary<int, List<Node>>();
        static void Main(string[] args)
        {
            GenerateTree();
            // getting the root node
            Node root = layers[0][0];
            //traversing the tree from the root node
            Traverse(root);
            Console.ReadKey();

        }
        /// <summary>
        /// Reading input Data and adding to Dictionary
        /// </summary>
        private static void ReadData()
        {
            using (StringReader reader = new StringReader(numbers))
            {
                string line;
                var layer = 0;
                Console.WriteLine("Sample Input:\r\n");
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line.Trim());
                    if (layer == 0)
                    {
                        layers.Add(layer, new List<Node>());
                        layers[layer].Add(new Node(int.Parse(line)));

                    }
                    else
                    {
                        var splitted = line.Trim().Split(' ');
                        for (int i = 0; i <= layer; i++)
                        {
                            if (!layers.ContainsKey(layer))
                            {
                                layers.Add(layer, new List<Node>());
                            }
                            layers[layer].Add(new Node(int.Parse(splitted[i])));
                        }
                    }

                    layer++;
                }
            }
 
        }

        /// <summary>
        /// Finishing the Node relations using the Dictionary
        /// </summary>
        private static void GenerateTree()
        {
            ReadData();
            for (int i = 0; i < layers.Count; i++)
            {
                for (int k = 0; k < layers[i].Count; k++)
                {
                    Node n = layers[i][k];
                    if (layers.ContainsKey(i + 1))
                    {
                        // adding children; first and second
                        n.Children.Add(layers[i + 1][k]);
                        n.Children.Add(layers[i + 1][k+1]);
                    }

                }
            }
        }

        private static void Traverse(Node root)
        {
            int sum = 0;
            Node nextChild = root.NextMaxChild();
            String path = root.Value + ",";

            sum += root.Value;

            TraverseRec(nextChild, ref sum, ref path);

            Console.WriteLine(String.Format("\r\nMax sum: {0}", sum));
            Console.WriteLine(String.Format("Path: {0}", path));

        }

        // recursively traversing through each node until reaching leaf node
        private static void TraverseRec(Node node, ref int sum, ref string path)
        {
            sum += node.Value;
            Node nextChild = node.NextMaxChild();

            path += node.Value + ",";

            if (nextChild != null)
            {
                TraverseRec(nextChild, ref sum, ref path);
            }
            else
            {
                // just for removing the last ,
                path = path.Substring(0, path.Length - 1);
            }

        }
    }
}
