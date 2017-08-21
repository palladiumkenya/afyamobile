using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
    public class FormJson : ISeedJson<Form>
    {
        public  List<Form> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^HTS Lab Form^,
    ^Display^: ^HTS Lab Form^,
    ^Description^: ^HTS Lab Form^,
    ^Rank^: 1,
    ^ModuleId^: ^b260c688-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Form>>(raw.Replace("^", "\""));
        }
    }
}

/*
{
    ^Id^: ^b25ec112-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^HTS Linkage Form^,
    ^Display^: ^HTS Linkage Form^,
    ^Description^: ^HTS Linkage Form^,
    ^Rank^: 2,
    ^ModuleId^: ^b260c688-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  }
 */
