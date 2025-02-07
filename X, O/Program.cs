using System.Collections.Specialized;
using System.Diagnostics;

namespace _1
{
	internal class Program
	{
		const int field_size = 3;

		static char[,] init_field()
		{
			char[,] field = new char[field_size, field_size];

			for (int i = 0; i < field_size; i++)
			{
				for (int j = 0; j < field_size; j++)
				{
					field[i, j] = '.';
				}
			}

			return field;
		}

		static void print_field(char[,] field, char winner)
		{
			Console.Clear();

			Console.Write(" ");

			for (int i = 0; i < field_size; i++)
			{
				Console.Write($"|{i + 1}");
			}
			
			Console.WriteLine("|");

			for (int i = 0; i < field_size; i++)
			{
				Console.Write($"{i + 1}");

				if (winner == ' ')
				{
					for (int j = 0; j < field_size; j++)
					{
						Console.Write($"|{field[i, j]}");
					}
				}
				else
				{
					for (int j = 0; j < field_size; j++)
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
					if (n < 1 || n > field_size)
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

		static void process(char[,] field)
		{
            bool sumbol = false;
			char s;

			char winner = ' ';

			int[] coords = new int[2];

			do
			{
				try
				{
					coords = get_coords();
				}
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

				if (sumbol)
				{
					s = 'X';
				}
				else
				{
					s = 'O';
				}

                field[coords[1] - 1, coords[0] - 1] = s;

				winner = is_win(field);
				
				print_field(field, winner);

				if (winner != ' ')
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.WriteLine($"Выиграли {winner}.");
				}

				sumbol = !sumbol;
			} while (winner == ' ');
		}

		static char is_win(char[,] field)
		{
			for (int i = 0; i < field_size; i++)
			{
				if (field[i, 0] == field[i, 1] && field[i, 0] == field[i, 2] && field[i, 0] != '.')
				{
					return field[i, 0];
				}
			}

			for (int i = 0; i < field_size; i++)
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

			print_field(field, ' ');

			process(field);
		}
	}
}
