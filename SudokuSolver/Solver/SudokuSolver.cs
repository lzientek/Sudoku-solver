using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class SudokuSolver
    {
        public SudokuTable Table { get; set; }
        private DateTime _starTime;
        private DateTime _endTime;
        public const int SubBlockSize = 3;

        public SudokuSolver(SudokuTable table)
        {
            Table = table;
        }

        public bool Solve()
        {
            _starTime = DateTime.Now;
            try
            {
                RecursiveFind();
                _endTime = DateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _endTime = DateTime.Now;
                return false;
            }
            finally
            {
                Console.WriteLine("solved in {0}ms", (_endTime -_starTime).Milliseconds);
            }

        }

        private SudokuTableItem[][] SubBlock(SudokuTableItem item)
        {
            int blockX = (int)Math.Floor(item.PositionX / (double)SubBlockSize);
            int blockY = (int)Math.Floor(item.PositionY / (double)SubBlockSize);

            var result = new SudokuTableItem[SubBlockSize][];
            for (int y = 0; y < SubBlockSize; y++)
            {
                result[y] = new SudokuTableItem[SubBlockSize];
                for (int x = 0; x < SubBlockSize; x++)
                {
                    result[y][x] = Table[blockY * SubBlockSize + y, blockX * SubBlockSize + x];
                }
            }
            return result;
        }

        private void RecursiveFind()
        {
            bool solved = true;

            for (int i = 0; i < Table.Lenght; i++)
            {
                for (int j = 0; j < Table.Lenght; j++)
                {
                    if (!Table[i, j].HasAValue)
                    {
                        solved = false;
                        SearchPossibleValues(Table[i, j]);
                    }
                }
            }

            if (!solved)
            {
                RecursiveFind();
            }
        }


        private bool SearchPossibleValues(SudokuTableItem item)
        {
            if (item.HasAValue) { return true; }

            RemoveUnpossibleValues(item);

            //check result
            if (item.PossibleValues.Count == 1)
            {
                item.Value = item.PossibleValues.First();
                item.PossibleValues.Clear();
                return true;
            }

            PossibleValueCheck(item);
            if (item.HasAValue)// si le possible value check a donné quelque chose
            {
                return true;
            }

            if (item.PossibleValues.Count <= 0)
            {
                throw new Exception("Not valid sudoku");
            }
            
            return false;
        }

        private void RemoveUnpossibleValues(SudokuTableItem item)
        {
//check in block
            var subBlock = SubBlock(item);
            for (int i = 0; i < SubBlockSize; i++)
            {
                for (int j = 0; j < SubBlockSize; j++)
                {
                    if (subBlock[i][j].HasAValue)
                    {
                        item.PossibleValues.Remove(subBlock[i][j].Value);
                    }
                }
            }

            //on parcours la ligne et la collone
            for (int i = 0; i < Table.Lenght; i++)
            {
                if (Table[i, item.PositionX].HasAValue)
                {
                    item.PossibleValues.Remove(Table[i, item.PositionX].Value);
                }
                if (Table[item.PositionY, i].HasAValue)
                {
                    item.PossibleValues.Remove(Table[item.PositionY, i].Value);
                }
            }
        }

        private void PossibleValueCheck(SudokuTableItem item)
        {
            var subBlock = SubBlock(item);

            foreach (var possibleValue in item.PossibleValues)
            {
                bool valuePossibleX = true;
                bool valuePossibleY = true;
                bool valuePossibleBlock = true;

                //line or column check
                for (int i = 0; i < Table.Lenght; i++)
                {
                    if (item.PositionY != i &&
                        Table[i, item.PositionX].PossibleValues.Contains(possibleValue))
                    {
                        valuePossibleY = false;
                    }
                    if (item.PositionX != i &&
                        Table[item.PositionY, i].PossibleValues.Contains(possibleValue))
                    {
                        valuePossibleX = false;
                    }
                }

                //check in block
                for (int i = 0; i < SubBlockSize; i++)
                {
                    for (int j = 0; j < SubBlockSize; j++)
                    {
                        if ( !(item.PositionY == i && item.PositionX == j )
                            && subBlock[i][j].PossibleValues.Contains(possibleValue))
                        {
                            valuePossibleBlock = false;
                        }
                    }
                }

                if (valuePossibleX || valuePossibleY || valuePossibleBlock)
                {
                    item.Value = possibleValue;
                    item.PossibleValues.Clear();
                    return;
                }
            }
        }
    }
}
