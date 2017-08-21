using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class ConceptJson : ISeedJson<Concept>
    {
        public  List<Concept> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b25fb496-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Ever Tested^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f86d8-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fb5fe-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Re-Testing^,
    ^ConceptTypeId^: ^Numeric^,
    ^CategoryId^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fb86a-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Disability^,
    ^ConceptTypeId^: ^Multi^,
    ^CategoryId^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fbb30-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Consent^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f86d8-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fbcca-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Client tested as^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f8b88-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fbf5e-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Strategy^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fc0c6-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^TB Screening^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f911e-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fc698-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^HIV self-test^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f86d8-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fc864-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Remarks^,
    ^ConceptTypeId^: ^Text^,
    ^CategoryId^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fcac6-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^HIV Test^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f93d0-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fcd6e-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Kit Name:^,
    ^ConceptTypeId^: ^Text^,
    ^CategoryId^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fcecc-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Lot No:^,
    ^ConceptTypeId^: ^Text^,
    ^CategoryId^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fd1a6-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Expiry Date:^,
    ^ConceptTypeId^: ^DateTime^,
    ^CategoryId^: ^^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fd322-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Test Result:^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f951a-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fd62e-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Final Result^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f97a4-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fd78c-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Final Result Given?^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f86d8-852f-11e7-bb31-be2e44b06b42^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fd94e-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Couple Discordant^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f9952-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fdb9c-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^Linked to Care?^,
    ^ConceptTypeId^: ^Single^,
    ^CategoryId^: ^b25f86d8-852f-11e7-bb31-be2e44b06b42^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25fddea-852f-11e7-bb31-be2e44b06b34^,
    ^Name^: ^CCC Number^,
    ^ConceptTypeId^: ^Text^,
    ^CategoryId^: ^^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Concept>>(raw.Replace("^","\""));
        }
    }
}
