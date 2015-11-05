/* Worker.cs
 * Author: Alexander Wu
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.Scheduler
{
    /// <summary>
    /// A class to initialize a workers information
    /// </summary>
    public class Worker
    {
        private string _name;
        private bool[] _qualified;
        private int _timesScheduled;

        // Constructor for new Worker
        public Worker(string[] s)
        {
            _name = s[0];
            _qualified = new bool[s.Length - 1];

            int x;
            for (x = 1; x < s.Length; x++)
            {
                _qualified[x-1] = s[x] != "";
            }
        }
        public string Name
        {
            get { return _name; }
        }
        public int TimesScheduled
        {
            get {return _timesScheduled;}
        }
        public int numberOfTasks
        {
            get { return _qualified.Length; }
        }
        /// <summary>
        /// A method to determine whether the worker is qualified for a given task. 
        /// This method will take as its only parameter an int identifying the task, and it will return a bool
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool qualified(int i)
        {
            return _qualified[i];
        }
        /// <summary>
        /// A method to record that the worker has been scheduled one more time
        /// </summary>
        public void oneMoreTime()
        {
            _timesScheduled += 1;
        }
    }
}
