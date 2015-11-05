/* Scheduler.cs
 * Author: Alexander Wu
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.Scheduler
{
    public partial class Scheduler : Form
    {
        public Scheduler()
        {
            InitializeComponent();
        }
        private SearchableQueue<Worker> workerQueue = new SearchableQueue<Worker>();
        private int tasks;
        private int numberOfWorkers;

        /// <summary>
        ///  A method to read the input. This method will take as its only parameter a string giving the name of the file to read. 
        ///  It will return a SearchableQueue<Worker> containing the information read. It must first construct a new SearchableQueue<Worker>. 
        ///  Then with a using statement as in Lab Assignment 14, create a StreamReader from the given file name. Then for each line of the file, 
        ///  use the line's Split method to split it into fields delimited by commas (you can pass ',' as this method's only parameter). 
        ///  This method will return a string[ ] whose elements 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private SearchableQueue<Worker> readInput(string fileName)
        {
            int count = 0;
            SearchableQueue<Worker> SearchQueue = new SearchableQueue<Worker>();
            using (StreamReader input = new StreamReader(fileName))
            {
                while(!input.EndOfStream)
                {
                    string[] workerData = input.ReadLine().Split(',');
                    Worker newWorkerData = new Worker(workerData);
                    SearchQueue.Enqueue(newWorkerData);
                    count++;
                }
                tasks = SearchQueue.CurrentElement.Data.numberOfTasks;
                numberOfWorkers = count;
            }
            return SearchQueue;
        }

        /// <summary>
        /// A method to search the queue for the first worker qualified for a given task, and move that worker to the end of the queue. 
        /// This method will take as its only parameter an int identifying the task. It will return the worker it found. If you get to 
        /// the end of the queue without finding a qualified worker, throw an InvalidOperationException containing an appropriate message 
        /// (you will need to use the constructor that takes a string parameter).
        /// </summary>
        private LinkedListCell<Worker> Search(int task)
        {
            LinkedListCell<Worker> SearchCell = workerQueue.SetToFront();
            for (int i = 0; i < numberOfWorkers; i++)
            {
                if(SearchCell.Data.qualified(task))
                {
                    workerQueue.CurrentElement.Data.oneMoreTime();
                    return SearchCell;
                }
                SearchCell = workerQueue.AdvanceElement();
            }
            throw new InvalidOperationException("Unable to find Qualified worker for task " + task);
        }

        /// <summary>
        /// A method to display the minimum and maximum number of times a worker was scheduled. It should take no parameters and return nothing
        /// </summary>
        private void timesScheduled()
        {
            LinkedListCell<Worker> SearchCell = workerQueue.SetToFront();
            int MAX = 0;
            int MIN = SearchCell.Data.TimesScheduled; // start value for the minimum
            int searchValue;
            
            try
            {
                while (true)
                {
                    searchValue = SearchCell.Data.TimesScheduled;
                    if (searchValue > MAX)
                    {
                        MAX = searchValue;
                    }

                    if (searchValue < MIN)
                    {
                        MIN = searchValue;
                    }
                    SearchCell = workerQueue.AdvanceElement();
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("All Workers were scheduled at least " + MIN + " times and at most " + MAX + " times.");
                return;
            }
        }

        /// <summary>
        /// Event Handler for Input File button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxInput_Click(object sender, EventArgs e)
        {
            if(uxOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = uxOpenFileDialog.FileName;
                    uxTextBox.Text = fileName;
                    uxTextBox.SelectionStart = uxTextBox.Text.Length; // set cursor position to the right
                    uxComputeSchedule.Enabled = true;               
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        /// <summary>
        /// Event Handler for Compute Schedule Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uxComputeSchedule_Click(object sender, EventArgs e)
        {
            string fileName = uxTextBox.Text;
            workerQueue = readInput(fileName);
            int daysTotal = (int)uxNumericUpDown.Value;
            
            if(uxSaveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string saveFile = uxSaveDialog.FileName;

                    using (StreamWriter output = new StreamWriter(saveFile))
                    {
                        // Starts first row of CSV File
                        output.Write(",");
                        for(int i =0;i<tasks;i++)
                        {
                            output.Write(i + ",");
                        }
                        output.WriteLine();
                        LinkedListCell<Worker> SearchCell = workerQueue.SetToFront();
                        for (int days = 1; days <= daysTotal; days++)
                        {
                            output.Write((days).ToString() + ',');
                            for (int j = 0; j < tasks; j++)
                            {
                                SearchCell = Search(j);
                                output.Write(SearchCell.Data.Name + ',');
                                SearchCell = workerQueue.MoveToBack();
                                SearchCell = workerQueue.SetToFront();
                            }
                            output.WriteLine();
                        }
                        timesScheduled();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}