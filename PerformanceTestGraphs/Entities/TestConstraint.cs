using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace PerformanceTestGraphs
{
    [DataContract]
    public class TestConstraint
    {
        [DataMember]
        public long Limit { get; set; }

        [DataMember]
        public long Increment { get; set; }
    }
}