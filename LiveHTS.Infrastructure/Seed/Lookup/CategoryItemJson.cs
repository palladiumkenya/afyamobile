using System.Collections.Generic;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Model.Lookup;
using Newtonsoft.Json;

namespace LiveHTS.Infrastructure.Seed.Lookup
{
  public  class CategoryItemJson : ISeedJson<CategoryItem>
    {
        public  List<CategoryItem> Read()
        {
            string raw = @"
[
  {
    ^Id^: ^b25f2864-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f86d8-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f29e0-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f86d8-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f2b34-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed1c0-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f2dfa-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed332-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f2f76-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed648-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f30d4-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed7c4-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 4,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f3426-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed9ea-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 5,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f35d4-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8926-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25edb5c-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 6,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f38b8-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8b88-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ede36-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f3a20-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8b88-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ee0a2-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f3d2c-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ee20a-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f3e94-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ee476-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f4164-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ee642-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f42ea-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ee930-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 4,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f4592-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25eeab6-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 5,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f48b2-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f8fa2-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25eed36-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 6,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f4a42-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f911e-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25eefca-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f4baa-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f911e-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ef128-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f4e16-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f911e-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ef3d0-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f4f74-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f911e-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ef63c-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 4,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f529e-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f93d0-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ef7c2-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f5424-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f93d0-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ef90c-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f56f4-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f951a-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f5866-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f951a-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f5b2c-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f951a-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f001e-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f5cf8-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f97a4-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25efb78-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6004-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f97a4-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25efd8a-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f614e-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f97a4-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f017c-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6342-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9952-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25eccd4-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6608-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9952-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed04e-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6770-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9952-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25ed1c0-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6982-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9c72-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f0456-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6ae0-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9c72-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f05aa-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6df6-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9c72-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f0776-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 3,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f6f86-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9e16-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f0a50-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f70c6-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25f9e16-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f102c-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f73a0-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25fa190-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f136a-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 1,
    ^Voided^: 0
  },
  {
    ^Id^: ^b25f7508-852f-11e7-bb31-be2e44b06b34^,
    ^CategoryId^: ^b25fa190-852f-11e7-bb31-be2e44b06b34^,
    ^ItemId^: ^b25f159a-852f-11e7-bb31-be2e44b06b34^,
    ^Display^: ^^,
    ^Rank^: 2,
    ^Voided^: 0
  }
]
";
            return JsonConvert.DeserializeObject<List<CategoryItem>>(raw.Replace("^","\""));
        }
    }
}
