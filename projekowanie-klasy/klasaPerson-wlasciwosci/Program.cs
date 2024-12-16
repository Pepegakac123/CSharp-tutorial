using System;
namespace klasaPerson_wlasciwosci
{
    using System.Linq;
    public class Person
    {
        private string familyName;
        private string firstName;
        private DateTime birthday;

        public Person(string familyName, string firstName, DateTime birthday)
        {
            FamilyName = familyName;
            FirstName = firstName;
            Birthday = birthday;
        }

        public string FamilyName
        {
            get => familyName;
            set
            {
                if (value == null)
                    throw new ArgumentException("Incorrect data for FamilyName");
                string processedValue = value.Trim();
                if (!processedValue.All(c => char.IsLetter(c) || c == '-'))
                    throw new ArgumentException("Incorrect data for FamilyName");
                string[] parts = processedValue.Split('-');
                if (parts.Length > 2)
                    throw new ArgumentException("Incorrect data for FamilyName");
                foreach (string part in parts)
                {
                    if (part.Length < 2)
                        throw new ArgumentException("Incorrect data for FamilyName");
                    if (!char.IsUpper(part[0]) || !part.Skip(1).All(char.IsLower))
                        throw new ArgumentException("Incorrect data for FamilyName");
                }
                familyName = processedValue;
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (value == null)
                    throw new ArgumentException("Incorrect data for FamilyName");
                string processedValue = value.Trim();
                if (!processedValue.All(c => char.IsLetter(c)))
                    throw new ArgumentException("Incorrect data for FirstName");
                if (processedValue.Length < 2 || !char.IsUpper(processedValue[0]) || !processedValue.Skip(1).All(char.IsLower))
                    throw new ArgumentException("Incorrect data for FirstName");
                firstName = processedValue;
            }
        }

        public DateTime Birthday
        {
            get => birthday;
            set
            {
                // Sprawdzamy czy data nie jest domyślna (01.01.0001)
                if (value == default(DateTime))
                    throw new ArgumentException("Incorrect data for Birthday");

                // Sprawdzamy czy data nie jest z przyszłości
                if (value > DateTime.Now)
                    throw new ArgumentException("Incorrect data for Birthday");

                // Sprawdzamy czy rok jest sensowny (np. nie wcześniej niż 1900)
                if (value.Year < 1900)
                    throw new ArgumentException("Incorrect data for Birthday");
                birthday = value;
            }
        }

        public override string ToString() =>
            $"{FirstName} {FamilyName} ({Birthday:yyyy-MM-dd})";
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            /* Test: Utworzenie obiektu, dane poprawne */
            var p1 = new Person(familyName: "Abacki",
                                firstName: "John",
                                birthday: new DateTime(year: 2000, month: 1, day: 1)
                               );
            Console.WriteLine(p1.FamilyName);
            Console.WriteLine(p1.FirstName);
            Console.WriteLine($"{p1.Birthday:yyyy-MM-dd}");
        }
    }
}