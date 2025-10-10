using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using static System.Console;

namespace _1111
{
    internal class Funcs
    {
        /// <summary>
        /// Создает файл по массиву чисел
        /// </summary>
        /// <param name="path">файл</param>
        /// <param name="a"></param>
        static void CreateFile(string path, params int[] a)
        {
            try
            {
                using (var bw = new BinaryWriter(File.Create(path)))
                {
                    foreach (var x in a)
                        bw.Write(x);
                }

            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки: {e.Message}");
            }

        }
        static void PrintBinFile(string fname)
        {
            try
            {
                using (var br = new BinaryReader(File.Open(fname, FileMode.Open), Encoding.ASCII))
                {
                    var sz = br.BaseStream.Length / sizeof(int);
                    if (sz == 0)
                        Write("<empty file>");
                    for (int i = 0; i < sz; i++)
                        Write($"{br.ReadInt32()} ");
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

        static void binaryFILEnumbers(string fname, int N = 20, int a = -50, int b = 50)
        {
            
            try
            {
                using (var bw = new BinaryWriter(File.Create(fname)))
                {

                    while (N != 0)
                    {
                        Random random = new Random();
                        int randomNumber = random.Next(a, b);
                        bw.Write(randomNumber);
                        N--;
                    }
                }

            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки: {e.Message}");
            }
        }

        static int kTHelementFile(string path, int k)
        {
            if (k <= 0)
                throw new ArgumentException();
            try
            {
                using (var fs = File.Open(path, FileMode.Open))
                using (var br = new BinaryReader(fs, Encoding.ASCII))
                {
                    fs.Seek(sizeof(int) * (k - 1), SeekOrigin.Begin);
                    return br.ReadInt32();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("Элемент с указанным индексом не существует в файле (индекс за пределами файла).");
                return -1;
            }

        }

        static int f(string fname) => File.ReadAllText(fname).Split(new char[] { ' ', '\r', '\n' }).Count();



        static void Main()
        {
            binaryFILEnumbers("nat15.dat");
            PrintBinFile("nat15.dat");
            WriteLine();

            CreateFile("file2.dat", 1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            PrintBinFile("file2.dat");
            WriteLine(kTHelementFile("file2.dat", 11));

            WriteLine(f("12.txt"));
        }
    }
}
