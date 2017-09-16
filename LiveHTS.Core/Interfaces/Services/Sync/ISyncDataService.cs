using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.SyncModel;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface ISyncDataService
    {
        void UpdateMeta(Meta meta);

        void UpdateModules(List<Module>modules);

        void UpdateForms(List<Form> forms);

        void UpdateConcepts(List<Concept> concepts);

        void UpdateQuestions(List<Question> questions);

        void UpdateStaff(List<Person> persons);

        void Update<T>(T data) where T : class;
        void Update<T>(List<T> data) where T : class;
    }
}