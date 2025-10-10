namespace ConsoleAppконтрольная
{
    public class University
    {
        public string Name { get; } = "Юфу";

        public string Country { get; private set; } = "Россия";

        private int CountSt = 100;

        public int countSt
        {
            get { return CountSt; }

            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество студентов должно быть положительным");
                value = CountSt;
            }


        }
        public double StudentToTeacherProportion { get; private set; } = 1.0;
        public int ForeignStudentsPercentile { get; private set; } = 20;

        public University(string name, string country, int countST, double studentToTeacherProportion, int foreignStudentsPercentile)
        {
            Name = name;
            Country = country;
            CountSt = countST;
            StudentToTeacherProportion = studentToTeacherProportion;
            ForeignStudentsPercentile = foreignStudentsPercentile;
        }

        public override string ToString() => $"Название {Name}, Страна {Country}, Колво {countSt}, Пропорция {StudentToTeacherProportion}, Проценты {ForeignStudentsPercentile}";

        public int r => (int)StudentToTeacherProportion;
    }
}
