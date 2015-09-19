using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuSolver.Solver;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "File3.txt";
            if (args.Length > 1)
            {
                file = args[1];
            }
            var table = new SudokuTable(file);
            var solver = new  Solver.SudokuSolver(table);
            solver.Solve();


            table.DisplayConsole();
            Console.ReadLine();
        }
    }
}
