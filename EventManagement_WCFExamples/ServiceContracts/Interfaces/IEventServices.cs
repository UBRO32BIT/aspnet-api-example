using CoreWCF;
using EventManagement_WCFExamples.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement_WCFExamples.ServiceContracts.Interfaces
{
    [ServiceContract]
    public interface IEventServices
    {
        [OperationContract]
        Event GetEventById(string id);
    }
}
