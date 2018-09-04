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
        

        public Device Device { get; set; }
        public ServerConfig Local { get; set; }

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

        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; RaisePropertyChanged(() => CurrentStatus); }
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

                    if (version != 103)
                    {
                        _dialogService.Alert($"you are using an old version of LiveHAPI");
                        IsBusy = false;
                        CurrentStatus = $"send failed! updated LiveHAPI ";
                        return;
                    }

                    var pracs = _practiceSetupService.GetAll();
                    await _activationService.AttemptEnrollPractice(Address, pracs);
                }
                catch (Exception e)
                {
                    _dialogService.Alert($"connetion Error {e.Message}");
                    IsBusy = false;
                    return;
                }

               

                var ids = _clientReaderService.LoadClientIds();
                var count= ids.Count;
                int n = 0;
                
                var clientIdsDelete=new List<ClientToDeleteDTO>();
                var encountersDelete=new List<EnconterToDeleteDTO>();

                var practicses = _practiceSetupService.GetAll();

                foreach (var id in ids)
                {
                    var clientToDeleteDto = new ClientToDeleteDTO();
                    n++;
                    
                    var client = _clientReaderService.LoadClient(id);
                    
                    
                    if (null != client)
                    {
                        var clientInfo = new SyncClientDTO(client);

                        clientInfo.PracticeCode = GetCode(clientInfo.PracticeId, practicses);

                        CurrentStatus = showPerc("Clients", n, count);

                        var status= await _clientSyncService.AttempSendClients(Address, clientInfo);
                        if (status)
                        {
                            clientToDeleteDto = new ClientToDeleteDTO(client.Id, client.PersonId);
                        }
                        else
                        {
                            completed = false;
                        }
                    }

                    if (!clientToDeleteDto.NotSent)
                    {
                        var encounters = _clientReaderService.LoadEncounters(id);
                        if (null != encounters && encounters.Count > 0)
                        {
                            var syncEncounters = SyncClientEncounterDTO.Create(encounters, client);
                            var status=await _clientSyncService.AttempSendClientEncounters(Address, syncEncounters);
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

                        var shrs = _clientReaderService.LoadPSmartStores(id);

                        if (null != shrs && shrs.Count > 0)
                        {
                            await _clientSyncService.SendClientShrs(Address, shrs);
                        }

                        clientIdsDelete.Add(clientToDeleteDto);
                        foreach (var toDeleteDto in clientIdsDelete)
                        {
                            //TODO: ALLOW DELETE [DONE]
                            _clientReaderService.Purge(toDeleteDto);
                        }
                    }
                }
                
                //  send
                
            
                
                CurrentStatus = $"done! sent {clientIdsDelete.Count} of {count} ";

                if (completed)
                {
                    _dialogService.ShowToast("completed successfully");
                }
                else
                {
                    CurrentStatus = $"{CurrentStatus} with some Errors, Please check server logs";
                    _dialogService.Alert("send completed with errors");
                }
            }
            else
            {
                IsBusy = false;
                CurrentStatus = $"connection failed !!";

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
    }
}