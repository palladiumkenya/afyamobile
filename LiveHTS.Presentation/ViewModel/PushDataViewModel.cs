using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.SharedKernel.Custom;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class PushDataViewModel:MvxViewModel, IPushDataViewModel
    {
        //TODO: Update version
        private readonly int _hapiVersion = 109;

        //TODO: Allow deleteOnPush
        private bool deleteOnPush = true;

        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private readonly IActivationService _activationService;
        private readonly IClientReaderService _clientReaderService;
        private readonly IDeviceSetupService _deviceSetupService;
        private readonly IClientSyncService _clientSyncService;
        private readonly IPracticeSetupService _practiceSetupService;

        private string _address;
        private string _currentStatus;
        private int _currentStatusProgress;
        private string _overallStatus;
        private int _overallStatusProgress;
        private IMvxCommand _pushDataCommand;
        private bool _isBusy;
        private string _currentStatusTotal;
        private string _currentStatusSent;
        private string _currentStatusFailed;
        private string _currentStatusSummary;

        public Device Device { get; set; }
        public ServerConfig Local { get; set; }

        public Guid AppPracticeId
        {
            get { return GetGuid("livehts.practiceid"); }
        }
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; RaisePropertyChanged(() => IsBusy);}
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; RaisePropertyChanged(() => Address); }
        }

        public string CurrentStatusTotal
        {
            get { return _currentStatusTotal; }
            set { _currentStatusTotal = value; RaisePropertyChanged(() => CurrentStatusTotal); }
        }

        public string CurrentStatusSent
        {
            get { return _currentStatusSent; }
            set { _currentStatusSent = value; RaisePropertyChanged(() => CurrentStatusSent); }
        }
        public string CurrentStatusFailed
        {
            get { return _currentStatusFailed; }
            set { _currentStatusFailed = value; RaisePropertyChanged(() => CurrentStatusFailed); }
        }

        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; RaisePropertyChanged(() => CurrentStatus); }
        }
        public string CurrentStatusSummary
        {
            get { return _currentStatusSummary; }
            set { _currentStatusSummary = value; RaisePropertyChanged(() => CurrentStatusSummary); }
        }

        public int CurrentStatusProgress
        {
            get { return _currentStatusProgress; }
            set { _currentStatusProgress = value; RaisePropertyChanged(() => CurrentStatusProgress); }
        }

        public string OverallStatus
        {
            get { return _overallStatus; }
            set { _overallStatus = value; RaisePropertyChanged(() => OverallStatus); }
        }

        public int OverallStatusProgress
        {
            get { return _overallStatusProgress; }
            set { _overallStatusProgress = value; RaisePropertyChanged(() => OverallStatusProgress); }
        }

        public IMvxCommand PushDataCommand
        {
            get
            {
                _pushDataCommand = _pushDataCommand ?? new MvxCommand(PushData, CanPullData);
                return _pushDataCommand;
            }
        }
        public PushDataViewModel(IDialogService dialogService, ISettings settings, IDeviceSetupService deviceSetupService, IActivationService activationService, IClientSyncService clientSyncService, IClientReaderService clientReaderService, IPracticeSetupService practiceSetupService)
        {
            _dialogService = dialogService;
            _settings = settings;
            _deviceSetupService = deviceSetupService;
            _activationService = activationService;
            _clientSyncService = clientSyncService;
            _clientReaderService = clientReaderService;
            _practiceSetupService = practiceSetupService;
        }

        public void Init()
        {
            IsBusy = false;
            LoadInit();
        }

        public override void ViewAppeared()
        {
            IsBusy = false;
            LoadInit();
        }
        private void LoadInit()
        {
            var deviceJson = _settings.GetValue("device.id", "");
            var hapiLocal = _settings.GetValue("hapi.local", "");

            if (null == Device && !string.IsNullOrWhiteSpace(deviceJson))
            {
                Device = JsonConvert.DeserializeObject<Device>(deviceJson);
            }
            else
            {
                Device = _deviceSetupService.GetDefault();
                if (null == Device)
                {
                    Device = new Device();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Device);
                    _settings.AddOrUpdateValue("device.id", json);
                }
            }

            if (null == Local && !string.IsNullOrWhiteSpace(hapiLocal))
            {
                Local = JsonConvert.DeserializeObject<ServerConfig>(hapiLocal);
            }
            else
            {
                Local = _deviceSetupService.GetLocal();
                if (null == Local)
                {
                    Local = new ServerConfig();
                }
                else
                {
                    var json = JsonConvert.SerializeObject(Local);
                    _settings.AddOrUpdateValue("hapi.local", json);
                }
            }
            Address = Local.Address;
        }

        private bool CanPullData()
        {
            return !string.IsNullOrEmpty(Address);
        }

        private async void PushData()
        {
            IsBusy = true;
            CurrentStatusTotal = "";
            CurrentStatusSent =  "";
           CurrentStatusFailed =  "";
           CurrentStatusSummary = "";
           CurrentStatus = "";
           CurrentStatus = $"connecting...";


            var practice = await _activationService.GetLocal(Address);

            if (null != practice)
            {
                bool completed = true;

                try
                {
                    var response = await _activationService.AttempCheckVersion(Address);

                    if (string.IsNullOrWhiteSpace(response))
                    {

                        _dialogService.Alert($"you are using an old version of LiveHAPI");
                        IsBusy = false;
                        CurrentStatus = $"send failed! updated LiveHAPI ";
                        return;
                    }

                    int.TryParse(response, out var version);

                    if (version != _hapiVersion)
                    {
                        _dialogService.Alert($"you are using an old version of LiveHAPI");
                        IsBusy = false;
                        CurrentStatus = $"send failed! update LiveHAPI to v{_hapiVersion}";
                        return;
                    }

                    var pracs = _practiceSetupService.GetAll();
                    await _activationService.AttemptEnrollPractice(Address, pracs);
                }
                catch (Exception e)
                {
                    _dialogService.Alert($"connection Error {e.Message}");
                    IsBusy = false;
                    return;
                }

                var preseveIds=new List<Guid>();
                var liveClients=new List<LiveClient>();
                List<Guid> ids;
                 ids = _clientReaderService.LoadClientIds(AppPracticeId);
                var count= ids.Count;
                int n = 0;

                var clientIdsDelete=new List<ClientToDeleteDTO>();
                var clientIdsDeleteLater=new List<ClientToDeleteDTO>();
                var clientIdsDeleteNow=new List<ClientToDeleteDTO>();
                var encountersDelete=new List<EnconterToDeleteDTO>();
                int deleted = 0;
                var practicses = _practiceSetupService.GetAll();

                if (ids.Any())
                {
                    _clientReaderService.ResetState();
                }

                foreach (var id in ids)
                {
                    var clientToDeleteDto = new ClientToDeleteDTO();
                    n++;

                    Client client = null;

                    try
                    {
                        client = _clientReaderService.LoadClient(id);
                    }
                    catch (Exception e)
                    {

                    }

                    if (null != client)
                    {
                        var clientInfo = new SyncClientDTO(client);

                        clientInfo.PracticeCode = GetCode(clientInfo.PracticeId, practicses);

                        CurrentStatus = showPerc("Clients", n, count);

                        bool status = false;
                        bool isCompleted = client.CanBeSynced();

                        if (client.Relationships.Any() && client.IsHtstEnrolled())
                        {
                            var liveClient = new LiveClient(client.Id, isCompleted,client.IsHtstEnrolled());
                            foreach (var relationship in client.Relationships)
                            {
                                liveClient.AddRelation(relationship.RelatedClientId,LiveClient.CheckIfCleared(relationship.RelatedClientId,liveClients));
                            }
                            liveClients.Add(liveClient);
                        }

                        if (!client.IsHtstEnrolled())
                        {
                            isCompleted = !RelatedLiveClient.CheckIfPrimaryNotCleared(client.Id, liveClients);
                        }

                        try
                        {
                            if (isCompleted)
                            {
                                status = await _clientSyncService.AttempSendClients(Address, clientInfo);
                            }
                            else
                            {

                            }
                        }
                        catch (Exception e)
                        {
                        }

                        if (status)
                        {
                            clientToDeleteDto = new ClientToDeleteDTO(client.Id, client.PersonId);
                            if (client.IsHtstEnrolled())
                            {
                                clientToDeleteDto.NotYet = liveClients.Any(x => x.ClientId == client.Id);
                            }
                            else
                            {
                                var rels = liveClients.SelectMany(x => x.RelatedClients).ToList();
                                clientToDeleteDto.NotYet = rels.Any(x => x.ClientId == client.Id);
                            }
                        }
                        else
                        {
                            completed = false;
                        }
                    }

                    if (!clientToDeleteDto.NotSent)
                    {
                        List<Encounter> encounters = new List<Encounter>();
                        try
                        {
                            encounters = _clientReaderService.LoadEncounters(id);

                        }
                        catch (Exception e)
                        {
                        }

                        if (null != encounters && encounters.Count > 0)
                        {
                            var syncEncounters = SyncClientEncounterDTO.Create(encounters, client);
                            bool status = false;
                            try
                            {
                                status = await _clientSyncService.AttempSendClientEncounters(Address, syncEncounters);
                            }
                            catch (Exception e)
                            {

                            }

                            if (status)
                            {
                                foreach (var encounter in encounters)
                                {
                                    clientToDeleteDto.AddEnounter(new EnconterToDeleteDTO(encounter.Id,
                                        encounter.EncounterType));
                                }
                            }
                            else
                            {
                                completed = false;
                            }

                        }

                        List<PSmartStore> shrs = new List<PSmartStore>();
                        try
                        {
                            shrs = _clientReaderService.LoadPSmartStores(id);
                        }
                        catch (Exception e)
                        {
                        }


                        if (null != shrs && shrs.Count > 0)
                        {
                            try
                            {
                                await _clientSyncService.SendClientShrs(Address, shrs);
                            }
                            catch (Exception e)
                            {
                            }
                        }

                        clientIdsDelete.Add(clientToDeleteDto);

                        if(!clientToDeleteDto.NotYet)
                            clientIdsDeleteNow.Add(clientToDeleteDto);

                        if(clientToDeleteDto.NotYet)
                            clientIdsDeleteLater.Add(clientToDeleteDto);

                        foreach (var toDeleteDto in clientIdsDeleteNow)
                        {
                            //TODO: ALLOW DELETE []
                            if (deleteOnPush)
                            {
                                try
                                {
                                    _clientReaderService.Purge(toDeleteDto);
                                }
                                catch (Exception e)
                                {
                                }
                            }

                        }
                    }
                }


                //// check rels

                if (clientIdsDeleteLater.Any())
                {
                    var notCleared = liveClients.Where(x => !x.IsCleared).ToList();
                    if (!notCleared.Any())
                    {
                        //remove all
                        foreach (var toDeleteDto in clientIdsDeleteLater)
                        {
                            //TODO: ALLOW DELETE []
                            if (deleteOnPush)
                            {
                                try
                                {
                                    _clientReaderService.Purge(toDeleteDto);
                                    deleted++;
                                }
                                catch (Exception e)
                                {
                                }
                            }

                        }
                    }
                    else
                    {

                    }
                }

                //  send

                CurrentStatusTotal = $"Total Clients {count}";
                CurrentStatusSent =  $"Total Sent {(clientIdsDeleteNow.Count + deleted)}";
                CurrentStatusFailed =$"Total Failed {count - (clientIdsDeleteNow.Count + deleted)}";
                var messageVerdict = $"Check and resolve any Incomplete records and review server logs";

                if (completed)
                {
                    if(count>(clientIdsDeleteNow.Count + deleted))
                        CurrentStatusSummary =messageVerdict;

                    _dialogService.ShowToast("process completed");
                }
                else
                {
                    CurrentStatusSummary = messageVerdict;
                    _dialogService.ShowToast("process completed");
                }
            }
            else
            {
                IsBusy = false;
                CurrentStatus = $"connection failed !!";
                CurrentStatusSummary = "Could not connect to server";

                _dialogService.Alert("Could not connect to server");
            }

            IsBusy = false;

        }

        private string GetCode(Guid? clientInfoPracticeId, List<Practice> practicses)
        {
            string code = string.Empty;

            if (!clientInfoPracticeId.IsNullOrEmpty())
            {
                var prac = practicses.FirstOrDefault(x => x.Id == clientInfoPracticeId.Value);
                code = null != prac ? prac.Code : string.Empty;
            }

            return code;
        }


        private string showPerc(string message, int complete,int total)
        {
            int percentComplete = (int)Math.Round((double)(100 * complete) / total);

            return $"uploading {message}...  {percentComplete}% ";
        }

        public Guid GetGuid(string key)
        {
            var guid = _settings.GetValue(key, "");

            if (string.IsNullOrWhiteSpace(guid))
                return Guid.Empty;

            return new Guid(guid);
        }
    }

    public class LiveClient
    {
        public Guid ClientId { get; }
        public bool IsCleared { get; }
        public bool IsIndex { get; }
        public List<RelatedLiveClient> RelatedClients { get; set; } = new List<RelatedLiveClient>();

        public LiveClient(Guid clientId, bool isCleared,bool isIndex)
        {
            ClientId = clientId;
            IsCleared = isCleared;
            IsIndex = isIndex;
        }

        public void AddRelation(Guid relatedClientId, bool isCleared)
        {
            RelatedClients.Add(new RelatedLiveClient(ClientId, relatedClientId, isCleared));
        }

        public static bool CheckIfCleared(Guid id,List<LiveClient> liveClients)
        {
            if (liveClients.Any(x => x.ClientId == id))
            {
                return liveClients.First(x=>x.ClientId==id).IsCleared;
            }
            return false;
        }
    }

    public class RelatedLiveClient
    {
        public Guid PrimaryClientId { get; }
        public Guid ClientId { get; }
        public bool IsCleared { get; }

        public RelatedLiveClient(Guid primaryClientId, Guid clientId, bool isCleared)
        {
            PrimaryClientId = primaryClientId;
            ClientId = clientId;
            IsCleared = isCleared;
        }

        public static bool CheckIfPrimaryNotCleared(Guid id,List<LiveClient> liveClients)
        {
            var allFailed = liveClients.Where(x => !x.IsCleared).ToList();

            if (allFailed.Any())
            {
                var allFailedRel = allFailed.SelectMany(x => x.RelatedClients).ToList();
                return allFailed.Any(x => x.ClientId == id);
            }
            return false;
        }
    }

}
