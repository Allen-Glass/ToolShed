using System;
using System.Collections.Generic;
using System.Text;

namespace Toolshed.Models.Web
{
    /// <summary>
    /// Contians the Users Card information
    /// </summary>
    public class Card
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ZipCode { get; set; }
        public string CCVNumber { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }

    }

}

