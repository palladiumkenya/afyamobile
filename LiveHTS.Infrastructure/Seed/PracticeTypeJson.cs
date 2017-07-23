using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed
{
  public  class PracticeTypeJson
    {
        public static List<PracticeType> Read()
        {
            string raw = @"
"A";"B";"C"
            "Name"; "Id"; "Voided"
            "Facility Based"; "Facility"; 0

";
            return JsonConvert.DeserializeObject<List<PracticeType>>(raw.Replace("^","\""));
        }
    }
}
