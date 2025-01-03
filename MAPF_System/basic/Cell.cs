using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Cell
    {
        public TunellInterface tunell;
        public bool isBlock;
        public bool wasvisited { get; private set; }
        public int idVisit { get; private set; }
        public bool isBad { get; private set; }
        public bool isTunell
        {
            get { return !(tunell is null); }
        }
        public String str 
        { 
            get { return isBlock + " " + wasvisited + " " + idVisit + " " + isBad; } 
        }
        public Cell copyWithoutBlock
        { 
            get { return new Cell(false, wasvisited, idVisit, isBad); }
        } 

        public Cell(bool isBlock, bool wasvisited = false, int idVisit = -1, bool isBad = false)
        {
            MakeCell(isBlock, wasvisited, idVisit, isBad);
        }
        public Cell(string str)
        {
            string[] arr = str.Split(' ');
            MakeCell(arr[0] == "True", arr[1] == "True", int.Parse(arr[2]), arr[3] == "True");
        }
        private void MakeCell(bool isBlock, bool wasvisited, int idVisit, bool isBad)
        {
            this.isBlock = isBlock;
            this.wasvisited = wasvisited;
            this.idVisit = idVisit;
            this.isBad = isBad;
        }
        public void MakeBad() { isBad = true; }
        public void MakeVisit(int n)
        {
            wasvisited = true;
            idVisit = n;
        }
        public int ReversBlock()
        {
            if (isBlock = !isBlock)
                return 1;
            return 2;
        }
    }
}
