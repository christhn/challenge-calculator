using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator
    {
        private static Calculator calc = new Calculator();

        private void Calculate(string input, string defaultDelimiter, bool deny, int upperBound)
        {
            input = calc.NormalizeString(input, defaultDelimiter);
            string[] numbers = input.Split(',');
            ArrayList negNumbers = new ArrayList();
            string formula = null;
            int total = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                try
                {
                    if (int.Parse(numbers[i]) > upperBound)
                    {
                        throw new FormatException();
                    }
                    else if (deny && (int.Parse(numbers[i]) < 0))
                    {
                        negNumbers.Add(numbers[i]);
                    }
                    else
                    {
                        total += int.Parse(numbers[i]);
                        formula = createFormula(formula, numbers[i], false);
                    }
                }
                catch (FormatException)
                {
                    formula = createFormula(formula, numbers[i], true);
                }
            }

            formula += " = " + total;
            Console.WriteLine("Total: " + total);
            Console.WriteLine("Formula: " + formula);
            calc.PrintNegNumbers(negNumbers);
        }

        public string createFormula(string formula, string number, bool exception)
        {
            if (!exception)
            {
                if (formula == null)
                {
                    formula = int.Parse(number).ToString();
                }
                else
                {
                    formula += "+" + int.Parse(number).ToString();
                }
            }
            else
            {
                if ((formula == null) && (number != ""))
                {
                    formula += "0";
                }
                else if (number != "")
                {
                    formula += "+0";
                }
            }

            return formula;
        }

        // I'm certain there's a better way to do this (possibly with regex) but would need more time
        // Just thinking outloud: can this also be done with a configuration file or a map?
        public string NormalizeString(string input, string defaultDelimiter)
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

            delimiter = (defaultDelimiter);
            input = calc.swapDelimiter(input, delimiter);

            return input;
        }

        // convert all delimiters to commas for easier management
        public string swapDelimiter(String input, String delimiter)
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
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            // can also make these options global variables?
            Console.Write("Alternate delimiter (default is \\n): ");
            string defaultDelimiter = Console.ReadLine();
            Console.WriteLine();

            bool deny = true;
            try
            {
                Console.Write("Deny negative numbers (true/false): ");
                deny = bool.Parse(Console.ReadLine());
                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid response! Defaulting to true.\n");
            }

            int upperBound = 1000;
            try
            {
                Console.Write("Upper bound (default is 1000): ");
                upperBound = int.Parse(Console.ReadLine());
                Console.WriteLine();
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid response! Defaulting to 1000.\n");
            }

            Console.Write("Input string: ");
            string input = Console.ReadLine();
            calc.Calculate(input, defaultDelimiter, deny, upperBound);
            Console.ReadLine();
        }
    }
}
