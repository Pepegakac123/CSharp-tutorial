using System;

namespace Zadanie2
{


    public abstract class Vehicle
    {
        // Konstruktor klasy bazowej
        public Vehicle()
        {
            Console.WriteLine("Vehicle created");
        }

        // Metoda abstrakcyjna, która musi być zaimplementowana przez klasy pochodne
        public abstract void StartEngine();
    }


    public class Car : Vehicle
    {
        public override void StartEngine()
        {
            Console.WriteLine("Car engine started");
        }
    }

    public class Bicycle : Vehicle
    {
        public override void StartEngine()
        {
            Console.WriteLine("Bicycle has no engine to start!");
        }
    }

    /*
    {twoj kod implementujący klasy Car i Bicycle}
    */
    class Zadanie2
    {
        static void Main(string[] args)
        {
            Car myCar = new Car();
            myCar.StartEngine();
            Bicycle myBicycle = new Bicycle();
            myBicycle.StartEngine();

            Console.WriteLine("\nTest dla zmiennej typu vehicle");
            Vehicle vehicle1 = myCar;
            Vehicle vehicle2 = myBicycle;
            vehicle1.StartEngine();
            vehicle2.StartEngine();
        }
    }
}