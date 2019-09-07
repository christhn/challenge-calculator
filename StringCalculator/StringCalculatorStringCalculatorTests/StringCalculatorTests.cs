using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.StringCalculatorTests
{
    [TestClass()]
    public class CalculatorStringCalculatorTests
    {
        [TestMethod()]
        public void swapDelimiter_NewLine()
        {
            // arrange
            string delimiter = "\n";
            string input = "1\n2,3";
            string expected = "1,2,3";
            var calc = new Calculator();

            // act
            string actual = calc.swapDelimiter(input, delimiter);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void swapDelimiter_OneCustomSingle()
        {
            string delimiter = ";";
            string input = "\n2;5";
            string expected = "\n2,5";
            var calc = new Calculator();
            string actual = calc.swapDelimiter(input, delimiter);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void swapDelimiter_OneCustomAny()
        {
            string delimiter = "***";
            string input = "\n11***222***333";
            string expected = "\n11,222,333";
            var calc = new Calculator();
            string actual = calc.swapDelimiter(input, delimiter);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void normalizeString_NewLine()
        {
            string input = "1\\n2,3";
            string expected = "1,2,3";
            var calc = new Calculator();
            string actual = calc.NormalizeString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void normalizeString_OneCustomSingle()
        {
            string input = "//;\\n2;5";
            string expected = ",,2,5";
            var calc = new Calculator();
            string actual = calc.NormalizeString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void normalizeString_OneCustomAny()
        {
            string input = "//[***]\\n11***22***33";
            string expected = ",11,22,33";
            var calc = new Calculator();
            string actual = calc.NormalizeString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void normalizeString_MultipleCustom()
        {
            string input = "//[*][!!][r9r]\\n11r9r22*33!!44";
            string expected = ",11,22,33,44";
            var calc = new Calculator();
            string actual = calc.NormalizeString(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void createFormula_FirstNumber()
        {
            string input = "20";
            string expected = "20";
            var calc = new Calculator();
            string actual = calc.createFormula(null, input, false);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void createFormula_FirstNumberException()
        {
            string number = "tytyt";
            string expected = "0";
            var calc = new Calculator();
            string actual = calc.createFormula(null, number, true);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void createFormula_SubsequentNumber()
        {
            string formula = "11+22+33";
            string number = "44";
            string expected = "11+22+33+44";
            var calc = new Calculator();
            string actual = calc.createFormula(formula, number, false);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void createFormula_SubsequentNumberException()
        {
            string formula = "5";
            string number = "tytyt";
            string expected = "5+0";
            var calc = new Calculator();
            string actual = calc.createFormula(formula, number, true);

            Assert.AreEqual(expected, actual);
        }
    }
}