using NUnit.Framework;

namespace StringCalculatorKata.Test
{

    [TestFixture]
    public class StringCalculatorTest
    {
        private StringCalculator stringCalculator;

        [SetUp]
        public void InitTest()
        {
            stringCalculator = new StringCalculator();
        }

        [Test]
        public void Add_WhenEmptyString_Returns0()
        {
            
            ArrangeActAndAssert("", 0);
        }

        [TestCase("1", 1)]
        [TestCase("2", 2)]
        public void Add_WhenSingleNumber_ReturnNumber(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        [TestCase("1,2", 3)]
        [TestCase("2,3", 5)]
        [TestCase("10,11", 21)]
        public void Add_WhenMultipleNumbers_ReturnSumOfNumbers(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        [TestCase("1,2,3", 6)]
        [TestCase("1,2,3,4", 10)]
        public void Add_WhenUnknownAmountOfNumbers_ReturnSumOfNumbers(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        public void Add_EmptyStringWithNewLine_Returns0(string numbers, int excepted)
        {
            ArrangeActAndAssert("\n", 0);
        }

        [TestCase("1\n2,3\n4", 10)]
        [TestCase("5,5\n5\n5", 20)]
        public void Add_WhenNumbersSplitWithNewLine_ReturnSumOfNumbers(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        [TestCase("//[;]\n1;2;3;4", 10)]
        [TestCase("//[&]\n3&4&5&6", 18)]
        public void Add_WithCustomSeparator_ReturnSumOfNumbers(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        [TestCase("//[;;]\n1;;2;;3;;4", 10)]
        [TestCase("//[++]\n3++4++5++6", 18)]
        public void Add_WithCustomLongSeparator_ReturnSumOfNumbers(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        [TestCase("//[;][+]\n1;2+3;4", 10)]
        [TestCase("//[+][;][&]\n3+4&5;6", 18)]
        public void Add_WithMultipleSeparators_ReturnSumOfNumbers(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        [Test]
        public void Add_ThrowsOnNegativeArgs()
        {
            var testDelegate = new TestDelegate(() => ArrangeActAndAssert("1,-1,2,3", 0));
            //Catch exception
            Assert.Catch(testDelegate, "Negative args");
        }

        [TestCase("1000,10,30,50",90)]
        public void Add_WithNumbersMoreThan1000_ReturnSumWithIgnoredNumbersMoreThan1000(string numbers, int excepted)
        {
            ArrangeActAndAssert(numbers, excepted);
        }

        private void ArrangeActAndAssert(string numbers, int excepted)
        {
            var result = stringCalculator.Add(numbers);
            Assert.AreEqual(result, excepted);
        }
    }
}
