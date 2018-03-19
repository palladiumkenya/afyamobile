using System;
using System.Collections.Generic;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Model;
using LiveHTS.Core.Model.SmartCard;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class SmartCardViewModel : MvxViewModel, ISmartCardViewModel
    {
        private readonly IDialogService _dialogService;
        private readonly ISettings _settings;
        private IMvxCommand _readCardCommand;
        private IMvxCommand _writeCardCommand;
        private IMvxCommand _testingCommand;
        private SmartClientDTO _smartClient;
        private List<HIVTestHistoryDTO> _hivTestHistories;
        private List<string> _shrErrors;
        private Exception _shrException;
        private string _shrMessage;

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

        

        public SmartCardViewModel(IDialogService dialogService, ISettings settings)
        {
            _dialogService = dialogService;
            _settings = settings;
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
        private void Testing()
        {
            throw new System.NotImplementedException();
        }

        public void ReadCardDone()
        {
            if (!string.IsNullOrWhiteSpace(ShrMessage))
            {
                try
                {
                    Shr = JsonConvert.DeserializeObject<SHR>(ShrMessage);
                    if (null==Shr)
                        throw new Exception("invalid SHR");

                    SmartClient=SmartClientDTO.Create(Shr);

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
        }
    }
}