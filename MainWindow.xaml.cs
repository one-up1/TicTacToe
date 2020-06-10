using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
                cells[i] = new int[Size];
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            label.Content = "X";

            int row = Grid.GetRow(label);
            int col = Grid.GetColumn(label);

            cells[row][col] = 1;

            Check(1);
        }

        private bool Check(int i)
        {
            bool success;

            // Horizontaal.
            for (int row = 0; row < Size; row++)
            {
                success = true;
                for (int col = 0; col < Size; col++)
                {
                    if (cells[row][col] != i)
                    {
                        success = false;
                        break;
                    }
                }
                if (success)
                {
                    Console.WriteLine("Horizontal at row " + row);
                    return true;
                }
            }

            // Vertikaal.
            for (int col = 0; col < Size; col++)
            {
                success = true;
                for (int row = 0; row < Size; row++)
                {
                    if (cells[row][col] != i)
                    {
                        success = false;
                        break;
                    }
                }
                if (success)
                {
                    Console.WriteLine("Vertical at col " + col);
                    return true;
                }
            }

            // Diagonaal (naar rechts).
            success = true;
            for (int x = 0; x < Size; x++)
            {
                if (cells[x][x] != i)
                {
                    success = false;
                    break;
                }
            }
            if (success)
            {
                Console.WriteLine("Right diagonal");
                return true;
            }

            // Diagonaal (naar links).
            success = true;
            for (int x = Size - 1; x >= 0; x--)
            {
                if (cells[x][Size - x - 1] != i)
                {
                    success = false;
                    break;
                }
            }
            if (success)
            {
                Console.WriteLine("Left diagonal");
                return true;
            }

            Console.WriteLine("No success");
            return false;
        }
    }
}
