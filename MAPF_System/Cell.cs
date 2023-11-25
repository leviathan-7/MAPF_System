﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace MAPF_System
{
    public class Cell
    {
        private bool isBlock;
        private bool wasvisited;
        private int idVisit;

        public Cell(bool isBlock, bool wasvisited = false)
        {
            this.isBlock = isBlock;
            this.wasvisited = wasvisited;
        }
        public Cell(string str)
        {
            string[] arr = str.Split(' ');
            // Задание параметров клетки на основе строки из файла
            isBlock = arr[0] == "True";
            wasvisited = arr[1] == "True";
        }
        public bool IsBlock() { return isBlock; }
        public void MakeBlock() { isBlock = true; }
        public Cell CopyWithoutBlock() { return new Cell(false, wasvisited); }
        public void MakeVisit(int n) { 
            wasvisited = true;
            idVisit = n;
        }
        public bool WasVisit() { return wasvisited; }
        public int IdVisit() { return idVisit; }
        public string ToStr() { return isBlock + " " + " " + wasvisited; }

    }
}
