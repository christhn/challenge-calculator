using System;

namespace StringCalculator
{
    class Calculator
    {
        private void Calculate(String input)
        {
            string[] numbers = input.Split(',');
            int total = 0;
            for (int i = 0; i < 2; i++)
            {
                if (i < numbers.Length)
                {
                    try
                    {
                        total += int.Parse(numbers[i]);
                    }
                    catch (FormatException e)
                    {
                        total += 0;
                    }
                }
            }

            Console.WriteLine("Total: " + total);
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Console.Write("Input string: ");
            String input = Console.ReadLine();

            Calculator calc = new Calculator();
            calc.Calculate(input);
        }
    }
}
