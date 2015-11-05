/* Author: Alex Wu
 * Calculator HW 1
 */


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.Calculator
{
    /// <summary>
    /// Program to emulate a 5 function calculator
    /// </summary>
    public partial class Calculator : Form
    {
        // Private field for Calculator display Mode
        private bool mDisplayMode;
        private Stack screenChars = new Stack();
        
        /// <summary>
        /// Organizes the GUI
        /// </summary>
        public Calculator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Retrieves the substring containing the number
        /// </summary>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private string RetrieveNumber(string s, int i)
        {
            int curIndex;
            int termIndex = -1;
            for (curIndex = i; curIndex < s.Length;curIndex++ )
            {
                if (s[curIndex] == '+' || 
                    s[curIndex] == '-' ||
                    s[curIndex] == 'X' ||
                    s[curIndex] == '/' ||
                    s[curIndex] == '^' ||
                    s[curIndex] == ')')
                {
                    termIndex = curIndex;
                    break;
                }
            }
            return s.Substring(i, termIndex - i);
        }

        /// <summary>
        /// A method to find an Operators Precedance
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private int precedance(char c)
        {
            switch (c)
            {
                case '(':
                case ')':
                    return 0;
                case '+':
                case '-':
                    return 1;
                case 'X':
                case '/':
                    return 2;
                case '^':
                    return 3;
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// A method to perform an operation
        /// </summary>
        /// <param name="first"></param>
        /// <param name="binOperator"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private double binOperation(double first, char binOperator, double second)
        {
            switch(binOperator)
            {
                case '+':
                    return first + second;
                case '-':
                    return first - second;
                case 'X':
                    return first * second;
                case '/':
                    return first / second;
                case '^':
                    return Math.Pow(first, second);
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Method to simplify the stack
        /// </summary>
        /// <param name="st"></param>
        /// <param name="c"></param>
        void simplifyStack(Stack st, char c)
        {

            if (c =='^')
            {
                st.Push(c);
            }

            else
            {
                double result = (double)st.Pop();
                char lOperator = (char)st.Peek();
                
                while( precedance(lOperator) >= precedance(c))
                {
                    ///////////////////// ENTER LOOP ///////////////////////////////
                    if ( (char)st.Peek() == '(' )
                    {
                        bool isStackEmpty;
                        st.Pop();
                        isStackEmpty = (st.Count == 0);
                        
                        if (!isStackEmpty)
                        {
                            //we have a char to evaluate
                            if ((char)st.Peek() == '~')
                            {
                                st.Pop();
                                st.Push(result * -1);
                                return;
                            }
                            else
                            {
                                st.Push(result);
                                return;
                            }
                        }
                        else
                        {
                            st.Push(result);
                            return;
                        }

                    }
                    else
                    {
                        //lOperator is not '(', is an operation:
                        //pop operator:
                        lOperator = (char)st.Pop();
                        double number2 = (double)st.Pop();
                        result = binOperation(number2, lOperator, result);
                        lOperator = (char)st.Peek();
                    }
                    ///////////////////// EXIT LOOP ///////////////////////////////
                }
                st.Push(result);
                st.Push(c);
            }
            return;
        }

        /// <summary>
        /// Event handler for inputting buttons onto the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            // Stores which button was clicked
            Button ClickedButton = sender as Button;

            // clear the display if we are in display mode
            if (mDisplayMode)
            {
                uxScreen.Clear();
                mDisplayMode = false;
            }

            // Append the text of the button that was clicked
            uxScreen.AppendText(ClickedButton.Text);
        }

        /// <summary>
        /// Event handler for Clear button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxClear_Click(object sender, EventArgs e)
        {
            if (uxScreen.TextLength>0)
            {
                uxScreen.Text = uxScreen.Text.Remove(uxScreen.TextLength - 1);
            }

            if (mDisplayMode)
            {
                uxScreen.Clear();
            }
        }

        /// <summary>
        /// Function to determine if char is an operator
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool isOperator(char c)
        {
            if (c == '+' ||
                c == '-' ||
                c == 'X' ||
                c == '/' ||
                c == '(' ||
                c == ')' ||
                c == '^')
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// check if an object is a character.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool isCharacter(object c)
        {
            try
            {
                char asdf = (char)c;
            }catch (Exception)
            {
                return false; 
            }
                return true;
        }

        /// <summary>
        /// Event handler for pressing the '=' button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxEqual_Click(object sender, EventArgs e)
        {
            try
            {
                // Evaluate each character in the screen
                int curIndex;
                string toParse=  '(' +  uxScreen.Text + ')';

                //GET DA STAXXX
                for (curIndex = 0; curIndex < toParse.Length; curIndex++)
                {
                    char curChar = toParse[curIndex];

                    // Push a left parenthesis into the stack
                    if (curChar == '(')
                    {
                        screenChars.Push(curChar);
                    }

                    // Check if top element in stack is an operator
                    else if ( isCharacter(screenChars.Peek()) )
                    {
                        // Check to see if current character is a minus sign
                        if (curChar == '-')
                        {
                            // double negation, so pop the last minus sign
                            if ((char)screenChars.Peek() == '~')
                            {
                                screenChars.Pop();
                            }
                            // Push a negation
                            else
                            {
                                screenChars.Push('~');
                            }
                        }

                        // Next character must be beginning of a number
                        else
                        {
                            string s = RetrieveNumber(toParse, curIndex);
                            int skipIndex = s.Length;
                            double number = Convert.ToDouble(s);

                            if ((char)screenChars.Peek() == '~')
                            {
                                number *= -1;
                                screenChars.Pop();
                            }
                            screenChars.Push(number);
                            curIndex += (skipIndex -1);       
                        }
                    }
                    else
                    {
                        //"we may be able to simplify the stack..."
                        simplifyStack(screenChars, curChar);
                    }
                }
            }
            catch (Exception)
            {
                uxScreen.Text = "Error1";
            }

                if (screenChars.Count == 1)
                {
                    try
                    {
                        double result = (double)screenChars.Pop();
                        uxScreen.Text = result.ToString();
                    }catch
                    {
                        uxScreen.Text = "Error2";
                    }
                    
                }
                else
                {
                    uxScreen.Text = "Error3";
                }
                screenChars = new Stack();

                //set display mode to clear screen on next input:
                mDisplayMode = true;
            }
        }
    }
