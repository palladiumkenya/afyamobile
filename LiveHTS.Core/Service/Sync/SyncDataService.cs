using System.Collections.Generic;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.SyncModel;
using SQLite;

namespace LiveHTS.Core.Service.Sync
{
    public class SyncDataService : ISyncDataService
    {
        private readonly ISyncDataRepository _syncDataRepository;

        public SyncDataService(ISyncDataRepository syncDataRepository)
        {
            _syncDataRepository = syncDataRepository;
        }


        public void UpdateMeta(SyncModel.Meta meta)
        {
            Update(meta.PracticeTypes);
            Update(meta.IdentifierTypes);
            Update(meta.RelationshipTypes);
            Update(meta.KeyPops);
            Update(meta.MaritalStatuses);
            Update(meta.ProviderTypes);
            Update(meta.Actions);
            Update(meta.Conditions);
            Update(meta.ConceptTypes);
            Update(meta.ValidatorTypes);
            Update(meta.Validators);
            Update(meta.EncounterTypes);
        }

        public void UpdateModules(List<Module> modules)
        {
            foreach (var module in modules)
            {
                Update(module);
                Update(module.Forms);
            }
        }

        public void UpdateForms(List<Form> forms)
        {
            foreach (var form in forms)
            {
                Update(form);
                Update(form.Programs);
            }
        }

        public void UpdateConcepts(List<Concept> concepts)
        {
            Update(concepts);
        }

        public void UpdateQuestions(List<Question> questions)
        {
            foreach (var question in questions)
            {
                Update(question);
                Update(question.Validations);
                Update(question.ReValidations);
                Update(question.Branches);
                Update(question.Transformations);
                Update(question.RemoteTransformations);

            }
        }

        public void UpdateStaff(List<Person> persons)
        {
            foreach (var person in persons)
            {
                Update(person);
                Update(person.Addresses);
                Update(person.Contacts);
            }
        }

    

        public void Update<T>(T data) where T : class
        {
            _syncDataRepository.Update(data);
        }

        public void Update<T>(List<T> data) where T : class
        {
            foreach (var d in data)
            {
                 Update(d);
            }
        }
    }
}
