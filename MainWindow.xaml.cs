using System;
using System.Windows;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Size = 3;

        private readonly int[][] cells = new int[Size][];

        public MainWindow()
        {
            InitializeComponent();

            // Initialiseer alle cellen.
            for (int i = 0; i < Size; i++)
            {
                cells[i] = new int[cells.Length];
            }

            for (int i = 0; i < Size; i++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Console.WriteLine(i + ":" + x);
                }
            }

            Console.WriteLine();

            for (int x = 0; x < Size; x++)
            {
                Console.WriteLine(x + ":" + x);
            }

            Console.WriteLine();

            for (int x = Size; x > 0; x--)
            {
                Console.WriteLine(Size - x + ":" + (x - 1));
            }
        }
    }
}
