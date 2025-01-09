using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        /* Test: tworzenie obiektu Child 
           brak jednego z rodziców */
        try
        {
            Person o = new Person(familyName: "Molenda", firstName: "Krzysztof", age: 30);
            Person m = new Person(familyName: "Molenda", firstName: "Ewa", age: 29);
            Child d = new Child(familyName: "Molenda", firstName: "Anna", age: 14, father: o);
            Console.WriteLine(d);
            d = new Child(familyName: "Molenda", firstName: "Anna", age: 14, mother: m);
            Console.WriteLine(d);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

public class Person
{
    private string _firstName;
    private string _familyName;
    private int _age;

    public string FirstName { get => _firstName; protected set => _firstName = validateName(value); }
    public string FamilyName { get => _familyName; protected set => _familyName = validateName(value); }
    public virtual int Age
    {
        get => _age;
        protected set
        {
            if (value < 0)
                throw new ArgumentException("Age must be positive!");
            _age = value;
        }
    }

    public Person(string firstName, string familyName, int age)
    {
        FirstName = firstName;
        FamilyName = familyName;
        Age = age;
    }

    public override string ToString()
    {
        return $"{FirstName} {FamilyName} ({Age})";
    }

    public void modifyFirstName(string newFirstName)
    {
        FirstName = newFirstName;  // Setter właściwości FirstName wykona validateName
    }

    public void modifyFamilyName(string newFamilyName)
    {
        FamilyName = newFamilyName;  // Setter właściwości FamilyName wykona validateName
    }

    public virtual void modifyAge(int newAge)
    {
        Age = newAge;  // Setter właściwości Age sprawdzi czy wartość nie jest ujemna
    }

    private static string validateName(string name)
    {
        if (name == null || name.Trim().Length == 0)
            throw new ArgumentException("Wrong name!");

        string processedValue = name.Trim().Replace(" ", "");

        if (processedValue.Any(c => !char.IsLetter(c)))
            throw new ArgumentException("Wrong name!");

        processedValue = char.ToUpper(processedValue[0]) + processedValue.Substring(1).ToLower();
        return processedValue;
    }
}

public class Child : Person
{
    public Person Mother { get; }
    public Person Father { get; }

    public override int Age
    {
        get => base.Age;
        protected set
        {
            if (value >= 15)
                throw new ArgumentException("Child’s age must be less than 15!");
            base.Age = value;  // to wywołuje walidację z klasy bazowej (sprawdzenie czy >= 0)
        }
    }

    public Child(string firstName, string familyName, int age, Person mother = null, Person father = null)
        : base(firstName, familyName, age)
    {
        Mother = mother;
        Father = father;
        if (age > 15)
            throw new ArgumentException("Child’s age must be less than 15!");

    }

    public override void modifyAge(int newAge)
    {
        if (newAge >= 15)
            throw new ArgumentException("Child’s age must be less than 15!");
        base.modifyAge(newAge);  // to wywołuje walidację z klasy bazowej
    }
    public override string ToString()
    {
        string motherInfo = Mother == null ? "No data" : Mother.ToString();
        string fatherInfo = Father == null ? "No data" : Father.ToString();

        return $"{FirstName} {FamilyName} ({Age})\nmother: {motherInfo}\nfather: {fatherInfo}";
    }
}
