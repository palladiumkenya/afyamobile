using System;
using System.Collections.Generic;
using System.Linq;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Clients;
using LiveHTS.Core.Model;
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
    public class SmartCardViewModel : MvxViewModel, ISmartCardViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private readonly IRegistryService _registryService;

        private IMvxCommand _readCardCommand;
        private IMvxCommand _writeCardCommand;
        private IMvxCommand _testingCommand;
        private SmartClientDTO _smartClient;
        private List<HIVTestHistoryDTO> _hivTestHistories=new List<HIVTestHistoryDTO>();
        private List<string> _shrErrors;
        private Exception _shrException;
        private string _shrMessage;

        public Guid AppUserId
        {
            get { return GetGuid("livehts.userid"); }
        }

        public Guid AppProviderId
        {
            get { return GetGuid("livehts.providerid"); }
        }

        public Guid AppPracticeId
        {
            get { return GetGuid("livehts.practiceid"); }
        }

        public Guid AppDeviceId
        {
            get { return GetGuid("livehts.deviceid"); }
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

        

        public SmartCardViewModel(IDialogService dialogService, ISettings settings, IRegistryService registryService)
        {
            _dialogService = dialogService;
            _settings = settings;
            _registryService = registryService;
        }

        private bool CanReadCard()
        {
            return true;
        }

        private bool CanWriteCard()
        {
            return false;
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
            throw new System.NotImplementedException();
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