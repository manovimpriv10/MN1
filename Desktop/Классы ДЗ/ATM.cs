using static System.Console;
namespace Классы_ДЗ
{
    public class ATM
    {
        /// <summary>
        /// id банкомата
        /// </summary>
        public long ATMid { get; } 

        /// <summary>
        /// Банк, которому принадлежит банкомат
        /// </summary>
        public string Bank { get; }

        /// <summary>
        /// Кассета с принятыми деньгами
        /// </summary>
        private Dictionary<int, int> CassetteWITHmoney = new Dictionary<int, int> { };

        /// <summary>
        /// История транзакций
        /// </summary>
        private List<string> TransactionHistory = new List<string> { };

        /// <summary>
        /// Секретный ключ для валидации инкассаторов
        /// </summary>
        private string SecretKey = "LeoMessi";

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="bank">Банк, которому принадлежит банкомат</param>
        public ATM(string bank)
        {
            var random = new Random();
            ATMid = random.NextInt64();
            if (!string.IsNullOrEmpty(bank))
                Bank = bank;
        }

        /// <summary>
        /// метод ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"ID банкомата - {ATMid};\nБанк, которому принадлежит банкомат - {Bank};\nКассета с принятыми деньгами - {CassetteWITHmoney.Aggregate("", (current, next) =>current + (current == "" ? "" : " и ") + $"{next.Value} по {next.Key}")};\nИстория транзакций - {string.Join("\n", TransactionHistory)};\nСекретный ключ - {SecretKey};";

        /// <summary>
        /// Свойство возвращает общее количество купюр в кассете банкомата
        /// </summary>
        public int CashAmount
        {
            get { return CassetteWITHmoney.Values.Sum(); }
        }

        public void Replenishment(Card card, Dictionary<int, int> banknotes)
        {
            int AmountDeposit = 0;

            int[] denominationsOfbills = new int[] { 50, 100, 200, 500, 1000, 2000, 5000 };

            foreach (var denomination in banknotes)
            {
                if (!denominationsOfbills.Contains(denomination.Key))
                {
                    TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({denomination.Key}) => Такого номинала не существует!]");
                    WriteLine("Нельзя внести купюру!");
                    return;
                }
                AmountDeposit += denomination.Key * denomination.Value;

            }

            string[] d1 = card.MonthANDyearexpiration.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string currentdate = "04/25";
            string[] d2 = currentdate.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (int.Parse(d1[1]) < int.Parse(d2[1]))
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit}) => Истек срок действия карты!]");
                WriteLine("Истек срок действия карты!");
                return;
            }

            else if (int.Parse(d1[1]) == int.Parse(d2[1]) && int.Parse(d1[0]) < int.Parse(d2[0]))
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit}) => Введена не коректная дата!]");
                WriteLine("Введена не коректная дата!");
                return;
            }


            if (card.NameCard != Bank)
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit}) => Банк-эмитент карты не совпадает с банком, владеющим банкоматом, вводится комиссия 5%");
                WriteLine("Банк-эмитент карты не совпадает с банком, владеющим банкоматом, вводится комиссия 5%");

                foreach (var denomination in banknotes)
                {
                    card.Balance += denomination.Key * denomination.Value * 0.95;

                    if (CassetteWITHmoney.ContainsKey(denomination.Key))
                    {
                        CassetteWITHmoney[denomination.Key] += denomination.Value;
                    }
                    else
                    {
                        CassetteWITHmoney[denomination.Key] = denomination.Value;
                    }
                }

                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit * 0.95}) => Баланс пополнен!]");
                WriteLine("Баланс пополнен!");
            }

            else
            {
                foreach (var denomination in banknotes)
                {
                    card.Balance += denomination.Key * denomination.Value;

                    if (CassetteWITHmoney.ContainsKey(denomination.Key))
                    {
                        CassetteWITHmoney[denomination.Key] += denomination.Value;
                    }
                    else
                    {
                        CassetteWITHmoney[denomination.Key] = denomination.Value;
                    }
                }

                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit}) => Баланс пополнен!]");
                WriteLine("Баланс пополнен!");
            }

        }

        public void Withdrawingmoney(Card card, int withdrawalAmount)
        {
            string[] d1 = card.MonthANDyearexpiration.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string currentdate = "04/25";
            string[] d2 = currentdate.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (int.Parse(d1[1]) < int.Parse(d2[1]))
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Истек срок действия карты!]");
                WriteLine("Истек срок действия карты!");
                return;
            }

            else if (int.Parse(d1[1]) == int.Parse(d2[1]) && int.Parse(d1[0]) < int.Parse(d2[0]))
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Введена не коректная дата!]");
                WriteLine("Введена не коректная дата!");
                return;
            }

            if (card.Balance < withdrawalAmount)
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Не достаточно средств для снятия!");
                WriteLine("Не достаточно средств для снятия!");
                return;
            }


            if (card.NameCard != Bank)
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Банк-эмитент карты не совпадает с банком, владеющим банкоматом, вводится комиссия 5%");
                WriteLine("Банк-эмитент карты не совпадает с банком, владеющим банкоматом, вводится комиссия 5%");

                card.Balance -= withdrawalAmount * 0.95;

                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount * 0.95}) => Снято с баланса!]");
                WriteLine($"Снято с баланса! {withdrawalAmount * 0.95}");
            }

            else
            {
                card.Balance -= withdrawalAmount;
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Снято с баланса!]");
                WriteLine($"Снято с баланса! {withdrawalAmount}");
            }

            
        }







    }
}
