using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Huffman
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string text = System.IO.File.ReadAllText(@"your_text_file.txt");

            byte[] bytes = System.IO.File.ReadAllBytes(@"your_binary_file_path");




            //string input
            Huffman huffman = new Huffman(text);
            string encoded = huffman.Encode(text);
            string decoded = huffman.Decode(encoded);
            Console.WriteLine("---------Text file---------");
            Console.WriteLine("Original: {0}", text);
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Encoded: {0}", encoded);
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Decoded: {0}", decoded);
            Console.WriteLine("---------------------------------");

            huffman.DisplayFrequencyTable();
            Console.WriteLine("Compression ratio: {0}", huffman.CompressionRatio());

            Console.WriteLine("---------Binary file---------");

            //binary input
            Huffman binaryHuffman = new Huffman(bytes);
            string binaryEncoded = binaryHuffman.EncodeBinary(bytes);
            string binaryDecoded = binaryHuffman.Decode(binaryEncoded);
            string binaryToString = Encoding.ASCII.GetString(bytes);
            Console.WriteLine("Original: {0}", binaryToString);
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Encoded: {0}", binaryEncoded);
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Decoded: {0}", binaryDecoded);
            Console.WriteLine("---------------------------------");
          
          

            binaryHuffman.DisplayFrequencyTable();
  
            Console.WriteLine("Compression ratio: {0}", binaryHuffman.CompressionRatio());






            Console.ReadLine();





        }

      
        

    }  
}
