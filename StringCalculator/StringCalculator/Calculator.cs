using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    class Calculator
    {
        private static Calculator calc = new Calculator();

        private void Calculate(string input)
        {
            input = calc.NormalizeString(input);
            string[] numbers = input.Split(',');
            ArrayList negNumbers = new ArrayList();
            string formula = null;
            int total = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                try
                {
                    if (int.Parse(numbers[i]) > 1000)
                    {
                        throw new FormatException();
                    }
                    else if ((int.Parse(numbers[i]) > 0))
                    {
                        total += int.Parse(numbers[i]);
                        if (formula == null) 
                        {
                            formula = int.Parse(numbers[i]).ToString();
                        }
                        else
                        {
                            formula += "+" + int.Parse(numbers[i]).ToString();
                        }
                    }
                    else if (int.Parse(numbers[i]) < 0)
                    {
                        negNumbers.Add(numbers[i]);
                    }
                }
                catch (FormatException e)
                {
                    if ((i == 0) && (numbers[i] != ""))
                    {
                        formula += "0";
                    }
                    else if (i > 0)
                    {
                        formula += "+0";
                    }
                }
            }

            formula += " = " + total;
            Console.WriteLine("Total: " + total);
            Console.WriteLine("Formula: " + formula);
            calc.PrintNegNumbers(negNumbers);
        }

        // I'm certain there's a better way to do this (possibly with regex) but would need more time
        // Just thinking outloud: can this also be done with a configuration file or a map?
        private string NormalizeString(String input)
        {
            // remove the // and create a common delimiter to normalize the string
            int index = input.IndexOf("//");
            string delimiter = null;
            if (index == 0)
            {
                if (input.Length > 2)
                {
                    input = input.Remove(0, 2);
                    if (!input[0].Equals('['))
                    {
                        delimiter = input[0].ToString();
                        input = calc.swapDelimiter(input, delimiter);
                    }
                    else
                    {
                        while (input[0].Equals('['))
                        {
                            delimiter = input.Substring(1, input.IndexOf(']') - 1);
                            input = input.Remove(0, input.IndexOf(']') + 1);
                            input = calc.swapDelimiter(input, delimiter);
                        }
                    }
                }
            }

            delimiter = ("\\n");
            input = calc.swapDelimiter(input, delimiter);

            return input;
        }

        // convert all delimiters to commas for easier management
        private string swapDelimiter(String input, String delimiter)
        {
            if (input.Contains(delimiter))
            {
                input = input.Replace(delimiter, ",");
            }

            return input;
        }

        private void PrintNegNumbers(ArrayList negNumbers)
        {
            if (negNumbers.Count > 0)
            {
                Console.Write("Negative numbers denied: ");
                for (int i = 0; i < negNumbers.Count; i++)
                {
                    Console.Write(negNumbers[i] + " ");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Input string: ");
            string input = Console.ReadLine();
            calc.Calculate(input);
            Console.ReadLine();
        }
    }
}
