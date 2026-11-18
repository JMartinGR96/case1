namespace calculator.lib
{
    public static class PrimeFactorizer
    {
        public static List<int> Factorize(int number)
        {
            var factors = new List<int>();
            if (number > 1)
            {
                factors.Add(number);
            }
            return factors;
        }
    }
}
