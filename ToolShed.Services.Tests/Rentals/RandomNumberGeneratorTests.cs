using ToolShed.Services.Rentals;
using Xunit;

namespace ToolShed.Services.Tests.Rentals
{
    public class RandomNumberGeneratorTests
    {
        [Fact]
        public void CreateLockerComboSixLength()
        {
            var length = 6;
            var randomCodeGenerator = new RandomCodeGenerator(length);
            var lockerCombo = randomCodeGenerator.CreateLockerCombo();

            Assert.Equal(length, lockerCombo.Length);
        }
    }
}
