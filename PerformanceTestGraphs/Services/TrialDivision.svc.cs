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

namespace PerformanceTestGraphs.Services
{
    [ServiceContract(Namespace = "PrimeTime.Services")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TrialDivision
    {
        [OperationContract]
        public Performance GetPerformance(TestConstraint testConstraints)
        {                        
            var trialDivison = new TrialDivisionByPrimes();

            Stopwatch sw = Stopwatch.StartNew();           
            var primes = trialDivison.Primes.TakeWhile(p => p <= testConstraints.Limit).ToList();
            sw.Stop();

            return new Performance
            {
                RangeLimit = testConstraints.Limit,
                NoPrimesFound = primes.LongCount(),
                LastPrimeFound = primes.LastOrDefault(),
                TimeTaken = sw.ElapsedMilliseconds
            };
        }
    }
}
