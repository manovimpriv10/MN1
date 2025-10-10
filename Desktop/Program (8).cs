using static System.Console;
using System.Text;
using System.Xml.Linq;

namespace Lab9
{
    internal class Program
    {
        /// <summary>
        /// метод, который выводит на консоль содержимое файла 
        /// </summary>
        /// <param name="fname">Файл</param>
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

        /// <summary>
        /// Вычисляет среднее арифметическое всех положительных элементов файла
        /// </summary>
        /// <param name="path">файл</param>
        /// <returns></returns>
        static double Average2(string path)
        {
            int sum = 0, count = 0;
            try
            {
                using (var br = new BinaryReader(File.Open(path, FileMode.Open), Encoding.ASCII))
                {
                    var sz = br.BaseStream.Length / sizeof(int);
                    for (int i = 0; i < sz; i++)
                    {
                        int x = br.ReadInt32();
                        if (x > 0)
                            sum += x;
                        count++;
                    }
                }

            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки: {e.Message}");
            }
            return count == 0 ? 0.0 : 1.0 * sum / count;
        }

        /// <summary>
        /// Создает два новых файла, записав в первый все элементы исходного, большие заданного числа K, а во второй — все остальные.
        /// </summary>
        /// <param name="path">изначальный файл</param>
        /// <param name="k">Число K</param>
        /// <param name="greater">Файл с числами большими K</param>
        /// <param name="less">Файл с числами меньшими K</param>
        static void MorelessK(string path, int k, string greater, string less)
        {
            try
            {
                using (var br = new BinaryReader(File.Open(path, FileMode.Open), Encoding.ASCII))
                using (var bw1 = new BinaryWriter(File.Create(greater)))
                using (var bw2 = new BinaryWriter(File.Create(less)))
                {
                    var sz = br.BaseStream.Length / sizeof(int);
                    for (int i = 0; i < sz; i++)
                    {
                        int x = br.ReadInt32();
                        if (x > k)
                            bw1.Write(x);
                        else
                            bw2.Write(x);
                    }
                }
            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки: {e.Message}");
            }

        }
        /// <summary>
        /// Удаляет все его элементы, не удовлетворяющие заданному предикату
        /// </summary>
        /// <param name="path">файл</param>
        /// <param name="p">предикат</param>
        static void SatisfactionofPredicate(string path, Predicate<int> p)
        {
            try
            {
                using (var br = new BinaryReader(File.Open(path, FileMode.Open), Encoding.ASCII))
                using (var bww = new BinaryWriter(File.Create("temp.dat")))
                {
                    var sz = br.BaseStream.Length / sizeof(int);
                    for (int i = 0; i < sz; i++)
                    {
                        int x = br.ReadInt32();
                        if (p(x))
                            bww.Write(x);
                    }
                }
                File.Delete(path);
                File.Move("temp.dat", path);
            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки: {e.Message}");
            }
        }

        /// <summary>
        /// Обнуляет минимальный элемент 
        /// </summary>
        /// <param name="path">файл</param>
        static void ZeroOUTminimumelement(string path)
        {
            try
            {
                using (var fs = File.Open(path, FileMode.Open))
                using (var br = new BinaryReader(fs, Encoding.ASCII))
                using (var bw = new BinaryWriter(fs, Encoding.ASCII))
                {
                    int mn = int.MaxValue, mn_Pos = -1;
                    var sz = br.BaseStream.Length / sizeof(int);
                    for (int i = 0; i < sz; i++)
                    {
                        int x = br.ReadInt32();
                        if (x < mn)
                            (mn, mn_Pos) = (x, (int)fs.Position);
                    }
                    if (mn_Pos != -1)
                    {
                        fs.Seek(mn_Pos - sizeof(int), SeekOrigin.Begin);
                        bw.Write(0);
                    }
                }
            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки: {e.Message}");
            }
        }

        static int CountWords(string path) => File.ReadAllLines(path).Select(x => x.Split(' ', '\r', '\n')).Count();
      

        static void Main()
        {
            CreateFile("file0.dat");
            PrintBinFile("file0.dat");
            WriteLine();

            CreateFile("file1.dat", 10);
            PrintBinFile("file1.dat");
            WriteLine();

            CreateFile("file2.dat", -1, -2, -5);
            PrintBinFile("file2.dat");
            WriteLine();

            CreateFile("file3.dat", 1, 2, 5);
            PrintBinFile("file3.dat");
            WriteLine();

            CreateFile("file4.dat", -1, 45, -23, 34, 12, -3);
            PrintBinFile("file4.dat");
            WriteLine();
            WriteLine("_______________________________________________________________________");

            for (int i = 0; i < 5; i++)
            {
                WriteLine($"Элементы файла file{i}.dat:");
                PrintBinFile($"file{i}.dat");
                WriteLine($"Среднее арифметическое: {Average2($"file{i}.dat")}");
                WriteLine();

                int k = 0;
                MorelessK($"file{i}.dat", k, $"greater{i}.dat", $"less{i}.dat");
                WriteLine($"Файл с числами больших {k}");
                PrintBinFile($"greater{i}.dat");
                WriteLine();
                WriteLine($"Файл с числами меньшими {k}");
                PrintBinFile($"less{i}.dat");
                WriteLine();


                SatisfactionofPredicate($"file{i}.dat", x => x < 0);
                WriteLine("Элементы файла удовлетворяющее предикату:");
                PrintBinFile($"file{i}.dat");
                ZeroOUTminimumelement($"file{i}.dat");
                WriteLine("Изменненный файл где обнулен минимальный элемент:");
                PrintBinFile($"file{i}.dat");
                WriteLine("_______________________________________________________________________");
            }
        }
    }
}

/*< empty file >

10

- 1 - 2 - 5

1 2 5

- 1 45 - 23 34 12 - 3

_______________________________________________________________________
Элементы файла file0.dat:
< empty file >
Среднее арифметическое: 0

Файл с числами больших 0
<empty file>

Файл с числами меньшими 0
<empty file>

Элементы файла удовлетворяющее предикату:
< empty file >
Изменненный файл где обнулен минимальный элемент:
< empty file >
_______________________________________________________________________
Элементы файла file1.dat:
10
Среднее арифметическое: 10

Файл с числами больших 0
10

Файл с числами меньшими 0
<empty file>

Элементы файла удовлетворяющее предикату:
< empty file >
Изменненный файл где обнулен минимальный элемент:
< empty file >
_______________________________________________________________________
Элементы файла file2.dat:
-1 - 2 - 5
Среднее арифметическое: 0

Файл с числами больших 0
<empty file>

Файл с числами меньшими 0
-1 -2 -5

Элементы файла удовлетворяющее предикату:
-1 - 2 - 5
Изменненный файл где обнулен минимальный элемент:
-1 - 2 0
_______________________________________________________________________
Элементы файла file3.dat:
1 2 5
Среднее арифметическое: 2,6666666666666665

Файл с числами больших 0
1 2 5

Файл с числами меньшими 0
<empty file>

Элементы файла удовлетворяющее предикату:
< empty file >
Изменненный файл где обнулен минимальный элемент:
< empty file >
_______________________________________________________________________
Элементы файла file4.dat:
-1 45 - 23 34 12 - 3
Среднее арифметическое: 15,166666666666666

Файл с числами больших 0
45 34 12

Файл с числами меньшими 0
-1 -23 -3

Элементы файла удовлетворяющее предикату:
-1 - 23 - 3
Изменненный файл где обнулен минимальный элемент:
-1 0 - 3
_______________________________________________________________________*/
