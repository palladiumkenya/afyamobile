using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Model.Config;
using LiveHTS.SharedKernel.Model;


namespace LiveHTS.Core.SyncModel
{
   public class Meta
    {
        public List<PracticeType> PracticeTypes { get; set; }=  new List<PracticeType>();
        public List<IdentifierType> IdentifierTypes { get; set; }=new List<IdentifierType>();
        public List<RelationshipType> RelationshipTypes { get; set; }= new List<RelationshipType>();
        public List<KeyPop> KeyPops { get; set; }=new List<KeyPop>();
        public List<MaritalStatus> MaritalStatuses { get; set; }=new List<MaritalStatus>();
        public List<ProviderType> ProviderTypes { get; set; }=new List<ProviderType>();
        public List<Action> Actions { get; set; }=new List<Action>();
        public List<Condition> Conditions { get; set; }=new List<Condition>();
        public List<ConceptType> ConceptTypes { get; set; }=new List<ConceptType>();
        public List<ValidatorType> ValidatorTypes { get; set; }=new List<ValidatorType>();
        public List<Validator> Validators { get; set; }=new List<Validator>();
        public List<EncounterType> EncounterTypes { get; set; }=new List<EncounterType>();
    }
}
