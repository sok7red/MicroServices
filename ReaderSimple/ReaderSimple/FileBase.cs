using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaderSimple
{
    public class FileBase
    {
        public string TaskId { get; set; }


        public string BaselineTaskId { get; set; }


        public string ScenarioLabel { get; set; }


        public DateTime ValuationDate { get; set; }


        public string CounterpartyId { get; set; }


        public string LegalDocumentId { get; set; }


        public string InternalEntityId { get; set; }


        public DateTime Date { get; set; }


        public string Path { get; set; }

        
        public string HierarchyId { get; set; }

        public double Percentile { get; set; }

        public string Measure { get; set; }

        public string MeasureValue { get; set; }

        public string Error { get; set; }
    }

    //public sealed class FileBaseMap : ClassMap<FileBase>
    //{
    //    public FileBaseMap()
    //    {
    //        AutoMap(CultureInfo.InvariantCulture);
    //        Map(m => m.HierarchyId).Ignore();
    //    }
    //}
}
