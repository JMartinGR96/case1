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

        // Casos difíciles: primos grandes, potencias máximas que caben en int
        // y números con dos factores primos grandes (el peor caso para el algoritmo).
        [Theory]
        [InlineData(65537, 65537)]                             // primo de Fermat
        [InlineData(998244353, 998244353)]                     // primo grande
        [InlineData(999999937, 999999937)]                     // mayor primo por debajo de 10^9
        [InlineData(1000000007, 1000000007)]                   // primo grande (10^9 + 7)
        [InlineData(2000000014, 2, 1000000007)]                // 2 x primo grande
        [InlineData(1000000, 2, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5, 5)] // 10^6 = 2^6 x 5^6
        [InlineData(1073741824, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                                2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2)] // 2^30
        [InlineData(1162261467, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
                                3, 3, 3, 3, 3, 3, 3, 3, 3)]    // 3^19, mayor potencia de 3 en int
        [InlineData(1220703125, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5)] // 5^13
        [InlineData(1977326743, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7)] // 7^11
        [InlineData(223092870, 2, 3, 5, 7, 11, 13, 17, 19, 23)] // producto de los 9 primeros primos
        [InlineData(1234567890, 2, 3, 3, 5, 3607, 3803)]
        [InlineData(2147483645, 5, 19, 22605091)]              // int.MaxValue - 2
        [InlineData(2145747229, 32749, 65521)]                 // dos primos grandes distintos
        [InlineData(2146654199, 46327, 46337)]                 // los dos mayores primos < raíz de int.MaxValue
        public void Factorize_LargeAndSpecialNumbers(int number, params int[] expectedFactors)
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

        // Tests de propiedades: en vez de escribir el resultado esperado a mano,
        // se comprueba que se cumplen las reglas de la descomposición para 1..10000.
        private const int PropertyLimit = 10_000;

        [Fact]
        public void Factorize_ProductOfFactorsIsTheOriginalNumber()
        {
            for (int number = 1; number <= PropertyLimit; number++)
            {
                var product = PrimeFactorizer.Factorize(number).Aggregate(1L, (acc, f) => acc * f);

                Assert.True(product == number, $"Para {number} el producto de los factores es {product}");
            }
        }

        [Fact]
        public void Factorize_AllFactorsArePrime()
        {
            for (int number = 1; number <= PropertyLimit; number++)
            {
                foreach (var factor in PrimeFactorizer.Factorize(number))
                {
                    Assert.True(IsPrimeByTrialDivision(factor), $"Para {number} el factor {factor} no es primo");
                }
            }
        }

        [Fact]
        public void Factorize_FactorsAreInAscendingOrder()
        {
            for (int number = 1; number <= PropertyLimit; number++)
            {
                var factors = PrimeFactorizer.Factorize(number);

                Assert.True(factors.SequenceEqual(factors.Order()), $"Para {number} los factores no están ordenados: [{string.Join(", ", factors)}]");
            }
        }

        [Fact]
        public void Factorize_PrimeNumbersReturnThemselves()
        {
            for (int number = 2; number <= PropertyLimit; number++)
            {
                if (IsPrimeByTrialDivision(number))
                {
                    Assert.Equal(new[] { number }, PrimeFactorizer.Factorize(number));
                }
            }
        }

        // Comprobación de primalidad independiente de la implementación, solo para los tests.
        private static bool IsPrimeByTrialDivision(int n)
        {
            if (n < 2) return false;
            for (int d = 2; (long)d * d <= n; d++)
            {
                if (n % d == 0) return false;
            }
            return true;
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
