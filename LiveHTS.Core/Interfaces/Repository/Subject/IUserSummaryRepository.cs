using System;
using System.Collections.Generic;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Core.Interfaces.Repository.Subject
{
    public interface IUserSummaryRepository : IRepository<UserSummary, Guid>
    {
        IEnumerable<UserSummary> GetByUserId(Guid clientId);

        void SaveSummary(UserSummary clientState);
        void SaveSummary(List<UserSummary> clientStates);

        void DeleteSummary(Guid clientId);
    }
}