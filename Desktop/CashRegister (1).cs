using System.Xml.Linq;

namespace Lab11
{
    public class CashRegister
    {
        /// <summary>
        /// Название магазина
        /// </summary>
        public string ShopName { get; } = "AppleStore";

        /// <summary>
        /// Номер кассы
        /// </summary>
        private int registerNumber = 1;

        /// <summary>
        /// Свойство для номера кассы
        /// </summary>
        public int RegisterNumber
        {
            get { return registerNumber; }
            set { if (value > 0) registerNumber = value; }
        }

        /// <summary>
        /// Выручка за день
        /// </summary>
        public int Revenue { get; private set; } = 0;

        /// <summary>
        /// История покупок
        /// </summary>
        private List<Product[]> purchaseHistory;

        /// <summary>
        /// Конструктор класса CashRegister 
        /// </summary>
        /// <param name="shopName">Название магазина</param>
        /// <param name="registerNumber">Номер кассы</param>
        /// <param name="revenue">Выручка за день</param>
        public CashRegister(string shopName, int registerNumber, int revenue)
        {
            if (!string.IsNullOrEmpty(shopName))
                ShopName = shopName;
            RegisterNumber = registerNumber;
            if (revenue > 0)
                Revenue = revenue;
            purchaseHistory = new List<Product[]>();
        }

        /// <summary>
        /// Метод ToString для вывода объектов класса
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Название магазина: {ShopName}\nНомер кассы: {RegisterNumber}\nВыручка: {Revenue}";

        /// <summary>
        /// Метод обновляет дневную выручку, прибавляя к ней сумму цен товаров, а также обновляет историю покупок.
        /// </summary>
        /// <param name="cart">Корзина покупок</param>
        public void makePurchase(Product[] cart)
        {
            foreach (var p in cart)
                Revenue += p.Price;
            purchaseHistory.Add(cart);
        }

        /// <summary>
        /// свойство, возвращающее количество продаж за день
        /// </summary>
        public int SalesCount
        {
            get { return purchaseHistory.Count; }
        }

        /// <summary>
        /// Создаёт текстовый файл-отчёт, в который записывает: номер кассы, общую выручку за день, историю покупок за день и обнуляет выручку за день, очищает историю покупок
        /// </summary>
        public void endDaySession()
        {
            using (var sw = new StreamWriter($"DayReport{RegisterNumber}.txt"))
            {
                sw.WriteLine($"Номер кассы: {RegisterNumber}\nВыручка: {Revenue}\n\nИстория покупок: ");
                foreach (var arr in purchaseHistory)
                    foreach (var p in arr)
                        sw.WriteLine(p);
            }
            Revenue = 0;
            purchaseHistory.Clear();
        }

    }
}
