using Microsoft.Crm.Sdk.Messages;
using Microsoft.Crm.Sdk.Samples;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AWCRMDevTools
{
    public class ImportSolution
    {
        public void Run(ServerConnection.Configuration serverConfig, string solutionPath)
        {
            try
            {
                using (OrganizationServiceProxy _serviceProxy = new OrganizationServiceProxy(serverConfig.OrganizationUri, serverConfig.HomeRealmUri, serverConfig.Credentials, serverConfig.DeviceCredentials))
                {
                    byte[] data = File.ReadAllBytes(solutionPath);
                    Guid importId = Guid.NewGuid();

                    Console.WriteLine("\n Importing solution {0} into Server {1}.", solutionPath, serverConfig.OrganizationUri);

                    _serviceProxy.EnableProxyTypes();
                    ImportSolutionRequest importSolutionRequest = new ImportSolutionRequest()
                    {
                        CustomizationFile = data,
                        ImportJobId = importId
                    };

                    ThreadStart starter = () =>ProgressReport(serverConfig, importId);
                    Thread t = new Thread(starter);
                    t.Start();

                    _serviceProxy.Execute(importSolutionRequest);
                    Console.Write("Solution {0} successfully imported into {1}", solutionPath, serverConfig.OrganizationUri);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static void ProgressReport(ServerConnection.Configuration serverConfig, object importId)
        {                        
            try
            {
                using (OrganizationServiceProxy _serviceProxy = new OrganizationServiceProxy(serverConfig.OrganizationUri, serverConfig.HomeRealmUri, serverConfig.Credentials, serverConfig.DeviceCredentials))
                {
                    var job = _serviceProxy.Retrieve("importjob", (Guid)importId, new ColumnSet("solutionname", "progress"));
                    decimal progress = Convert.ToDecimal(job["progress"]);

                    Console.WriteLine("{0:N0}%", progress);

                    if (progress == 100) { return; }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Thread.Sleep(2000);
            ProgressReport(serverConfig, importId);
        }
    }
}
