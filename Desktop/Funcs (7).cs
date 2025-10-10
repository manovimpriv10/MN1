using System.Security.Cryptography;

namespace ConsoleApp1
{
    public class Funcs
    {
        /// <summary>
        /// Метод возвращает массив переведённых в нижний регистр слов, которые начинаются в тексте c заглавной буквы.
        /// </summary>
        /// <param name="s">строка</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static string[] LowerCaseANDcapitalLetter(string s)
        {
            if (s == null)
                throw new ArgumentNullException();
            if (s.Length == 0)
                throw new ArgumentException();
            return s.Split(new char[] { '.', ',', ':', ';', '-', '!', '?', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries).Where(w => char.IsUpper(w.First())).Select(w => w.ToLower()).ToArray();
        }

        /// <summary>
        /// Функция находит самые короткие строки нечётной длины в рядах (rows) ступенчатого массива и возвращает массив, состоящий из них
        /// </summary>
        /// <param name="arr">ступенчатый массив</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArithmeticException"></exception>
        public static string[] shortestLinesOfoddLengthINrows(string[][] arr)
        {
            if (arr == null)
                throw new ArgumentNullException();
            if (arr.Length == 0)
                throw new ArgumentException();
            var res = new List<string>();
            foreach (var row in arr)
            {
                var tmp = row.Where(x => x.Length % 2 != 0).ToList();
                if (tmp.Count > 0)
                {
                    string s = tmp.OrderByDescending(x => x.Length).Last();
                    res.Add(s);
                }
            }
            return res.ToArray();
        }

        static void Main()
        {

        }
    }
}