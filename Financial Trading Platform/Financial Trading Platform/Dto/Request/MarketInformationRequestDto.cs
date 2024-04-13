using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financial_Trading_Platform.Dto.Request
{
    internal class MarketInformationRequestDto
    {

        public required string StockSymbol { get; set; }

        public required string OpenPrice { get; set; }

        public required string ClosePrice { get; set; }

        public required string volume { get; set; }

    }
}
