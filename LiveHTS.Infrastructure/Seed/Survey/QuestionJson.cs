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
    ^^: ^^
  },
  {
    ^Id^: ^b26039a2-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fb5fe-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 2,
    ^Display^: ^Re-Testing (No. Months since last test)^,
    ^Description^: ^Re-Testing (No. Months since last test)^,
    ^Rank^: 2,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2603c5e-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fb86a-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 3,
    ^Display^: ^Disability^,
    ^Description^: ^Disability^,
    ^Rank^: 3,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2603dc6-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fbb30-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 4,
    ^Display^: ^Consent^,
    ^Description^: ^Consent^,
    ^Rank^: 4,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b260401e-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fbcca-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 5,
    ^Display^: ^Client tested as^,
    ^Description^: ^Client tested as^,
    ^Rank^: 5,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b260417c-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fbf5e-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 6,
    ^Display^: ^Strategy^,
    ^Description^: ^Strategy^,
    ^Rank^: 6,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b26045aa-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fcd6e-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 8,
    ^Display^: ^HIV Test 1 Kit Name^,
    ^Description^: ^HIV Test 1 Kit Name^,
    ^Rank^: 8,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b260487a-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fcecc-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 9,
    ^Display^: ^HIV Test 1 Lot number of the test kit^,
    ^Description^: ^HIV Test 1 Lot number of the test kit^,
    ^Rank^: 9,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b26049f6-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fd1a6-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 10,
    ^Display^: ^HIV Test 1 Expiry date of the test kit.^,
    ^Description^: ^HIV Test 1 Expiry date of the test kit.^,
    ^Rank^: 10,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2604ce4-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fd322-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 11,
    ^Display^: ^HIV Test 1 Test Result^,
    ^Description^: ^HIV Test 1 Test Result^,
    ^Rank^: 11,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b260511c-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fcd6e-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 13,
    ^Display^: ^HIV Test 2 Kit Name^,
    ^Description^: ^HIV Test 2 Kit Name^,
    ^Rank^: 13,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b260525c-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fcecc-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 14,
    ^Display^: ^HIV Test 2 Lot number of the test kit^,
    ^Description^: ^HIV Test 2 Lot number of the test kit^,
    ^Rank^: 14,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2605540-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fd1a6-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 15,
    ^Display^: ^HIV Test 2 Expiry date of the test kit.^,
    ^Description^: ^HIV Test 2 Expiry date of the test kit.^,
    ^Rank^: 15,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2605694-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fd322-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 16,
    ^Display^: ^HIV Test 2 Test Result^,
    ^Description^: ^HIV Test 2 Test Result^,
    ^Rank^: 16,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2605964-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fd62e-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 17,
    ^Display^: ^Final Result^,
    ^Description^: ^Final Result^,
    ^Rank^: 17,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2605ab8-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fd78c-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 18,
    ^Display^: ^Final Result Given?^,
    ^Description^: ^Final Result Given?^,
    ^Rank^: 18,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2605c98-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fd94e-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 19,
    ^Display^: ^Couple Discordant^,
    ^Description^: ^Couple Discordant^,
    ^Rank^: 19,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b2605f54-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fc0c6-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 20,
    ^Display^: ^TB Screening^,
    ^Description^: ^TB Screening^,
    ^Rank^: 20,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b26060bc-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fdb9c-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 21,
    ^Display^: ^Linked to Care?^,
    ^Description^: ^Linked to Care?^,
    ^Rank^: 21,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b26062e2-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fddea-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 22,
    ^Display^: ^(Enter CCC#)^,
    ^Description^: ^(Enter CCC#)^,
    ^Rank^: 22,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b260644a-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fc698-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 23,
    ^Display^: ^Ever had an HIV self-test in the past 12 months?^,
    ^Description^: ^Ever had an HIV self-test in the past 12 months?^,
    ^Rank^: 23,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b260665c-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fc864-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: 24,
    ^Display^: ^Remarks^,
    ^Description^: ^Remarks^,
    ^Rank^: 24,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  },
  {
    ^Id^: ^b26067b0-852f-11e7-bb31-be2e44b06b34^,
    ^ConceptId^: ^b25fdf3e-852f-11e7-bb31-be2e44b06b34^,
    ^Ordinal^: ^18.a^,
    ^Display^: ^Reason Result Not Given^,
    ^Description^: ^Reason Result Not Given^,
    ^Rank^: 18.1,
    ^FormId^: ^b25ebcda-852f-11e7-bb31-be2e44b06b34^,
    ^^: ^^
  }
]
";
            return JsonConvert.DeserializeObject<List<Question>>(raw.Replace("^","\""));
        }
    }
}
