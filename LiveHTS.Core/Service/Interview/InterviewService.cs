using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveHTS.Core.Interfaces.Repository.Interview;
using LiveHTS.Core.Interfaces.Services.Interview;
using LiveHTS.Core.Model.Interview;
using LiveHTS.SharedKernel.Custom;

namespace LiveHTS.Core.Service.Interview
{
   public class InterviewService: IInterviewService
   {
       private readonly IEncounterRepository _encounterRepository;

       public InterviewService(IEncounterRepository encounterRepository)
       {
           _encounterRepository = encounterRepository;
       }

       public IEnumerable<Encounter> LoadEncounters(Guid clientId, Guid formId)
       {
           return _encounterRepository.LoadAll(formId, clientId);
       }

       public IEnumerable<Encounter> LoadEncounters(Guid clientId, Guid formId, Guid indexClient)
       {
           return _encounterRepository.LoadAll(formId, clientId, indexClient);
       }

       public IEnumerable<Encounter> LoadKeyEncounters(Guid clientId)
       {
           return _encounterRepository.LoadAllKey(clientId);
       }
   }
}