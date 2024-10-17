namespace Konteh.BackOfficeApi.Utils
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random = new Random();

        public int Next()
        {
            return _random.Next();
        }
    }
}
