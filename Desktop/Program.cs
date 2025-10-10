using System.Text;
using static System.Console;
namespace CW2
{
    public class Print
    {
        public static void PrintBinFile(string s)
        {
            try
            {
                using (var a = new BinaryReader(File.Open(s, FileMode.Open), Encoding.ASCII))
                {
                    var sz = a.BaseStream.Length / sizeof(double);
                    if (sz == 0)
                        Write("<empty file> ");
                    for (int i = 0; i < sz; i++)
                        Write($"{a.ReadDouble()} ");
                    WriteLine();
                }
            }
            catch (FileNotFoundException)
            {

            }
        }

        public static void PrintIntBinFile(string s)
        {
            try
            {
                using (var a = new BinaryReader(File.Open(s, FileMode.Open), Encoding.ASCII))
                {
                    var sz = a.BaseStream.Length / sizeof(int);
                    if (sz == 0)
                        Write("<empty file> ");
                    for (int i = 0; i < sz; i++)
                        Write($"{a.ReadInt32()} ");
                    WriteLine();
                }
            }
            catch (FileNotFoundException)
            {

            }
        }
    }

    public class CW
    {
        public static Dictionary<string, int> FirstA(string s)
        {
            var dict = new Dictionary<string, int>();
            try
            {
                foreach (var sw in File.ReadAllLines(s))
                {
                    var parts = sw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var name = parts[0];
                    if (!int.TryParse(parts[1], out int price))
                    {
                        WriteLine("Невозможно запарсить");
                        continue;
                    }
                    if (dict.ContainsKey(name))
                        dict[name] = Math.Min(dict[name], price);
                    else
                        dict[name] = price;
                }
            }
            catch (IOException err)
            {
                WriteLine($"Ошибка: {err.Message}");
            }
            return dict;
        }
        public static HashSet<string>[] FirstB(int N)///Тупо список продуктов
        {
            var arr = new HashSet<string>[N];
            for (int i = 0; i < N; i++)
                arr[i] = File.ReadAllLines($"file{i}.txt").Select(p => p.Replace(" ", "")).ToHashSet();
            return arr;
        }

        public static double[] SecondA(string FilePath) => File.ReadAllLines(FilePath)
                                                            .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                                            .Select(word => word.Length).Average()).ToArray();
        public static void ToBinaryFile(double[] averages, string outputPath)
        {
            try
            {
                using (var writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
                {
                    foreach (double avg in averages)
                    {
                        writer.Write(avg);
                    }
                }
            }
            catch (IOException err)
            {
                WriteLine($"Ошибка: {err.Message}");
            }
        }                                                
        public static void SecondB(string inputPath, string outputPath)
        {
            string text = File.ReadAllText(inputPath);
            var parts = text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                Console.WriteLine("Входной файл не содержит чисел.");
                return;
            }

            var numbers = parts.Select(int.Parse).ToArray();
            using (BinaryWriter writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
            {
                int currentMax = numbers[0]; // Первый элемент - начальный максимум

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (i == 0)
                        writer.Write(currentMax);
                    else
                    {
                        currentMax = Math.Max(currentMax, numbers[i]);
                        writer.Write(currentMax);
                    }
                }
            }
        }

        public static List<University> ParsingUniversities(string fname)
        {
            return File.ReadAllLines(fname)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line =>
                {
                    var parts = line.Split(',');
                    return new University(
                        parts[0].Replace(" ", ""),
                        parts[1].Replace(" ", ""),
                        int.Parse(parts[2].Replace(" ", "")),
                        double.Parse(parts[3].Replace(" ", "")),
                        int.Parse(parts[4].Replace(" ", ""))
                    );
                })
                .ToList();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = CW.FirstA("Need.txt"); //тут имя файла
            foreach (var pair in s)
                WriteLine($"{pair.Key} {pair.Value}");


            int N = 3; // Замените на нужное количество файлов
            var arr = CW.FirstB(N);
            var allProducts = arr.Aggregate(new HashSet<string>(), (x, y) => { x.UnionWith(y); return x; });

            var commonProducts = arr.Aggregate(arr.First(), (x, y) => x.Intersect(y).ToHashSet());

            WriteLine("Продукты, купленные хотя бы одним покупателем:");
            foreach (var product in allProducts)
                WriteLine(product);

            WriteLine("\nПродукты, купленные всеми покупателями:");
            foreach (var product in commonProducts)
                WriteLine(product);

            string inputFile = "input.txt";
            string outputFile = "output.dat";

            var averages = CW.SecondA(inputFile);
            CW.ToBinaryFile(averages, outputFile);

            WriteLine("Результат записан в " + outputFile);
            Print.PrintBinFile(outputFile);


            string inputFilePath = "input2.txt";
            string outputFilePath = "output2.dat";

            try
            {
                CW.SecondB(inputFilePath, outputFilePath);
                WriteLine("\nБинарный файл успешно создан.");
                Print.PrintIntBinFile(outputFilePath);
            }
            catch (Exception ex)
            {
                WriteLine($"Ошибка: {ex.Message}");
            }


            var un = new University("ЮФУ", "Россия", 35763, 10.0, 17);
            WriteLine(un.ToString()); // Явный вызов ToString
            WriteLine(un);           // Неявный вызов ToString

            // Демонстрация парсинга
            try
            {
                var universities = CW.ParsingUniversities("universities.csv");
                foreach (var university in universities)
                    WriteLine(university);
            }
            catch (Exception ex)
            {
               WriteLine($"Ошибка при парсинге: {ex.Message}");
            }
        }
    }
}
