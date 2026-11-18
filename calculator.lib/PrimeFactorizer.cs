namespace calculator.lib
{
    public static class PrimeFactorizer
    {
        public static List<int> Factorize(int number)
        {
            var factors = new List<int>();
            // Basta con probar divisores hasta la raíz cuadrada del número.
            // Se usa divisor <= number / divisor para evitar el desbordamiento de divisor * divisor.
            for (int divisor = 2; divisor <= number / divisor; divisor++)
            {
                while (number % divisor == 0)
                {
                    factors.Add(divisor);
                    number /= divisor;
                }
            }
            // Si queda algo mayor que 1, es un factor primo.
            if (number > 1)
            {
                factors.Add(number);
            }
            return factors;
        }
    }
}
