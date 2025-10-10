using System.Xml.Linq;
using static System.Console;
using static System.IO.File;
using static System.Math;

namespace ConsoleAppконтрольная
{
    public static class PrintSet
    {
        /// <summary>
        /// Выводит множество на консоль
        /// </summary>
        /// <typeparam name="T">Тип</typeparam>
        /// <param name="a">Множество</param>
        public static void Println<T>(this HashSet<T> a)
        {
            foreach (var x in a)
                Write($"{x} ");
            WriteLine();
        }
    }
    internal class Program
    {




        // Задание 1 Словари
        static Dictionary<string, int> f(string fname)
        {
            if (string.IsNullOrEmpty(fname))
                throw new ArgumentNullException();
            Dictionary<string, int> Di = new Dictionary<string, int>();
            try
            {
                var P = ReadLines(fname);
                foreach (string line in P)
                {
                    string[] prc = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (Di.ContainsKey(prc[0]))
                        Di[prc[0]] = Min(int.Parse(prc[1]), Di[prc[0]]);
                    else Di[prc[0]] = int.Parse(prc[1]);
                }
            }

            catch (IOException e)
            {
                WriteLine(e.Message);
            }
            return Di;
        }

        static (HashSet<string>, HashSet<string>) f(int n)
        {
            var res = new HashSet<string>[n];

            for (int i = 0; i < n; i++)
            {
                res[i] = File.ReadAllLines("  ").ToHashSet();
            }
            return (res.Aggregate(res[0], (x, y) => x.Union(y).ToHashSet()), res.Aggregate(res[0], (x, y) => x.Intersect(y).ToHashSet()));
        }
            
        

        // Задание 2 Bin1
        static void Task2Bin1(string fname)
        {
            if (string.IsNullOrEmpty(fname))
                throw new ArgumentException("Ошибка: ");
            var arr = ReadAllLines(fname).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(w => w.Length).Average()).ToArray();
            try
            {
                using (BinaryWriter bw = new BinaryWriter(Create($"{fname.Split('.', StringSplitOptions.RemoveEmptyEntries)[0]}.dat")))
                {
                    foreach (var avg_len in arr)
                    {
                        bw.Write(avg_len);
                        Write($"{avg_len} ");
                    }
                }
            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки: {e}");
            }
        }

        static void Task2B(string path)
        {
            var f = File.ReadAllText(path).Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            var max = int.MinValue;
            using (var bw = new BinaryWriter(File.Create("task123123")))
            {
                max = f[0];
                bw.Write(max);
                Console.Write($"{max} ");
                foreach (var elem in f[1..])
                {
                    if (max < elem)
                        max = elem;
                    bw.Write(max);
                    Console.Write($"{max}  ");
                }
            }
        }






        static void Main()
        {
            var s = f("input-files.txt"); //тут имя файла
            foreach (var pair in s)
                WriteLine($"{pair.Key} {pair.Value}");


            Task2Bin1("1.txt");
            WriteLine();
            Task2B("2.txt");

            var un = new University("ЮФУ", "Россия", 35763, 10.0, 17);
            WriteLine(un.ToString()); // Явный вызов ToString
            WriteLine(un);           // Неявный вызов ToString



        }
    }
}
