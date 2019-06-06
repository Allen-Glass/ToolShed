using System;
using System.Collections.Generic;
using System.Text;

namespace ToolShed.Helpers
{
    public static class NullCheckHelpers
    {
        public static void EnsureArgumentIsNotNullOrEmpty(object input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
        }

        public static void EnsureArgumentIsNotNullOrEmpty(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));
        }

        public static void EnsureArgumentIsNotNullOrEmpty(Guid input)
        {
            if (input == Guid.Empty)
                throw new ArgumentNullException(nameof(input));
        }
    }
}
