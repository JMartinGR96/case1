using calculator.lib;

namespace calculator.lib.test
{
    public class PrimeFactorizerTests
    {
        [Theory]
        [InlineData(1)]
        public void Factorize_ReturnsPrimeFactorsInAscendingOrder(int number, params int[] expectedFactors)
        {
            var factors = PrimeFactorizer.Factorize(number);

            Assert.Equal(expectedFactors, factors);
        }
    }
}
