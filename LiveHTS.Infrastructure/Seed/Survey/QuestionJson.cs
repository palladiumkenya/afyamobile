using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Survey
{
  public  class QuestionJson : ISeedJson<Question>
    {
        public  List<Question> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b2603772-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fb496-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 1,
    ^Display^: ^Ever Tested?^,
    ^Description^: ^Ever Tested?^,
    ^Rank^: 1,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b26039a2-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fb5fe-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 2,
    ^Display^: ^Re-Testing (No. Months since last test)^,
    ^Description^: ^Re-Testing (No. Months since last test)^,
    ^Rank^: 2,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b2603c5e-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fb86a-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 3,
    ^Display^: ^Disability^,
    ^Description^: ^Disability^,
    ^Rank^: 3,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b2603dc6-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fbb30-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 4,
    ^Display^: ^Consent^,
    ^Description^: ^Consent^,
    ^Rank^: 4,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fbcca-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 5,
    ^Display^: ^Client tested as^,
    ^Description^: ^Client tested as^,
    ^Rank^: 5,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260417c-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fbf5e-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 6,
    ^Display^: ^Strategy^,
    ^Description^: ^Strategy^,
    ^Rank^: 6,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b2605f54-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fc0c6-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 20,
    ^Display^: ^TB Screening^,
    ^Description^: ^TB Screening^,
    ^Rank^: 20,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b26060bc-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fdb9c-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 21,
    ^Display^: ^Linked to Care?^,
    ^Description^: ^Linked to Care?^,
    ^Rank^: 21,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b26062e2-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fddea-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 22,
    ^Display^: ^(Enter CCC#)^,
    ^Description^: ^(Enter CCC#)^,
    ^Rank^: 22,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b260665c-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fc864-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 24,
    ^Display^: ^Remarks^,
    ^Description^: ^Remarks^,
    ^Rank^: 24,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Question>>(raw.Replace("^","\""));
        }
    }
}
