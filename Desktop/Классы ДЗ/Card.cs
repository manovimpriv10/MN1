
namespace Классы_ДЗ
{
    public class Card
    {
        /// <summary>
        /// Номер карты
        /// </summary>
        public string CardNumber { get; } = "1111111111111111";

        /// <summary>
        /// Имя владельца
        /// </summary>
        public string OwnerName { get; } = "Никита";

        /// <summary>
        /// Месяц и год окончания действия карты
        /// </summary>
        public string MonthANDyearexpiration { get; } = "12/2026";

        /// <summary>
        /// Банк-эмитент карты
        /// </summary>
        public string NameCard { get; } = "ВТБ";

        /// <summary>
        /// Сумма денег на счету
        /// </summary>
        private double balance = 10000;

        /// <summary>
        /// Поверка на корректность устанавливаемого значения
        /// </summary>
        public double Balance
        {
            get { return balance; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Баланс не может быть отрицательным!!!");
                }
                balance = value;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="cardNumber">Номер карты</param>
        /// <param name="ownerName">OwnerName</param>
        /// <param name="monthANDyearexpiration">Месяц и год окончания действия карты</param>
        /// <param name="nameCard">Банк-эмитент карты</param>
        /// <param name="balance"> Сумма денег на счету</param>
        public Card(string cardNumber, string ownerName, string monthANDyearexpiration, string nameCard,  double balance)
        {
            if (!string.IsNullOrEmpty(cardNumber) || cardNumber.Length == 16)
                CardNumber = cardNumber;

            if (!string.IsNullOrEmpty(ownerName))
                OwnerName = ownerName;

            if (!string.IsNullOrEmpty(monthANDyearexpiration))
                MonthANDyearexpiration = monthANDyearexpiration;


            if (!string.IsNullOrEmpty(nameCard))
                NameCard = nameCard;

            if (balance >= 0)
                Balance = balance;
            
        }

        /// <summary>
        /// метод ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Номер карты - {CardNumber};\nИмя владельца - {OwnerName};\nМес/Год окончания действия карты - {MonthANDyearexpiration};\nНазвание карты - {NameCard};\nБаланс карты - {Balance};";
          
    }
}
