using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;

namespace Konteh.Test.Infrastructure
{
    public class TestRandomGenerator : IRandomGenerator
    {
        private int _random;
        public int Next() => _random++;
    }
}
