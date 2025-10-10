namespace CW2
{
    public class University
    {
        public string Name { get; } = "ЮФУ";
        public string Country { get; private set; } = "Россия";

        private int studentCount = 100;
        public int StudentCount
        {
            get { return studentCount; }
            set {
                if (value <= 0)
                    throw new ArgumentException("Количество студентов должно быть положительным");
                studentCount = value;
            }
        }

        public double StudentToTeacherProportion { get; private set; } = 1.0;
        public int ForeignStudentsPercentile { get; private set; } = 20;

        public University(string name, string country, int studentCount,
                         double proportion, int foreignPercent)
        {
            Name = name;
            Country = country;
            StudentCount = studentCount;
            StudentToTeacherProportion = proportion;
            ForeignStudentsPercentile = foreignPercent;
        }

        public override string ToString() =>
            $"Название: {Name}, Страна: {Country}, Студентов: {StudentCount}, " +
            $"Соотношение студентов/преподавателей: {StudentToTeacherProportion}, " +
            $"Процент иностранцев: {ForeignStudentsPercentile}%";

        public int ProportionAsInt => (int)StudentToTeacherProportion;
    }
}
