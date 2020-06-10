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
        private readonly List<Cell> availableCells = new List<Cell>();
        private readonly Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            // Voeg rijen en kolommen toe aan de grid.
            for (int i = 0; i < Size; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            InitCells();
        }

        private void InitCells()
        {
            // Initialiseer cellen en voeg per cel een Label toe aan het Grid.
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

                    // Het Cell object en de Label hebben een referentie naar elkaar
                    // zodat we dit kunnen gebruiken bij het maken van een zet.
                    Cell cell = new Cell(row, col, label);
                    label.Tag = cell;
                    availableCells.Add(cell);

                    grid.Children.Add(label);
                    Grid.SetRow(label, row);
                    Grid.SetColumn(label, col);
                }
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Maak een zet als er nog niet eerder op dezelfde Label is geklikt.
            // Als het spel hierna door kan gaan maken we een willekeurige zet.
            Cell cell = (Cell)((Label)sender).Tag;
            if (cells[cell.Row][cell.Col] == 0)
            {
                if (Set(cell, 1))
                {
                    Set(availableCells[random.Next(0, availableCells.Count)], 2);
                }
            }
        }

        private bool Set(Cell cell, int value)
        {
            // Zet de cel op de waarde en de Label op een X of een O.
            Console.WriteLine("Setting " + cell + " to " + value);
            cells[cell.Row][cell.Col] = value;
            cell.Label.Content = value == 1 ? "X" : "O";

            // Als dit leidt tot 3 op een rij heeft de gebruiker gewonnen of verloren.
            if (Check(value))
            {
                GameOver(value == 1 ? "Gewonnen!" : "Verloren!");
                return false;
            }

            // Als er geen cellen meer over zijn is het gelijkspel.
            if (availableCells.Count == 1)
            {
                GameOver("Gelijkspel");
                return false;
            }

            // En anders halen we de cel uit de List en kan het spel doorgaan.
            availableCells.Remove(cell);
            return true;
        }

        private bool Check(int value)
        {
            bool success;

            // Horizontaal.
            for (int row = 0; row < Size; row++)
            {
                success = true;
                for (int col = 0; col < Size; col++)
                {
                    if (cells[row][col] != value)
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
                    if (cells[row][col] != value)
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
            for (int i = 0; i < Size; i++)
            {
                if (cells[i][i] != value)
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
            for (int i = Size - 1; i >= 0; i--)
            {
                if (cells[i][Size - i - 1] != value)
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

        private void GameOver(string message)
        {
            // Toon bericht, maak de cellen leeg en initialiseer ze opnieuw.
            MessageBox.Show(message, Title, MessageBoxButton.OK, MessageBoxImage.Information);
            availableCells.Clear();
            grid.Children.Clear();
            InitCells();
        }

        private class Cell
        {
            public Cell(int row, int col, Label label)
            {
                Row = row;
                Col = col;
                Label = label;
            }

            public override string ToString()
            {
                return Row + "," + Col;
            }

            public int Row { get; }

            public int Col { get; }

            public Label Label { get; }
        }
    }
}
