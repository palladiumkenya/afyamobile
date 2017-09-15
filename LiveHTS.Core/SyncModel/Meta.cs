using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;


namespace LiveHTS.Core.SyncModel
{
   public class Meta
    {
        public List<PracticeType> PracticeTypes { get; set; }
        public List<IdentifierType> IdentifierTypes { get; set; }
        public List<RelationshipType> RelationshipTypes { get; set; }
        public List<KeyPop> KeyPops { get; set; }
        public List<MaritalStatus> MaritalStatuses { get; set; }
        public List<ProviderType> ProviderTypes { get; set; }
        public List<Action> Actions { get; set; }
        public List<Condition> Conditions { get; set; }
        public List<ConceptType> ConceptTypes { get; set; }
        public List<ValidatorType> ValidatorTypes { get; set; }
        public List<Validator> Validators { get; set; }
        public List<EncounterType> EncounterTypes { get; set; }
    }
}
