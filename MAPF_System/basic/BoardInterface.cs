﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public interface BoardInterface
    {
        string name { get; }
        bool WasGame { get; }
        int X { get; }
        int Y { get; }
        bool isEnd { get; }
        void Draw(Graphics t, bool b = true, Tuple<int, int> C = null, Tuple<int, int> C1 = null, bool viewtunnel = true);
        BoardInterface CopyWithoutBlocks();
        void MakeStep(BoardInterface Board, int kol_iter_a_star);
        string Save(string name_, bool b = false);
        int ReversBlock(Tuple<int, int> c);
        bool Move(Tuple<int, int> C0, Tuple<int, int> C1);
        void PlusUnit();
        Tuple<Tuple<int, int>, Tuple<int, int>> MinusUnit();
        void PlusColumn();
        void PlusRow();
        void DelBlokcs();
        bool DelUnits();
    }
}
