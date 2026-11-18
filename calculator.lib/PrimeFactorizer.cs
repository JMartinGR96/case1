namespace calculator.lib
{
    public static class PrimeFactorizer
    {
        public static List<int> Factorize(int number)
        {
            var factors = new List<int>();
            for (int divisor = 2; number > 1; divisor++)
            {
                while (number % divisor == 0)
                {
                    factors.Add(divisor);
                    number /= divisor;
                }
            }
            return factors;
        }
    }
}
