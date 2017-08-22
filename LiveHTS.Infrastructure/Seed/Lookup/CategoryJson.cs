using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Lookup;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Lookup
{
  public  class CategoryJson : ISeedJson<Category>
    {
        public  List<Category> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b25f86d8-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^YesNo^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^Disability^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f8b88-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^TestedAs^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^Strategy^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f911e-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^TBScreening^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f93d0-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^HIVTest^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f951a-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^TestResult^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f97a4-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^FinalResult^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f9952-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^YesNoNa^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f9c72-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^KitName^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Category>>(raw.Replace("^","\""));
        }
    }
}
