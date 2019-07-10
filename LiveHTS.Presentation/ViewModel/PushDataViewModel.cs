using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class PushDataViewModel : MvxViewModel, IPushDataViewModel
    {
        //TODO: Update version
        private readonly int _hapiVersion = 110;

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
        private List<Guid> _sentIds = new List<Guid>();
        private List<Guid> _relsIds=new List<Guid>();

        public Device Device { get; set; }
        public ServerConfig Local { get; set; }

        public Guid AppPracticeId
        {
            get { return GetGuid("livehts.practiceid"); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
            }
        }

        public string CurrentStatusTotal
        {
            get { return _currentStatusTotal; }
            set
            {
                _currentStatusTotal = value;
                RaisePropertyChanged(() => CurrentStatusTotal);
            }
        }

        public string CurrentStatusSent
        {
            get { return _currentStatusSent; }
            set
            {
                _currentStatusSent = value;
                RaisePropertyChanged(() => CurrentStatusSent);
            }
        }

        public string CurrentStatusFailed
        {
            get { return _currentStatusFailed; }
            set
            {
                _currentStatusFailed = value;
                RaisePropertyChanged(() => CurrentStatusFailed);
            }
        }

        public string CurrentStatus
        {
            get { return _currentStatus; }
            set
            {
                _currentStatus = value;
                RaisePropertyChanged(() => CurrentStatus);
            }
        }

        public string CurrentStatusSummary
        {
            get { return _currentStatusSummary; }
            set
            {
                _currentStatusSummary = value;
                RaisePropertyChanged(() => CurrentStatusSummary);
            }
        }

        public int CurrentStatusProgress
        {
            get { return _currentStatusProgress; }
            set
            {
                _currentStatusProgress = value;
                RaisePropertyChanged(() => CurrentStatusProgress);
            }
        }

        public string OverallStatus
        {
            get { return _overallStatus; }
            set
            {
                _overallStatus = value;
                RaisePropertyChanged(() => OverallStatus);
            }
        }

        public int OverallStatusProgress
        {
            get { return _overallStatusProgress; }
            set
            {
                _overallStatusProgress = value;
                RaisePropertyChanged(() => OverallStatusProgress);
            }
        }

        public IMvxCommand PushDataCommand
        {
            get
            {
                _pushDataCommand = _pushDataCommand ?? new MvxCommand(PushDataRelations, CanPullData);
                return _pushDataCommand;
            }
        }

        public PushDataViewModel(IDialogService dialogService, ISettings settings,
            IDeviceSetupService deviceSetupService, IActivationService activationService,
            IClientSyncService clientSyncService, IClientReaderService clientReaderService,
            IPracticeSetupService practiceSetupService)
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

        private async void PushDataRelations()
        {
            bool completed = true;
            bool completedRelations = true;
            List<Guid> clientRelationsIds = new List<Guid>();
            List<Guid> clientNoRelationsIds = new List<Guid>();
            List<ClientToDeleteDTO> clientIdsDelete = new List<ClientToDeleteDTO>();
            int n = 0;

            #region Initialize

            InitSend();

            var practice = await GetPractice();

            if (null == practice)
                return;

            bool serverOnline = await CheckConnection();

            if (!serverOnline)
                return;

            var practicses = _practiceSetupService.GetAll();

            #endregion

            #region LoadIds

            _clientReaderService.ResetState();

            var allClientsIds = _clientReaderService.LoadClientIds(AppPracticeId);
            clientNoRelationsIds = allClientsIds;
            var totalCount = allClientsIds.Count;

            var clientIdsWithRelations = _clientReaderService.LoadClientIdsWithRelations(AppPracticeId);
            clientRelationsIds = clientIdsWithRelations.Select(x => x.IndexId).ToList();
            if (clientRelationsIds.Any())
            {
                _relsIds.AddRange(clientIdsWithRelations.Select(x => x.IndexId));
                _relsIds.AddRange(clientIdsWithRelations.SelectMany(x => x.SecondaryIds));
            }

            if (_relsIds.Any())
            {
                clientNoRelationsIds = allClientsIds.Where(x => !_relsIds.Contains(x)).ToList();
            }
            #endregion

            #region CLIENTSONLY

            foreach (var id in clientNoRelationsIds)
            {
                var clientToDeleteDto = new ClientToDeleteDTO();
                n++;

                #region LoadClients

                Client client = null;
                try
                {
                    client = _clientReaderService.LoadClient(id);
                }
                catch (Exception e)
                {
                }

                #endregion

                #region SendClients

                if (null != client)
                {
                    var clientInfo = new SyncClientDTO(client);

                    clientInfo.PracticeCode = GetCode(clientInfo.PracticeId, practicses);

                    CurrentStatus = showPerc("Clients", n, totalCount);

                    bool status = false;
                    bool isCompleted = client.CanBeSynced();

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
                    }
                    else
                    {
                        completed = false;
                    }
                }
                #endregion

                #region DeleteClients
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

                    clientIdsDelete.Add(clientToDeleteDto);


                    foreach (var toDeleteDto in clientIdsDelete)
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
                #endregion
            }

            #endregion

            #region CLIENTRELATIONS

            foreach (var id in clientRelationsIds)
            {
                var clientToDeleteDto = new ClientToDeleteDTO();
                n++;

                Client client = null;
                List<Client> clientContacts = new List<Client>();
                int contactCount = 0;

                #region LoadClientWithRelations

                try
                {
                    client = _clientReaderService.LoadClient(id);
                    var contact = clientIdsWithRelations.FirstOrDefault(x => x.IndexId == id);
                    contactCount = contact.SecondaryIds.Count;
                    if (null != contact && client.CanBeSynced())
                    {
                        foreach (var secondaryId in contact.SecondaryIds)
                        {
                            var clientContact = _clientReaderService.LoadClientContact(secondaryId);
                            if (null != clientContact)
                                clientContacts.Add(clientContact);
                        }
                    }
                }
                catch (Exception e)
                {

                }

                #endregion

                #region SendClientWithRelations

                if (null != client)
                {
                    var clientInfo = new SyncClientDTO(client);

                    clientInfo.PracticeCode = GetCode(clientInfo.PracticeId, practicses);

                    CurrentStatus = showPerc("Clients", n, totalCount);

                    bool status = false;

                    try
                    {
                        if (client.CanBeSynced())
                        {
                            var contactsCannotBeSynced =
                                clientContacts.Any() && clientContacts.Any(x => !x.CanBeSynced());

                            if (!contactsCannotBeSynced)
                            {
                                status = await _clientSyncService.AttempSendClients(Address, clientInfo);
                                foreach (var clientContact in clientContacts)
                                {
                                    var clientConactInfo = new SyncClientDTO(clientContact);
                                    clientConactInfo.PracticeCode =
                                        GetCode(clientConactInfo.PracticeId, practicses);

                                    n++;
                                    CurrentStatus = showPerc("Clients", n, totalCount);
                                    status = await _clientSyncService.AttempSendClients(Address, clientConactInfo);
                                }
                            }
                            else
                            {
                                CurrentStatus = showPerc("Clients", n + contactCount, totalCount);
                            }
                        }
                        else
                        {
                            CurrentStatus = showPerc("Clients", n + contactCount, totalCount);
                        }
                    }
                    catch (Exception e)
                    {
                    }

                    if (status)
                    {
                        clientToDeleteDto = new ClientToDeleteDTO(client.Id, client.PersonId);
                    }
                    else
                    {
                        completedRelations = false;
                    }
                }

                #endregion

                #region DeleteSentClientWithRelations

                if (!clientToDeleteDto.NotSent)
                {
                    List<Encounter> encounters = new List<Encounter>();
                    List<Encounter> contactEncounters = new List<Encounter>();
                    try
                    {
                        encounters = _clientReaderService.LoadEncounters(id);

                        var contact = clientIdsWithRelations.FirstOrDefault(x => x.IndexId == id);
                        foreach (var sid in contact.SecondaryIds)
                        {
                            var cencounters = _clientReaderService.LoadEncounters(sid);
                            encounters.AddRange(cencounters);
                        }

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
                            completedRelations = false;
                        }

                    }

                    clientIdsDelete.Add(clientToDeleteDto);
                    foreach (var ct in clientContacts)
                    {
                        clientIdsDelete.Add(new ClientToDeleteDTO(ct.Id, ct.PersonId));
                    }



                    foreach (var toDeleteDto in clientIdsDelete)
                    {
                        //TODO: ALLOW DELETE []
                        if (deleteOnPush)
                        {
                            try
                            {
                                _clientReaderService.Purge(toDeleteDto);
                                _sentIds.Add(toDeleteDto.Id);
                            }
                            catch (Exception e)
                            {
                            }
                        }

                    }
                }

                #endregion
            }

            #endregion

            CurrentStatusTotal = $"Total Clients {totalCount}";
            CurrentStatusSent = $"Total Sent {clientIdsDelete.Count}";
            CurrentStatusFailed = $"Total Failed {totalCount - clientIdsDelete.Count}";
            var messageVerdict = $"Check and resolve any Incomplete records and review server logs";

            if (completed && completedRelations)
            {
                if (totalCount > clientIdsDelete.Count)
                    CurrentStatusSummary = messageVerdict;

                _dialogService.ShowToast("process completed");
            }
            else
            {
                CurrentStatusSummary = messageVerdict;
                _dialogService.ShowToast("process completed");
            }

            IsBusy = false;
        }

        private void InitSend()
        {
            IsBusy = true;
            CurrentStatusTotal = "";
            CurrentStatusSent = "";
            CurrentStatusFailed = "";
            CurrentStatusSummary = "";
            CurrentStatus = "";
            CurrentStatus = $"connecting...";

        }

        private async Task<Practice> GetPractice()
        {
            var practice = await _activationService.GetLocal(Address);
            if (null == practice)
            {
                IsBusy = false;
                CurrentStatus = $"connection failed !!";
                CurrentStatusSummary = "Could not connect to server";

                _dialogService.Alert("Could not connect to server");
                return null;
            }

            return practice;
        }

        public async Task<bool> CheckConnection()
        {
            try
            {
                var response = await _activationService.AttempCheckVersion(Address);

                if (string.IsNullOrWhiteSpace(response))
                {

                    _dialogService.Alert($"you are using an old version of LiveHAPI");
                    IsBusy = false;
                    CurrentStatus = $"send failed! updated LiveHAPI ";
                    return false;
                }

                int.TryParse(response, out var version);

                if (version != _hapiVersion)
                {
                    _dialogService.Alert($"you are using an old version of LiveHAPI");
                    IsBusy = false;
                    CurrentStatus = $"send failed! update LiveHAPI to v{_hapiVersion}";
                    return false;
                }

                var pracs = _practiceSetupService.GetAll();
                var presponse= await _activationService.AttemptEnrollPractice(Address, pracs);
            }
            catch (Exception e)
            {
                _dialogService.Alert($"connection Error {e.Message}");
                IsBusy = false;
                return false;
            }

            return true;
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

        private string showPerc(string message, int complete, int total)
        {
            int percentComplete = (int) Math.Round((double) (100 * complete) / total);

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
}
