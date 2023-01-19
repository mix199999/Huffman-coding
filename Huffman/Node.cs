using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    internal class Node
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(char symbol, int frequency, Node left, Node right)
        {
            Symbol = symbol;
            Frequency = frequency;
            Left = left;
            Right = right;
        }

        //checks if the node is a leaf
        public bool IsLeaf
        {
            get { return Left == null && Right == null; }
        }







    }
}
