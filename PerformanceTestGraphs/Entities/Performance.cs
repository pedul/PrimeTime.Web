using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PerformanceTestGraphs
{
    [DataContract]
    public class Performance
    {
        [DataMember]
        public long RangeLimit { get; set; }

        [DataMember]
        public long NoPrimesFound { get; set; }

        [DataMember]
        public long LastPrimeFound { get; set; }

        [DataMember]
        public double TimeTaken { get; set; }
    }
}