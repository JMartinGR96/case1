namespace calculator.lib
{
    public static class PrimeFactorizer
    {
        public static List<int> Factorize(int number)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(number, 1);

            var factors = new List<int>();
            int divisor = 2;
            // Se prueban divisores hasta la raíz cuadrada de lo que queda por factorizar
            // (divisor <= number / divisor evita el desbordamiento de divisor * divisor).
            while (divisor <= number / divisor)
            {
                if (number % divisor == 0)
                {
                    factors.Add(divisor);
                    number /= divisor;
                }
                else
                {
                    // Después del 2 solo hace falta probar impares: 2, 3, 5, 7, 9...
                    divisor += divisor == 2 ? 1 : 2;
                }
            }
            // Lo que queda, si es mayor que 1, es el último factor primo.
            if (number > 1)
            {
                factors.Add(number);
            }
            return factors;
        }
    }
}
