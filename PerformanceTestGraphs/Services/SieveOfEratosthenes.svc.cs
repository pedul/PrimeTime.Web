﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Diagnostics;

namespace PerformanceTestGraphs.Services
{
    [ServiceContract(Namespace = "PrimeTime.Services")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SieveOfEratosthenes
    {
        [OperationContract]
        public Performance GetPerformance(TestConstraint testConstraints)
        {            
            var soe = new PrimeTime.SieveOfEratosthenes(testConstraints.Limit);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            var primes = soe.Primes.ToList();
            long timeTaken = sw.ElapsedMilliseconds;            

            return new Performance
            {
                RangeLimit = testConstraints.Limit,
                NoPrimesFound = primes.LongCount(),
                LastPrimeFound = primes.LastOrDefault(),
                TimeTaken = timeTaken
            };
        }
    }
}
