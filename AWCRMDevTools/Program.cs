using CommandLine;
using CommandLine.Text;
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
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {
                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration();
                Console.Write("\n Connection  Server: {0},  Org: {1},  User: {2}\t",
                         config.ServerAddress, config.OrganizationName, config.Credentials.UserName);

                // Values are available here
                if (!string.IsNullOrEmpty(options.ExportPackageLocation)
                    && !string.IsNullOrEmpty(options.SolutionName))
                {
                    ExportSolution app = new ExportSolution();
                    app.Run(config, false, options.ExportPackageLocation, options.SolutionName);
                }
            }
            Console.ReadLine();
        }
    }

    // Define a class to receive parsed values
    class Options
    {
        [Option('i', "import", 
          HelpText = "Path to package to be imported.")]
        public string ImportPackageLocation { get; set; }

        [Option('e', "export",
          HelpText = "Location of exported package.")]
        public string ExportPackageLocation { get; set; }

        [Option('s', "solutionname", 
            HelpText = "Name of solution in CRM")]
        public string SolutionName { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

}
