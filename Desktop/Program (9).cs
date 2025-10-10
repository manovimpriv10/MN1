using System.Collections.Generic;
using static System.Console;
using static System.Net.Mime.MediaTypeNames;

namespace lab10
{
    internal class Program
    {
        /// <summary>
        /// Создает текстовый файл, содержащий N строк, первая из которых состоит из одного символа C, вторая — из двух, третья — из трёх и т.д.
        /// </summary>
        /// <param name="fname">Название файла</param>
        /// <param name="N">Количество строк</param>
        /// <param name="C">символ</param>
        static void CreateNtextfile(string fname, int N, char C)
        {
            try
            {
                using (var sw = new StreamWriter(fname))
                {
                    for (int i = 1; i <= N; i++)
                        sw.WriteLine(new string(C, i));
                }
            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки {e.Message}");
            }
        }

        /// <summary>
        /// Дописывает содержимое второго файла в конец первого..
        /// </summary>
        /// <param name="fname">Имя файла</param>
        /// <param name="fname2">Имя файлап</param>
        static void Addсontent1_2(string fname, string fname2)
        {
            try
            {
                using (var sw = new StreamWriter(fname, true))
                using (var sr = new StreamReader(fname2))
                {
                    sw.Write('\n');
                    while (!sr.EndOfStream)
                        sw.WriteLine(sr.ReadLine());
                }
            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки {e.Message}");
            }
        }

        /// <summary>
        /// Находит количество пустых строк в этом файле
        /// </summary>
        /// <param name="fname">имя файла</param>
        /// <returns></returns>
        static int CountblankLinesINfile(string fname)
        {
            int c = 0;
            try
            {
                using (var sr = new StreamReader(fname))
                {
                    while (!sr.EndOfStream)
                        if (sr.ReadLine() == "")
                            c++;

                }
            }
            catch (IOException e)
            {
                WriteLine($"Код ошибки {e.Message}");
            }
            return c;
        }

        /// <summary>
        /// Создает текстовый файл и записать в него все слова длины K из исходного файла по одному слову в строке.
        /// </summary>
        /// <param name="fname">имя файла</param>
        /// <param name="k">длина</param>
        /// <exception cref="ArgumentException"></exception>
        static void wordsOFlengthK(string fname, int k)
        {
            if (k <= 0)
                throw new ArgumentException();
            File.WriteAllLines("res.txt", File.ReadAllText(fname).Split(new char[] { ' ', ',', '.', '!', '?', '\n', '\r' }).Where(w => w.Length == k).ToArray());
        }

        /// <summary>
        /// Определяет количество строк файла, совпадающих с его первой строкой
        /// </summary>
        /// <param name="fname">имя файла</param>
        /// <returns></returns>
        static int CountmatchesWITHthefirstline(string fname)
        {
            var arr = File.ReadAllLines(fname);
            return arr.Skip(1).Count(s => s == arr[0]);
        }

        static int sumAllnumbersIntheFile(string fname) => File.ReadAllLines(fname).Select(s => s.Split(',').Select(x => int.Parse(x)).Sum()).Sum();




        static void Main()
        {
            //Задание 1
            var arr = new int[] { 0, 1, 7 };
            char ch = '*';
            foreach (var N in arr)
            {
                CreateNtextfile($"file{N}.txt", N, ch);
            }

            //Задание 2
            string path = "input-files/";
            Addсontent1_2(path + "task1.txt", path + "task2.txt");

            //Задание 3
            string[] fnames = new string[] { "3Empty.txt", "3NotEMpty.txt", "3SomeEmpty.txt" };
            foreach (var fname in fnames)
            {
                WriteLine($"Пустых строк: {CountblankLinesINfile(path + fname)} ");
            }
            WriteLine();

            //Задание 4
            wordsOFlengthK(path + "4Task.txt", 4);

            //Задание 5
            string[] fnames5 = new string[] { "5EMPTY.txt", "5OneLINE.txt", "5.txt", "5AllLinesSame.txt" };
            foreach (var fname in fnames5)
            {
                WriteLine($"Количество строк файла, совпадающих с его первой строкой: {CountmatchesWITHthefirstline(path + fname)} ");
            }
            WriteLine();

            //Задание 6
            WriteLine($"Сумма всех чисел в файле: {sumAllnumbersIntheFile(path + "file.csv")}");


        }
    }
}
/*Пустых строк: 0
Пустых строк: 0
Пустых строк: 3
Количество строк файла, совпадающих с его первой строкой: 0
Количество строк файла, совпадающих с его первой строкой: 0
Количество строк файла, совпадающих с его первой строкой: 2
Количество строк файла, совпадающих с его первой строкой: 5
Сумма всех чисел в файле: 666*/