﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.Lookup;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Core.Model.Survey;
using LiveHTS.Core.SyncModel;

namespace LiveHTS.Core.Interfaces.Services.Sync
{
    public interface IClientSyncService
    {
        Task SendClients(string url,SyncClientDTO client);
        Task SendClientEncounters(string url, List<SyncClientEncounterDTO> encounters);
        Task SendClientShrs(string url, List<PSmartStore> pSmartStores);

        Task<bool> AttempSendClients(string url, SyncClientDTO client);
        Task<bool> AttempSendClientEncounters(string url, List<SyncClientEncounterDTO> encounters);
        Task<bool> AttempSendClientShrs(string url, List<PSmartStore> pSmartStores);

        Task<List<RemoteClientDTO>> SearchClients(string url, string name, Guid? practiceId = null);
        Task<RemoteClientDTO> DownloadClient(string url, Guid id);
    }
}