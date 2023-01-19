using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    internal class Huffman
    {

        private Dictionary<char, int> _frequencies;
        private Dictionary<char, string> _encodings;
        private Node _root;

        public Huffman(string text)
        {
            //build the frequency table
            BuildFrequencyTable(text);

            //build the huffman tree
            BuildTree();

            //build the encoding table
            BuildEncodingTable();
        }

        //binary constructor

    #region binary file
        public Huffman(byte[] bytes)
        {
            //build the frequency table
            BuildFrequencyTableBinary(bytes);

            //build the huffman tree
            BuildTree();

            //build the encoding table
            BuildEncodingTable();
        }


        //build the frequency table for binary input
        private void BuildFrequencyTableBinary(byte[] bytes)
        {
            _frequencies = new Dictionary<char, int>();

            foreach (byte b in bytes)
            {
                if (_frequencies.ContainsKey((char)b))
                {
                    _frequencies[(char)b]++;
                }
                else
                {
                    _frequencies.Add((char)b, 1);
                }
            }
        }


      
        public string EncodeBinary(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                sb.Append(_encodings[(char)b]);
            }

            return sb.ToString();
        }


        #endregion

        #region text file


        private void BuildFrequencyTable(string text)
        {
            _frequencies = new Dictionary<char, int>();
            foreach (char c in text)
            {
                if (_frequencies.ContainsKey(c))
                {
                    _frequencies[c]++;
                }
                else
                {
                    _frequencies.Add(c, 1);
                }
            }
        }


        private void BuildTree()
        {
            //create a list of nodes
            List<Node> nodes = new List<Node>();
            foreach (KeyValuePair<char, int> symbol in _frequencies)
            {
                nodes.Add(new Node(symbol.Key, symbol.Value, null, null));
            }

            //sort the list
            List<Node> sorted = nodes.OrderBy(node => node.Frequency).ToList();

            while (sorted.Count > 1)
            {
                //take first two items
                List<Node> taken = sorted.Take(2).ToList();

                //remove them from the list
                foreach (Node node in taken)
                {
                    sorted.Remove(node);
                }

                //create a parent node by combining the frequencies
                int sum = taken[0].Frequency + taken[1].Frequency;
                Node parent = new Node('\0', sum, taken[0], taken[1]);

                //add the new node to the list and resort
                sorted.Add(parent);
                sorted = sorted.OrderBy(node => node.Frequency).ToList();
            }

            //the huffman tree is ready!
            _root = sorted.FirstOrDefault();
        }

        private void BuildEncodingTable()
        {
            _encodings = new Dictionary<char, string>();
            Encode(_root, new StringBuilder());
        }

        private void Encode(Node node, StringBuilder builder)
        {
            //if this is a leaf node, store the encoding for this symbol
            if (node.IsLeaf)
            {
                _encodings.Add(node.Symbol, builder.ToString());
            }
            else
            {
                //traverse left
                builder.Append('0');
                Encode(node.Left, builder);
                builder.Remove(builder.Length - 1, 1);

                //traverse right
                builder.Append('1');
                Encode(node.Right, builder);
                builder.Remove(builder.Length - 1, 1);
            }
        }

        public string Encode(string text)
        {
            StringBuilder builder = new StringBuilder();
            foreach (char c in text)
            {
                builder.Append(_encodings[c]);
            }
            return builder.ToString();
        }




        public string Decode(string encodedText)
        {
            StringBuilder decoded = new StringBuilder();
            Node current = _root;
            foreach (char bit in encodedText)
            {
                if (bit == '0')
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }

                if (current.IsLeaf)
                {
                    decoded.Append(current.Symbol);
                    current = _root;
                }
            }
            return decoded.ToString();
        }

       
       

       
        public void DisplayFrequencyTable()
        {
            Console.WriteLine("Symbol\tFrequency\tEncoding");
            foreach (KeyValuePair<char, int> symbol in _frequencies)
            {
                Console.WriteLine("{0}\t{1}\t\t{2}", symbol.Key, symbol.Value, _encodings[symbol.Key]);
            }
            
            
            
        }

        public void PrintTree()
        {
            PrintTree(_root, 0, new List<bool>());
        }

        private void PrintTree(Node node, int depth, List<bool> branches)
        {
            if (node != null)
            {
                PrintTree(node.Right, depth + 1, AddBranch(branches, true));
                PrintNode(node, depth, branches);
                PrintTree(node.Left, depth + 1, AddBranch(branches, false));
            }
        }

        private void PrintNode(Node node, int depth, List<bool> branches)
        {
            Console.Write(new String(' ', depth * 2));
            foreach (bool b in branches)
            {
                Console.Write(b ? "| " : "  ");
            }
            Console.WriteLine(node.Symbol + " " + node.Frequency);
        }

        private List<bool> AddBranch(List<bool> branches, bool b)
        {
            List<bool> newBranches = new List<bool>(branches);
            newBranches.Add(b);
            return newBranches;
        }


        #endregion

        
        public double AverageLength()
        {
            double sum = 0;
            foreach (KeyValuePair<char, int> symbol in _frequencies)
            {
                sum += _encodings[symbol.Key].Length * symbol.Value;
            }
            return sum / _frequencies.Sum(x => x.Value);
        }


        
        public double CompressionRatio()
        {
            double sum = 0;
            foreach (KeyValuePair<char, int> symbol in _frequencies)
            {
                sum += _encodings[symbol.Key].Length * symbol.Value;
            }
            return sum / (_frequencies.Sum(x => x.Value) * 8);
        }

      
        


    }
}
