using System;

namespace nauka_klasy_obiekty
{
    class Program
    {
        static void Main(string[] args)
        {

            Human firstHuman = new Human("Tomek", 16);
            Human secondHuman = new Human("Andrzej", 32);

            Message.Greet(firstHuman.name);
            firstHuman.Eat("pasta");
            firstHuman.Sleep();
            Message.Greet(secondHuman.name);
            secondHuman.Eat("eggs");
            secondHuman.Sleep();
        }
    }

    class Human
    {
    public string name;
    public int age;
    public Human(String name, int age){
        this.name = name;
        this.age = age;
    }
        public void Eat(string food){
            Console.WriteLine($"{name} is eating {food}");
        }

        public void Sleep(){
            Console.WriteLine($"{name} is sleeping");
        }
    }
}
