using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ToolShed.Renting.Tests
{
    public class RandomNumberGeneratorTests
    {
        [Fact]
        public void CreateLockerComboSixLength()
        {
            var randomCodeGenerator = new RandomCodeGenerator(6);
            var lockerCombo = randomCodeGenerator.CreateLockerCombo();
        }
    }
}
