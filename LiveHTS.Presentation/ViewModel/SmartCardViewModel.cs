using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Interfaces.Services.SmartCard;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.Interview;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.DTO;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using LiveHTS.SharedKernel.Custom;
using LiveHTS.SharedKernel.Model;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class SmartCardViewModel : MvxViewModel, ISmartCardViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly IClientShrRecordService _clientShrRecordService;
        private readonly ISettings _settings;
        private readonly IRegistryService _registryService;
        private readonly IEncounterService _encounterService;
        private readonly IDashboardService _dashboardService;

        private IMvxCommand _readCardCommand;
        private IMvxCommand _writeCardCommand;
        private IMvxCommand _testingCommand;
        private SmartClientDTO _smartClient;
        private List<HIVTestHistoryDTO> _hivTestHistories=new List<HIVTestHistoryDTO>();
        private List<string> _shrErrors;
        private Exception _shrException;
        private string _shrMessage;
        private Client _clientShr;
        private Encounter _encounterShr;
        private ClientShrRecord _clientShrRecord;
        private bool _showTesting;
        private bool _showReadCard;

        public Guid AppUserId
        {
            get { return GetGuid("livehts.userid"); }
        }
        public Guid AppPracticeId
        {
            get { return GetGuid("livehts.practiceid"); }
        }
        public string PracticeCode
        {
            get { return _settings.GetValue("livehts.practicecode", ""); }
        }

        public ClientShrRecord ClientShrRecord
        {
            get => _clientShrRecord;
            set => _clientShrRecord = value;
        }

        public Client ClientShr
        {
            get => _clientShr;
            set => _clientShr = value;
        }

        public Encounter EncounterShr
        {
            get => _encounterShr;
            set => _encounterShr = value;
        }

        public string ShrMessage
        {
            get => _shrMessage;
            set
            {
                _shrMessage = value;
                RaisePropertyChanged(() => ShrMessage);
            }
        }

        public List<string> ShrErrors
        {
            get => _shrErrors;
            set
            {
                _shrErrors = value;
                RaisePropertyChanged(() => ShrErrors);
            }
        }

        public Exception ShrException
        {
            get => _shrException;
            set
            {
                _shrException = value;
                RaisePropertyChanged(() => ShrException);
            }
        }

        public SHR Shr { get; set; }

        public SmartClientDTO SmartClient
        {
            get { return _smartClient; }
            set
            {
                _smartClient = value;
                RaisePropertyChanged(() => SmartClient);
            }
        }

        public List<HIVTestHistoryDTO> HivTestHistories
        {
            get { return _hivTestHistories; }
            set
            {
                _hivTestHistories = value;
                RaisePropertyChanged(() => HivTestHistories);
            }
        }

        public bool ShowTesting
        {
            get { return _showTesting; }
            set
            {
                _showTesting = value;
                RaisePropertyChanged(() => ShowTesting);
            }
        }

        public bool ShowReadCard
        {
            get { return _showReadCard; }
            set
            {
                _showReadCard = value;
                RaisePropertyChanged(() => ShowReadCard);
            }
        }

        public IMvxCommand ReadCardCommand
        {
            get
            {
                _readCardCommand = _readCardCommand ?? new MvxCommand(ReadCard, CanReadCard);
                return _readCardCommand;
            }
        }

        public Action ReadCardAction { get; set; }


        public IMvxCommand WriteCardCommand
        {
            get
            {
                _writeCardCommand = _writeCardCommand ?? new MvxCommand(WriteCard, CanWriteCard);
                return _writeCardCommand;
            }
        }
      


        public IMvxCommand TestingCommand
        {
            get
            {
                _testingCommand = _testingCommand ?? new MvxCommand(Testing, CanTesting);
                return _testingCommand;
            }
        }

        public SmartCardViewModel(IDialogService dialogService, ISettings settings, IRegistryService registryService, IEncounterService encounterService, IDashboardService dashboardService, IClientShrRecordService clientShrRecordService)
        {
            _dialogService = dialogService;
            _settings = settings;
            _registryService = registryService;
            _encounterService = encounterService;
            _dashboardService = dashboardService;
            _clientShrRecordService = clientShrRecordService;
        }

        public void Init(string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                ShowTesting = ShowReadCard = false;
                //prepare SHR
                _settings.AddOrUpdateValue("shrmode", "write");
                ClientShr = _dashboardService.LoadClient(new Guid(id));
                ClientShrRecord = _clientShrRecordService.GetByClientId(new Guid(id));

                if (null != ClientShr)
                {
                    _settings.AddOrUpdateValue("ClientShr", JsonConvert.SerializeObject(ClientShr));
                    var cstate= ClientShr.ClientStates.FirstOrDefault(x =>
                        x.Status == LiveState.HtsTestedPos || x.Status == LiveState.HtsTestedNeg ||
                        x.Status == LiveState.HtsTestedInc);
                    if (null != cstate&&cstate.EncounterId.HasValue)
                    {
                        EncounterShr = _encounterService.LoadTesting(cstate.EncounterId.Value);
                    }

                    if(null!=EncounterShr)
                        _settings.AddOrUpdateValue("EncounterShr", JsonConvert.SerializeObject(EncounterShr));
                }

                if (null != ClientShrRecord)
                {
                    _settings.AddOrUpdateValue("ClientShrRecord", JsonConvert.SerializeObject(ClientShrRecord));
                }

            }
            else
            {
                _settings.AddOrUpdateValue("shrmode", "read");
                ShowTesting = ShowReadCard = true;
            }
        }

        public override void ViewAppeared()
        {
            var shrMode = _settings.GetValue("shrmode", "");

            if (!string.IsNullOrWhiteSpace(shrMode))
            {
                if (shrMode == "write")
                {
                    ShowTesting = ShowReadCard = false;
                    var clientShrJson = _settings.GetValue("ClientShr", "");
                    var encounterShrJson = _settings.GetValue("EncounterShr", "");
                    var clientShrRecordJson = _settings.GetValue("ClientShrRecord", "");

                    if (null == ClientShr&&!string.IsNullOrWhiteSpace(clientShrJson))
                    {
                        ClientShr = JsonConvert.DeserializeObject<Client>(clientShrJson);
                    }

                    if (null == EncounterShr && !string.IsNullOrWhiteSpace(encounterShrJson))
                    {
                        EncounterShr = JsonConvert.DeserializeObject<Encounter>(encounterShrJson);
                    }

                    if (null == ClientShrRecord && !string.IsNullOrWhiteSpace(clientShrRecordJson))
                    {
                        ClientShrRecord = JsonConvert.DeserializeObject<ClientShrRecord>(clientShrRecordJson);
                    }

                    PrepareShr();
                }
                else
                {
                    ShowTesting = ShowReadCard = true;
                }


            }
        }

        private void PrepareShr()
        {
            if (null != ClientShrRecord)
            {
                try
                {
                    Shr = JsonConvert.DeserializeObject<SHR>(ClientShrRecord.Shr);
                    Shr.UpdateFrom(ClientShr, PracticeCode);
                    if (null != EncounterShr)
                    {
                        var test = EncounterShr.ObsFinalTestResults.FirstOrDefault();
                        if (null != test)
                        {
                            if (test.FinalResult.HasValue)
                            {
                                Shr.UpdateTesting(EncounterShr.EncounterDate,test,PracticeCode);
                            }
                        }
                    }
                    SmartClient = SmartClientDTO.Create(Shr);
                    HivTestHistories = HIVTestHistoryDTO.Create(Shr);
                }
                catch (Exception e)
                {
                    
                }
            }
        }
        private bool CanReadCard()
        {
            return true;
        }

        private bool CanWriteCard()
        {
            return !ShowReadCard;
        }
        private bool CanTesting()
        {
            if (null == SmartClient)
                return false;

            if (HivTestHistories.Count == 0)
                return true;

            if (HivTestHistories.Count > 0)
                return !HivTestHistories.Any(x => x.Result.IsSameAs("POSITIVE"));

            return false;
        }
        private void ReadCard()
        {
            ReadCardAction?.Invoke();
        }
        private void WriteCard()
        {
            //B262F4EE-852F-11E7-BB31-BE2E44B06B34 e
            //B25EC568-852F-11E7-BB31-BE2E44B06B34 f
            //var testingEncounter =
        }

        private async void Testing()
        {
            if (null == Shr)
            {
                var shrJson = _settings.GetValue("shr", "");
                if (!string.IsNullOrWhiteSpace(shrJson))
                {
                    Shr = JsonConvert.DeserializeObject<SHR>(shrJson);
                }
            }

            if (null == Shr)
                _dialogService.Alert($"{ShrException.Message}", "colud not find any SHR data", "OK");

            var client = Shr.GetClient(AppPracticeId, AppUserId);
            try
            {

                var id = await _registryService.SaveShr(client);
                var shrJson = _settings.GetValue("shr", "");
                _clientShrRecordService.SaveOrUpdate(new ClientShrRecord(id, shrJson));

                if (!id.IsNullOrEmpty())
                    ShowViewModel<DashboardViewModel>(new { id = id.ToString() });
            }
            catch (Exception e)
            {
                _dialogService.Alert($"{e.Message}", "Error saving SHR to phone", "OK");
            }
            
           
        }

        public void ReadCardDone()
        {
            _settings.AddOrUpdateValue("shr", "");

            if (!string.IsNullOrWhiteSpace(ShrMessage))
            {
                try
                {
                    Shr = JsonConvert.DeserializeObject<SHR>(ShrMessage);
                    if (null==Shr)
                        throw new Exception("invalid SHR");

                    SmartClient=SmartClientDTO.Create(Shr);
                    HivTestHistories = HIVTestHistoryDTO.Create(Shr);

                    _settings.AddOrUpdateValue("shr", JsonConvert.SerializeObject(Shr));

                    _dialogService.ShowToast("Read successfully");
                }
                catch (Exception e)
                {
                    ShrException = e;
                }
            }
            if (null != ShrException)
            {
                _dialogService.Alert($"{ShrException.Message}", "Read Card Failed", "OK");
            }

            TestingCommand.RaiseCanExecuteChanged();
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