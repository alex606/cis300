/* SearchableQueue.cs
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
    /// SearchableQueue Class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchableQueue<T>
    {

        private LinkedListCell<T> headerCell = new LinkedListCell<T>();
        private LinkedListCell<T> CurCellPreceding;
        private LinkedListCell<T> lastCell;
        
        // Initial Searchable Queue construction
        public SearchableQueue()
        {
            CurCellPreceding = headerCell;
            lastCell = headerCell;
        }

        // Property to get whether the current element is past the end of the queue
        public bool pastEndQueue
        {
            get { return CurCellPreceding == lastCell; }
        }

        // A property to get the current element
        public LinkedListCell<T> CurrentElement
        {
            get 
            {
                if (pastEndQueue)
                {
                    throw new InvalidOperationException();
                }
                return CurCellPreceding.Next; 
            } 
        }

        // Enqueue an element at the back
        public void Enqueue(T x)
        {
            LinkedListCell<T> newCell= new LinkedListCell<T>();
            newCell.Data = x;
            if (headerCell.Next == null)
            {
                headerCell.Next = newCell;
                lastCell = newCell;
                return;
            }
            // Incrememnt the Current Cell and set the new cell as the back cell
            CurCellPreceding = lastCell;
            lastCell.Next = newCell;
            lastCell = newCell;
            CurCellPreceding.Next = lastCell;
        }

        // Advance the current element to the next element following the current element
        // If the current element is already past the end of the queue, it should do nothing
        public LinkedListCell<T> AdvanceElement()
        {
            if(pastEndQueue)
            {
                return CurrentElement;
            }

            CurCellPreceding = CurCellPreceding.Next;
            return CurCellPreceding.Next;
        }

        // Set the current element to be the element at the front of the queue
        public LinkedListCell<T> SetToFront()
        {
            CurCellPreceding = headerCell;
            return CurCellPreceding.Next;
        }

        // Move the current element to the back of the queue
        public LinkedListCell<T> MoveToBack()
        {
            if(pastEndQueue)
            {
                throw new InvalidOperationException();
            }

            if(CurrentElement == lastCell)
            {
                return CurrentElement;
            }

            lastCell.Next = CurrentElement;
            CurCellPreceding.Next = CurrentElement.Next;
            lastCell = lastCell.Next;
            lastCell.Next = null;
         
            return CurrentElement;
        }

    }
}
