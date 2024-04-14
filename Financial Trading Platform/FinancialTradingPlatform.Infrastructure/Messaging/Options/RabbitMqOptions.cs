using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialTradingPlatform.Infrastructure.Messaging.Options
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; }        

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool SslEnabled { get; set; }

        public string ServerName { get; set; }

        public string CertPath { get; set; }

        public string CertPassphrase { get; set; }
    }
}
