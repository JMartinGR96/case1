using calculator.lib;

namespace calculator.lib.test
{
    public class PrimeFactorizerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        public void Factorize_ReturnsPrimeFactorsInAscendingOrder(int number, params int[] expectedFactors)
        {
            var factors = PrimeFactorizer.Factorize(number);

            Assert.Equal(expectedFactors, factors);
        }
    }
}
