using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.SyncModel;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IUserSyncService
    {
        Task<List<UserSummary>> DownloadSummary(string url, Guid id);
    }
}