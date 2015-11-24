using Microsoft.Crm.Sdk.Messages;
using Microsoft.Crm.Sdk.Samples;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWCRMDevTools
{
    class Program
    {
        private OrganizationServiceProxy _serviceProxy;
        private IOrganizationService _service;

        // Define the IDs needed for this sample.
        private Guid _accountId;
        static void Main(string[] args)
        {
            //foreach (string arg in args)
            //{
            //    switch (arg)
            //    {
            //        case 1:
            //            Console.WriteLine("Case 1");
            //            break;
            //        case 2:
            //            Console.WriteLine("Case 2");
            //            break;
            //        default:
            //            Console.WriteLine("Default case");
            //            break;
            //    }
            //}
            ServerConnection serverConnect = new ServerConnection();
            ServerConnection.Configuration config = serverConnect.GetServerConfiguration();
            ExportSolution app = new ExportSolution();
            app.Run(config, false, "C:\\CRMexports", "CDSamples");
        }

    }
}
