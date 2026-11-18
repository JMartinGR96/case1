namespace calculator.lib
{
    public static class PrimeFactorizer
    {
        public static List<int> Factorize(int number)
        {
            var factors = new List<int>();
            while (number % 2 == 0)
            {
                factors.Add(2);
                number /= 2;
            }
            if (number > 1)
            {
                factors.Add(number);
            }
            return factors;
        }
    }
}
