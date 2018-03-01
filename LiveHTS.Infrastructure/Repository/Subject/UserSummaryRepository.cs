using System;
using System.Collections.Generic;
using System.Linq;
using LiveHTS.Core.Engine;
using LiveHTS.Core.Interfaces;
using LiveHTS.Core.Interfaces.Repository.Subject;
using LiveHTS.Core.Model.Subject;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;

namespace LiveHTS.Infrastructure.Repository.Subject
{
    public class UserSummaryRepository : BaseRepository<UserSummary, Guid>, IUserSummaryRepository
    {
        public UserSummaryRepository(ILiveSetting liveSetting) : base(liveSetting)
        {
        }
        public IEnumerable<UserSummary> GetByUserId(Guid clientId)
        {
            var states=GetAll(x => x.UserId == clientId).ToList();
            return states;
        }

        public void SaveSummary(UserSummary clientState)
        {
           InsertOrUpdate(clientState);
        }

        public void SaveSummary(List<UserSummary> clientStates)
        {
            foreach (var clientState in clientStates)
            {
                SaveSummary(clientState);
            }
        }

        public void DeleteSummary(Guid clientId)
        {
            _db.Execute($"DELETE FROM {nameof(UserSummary)} WHERE UserId=?", clientId.ToString());
        }

    }
}