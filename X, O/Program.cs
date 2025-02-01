using System.Collections.Specialized;
using System.Diagnostics;

namespace _1
{
    internal class Program
    {
        static char[,] init_field()
        {
            char[,] field = new char[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    field[i, j] = '.';
                }
            }

            return field;
        }

        static void print_field(char[,] field)
        {
            Console.Write(" ");

            for (int i = 0; i < 3; i++)
            {
                Console.Write($"|{i + 1}");
            }

            Console.WriteLine("|");

            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i + 1}");

                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"|{field[i, j]}");
                }

                Console.WriteLine("|");
            }
        }

        static int[] get_coords()
        {
            int i = 0, n;

            int[] coords = new int[2];

            string c;

            for (; i <= 1; i++)
            {
                c = Console.ReadLine();

                if (int.TryParse(c, out n))
                {
                    if (n < 1 || n > 3)
                    {
                        throw new InvalidOperationException("Неправильно введённое значение!");
                    }
                    else
                    {
                        coords[i] = n;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Неверный ввод!");
                }
            }

            return coords;
        }

        static void Win_print_field(char[,] field, char winner)
        {
            Console.Clear();

            Console.Write(" ");

            for (int i = 0; i < 3; i++)
            {
                Console.Write($"|{i + 1}");
            }

            Console.WriteLine("|");

            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i + 1}");

                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"|");

                    if (field[i, j] == winner)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(field[i, j]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(field[i, j]);
                    }
                }

                Console.WriteLine("|");
            }
        }

        static void process(char[,] field)
        {
            char winner;

            int[] coords = new int[2];

            do
            {
                coords = get_coords();

                field[coords[1] - 1, coords[0] - 1] = 'X';

                Console.Clear();

                print_field(field);

                winner = is_win(field);

                if (winner != ' ')
                {
                    Win_print_field(field, winner);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Выиграли {winner}.");
                }

                coords = get_coords();

                field[coords[1] - 1, coords[0] - 1] = 'O';

                Console.Clear();

                print_field(field);

                winner = is_win(field);

                if (winner != ' ')
                {
                    Win_print_field(field, winner);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine($"Выиграли {winner}.");
                }
            } while (winner == ' ');
        }

        static char is_win(char[,] field)
        {
            for (int i = 0; i < 3; i++)
            {
                if (field[i, 0] == field[i, 1] && field[i, 0] == field[i, 2] && field[i, 0] != '.')
                {
                    return field[i, 0];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                if (field[0, i] == field[1, i] && field[0, i] == field[2, i] && field[0, i] != '.')
                {
                    return field[0, i];
                }
            }

            if (field[0, 0] == field[1, 1] && field[0, 0] == field[2, 2] && field[0, 0] != '.')
            {
                return field[0, 0];
            }

            if (field[0, 2] == field[1, 1] && field[0, 2] == field[2, 0] && field[0, 2] != '.')
            {
                return field[0, 2];
            }

            return ' ';
        }

        static void Main(string[] args)
        {
            char[,] field = new char[,] { { } };

            field = init_field();

            print_field(field);

            try
            {
                process(field);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
