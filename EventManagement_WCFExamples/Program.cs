using CoreWCF.Description;
using CoreWCF;
using System.Web.Services.Description;
using EventManagement_WCFExamples.ServiceContracts.Interfaces;

namespace EventManagement_WCFExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a URI to serve as the base address.
            Uri baseAddress = new Uri("http://localhost:8000/GettingStarted/");

            // Step 2: Create a ServiceHost instance.
            //ServiceHost selfHost = new ServiceHost(typeof(IEventServices), baseAddress);
        }
    }
}
