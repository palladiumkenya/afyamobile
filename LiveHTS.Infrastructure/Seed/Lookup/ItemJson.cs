using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Lookup;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Lookup
{
  public  class ItemJson : ISeedJson<Item>
    {
        public  List<Item> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^Y^,
    ^Display^: ^Yes^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^N^,
    ^Display^: ^No^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ed1c0-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^NA^,
    ^Display^: ^NA: Not Applicable^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ed332-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^D^,
    ^Display^: ^D: Deaf/hearing impaired^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ed648-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^B^,
    ^Display^: ^B: Blind/Visually impaired^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ed7c4-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^M^,
    ^Display^: ^M: Mentally Challenged^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ed9ea-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^P^,
    ^Display^: ^P: Physically challenged^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25edb5c-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^O^,
    ^Display^: ^O: Other (specify)^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ede36-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^I^,
    ^Display^: ^I: Individual^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ee0a2-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^C^,
    ^Display^: ^C: Couple (includes polygamous)^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ee20a-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^HP^,
    ^Display^: ^HP: Health Facility Patients^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ee476-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^NP^,
    ^Display^: ^NP: Non-patients^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ee642-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^VI^,
    ^Display^: ^VI: Integrated VCT sites^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ee930-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^VS^,
    ^Display^: ^VS: Stand-alone VCT sites^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25eeab6-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^HB^,
    ^Display^: ^HB: Home-based^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25eed36-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^MO^,
    ^Display^: ^MO: Mobile and Outreach^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25eefca-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^Pr TB^,
    ^Display^: ^Pr TB: Presumed TB^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ef128-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^NS^,
    ^Display^: ^NS: No signs^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ef3d0-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^ND^,
    ^Display^: ^ND: Not done^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ef63c-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^TBRX^,
    ^Display^: ^TBRX: On TB treatment^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ef7c2-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^H1^,
    ^Display^: ^HIV Test-1^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25ef90c-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^H2^,
    ^Display^: ^HIV Test-2^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^N^,
    ^Display^: ^N: Negative (non-reactive)^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^P^,
    ^Display^: ^P: Positive (Reactive)^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f001e-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^I^,
    ^Display^: ^I: Invalid^,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f017c-852f-11e7-bb31-be2e44b06b34^,
    ^Code^: ^Ic^,
    ^Display^: ^Ic: Inconclusive^,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<Item>>(raw.Replace("^","\""));
        }
    }
}
