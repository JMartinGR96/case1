using calculator.lib;

namespace calculator.lib.test
{
    public class PrimeFactorizerTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 2, 2)]
        [InlineData(6, 2, 3)]
        [InlineData(8, 2, 2, 2)]
        [InlineData(9, 3, 3)]
        [InlineData(12, 2, 2, 3)]
        [InlineData(25, 5, 5)]
        [InlineData(30, 2, 3, 5)]
        [InlineData(97, 97)]
        [InlineData(360, 2, 2, 2, 3, 3, 5)]
        [InlineData(1001, 7, 11, 13)]
        [InlineData(30030, 2, 3, 5, 7, 11, 13)]
        [InlineData(2147117569, 46337, 46337)]
        [InlineData(2147483646, 2, 3, 3, 7, 11, 31, 151, 331)]
        public void Factorize_ReturnsPrimeFactorsInAscendingOrder(int number, params int[] expectedFactors)
        {
            var factors = PrimeFactorizer.Factorize(number);

            Assert.Equal(expectedFactors, factors);
        }

        [Fact(Timeout = 1000)]
        public async Task Factorize_LargePrime_IsFast()
        {
            var factors = await Task.Run(() => PrimeFactorizer.Factorize(int.MaxValue));

            Assert.Equal(new[] { int.MaxValue }, factors);
        }
    }
}
