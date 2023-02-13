// See https://aka.ms/new-console-template for more information

using CsvHelper;
using CsvHelper.Configuration;
using ReaderSimple;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using System;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Newtonsoft.Json;


namespace WorkerService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                .AddJsonFile($"json1.json")
                .Build();
            
            string fileName = "C:\\Temp\\qu\\ResultsCSV.csv";
            string destinationName = "C:\\Temp\\qu\\ResultsCSV_Processed.csv";

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            };

            var section = $"RequestTemplates:RiskTypeResultFilters";
            var template = configuration.GetSection(section);
            var template_local = configuration.GetSection(section).Get<RiskTypeResultFilters>();
            
//            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(template)

            Func<FileBase, bool> filterer = row =>
            {
                var hierarchyid = template.GetValue<string>("hierarchyId");
                var measure = template.GetValue<string>("measure");

                if (row.HierarchyId == template.GetValue<string>("hierarchyId") &&
                    row.Measure == template.GetValue<string>("measure"))
                    return false;
                return true;
            };

            try
            {
                var appendedLineCount = 0;
                using (var reader = new StreamReader(fileName))
                using (var writer = new StreamWriter(destinationName, append: true))
                using (var input = new CsvReader(reader, config))
                using (var output = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    //input.Context.RegisterClassMap<FileBaseMap>();

                    

                    var inputRecords = input.GetRecords<FileBase>();
                    //output.WriteRecord(string.Join(",", input.HeaderRecord));


                    foreach (var inputRecord in inputRecords)
                    {
                        if (filterer(inputRecord))
                            continue;

                        output.WriteRecord(inputRecord);
                        output.NextRecord();
                        appendedLineCount++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
    public class Calibration
    {
        public string hierarchyId { get; set; }
        public string measure { get; set; }
        public string classicName { get; set; }
    }

    public class Capital
    {
        public string hierarchyId { get; set; }
        public string measure { get; set; }
        public string classicName { get; set; }
    }

    public class Pfe
    {
        public string hierarchyId { get; set; }
        public string measure { get; set; }
        public string classicName { get; set; }
    }
    public class UncollateralisedEAD
    {
        public string hierarchyId { get; set; }
        public string measure { get; set; }
        public string classicName { get; set; }
    }

    public class RiskTypeResultFilters
    {
        public List<Pfe> Pfe { get; set; }
        public List<Capital> Capital { get; set; }
        public List<UncollateralisedEAD> UncollateralisedEAD { get; set; }
        public List<Calibration> Calibration { get; set; }
    }


}
