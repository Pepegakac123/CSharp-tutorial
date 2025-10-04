using System;
using System.Collections.Generic;

public class Vehicle
{
    public string brand;
    public string model;
    public int year;

    public Vehicle(string brand, string model, int year)
    {
        this.brand = brand;
        this.model = model;
        this.year = year;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Brand: {brand}, Model: {model}, Year: {year}");
    }

    public virtual void Start()
    {
        Console.WriteLine("Starting the vehicle");
    }
}

public class Car : Vehicle
{
    public Car(string brand, string model, int year)
        : base(brand, model, year)
    {
    }

    public override void Start()
    {
        Console.WriteLine("Starting the engine...");
    }
}

public class Bicycle : Vehicle
{
    public Bicycle(string brand, string model, int year)
        : base(brand, model, year)
    {
    }

    public override void Start()
    {
        Console.WriteLine("Starting to pedal...");
    }
}

class Program
{
    static void Main()
    {
        bool exit = false;
        List<Vehicle> vehicles = new List<Vehicle>();

        while (!exit)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add a car");
            Console.WriteLine("2. Add a bicycle");
            Console.WriteLine("3. Display all vehicles");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    CreateVehicle("car", vehicles);
                    break;
                case "2":
                    CreateVehicle("bicycle", vehicles);
                    break;
                case "3":
                    DisplayVehicles(vehicles);
                    break;
                case "4":
                    Console.WriteLine("Exiting the program.");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }

    static (string, string, int) GetValuesFromUser()
    {
        Console.Write("Enter brand: ");
        string brand = Console.ReadLine();
        Console.Write("Enter model: ");
        string model = Console.ReadLine();
        Console.Write("Enter year: ");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year))
        {
            Console.Write("Invalid input. Please enter a valid year: ");
        }
        return (brand, model, year);
    }

    static void CreateVehicle(string vehicleType, List<Vehicle> vehicles)
    {
        var (brand, model, year) = GetValuesFromUser();

        if (vehicleType.ToLower() == "car")
        {
            Car newCar = new Car(brand, model, year);
            vehicles.Add(newCar);
            Console.WriteLine("Car added successfully!");
        }
        else if (vehicleType.ToLower() == "bicycle")
        {
            Bicycle newBicycle = new Bicycle(brand, model, year);
            vehicles.Add(newBicycle);
            Console.WriteLine("Bicycle added successfully!");
        }
    }

    static void DisplayVehicles(List<Vehicle> vehicles)
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles to display.");
            return;
        }

        Console.WriteLine("\n--- All Vehicles ---");
        foreach (var vehicle in vehicles)
        {
            vehicle.DisplayInfo();
            vehicle.Start();
            Console.WriteLine();
        }
    }
}