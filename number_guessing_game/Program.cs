using System;

class numberGuessingGame{
    public static void Main(string[] args){
        int number;
        int guess;
        int tries;
        int min = 1;
        int max = 100;
        String response = "";
        bool continueGame = true;

        while(continueGame){
            guess = 0;
            tries = 0;
            number = new Random().Next(min,max+1);
            Console.WriteLine($"Guess the number between {min} and {max}");
            while(guess != number){
                try
                {
                    guess = Convert.ToInt32(Console.ReadLine());
                    if(guess > number){
                        Console.WriteLine("Too high");
                    }else if(guess < number){
                        Console.WriteLine("Too low");
                    }
                    tries++;
                    
                }catch(FormatException e){
                    Console.WriteLine("Please enter a number");
                    continue;
                }catch(OverflowException e){
                    Console.WriteLine("Number is too large or too small");
                    continue;
                }
            }
            Console.WriteLine($"The number was {number}");
            Console.WriteLine($"You got it in {tries} tries");
            Console.WriteLine("Would you like to play again? (y/n)");
            response = Console.ReadLine();
            response = response.ToLower();
            if (response == "y")
            {
                continueGame = true;
            }
            else
            {
                Console.WriteLine("Thanks for playing!");
                continueGame = false;
            }
           
        }
    }
}