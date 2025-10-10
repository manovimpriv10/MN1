uusing static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace HomeworkATM
{
    public class Banknote
    {
        public int Denomination { get; }
        public string SerialNumber { get; }

        public Banknote(int denomination, string serialNumber)
        {
            Denomination = denomination;
            SerialNumber = serialNumber;
        }

        public override string ToString()
        {
            return $"Купюра {Denomination} руб. (серия: {SerialNumber})";
        }
    }

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
        /// Кассета с принятыми деньгами (теперь просто стек купюр)
        /// </summary>
        private Stack<Banknote> CassetteWITHmoney = new Stack<Banknote>();

        /// <summary>
        /// История транзакций
        /// </summary>
        private List<string> TransactionHistory = new List<string>();

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
        public override string ToString()
        {
            var groups = CassetteWITHmoney.GroupBy(b => b.Denomination)
                                         .Select(g => $"{g.Count()} по {g.Key}");
            return $"ID банкомата - {ATMid};\nБанк, которому принадлежит банкомат - {Bank};\n" +
                   $"Кассета с принятыми деньгами - {(groups.Any() ? string.Join(" и ", groups) : "пусто")};\n" +
                   $"История транзакций - {string.Join("\n", TransactionHistory)};\n" +
                   $"Секретный ключ - {SecretKey};";
        }

        /// <summary>
        /// Свойство возвращает общую сумму денег в кассете банкомата
        /// </summary>
        public double CashAmount
        {
            get { return CassetteWITHmoney.Sum(b => b.Denomination); }
        }

        /// <summary>
        /// Проверяет купюры и пополняет карту
        /// </summary>
        public void Replenishment(Card card, List<Banknote> banknotes)
        {
            int AmountDeposit = banknotes.Sum(b => b.Denomination);
            int[] validDenominations = { 50, 100, 200, 500, 1000, 2000, 5000 };

            foreach (var banknote in banknotes)
            {
                if (!validDenominations.Contains(banknote.Denomination))
                {
                    TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({banknote.Denomination}) => Такого номинала не существует!]");
                    WriteLine($"Нельзя внести купюру! Номинал {banknote.Denomination} не поддерживается.");
                    return;
                }
            }

            // Проверка срока действия карты
            string[] cardDate = card.MonthANDyearexpiration.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string currentDate = "04/25";
            string[] current = currentDate.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (int.Parse(cardDate[1]) < int.Parse(current[1]) ||
                (int.Parse(cardDate[1]) == int.Parse(current[1]) && int.Parse(cardDate[0]) < int.Parse(current[0])))
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit}) => Проблема с датой карты!]");
                WriteLine("Проблема с датой карты!");
                return;
            }

            // Прием купюр
            foreach (var banknote in banknotes)
            {
                CassetteWITHmoney.Push(banknote);
                WriteLine($"Принята: {banknote}");
            }

            // Проверка банка-эмитента
            if (card.NameCard != Bank)
            {
                double amountWithCommission = AmountDeposit * 0.95;
                card.Balance += amountWithCommission;
                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit}) => Банк-эмитент другой, применена комиссия 5%. Баланс пополнен на {amountWithCommission} руб.]");
                WriteLine($"Баланс пополнен на {amountWithCommission} руб. (с учетом комиссии 5%)");
            }
            else
            {
                card.Balance += AmountDeposit;
                TransactionHistory.Add($"[{card.CardNumber}: <Пополнение> ({AmountDeposit}) => Баланс пополнен!]");
                WriteLine($"Баланс пополнен на {AmountDeposit} руб.");
            }
        }

        /// <summary>
        /// Снятие денег с карты
        /// </summary>
        public void Withdrawingmoney(Card card, int withdrawalAmount)
        {
            // Проверка срока действия карты
            string[] cardDate = card.MonthANDyearexpiration.Split('/', StringSplitOptions.RemoveEmptyEntries);
            string currentDate = "04/25";
            string[] current = currentDate.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (int.Parse(cardDate[1]) < int.Parse(current[1]) ||
                (int.Parse(cardDate[1]) == int.Parse(current[1]) && int.Parse(cardDate[0]) < int.Parse(current[0])))
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Проблема с датой карты!]");
                WriteLine("Проблема с датой карты!");
                return;
            }

            // Проверка баланса
            double amountToWithdraw = (card.NameCard != Bank) ? withdrawalAmount * 1.05 : withdrawalAmount;

            if (card.Balance < amountToWithdraw)
            {
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Недостаточно средств!]");
                WriteLine("Недостаточно средств для снятия!");
                return;
            }

            // Снятие денег
            if (card.NameCard != Bank)
            {
                card.Balance -= amountToWithdraw;
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Снято с учетом комиссии 5%: {amountToWithdraw} руб.]");
                WriteLine($"Снято {withdrawalAmount} руб. (с учетом комиссии 5%: {amountToWithdraw} руб.)");
            }
            else
            {
                card.Balance -= withdrawalAmount;
                TransactionHistory.Add($"[{card.CardNumber}: <Снятие> ({withdrawalAmount}) => Снято с баланса!]");
                WriteLine($"Снято {withdrawalAmount} руб.");
            }

            // Здесь должна быть логика выдачи купюр из кассеты
            // (упрощенно, без реального вывода купюр)
        }
    }
}