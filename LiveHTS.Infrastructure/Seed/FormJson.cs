using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed
{
    public class FormJson
    {
        public static List<Form> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^62040dcc-6260-11e7-907b-a6006ad3dba0^,
    ^Name^: ^HTS Lab Form^,
    ^Display^: ^HTS Lab Form^,
    ^Description^: ^HTS Lab Form^,
    ^Rank^: 1,
    ^ModuleId^: ^62040ce6-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  },
  {
    ^Id^: ^62040eb2-6260-11e7-907b-a6006ad3dba0^,
    ^Name^: ^HTS Linkage Form^,
    ^Display^: ^HTS Linkage Form^,
    ^Description^: ^HTS Linkage Form^,
    ^Rank^: 2,
    ^ModuleId^: ^62040ce6-6260-11e7-907b-a6006ad3dba0^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Form>>(raw.Replace("^", "\""));
        }
    }
}
