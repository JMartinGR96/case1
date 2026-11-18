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
        // Casos difíciles: primos grandes, potencias máximas que caben en int
        // y números con dos factores primos grandes.
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
        public void Factorize_ReturnsPrimeFactorsInAscendingOrder(int number, params int[] expectedFactors)
        {
            Assert.Equal(expectedFactors, PrimeFactorizer.Factorize(number));
        }

        [Theory]
        [InlineData(0)]                                        // profesor
        [InlineData(-1)]                                       // profesor
        [InlineData(-12)]
        [InlineData(int.MinValue)]
        public void Factorize_NumberLessThanOne_Throws(int number)
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => PrimeFactorizer.Factorize(number));

            Assert.Equal("number", exception.ParamName);
        }

        // Los casos más lentos para el algoritmo deben resolverse en menos de 1 segundo.
        [Theory(Timeout = 1000)]
        [InlineData(int.MaxValue, int.MaxValue)]               // profesor: el mayor primo que cabe en int
        [InlineData(2146654199, 46327, 46337)]                 // los dos mayores primos < raíz de int.MaxValue
        public async Task Factorize_WorstCases_AreFast(int number, params int[] expectedFactors)
        {
            var factors = await Task.Run(() => PrimeFactorizer.Factorize(number));

            Assert.Equal(expectedFactors, factors);
        }

        [Fact]
        public void Factorize_ReturnsANewListEachCall()
        {
            var first = PrimeFactorizer.Factorize(12);
            first.Clear(); // modificar el resultado no debe afectar a llamadas posteriores

            Assert.Equal(new[] { 2, 2, 3 }, PrimeFactorizer.Factorize(12));
        }

        // Test de propiedades: para cada número de 1 a 10000 se comprueba que el resultado
        // es una descomposición válida: factores primos, en orden y cuyo producto es el número.
        // Por el teorema fundamental de la aritmética solo existe una, así que es la correcta.
        [Fact]
        public void Factorize_ReturnsAValidFactorizationFrom1To10000()
        {
            for (int number = 1; number <= 10_000; number++)
            {
                var factors = PrimeFactorizer.Factorize(number);
                var description = $"{number} -> [{string.Join(", ", factors)}]";

                Assert.True(factors.All(IsPrime), $"Hay un factor que no es primo: {description}");
                Assert.True(factors.SequenceEqual(factors.Order()), $"Factores desordenados: {description}");
                Assert.True(factors.Aggregate(1L, (acc, f) => acc * f) == number, $"El producto no da el número: {description}");
            }
        }

        // Comprobación de primalidad independiente de la implementación, solo para los tests.
        private static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int d = 2; (long)d * d <= n; d++)
            {
                if (n % d == 0) return false;
            }
            return true;
        }
    }
}
