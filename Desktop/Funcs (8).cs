using static System.Console;
namespace Lesson8
{
    internal class Funcs
    {
        /// <summary>
        /// метод, который выводит на консоль содержимое файла 
        /// </summary>
        /// <param name="fname">Файл</param>
        static void PrintBinFile(string fname)
        {
            try
            {
                using (var br = new BinaryReader(File.Open(fname, FileMode.Open)))
                {
                    while (br.PeekChar() != -1)
                        Write($"{br.ReadInt32()} ");
                    /*Write($"{br.ReadInt32()} ");*/
                    WriteLine();
                }
            }
            catch (FileNotFoundException)
            {
                WriteLine("Файл не найден");
            }
            catch (PathTooLongException)
            {
                WriteLine("Длинный путь к файлу");
            }
            catch (DirectoryNotFoundException)
            {
                WriteLine("Не найден путь");
            }
            catch (EndOfStreamException)
            {
                WriteLine("Чтение за концом файла");
            }
            
        }

        /// <summary>
        /// вывод на консоль квадратов всех элементов файла
        /// </summary>
        /// <param name="fname">Файл</param>
        static void PrintsquaresBinFile(string fname)
        {
            try
            {
                using (var br = new BinaryReader(File.Open(fname, FileMode.Open)))
                {
                    while (br.PeekChar() != -1)
                    {
                        int x = br.ReadInt32();
                        Write($"{x * x} ");
                    }
                    WriteLine();
                }
            }
            catch (IOException e)
            {
                WriteLine(e.Message);
            }
        }

        /// <summary>
        /// метод, вычисляющий количество чётных и нечётных элементов файла
        /// </summary>
        /// <param name="fname">файл</param>
        /// <returns></returns>
        static (int, int) CountOddANDeVENBinFile(string fname)
        {
            int even = 0, odd = 0;
            try
            {
                using (var br = new BinaryReader(File.Open(fname, FileMode.Open)))
                {
                    while (br.PeekChar() != -1)
                        if (br.ReadInt32() % 2 == 0)
                            even++;
                        else
                            odd++;

                }
            }
            catch (FileNotFoundException)
            {
                WriteLine("Файл не найден");
            }
            catch (PathTooLongException)
            {
                WriteLine("Длинный путь к файлу");
            }
            catch (DirectoryNotFoundException)
            {
                WriteLine("Не найден путь");
            }
            catch (EndOfStreamException)
            {
                WriteLine("Чтение за концом файла");
            }
            return (even, odd);
        }

        /// <summary>
        /// печатает содержимое файла по 15, 10 и 5 элементов в строке
        /// </summary>
        /// <param name="fname">Файл</param>
        /// <param name="n"></param>
        static void PrintBinFile_(string fname, int n = 10)
        {
            try
            {
                using (var br = new BinaryReader(File.Open(fname, FileMode.Open)))
                {
                    if (br.PeekChar() == -1)
                        WriteLine("<empty file>");
                    while (br.PeekChar() != -1)
                    {
                        for (int i = 0; i < n && br.PeekChar() != -1; i++)
                            Write($"{br.ReadInt32()} ");
                        WriteLine();
                    }
                }
            }
            catch (FileNotFoundException)
            {
                WriteLine("Файл не найден");
            }
            catch (PathTooLongException)
            {
                WriteLine("Длинный путь к файлу");
            }
            catch (DirectoryNotFoundException)
            {
                WriteLine("Не найден путь");
            }
            catch (EndOfStreamException)
            {
                WriteLine("Чтение за концом файла");
            }

        }



        static void Main()
        {
            PrintBinFile("nat15.dat");
            WriteLine();  

            PrintsquaresBinFile("nat15.dat");
            WriteLine();

            WriteLine($"Количество четных и нечетных чисел в файле: {CountOddANDeVENBinFile("nat15.dat")}");
            WriteLine();

            PrintBinFile_("nat15.dat", 5);
            WriteLine();

            PrintBinFile_("nat15.dat");
            WriteLine();

            PrintBinFile_("nat15.dat", 15);
            WriteLine();

            PrintBinFile_("empty.dat", 15);
            WriteLine();

            using (var bw = new BinaryWriter(File.Create("one.dat")))
                bw.Write(33);
            WriteLine();
            PrintBinFile_("one.dat");
        }
    }
}
/*LOG1
 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15

1 4 9 16 25 36 49 64 81 100 121 144 169 196 225

Количество четных и нечетных чисел в файле: (7, 8)

1 2 3 4 5
6 7 8 9 10
11 12 13 14 15

1 2 3 4 5 6 7 8 9 10
11 12 13 14 15

1 2 3 4 5 6 7 8 9 10 11 12 13 14 15

<empty file>


33*/