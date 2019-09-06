using System;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    class Calculator
    {
        private void Calculate(String input)
        {
            string[] numbers = input.Split(',');
            int total = 0;
            for (int i = 0; i < numbers.Length; i++)
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

            Console.WriteLine("Total: " + total);
            Console.ReadLine();
        }

        // I'm certain there's a better way to do this (possibly with regex) but would need more time
        private String FormatDelimitter(String input)
        {
            if (input.Contains("\\n"))
            {
                input = input.Replace("\\n", ",");
            }

            return input;
        }

        static void Main(string[] args)
        {
            Calculator calc = new Calculator();

            Console.Write("Input string: ");
            String input = Console.ReadLine();
            input = calc.FormatDelimitter(input);
            calc.Calculate(input);
        }
    }
}
