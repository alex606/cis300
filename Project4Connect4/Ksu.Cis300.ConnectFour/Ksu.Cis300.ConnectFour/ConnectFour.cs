/* Author: Alexander Wu
 * Connect Four game
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

namespace Ksu.Cis300.ConnectFour
{
    public partial class ConnectFour : Form
    {
        public ConnectFour()
        {
            InitializeComponent();
            pushButtons[0] = uxButton1;
            pushButtons[1] = uxButton2;
            pushButtons[2] = uxButton3;
            pushButtons[3] = uxButton4;
            pushButtons[4] = uxButton5;
            InitializeBoard();
        }

        // Private Constant Fields
        private const int MAX_ROWS = 4;
        private const int MAX_COLUMNS = 5;
        private const int MAX_CELLS = MAX_ROWS * MAX_COLUMNS;

        // Private fields for the grid
        private Button[] pushButtons = new Button[MAX_COLUMNS];
        private Button[,] BoardButtons = new Button[MAX_ROWS, MAX_COLUMNS];
        private string[,] BoardValues = new string[MAX_ROWS, MAX_COLUMNS];
        private int[] numColumnPieces = new int[MAX_COLUMNS];

        // tracker
        private int numPlays;

        // Players
        private string[] player = { "X", "O" };


        /// <summary>
        /// Method responsible for placing the 20 buttons forming the grid onto the board
        /// </summary>
        private void InitializeBoard()
        {
            uxFlowLayoutPanel.Controls.Clear();
            uxTextBox.Text = "Your move";
            numPlays = 0;
            for (int i = 0; i < MAX_COLUMNS;i++)
            {
                pushButtons[i].Enabled = true;
                numColumnPieces[i] = 0;
            }

            for (int x = MAX_ROWS - 1; x >= 0; x--)
            {
                for (int y = 0; y < MAX_COLUMNS; y++)
                {
                    Button newButtonToAdd = new Button();
                    newButtonToAdd.Size = new Size(uxButton1.Width, uxFlowLayoutPanel.Height / 5);
                    newButtonToAdd.Font = new Font(FontFamily.GenericSansSerif, 14.0F);
                    newButtonToAdd.Enabled = false;
                    uxFlowLayoutPanel.Controls.Add(newButtonToAdd);
                    BoardButtons[x, y] = newButtonToAdd;
                }
            }
        }

        /// <summary>
        /// A method to find the length of a sequence of pieces
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="vertInc"></param>
        /// <param name="horInc"></param>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        private int findLengthSequence(int row, int column, int vertInc, int horInc, int playerIndex)
        {
            int piecelength = 0;
            if (row >= MAX_ROWS || row < 0 || column >= MAX_COLUMNS || column < 0) // check if gone out of bounds
            {
                return piecelength;
            }
            while (player[playerIndex] == BoardValues[row, column])
            {
                row += vertInc;
                column += horInc;
                piecelength++;
                if (row >= MAX_ROWS || row < 0 || column >= MAX_COLUMNS || column < 0) // check if gone out of bounds
                {
                    break;
                }
            }
            return piecelength;
        }

        /// <summary>
        /// Method to make a given play on the baord. Returns a boolean to indicate if the game is won
        /// </summary>
        /// <param name="column"></param>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        private bool MakeGivenPlay(int column, int playerIndex)
        {
            // Increment the pieces for the column and the number of plays
            BoardValues[numColumnPieces[column], column] = player[playerIndex];
            numColumnPieces[column]++;
            numPlays++;

            // Determine if play wins the game
            int sequenceLength = 0;
            int[,] directions = new int[7, 2] { { -1, -1 }, { 1, 1 }, { -1, 0 }, { 1, 0 }, { -1, 1 }, { 1, -1 }, { 0, -1 } };

            for (int x = 0; x < directions.GetLength(0); x++)
            {
                if (x % 2 == 0)
                {
                    sequenceLength = 0; // zeroize when looking at new direction
                }

                int horInc = directions[x, 0];
                int vertInc = directions[x, 1];
                sequenceLength += findLengthSequence(numColumnPieces[column] + vertInc - 1, column + horInc, vertInc, horInc, playerIndex);
                if (sequenceLength >= 3)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to undo the last play on the given column for the internal representation
        /// </summar>y
        /// <param name="column"></param>
        private void Undo(int column)
        {
            BoardValues[numColumnPieces[column]-1, column] = null;
            numPlays--;
            numColumnPieces[column]--;
        }

        /// <summary>
        /// A method to find the value of a given play
        /// </summary>
        /// <param name="column"></param>
        /// <param name="playerIndex"></param>
        /// <param name="BestResponse"></param>
        /// <returns></returns>
        private int FindPlayValue(int column, int playerIndex, out int BestResponse)
        {
            // Initialize out variable to 1
            BestResponse = -1;

            // if given play ends the game, out parameter should be set to -1
            if (MakeGivenPlay(column, playerIndex))
            {
                Undo(column);
                return 1;
            }

            // Draw determination
            else if (numPlays == MAX_CELLS)
            {
                Undo(column);
                return 0;
            }

            int dummy = 0;
            int guess = -1;
            for (int i = 0; i < MAX_COLUMNS;i++ )
            {
                if(numColumnPieces[i] < MAX_ROWS)
                {
                    int temp = FindPlayValue(i, 1 - playerIndex, out dummy);
                    if(temp > guess)
                    {
                        guess = temp;
                        BestResponse = i;
                    }
                    if(temp == 1)
                    {
                        break;
                    }
                }
            }

            Undo(column);
            return -guess;
        }

        /// <summary>
        /// A Method to end the game
        /// </summary>
        /// <param name="s"></param>
        private void EndGame(string s)
        {
            if (MessageBox.Show(s, s, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                InitializeBoard();
            }
            else
            {
                Application.Exit();
            }
        }

        private void ProcessUserPlay(int column)
        {
            // Disable the buttons on top
            foreach (Button button in pushButtons)
            {
                button.Enabled = false;
            }

            // Update text on the appropriate button
            BoardButtons[numColumnPieces[column], column].Text = player[0];

            // Update text for text box
            uxTextBox.Text = "My Move";

            // Call the update method
            Update();

            // Determine the the best response to the user's play
            // Subtype 1: Opening move
            int BestResponse;
            if (numPlays == 0)
            {
                int[] Responses = { 1, 0, 0, 4, 3 };
                BestResponse = Responses[column];
            }

            // Subtype 2: other moves afterwards
            else
            {
                // Make the user's play on the internal board
                FindPlayValue(column, 0, out BestResponse);
            }
            
            MakeGivenPlay(column, 0);
            BoardButtons[numColumnPieces[BestResponse], BestResponse].Text = "O";
            
            if (MakeGivenPlay(BestResponse, 1))
            {
                EndGame("I win, play again?");
            }

            // If game was not won, determine whether the game board is fill. If so, end the game appropriately
            else if (numPlays == MAX_CELLS)
            {
                EndGame("The game is a draw. Play again?");
            }

            else
            {
                // Re-Enable buttons that are still in play
                uxTextBox.Text = "Your move";
                for (int x = 0; x < MAX_COLUMNS; x++)
                {
                    pushButtons[x].Enabled = (numColumnPieces[x] < MAX_ROWS);
                }
            }
            
        }

        /// <summary>
        /// Event Handlers for clicking the buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxButton1_Click(object sender, EventArgs e)
        {
            ProcessUserPlay(0);
        }
        private void uxButton2_Click(object sender, EventArgs e)
        {
            ProcessUserPlay(1);
        }
        private void uxButton3_Click(object sender, EventArgs e)
        {
            ProcessUserPlay(2);
        }
        private void uxButton4_Click(object sender, EventArgs e)
        {
            ProcessUserPlay(3);
        }
        private void uxButton5_Click(object sender, EventArgs e)
        {
            ProcessUserPlay(4);
        }
    }
}
