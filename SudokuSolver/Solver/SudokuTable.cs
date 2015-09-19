using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Solver
{
    public class SudokuTable
    {
        public const int TableHeight = 9;
        public const int TableWidth = 9;

        private string _rawFile;

        public SudokuTableItem[][] SudokuTableItems { get; set; }

        public int Lenght => SudokuTableItems.Length;

        public SudokuTable(string filePath)
        {
            _rawFile = File.ReadAllText(filePath);
           
            InitTableArray();
            GenerateSudokuTableFromString();
        }

        /// <summary>
        /// initialising the array
        /// </summary>
        private void InitTableArray()
        {
            SudokuTableItems = new SudokuTableItem[TableHeight][];
            for (int i = 0; i < TableHeight; i++)
            {
                SudokuTableItems[i] = new SudokuTableItem[TableWidth];
            }
        }

        /// <summary>
        /// fill the array with values
        /// </summary>
        private void GenerateSudokuTableFromString()
        {
            int x = 0, y = 0;
            var lines = _rawFile.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length != TableHeight)
            {
                throw new Exception("To many lines or not enough lines");
            }

            foreach (var line in lines)
            {
                var values = line.Split(' ');
                if (values.Length != TableWidth)
                {
                    throw new Exception($"To many values or not enough values on line{(x + 1)}");
                }
                x = 0;
                foreach (var value in values)
                {
                    int result = 0;
                    if (int.TryParse(value, out result))
                    {
                        if (result >= 0 && result <= TableWidth)//0 if empty
                        {
                            SudokuTableItems[y][x] = new SudokuTableItem(result,x,y);
                        }
                        else
                        {
                            throw new Exception($"Not valid number on line{(x + 1)} value:{(y + 1)}");
                        }
                    }
                    else
                    {
                        throw new Exception($"Not valid value on line{(x + 1)} value:{(y + 1)}");
                    }
                    x++;
                }
                y++;
            }
        }

        public SudokuTable(SudokuTableItem[][] items)
        {
            SudokuTableItems = items;
        }

        public SudokuTableItem this[int y,int x] => SudokuTableItems[y][x];

        public void DisplayConsole()
        {
            for (int i = 0; i < Lenght; i++)
            {
                for (int j = 0; j < Lenght; j++)
                {
                    Console.Write("{0} ",SudokuTableItems[i][j]);
                }
                Console.WriteLine();
            }
        }
    }
}
