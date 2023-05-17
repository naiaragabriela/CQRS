using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Repository
{
    public class MongoRepositorySettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public class Rootobject
        {
            public Logging Logging { get; set; }
            public Mongorepositorysettings MongorepositorySettings { get; set; }
            public string AllowedHosts { get; set; }
        }

        public class Logging
        {
            public Loglevel LogLevel { get; set; }
        }

        public class Loglevel
        {
            public string Default { get; set; }
            public string MicrosoftAspNetCore { get; set; }
        }

        public class Mongorepositorysettings
        {
            public string ConnectionString { get; set; }
            public string DatabaseName { get; set; }
        }

    }
}
