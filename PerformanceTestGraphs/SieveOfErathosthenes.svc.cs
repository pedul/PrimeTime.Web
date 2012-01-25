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
    [ServiceContract(Namespace = "PrimeTime")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SieveOfErathosthenes
    {
        [OperationContract]
        public List<Performance> DoWork(TestConstraints testConstraints)
        {
            return GetSieveOfErathosthenesPeformanceTimes(testConstraints.Limit, testConstraints.Increment);
        }

        [OperationContract]
        public Performance GetPerformance(TestConstraints testConstraints)
        {
            Stopwatch sw = new Stopwatch();            
            var trialDivison = new SieveOfEratosthenes(testConstraints.Limit);

            sw.Start();
            long prime = trialDivison.Primes.ToList().Last();

            return new Performance { RangeLimit = testConstraints.Limit, TimeTaken = sw.ElapsedMilliseconds };            
        }

        List<Performance> GetSieveOfErathosthenesPeformanceTimes(long to, long increment)
        {
            var performanceValues = new List<Performance>();

            for (long i = 1000; i <= to; i += increment)
            {
                Stopwatch sw = new Stopwatch();
                var trialDivison = new SieveOfEratosthenes(i);

                sw.Start();
                long prime = trialDivison.Primes.ToList().Last();

                var per = new Performance { RangeLimit = i, TimeTaken = sw.ElapsedMilliseconds };
                performanceValues.Add(per);
            }

            return performanceValues;
        }
    }
}
