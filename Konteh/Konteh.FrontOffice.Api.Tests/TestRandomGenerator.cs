using Konteh.FrontOfficeApi.Features.Exams.RandomGenerator;

namespace Konteh.FrontOffice.Api.Tests
{
    public class TestRandomGenerator : IRandomGenerator
    {
        private int _random;
        public int Next() => _random++;
    }
}
