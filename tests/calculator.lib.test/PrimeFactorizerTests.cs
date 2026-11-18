using calculator.lib;

namespace calculator.lib.test
{
    // Los casos marcados con "// profesor" están también en la secuencia de tests
    // del profesor (FactorizationTests). Los que no llevan comentario son propios.
    public class PrimeFactorizerTests
    {
        [Theory]
        [InlineData(1)]                                        // profesor
        [InlineData(2, 2)]                                     // profesor
        [InlineData(3, 3)]                                     // profesor
        [InlineData(4, 2, 2)]                                  // profesor
        [InlineData(6, 2, 3)]                                  // profesor
        [InlineData(8, 2, 2, 2)]                               // profesor
        [InlineData(9, 3, 3)]                                  // profesor
        [InlineData(10, 2, 5)]                                 // profesor
        [InlineData(12, 2, 2, 3)]                              // profesor
        [InlineData(25, 5, 5)]                                 // profesor
        [InlineData(27, 3, 3, 3)]                              // profesor
        [InlineData(30, 2, 3, 5)]                              // profesor
        [InlineData(49, 7, 7)]                                 // profesor
        [InlineData(77, 7, 11)]                                // profesor
        [InlineData(97, 97)]
        [InlineData(121, 11, 11)]                              // profesor
        [InlineData(210, 2, 3, 5, 7)]                          // profesor
        [InlineData(360, 2, 2, 2, 3, 3, 5)]                    // profesor
        [InlineData(1001, 7, 11, 13)]
        [InlineData(1009, 1009)]                               // profesor
        [InlineData(2197, 13, 13, 13)]                         // profesor
        [InlineData(30030, 2, 3, 5, 7, 11, 13)]
        [InlineData(2147117569, 46337, 46337)]
        [InlineData(2147483646, 2, 3, 3, 7, 11, 31, 151, 331)]
        public void Factorize_ReturnsPrimeFactorsInAscendingOrder(int number, params int[] expectedFactors)
        {
            var factors = PrimeFactorizer.Factorize(number);

            Assert.Equal(expectedFactors, factors);
        }

        [Theory]
        [InlineData(0)]                                        // profesor
        [InlineData(-1)]                                       // profesor
        [InlineData(-12)]
        public void Factorize_NumberLessThanOne_Throws(int number)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => PrimeFactorizer.Factorize(number));
        }

        // profesor: int.MaxValue también está en su secuencia (él lo tiene dentro del Theory).
        [Fact(Timeout = 1000)]
        public async Task Factorize_LargePrime_IsFast()
        {
            var factors = await Task.Run(() => PrimeFactorizer.Factorize(int.MaxValue));

            Assert.Equal(new[] { int.MaxValue }, factors);
        }
    }
}
