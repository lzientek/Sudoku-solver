using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class SudokuTableItem
    {
        private int _value;
        private bool _hasAValue;

        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public static List<int> AllPossibleValues
        {
            get { return new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; }
        }

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                HasAValue = _value > 0;
            }
        }

        public bool HasAValue
        {
            get { return _hasAValue; }
            private set
            {
                _hasAValue = value;
            }
        }

        public List<int> PossibleValues { get; set; }

        public SudokuTableItem(int value, int posX, int posY)
        {
            Value = value;
            if (!HasAValue)
            {
                PossibleValues = AllPossibleValues;
            }
            else
            {
                PossibleValues = new List<int>();
            }
            PositionX = posX;
            PositionY = posY;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
