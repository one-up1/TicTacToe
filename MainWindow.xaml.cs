using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int Size = 3;

        private readonly int[][] cells = new int[Size][];

        private List<Cell> availableCells;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < Size; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            InitCells();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label label = (Label)sender;
            label.Content = "X";

            int row = Grid.GetRow(label);
            int col = Grid.GetColumn(label);
            Console.WriteLine(row + "," + col);

            if (cells[row][col] == 0)
            {
                cells[row][col] = 1;
                availableCells.Remove(new Cell(row, col, label));
                if (Check(1))
                {
                    Console.WriteLine("Gewonnen!");
                    grid.Children.Clear();
                    InitCells();
                }
                else
                {
                    Cell cell = availableCells[random.Next(0, availableCells.Count)];
                    Console.WriteLine("making move: " + cell.Row + "," + cell.Col);
                    cells[cell.Row][cell.Col] = 2;
                    cell.Label.Content = "O";
                    availableCells.Remove(cell);
                    if (Check(2))
                    {
                        Console.WriteLine("Verloren!");
                        //grid.Children.Clear();
                        //InitCells();
                    }
                }
            }
        }

        private void InitCells()
        {
            availableCells = new List<Cell>();
            for (int row = 0; row < Size; row++)
            {
                cells[row] = new int[Size];
                for (int col = 0; col < Size; col++)
                {
                    Label label = new Label();
                    label.HorizontalContentAlignment = HorizontalAlignment.Center;
                    label.VerticalContentAlignment = VerticalAlignment.Center;
                    label.BorderThickness = new Thickness(2);
                    label.BorderBrush = Brushes.Black;
                    label.FontSize = 72;
                    label.FontWeight = FontWeights.Bold;
                    label.MouseDown += Label_MouseDown;

                    grid.Children.Add(label);
                    Grid.SetRow(label, row);
                    Grid.SetColumn(label, col);

                    availableCells.Add(new Cell(row, col, label));
                }
            }
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

        private class Cell
        {
            public Cell(int row, int col, Label label)
            {
                Row = row;
                Col = col;
                Label = label;
            }

            // Equals() and GetHashCode() zijn voor het verwijderen van cellen uit availableCells.

            public override bool Equals(object obj)
            {
                if (obj is Cell cell)
                {
                    return Row == cell.Row && Col == cell.Col;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return Row + Col;
            }

            public int Row { get; }

            public int Col { get; }

            public Label Label { get; }
        }
    }
}
