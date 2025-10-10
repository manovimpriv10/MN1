using static System.Console;
namespace Lab11
{
    internal class Funcs
    {
        static void Main()
        {
            var p1 = new Product("abs", "", -10);
            WriteLine(p1);
            WriteLine();

            var p2 = new Product("1234567891234", "Lamborghini", 100000000);
            WriteLine(p2);
            WriteLine();

            var c1 = new CashRegister("", -5, -100);
            WriteLine(c1);
            WriteLine();

            var c2 = new CashRegister("LamborghiniShop", 123, 50000000);
            WriteLine(c2);
            WriteLine();

            WriteLine($"Текущее количество покупок: {c1.SalesCount}");
            c1.makePurchase(new Product[] { p1, p2, p1});
            WriteLine($"Текущее количество покупок: {c1.SalesCount}");
            c1.endDaySession();
        }
    }
}
/*Артикул: 0000000000001
Наименование товар: Iphone
Цена товара: 50000

Артикул: 1234567891234
Наименование товар: Lamborghini
Цена товара: 100000000

Название магазина: AppleStore
Номер кассы: 1
Выручка: 0

Название магазина: LamborghiniShop
Номер кассы: 123
Выручка: 50000000

Текущее количество покупок: 0
Текущее количество покупок: 1*/