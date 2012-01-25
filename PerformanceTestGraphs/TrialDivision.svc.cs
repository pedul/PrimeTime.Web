using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;
using PrimeTime;

namespace PerformanceTestGraphs
{
    [DataContract]
    public class Performance
    {
        [DataMember]
        public long RangeLimit { get; set; }
        
        [DataMember]
        public double TimeTaken { get; set; }
    }

    [DataContract]
    public class TestConstraints
    {
        [DataMember]
        public long Limit { get; set; }

        [DataMember]
        public long Increment { get; set; }
    }

    [ServiceContract(Namespace = "PrimeTime")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TrialDivision
    {
        [OperationContract]
        public List<Performance> DoWork(TestConstraints testConstraints)
        {
            return GetTrialDivisonPeformanceTimes(testConstraints.Limit, testConstraints.Increment);
        }

        [OperationContract]
        public Performance GetPerformance(TestConstraints testConstraints)
        {
            Stopwatch sw = new Stopwatch();
            var trialDivison = new TrialDivisionByPrimes();

            sw.Start();
            long prime = trialDivison.Primes.TakeWhile(p => p < testConstraints.Limit).ToList().Last();

            return new Performance { RangeLimit = testConstraints.Limit, TimeTaken = sw.ElapsedMilliseconds };            
        }

        List<Performance> GetTrialDivisonPeformanceTimes(long to, long increment)
        {
            var performanceValues = new List<Performance>();

            for (long i = 1000; i <= to; i += increment)
            {
                Stopwatch sw = new Stopwatch();
                var trialDivison = new TrialDivisionByPrimes();

                sw.Start();
                long prime = trialDivison.Primes.TakeWhile(p => p < i).ToList().Last();

                var per = new Performance { RangeLimit = i, TimeTaken = sw.ElapsedMilliseconds };
                performanceValues.Add(per);
            }

            return performanceValues;
        }
    }
}
