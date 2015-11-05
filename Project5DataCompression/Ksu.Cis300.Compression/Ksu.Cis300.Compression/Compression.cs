/* Author: Alex Wu
 * Compression and decompression
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using KansasStateUniversity.TreeViewer2;
using Ksu.Cis300.ImmutableBinaryTrees;
using Ksu.Cis300.Sort;
using RodHowell.Cis300.BitIO;

namespace Ksu.Cis300.Compression
{
    public partial class Compression : Form
    {
        public Compression()
        {
            InitializeComponent();
        }
         /// <summary>
        /// build a frequency table from a given file
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private long[] BuildFrequencyTable(string fn, out long numBytes)
        {
            // Counter to keep track of the number of Bytes for File Size
            numBytes = 0;

            // Long array to keep track of the bytes
            long[] ByteArray = new long[256];

            // Open filestream to read file
            using (FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read))
            {
                int k;
                while((k = fs.ReadByte()) != -1)
                {
                    ByteArray[k]++;
                    numBytes++;
                }
            }
            return ByteArray;
        }

        /// <summary>
        /// Builds the leaves of the Huffman tree
        /// </summary>
        /// <param name="longArray"></param>
        /// <returns></returns>
        private MinPriorityQueue<long, BinaryTreeNode<byte>> BuildHuffmanTreeLeaves(long[] ByteArray)
        {
            // Build a new Priority Queue
            MinPriorityQueue<long, BinaryTreeNode<byte>> q = new MinPriorityQueue<long, BinaryTreeNode<byte>>();
            for(int x = 0 ;x < ByteArray.Length;x++)
            {
                // Check if non-zero
                if(ByteArray[x] != 0)
                {
                    // Build new Tree Node with the indicated byte and add to the Byte Array
                    BinaryTreeNode<byte> newNode = new BinaryTreeNode<byte>((byte)x,null, null);
                    q.Add(newNode, ByteArray[x]);
                }
            }
            return q;
        }

        /// <summary>
        /// Build huffman tree from given priority queue
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        private BinaryTreeNode<byte> BuildHuffmanTree(MinPriorityQueue<long,BinaryTreeNode<byte>> q)
        {
            while(q.Count>1)
            {
                // Get and remove the two smallest priorities and their associated trees
                long temp1 = q.MininumPriority;
                BinaryTreeNode<byte> tree1 = q.RemoveMinimumPriority();
                long temp2 = q.MininumPriority;
                BinaryTreeNode<byte> tree2 = q.RemoveMinimumPriority();

                // Construct a new binary tree with these trees as its children and 0 as its data (which will be unused).
                BinaryTreeNode<byte> newBinTree = new BinaryTreeNode<byte>(0,tree1,tree2);

                // Add the resulting tree to the min-priority queue with a priority equal to the sum of the priorities of its children.
                q.Add(newBinTree, temp1+temp2);
            }
            return q.RemoveMinimumPriority();
        }

        /// <summary>
        /// Return an int giving the node number of the root
        /// </summary>
        /// <param name="huffTree"></param>
        /// <param name="BOS"></param>
        /// <param name="NodesWritten"></param>
        /// <returns></returns>
        private int FindNodeNumber(BinaryTreeNode<byte> huffTree, BitOutputStream BOS, int NodesWritten)
        {
            if(huffTree.LeftChild == null && huffTree.RightChild == null)
            {
                BOS.WriteBits(0, 1);
                BOS.WriteBits(huffTree.Data, 8);
                NodesWritten++;
                return NodesWritten;
            }
            else
            {
                // Recursively write the children
                int leftChild = FindNodeNumber(huffTree.LeftChild, BOS, NodesWritten);
                int rightChild = FindNodeNumber(huffTree.RightChild, BOS, leftChild);
                
                // Parent node will be +1 from right child
                NodesWritten = rightChild + 1;
                
                BOS.WriteBits(1, 1);
                BOS.WriteBits(leftChild, 9);
                BOS.WriteBits(rightChild, 9);

                return NodesWritten;
            }
        }

        /// <summary>
        /// Traverses given tree and for each byte stored in the leaf, place the encoding for that byte in the given long[]
        /// </summary>
        /// <param name="huffTree"></param>
        /// <param name="bitPath"></param>
        /// <param name="depth"></param>
        /// <param name="storage"></param>
        /// <param name="numBits"></param>
        private void GetVariableWidthEncoding(BinaryTreeNode<byte> huffTree, long bitPath, int depth, long[] storage, int[] numBits)
        {
            // If the given tree is a leaf
            if(huffTree.LeftChild == null && huffTree.RightChild == null)
            {
                int index = huffTree.Data;
                storage[index] = bitPath;
                numBits[index] = depth;
            }
            else
            {
                // Append a zero at the beginning for the left child
                bitPath = bitPath << 1;
                GetVariableWidthEncoding(huffTree.LeftChild, bitPath, depth + 1, storage, numBits);
                
                // Change the zero to a 1 for the right child
                bitPath++;
                GetVariableWidthEncoding(huffTree.RightChild, bitPath, depth + 1, storage, numBits);
            }
        }
        
        /// <summary>
        /// Method to compress the file
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="BOS"></param>
        /// <param name="VarWidEnc"></param>
        /// <param name="lengths"></param>
        private void CompressFile(string fn, BitOutputStream BOS, long[] VarWidEnc, int[] lengths)
        {
            using (FileStream fs = new FileStream(fn, FileMode.Open, FileAccess.Read))
            {
                // Read and append to Output stream
                int k;
                while((k=fs.ReadByte()) != -1)
                {
                    if(lengths[k] == 0)
                    {
                        return;
                    }
                    else
                    {
                        BOS.WriteBits(VarWidEnc[k], lengths[k]);
                    }
                }
            }
        }

        /// <summary>
        ///  Event handler for compression button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxCompressButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (uxOpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string openFile = uxOpenFileDialog1.FileName;
                    long numBytes;
                    long[] ByteArray = BuildFrequencyTable(openFile, out numBytes);

                    MinPriorityQueue<long, BinaryTreeNode<byte>> q = BuildHuffmanTreeLeaves(ByteArray);
                    int numNodes = q.Count * 2 - 1;
                    BinaryTreeNode<byte> huffTree = BuildHuffmanTree(q);

                    long[] bitPaths = new long[256];
                    int[] pathLengths = new int[256];
                    GetVariableWidthEncoding(huffTree,0,0,bitPaths,pathLengths);

                    string saveFile = null;
                    if (uxSaveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        saveFile = uxSaveFileDialog1.FileName;
                        using (BitOutputStream BOS = new BitOutputStream(saveFile))
                        {
                            // Writes first 63 bits, which describes length of file
                            BOS.WriteBits(numBytes, 63);
                            // Writes 9 bits, number of nodes
                            BOS.WriteBits(numNodes, 9);

                            // Post order tree traversal to assign node numbers
                            int rootNumber = FindNodeNumber(huffTree, BOS, -1);
                            
                            CompressFile(openFile, BOS, bitPaths, pathLengths);
                        }
                        MessageBox.Show("Compressed File Written");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Decompression part------------------------------------------------------------------

        /// <summary>
        /// Read the given number of bits
        /// </summary>
        /// <param name="numBits"></param>
        /// <param name="BIS"></param>
        /// <returns></returns>
        private long ReadGivenBits(int numBits, BitInputStream BIS)
        {
            int bitsRead = 0;
            long check = BIS.ReadBits(numBits, out bitsRead);
            if(numBits != bitsRead)
            {
                throw new IOException();
            }
            return check;
        }

        /// <summary>
        /// Method to rebuild the Huffman tree
        /// </summary>
        /// <param name="BIS"></param>
        /// <param name="numNodes"></param>
        /// <returns></returns>
        private BinaryTreeNode<byte> ReadHuffManTree(BitInputStream BIS, int numNodes)
        {
            BinaryTreeNode<byte>[] nodeArray = new BinaryTreeNode<byte>[numNodes];
            int numBits = numNodes * 14 - 5;
            int counter = 0;
            
            while(counter != numNodes)
            {
                long k = ReadGivenBits(1, BIS);
                if (k == 0)
                {
                    long byteValue = ReadGivenBits(8, BIS);
                    BinaryTreeNode<byte> newLeafNode = new BinaryTreeNode<byte>((byte)byteValue, null, null);
                    nodeArray[counter] = newLeafNode;
                }
                else
                {
                    long leftChildIndex = ReadGivenBits(9, BIS);
                    long rightChildIndex = ReadGivenBits(9, BIS);
                    BinaryTreeNode<byte> newBinNode = new BinaryTreeNode<byte>(0,nodeArray[leftChildIndex], nodeArray[rightChildIndex]);
                    nodeArray[counter] = newBinNode;
                }
                counter++;
            }
            return nodeArray[numNodes - 1];
        }

        /// <summary>
        /// Read to get the values for each huffman leave
        /// </summary>
        /// <param name="BIS"></param>
        /// <param name="huffTree"></param>
        /// <returns></returns>
        private byte ReadCompressedByte(BitInputStream BIS, BinaryTreeNode<byte> huffTree)
        {
            while(true)
            {
                if (huffTree.LeftChild == null && huffTree.RightChild == null)
                {
                    return huffTree.Data;
                }
                if(ReadGivenBits(1,BIS) == 0)
                {
                    huffTree = huffTree.LeftChild;
                }
                else
                {
                    huffTree = huffTree.RightChild;
                }
            }
        }


        /// <summary>
        /// Method to Decompress the file
        /// </summary>
        /// <param name="BIS"></param>
        /// <param name="fn"></param>
        /// <param name="numbytes"></param>
        /// <param name="HuffTree"></param>
        private void DecompressFile(BitInputStream BIS, string fn, long numbytes, BinaryTreeNode<byte> HuffTree)
        {
            using (FileStream fs = new FileStream(fn, FileMode.Create, FileAccess.Write))
            {
                int counter = 0;
                while(counter != numbytes)
                {
                    byte DataToWrite = ReadCompressedByte(BIS, HuffTree);
                    fs.WriteByte(DataToWrite);
                    counter++;
                }
            }
        }

        /// <summary>
        ///  Event Handler for Decompression Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxDecompressButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(uxOpenFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    string OpenFile = uxOpenFileDialog2.FileName;
                    using (BitInputStream BIS = new BitInputStream(OpenFile))
                    {
                        long first63Bits = ReadGivenBits(63, BIS);
                        long next9Bits = ReadGivenBits(9, BIS);
                        BinaryTreeNode<byte> huffTree;
                        
                        // If file length is 0, use a null HuffMan Tree
                        if (first63Bits == 0) 
                        {
                            huffTree = null;
                        }
                        else
                        {
                            huffTree = ReadHuffManTree(BIS, (int)next9Bits);
                        }
                        
                        if (uxSaveFileDialog2.ShowDialog() == DialogResult.OK)
                        {
                            string SaveFile = uxSaveFileDialog2.FileName;
                            DecompressFile(BIS, SaveFile, first63Bits, huffTree);
                            MessageBox.Show("Decompressed File Written");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
