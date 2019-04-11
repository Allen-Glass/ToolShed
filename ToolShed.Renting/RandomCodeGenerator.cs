using System;

namespace ToolShed.Renting
{
    public class RandomCodeGenerator
    {
        private readonly int length;
        private readonly Random getRandom = new Random();

        public RandomCodeGenerator(int length)
        {
            this.length = length;
        }

        public string CreateLockerCombo()
        {
            lock (getRandom)
            {
                var lockerCombo = 0;
                for (int i = 0; i < length; i++)
                {
                    lockerCombo = (lockerCombo * 10) + getRandom.Next(0, 9);
                }
                return lockerCombo.ToString();
            }
        }
    }
}