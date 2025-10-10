using static System.Console;
namespace Классы_ДЗ
{
    internal class Program
    {
        static void Main()
        {
            var c = new Card("1111111111111111", "Никита", "11/25", "ВТБ", 10000);
            WriteLine(c);
            WriteLine();

            var k = new ATM("ВТБ");
            WriteLine(k);
            WriteLine();

            var depositMoney = new Dictionary<int, int> { { 1000, 2 }, { 500, 3 } };

            k.Replenishment(c, depositMoney);
            WriteLine() ;
            WriteLine(k);





        }
    }
}
