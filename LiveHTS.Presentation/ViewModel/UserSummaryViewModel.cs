using System;
using System.Collections.Generic;
using Cheesebaron.MvxPlugins.Settings.Interfaces;
using LiveHTS.Core.Interfaces.Services.Access;
using LiveHTS.Core.Interfaces.Services.Config;
using LiveHTS.Core.Interfaces.Services.Sync;
using LiveHTS.Core.Model.Config;
using LiveHTS.Core.Model.Subject;
using LiveHTS.Presentation.Interfaces;
using LiveHTS.Presentation.Interfaces.ViewModel;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace LiveHTS.Presentation.ViewModel
{
    public class UserSummaryViewModel:MvxViewModel, IUserSummaryViewModel
    {
        private readonly ISettings _settings;
        private readonly IAuthService _authService;
        private readonly IUserSyncService _userSyncService;
        private readonly IDialogService _dialogService;
        private readonly IDeviceSetupService _deviceSetupService;
        private IEnumerable<UserSummary> _summaries;
        private IMvxCommand _refreshCommand;

        public UserSummaryViewModel(IDialogService dialogService, ISettings settings, IAuthService authService, IUserSyncService userSyncService, IDeviceSetupService deviceSetupService)
        {
            _dialogService = dialogService;
            _settings = settings;
            _authService = authService;
            _userSyncService = userSyncService;
            _deviceSetupService = deviceSetupService;
        }

        public IEnumerable<UserSummary> Summaries
        {
            get {return _summaries; }
            set { _summaries = value; RaisePropertyChanged(() => Summaries);}
        }

        public IMvxCommand RefreshCommand
        {
            get
            {
                _refreshCommand = _refreshCommand ?? new MvxCommand(Refresh);
                return _refreshCommand;
            }
        }
        public Guid AppUserId
        {
            get { return GetGuid("livehts.userid"); }
        }
        public Device Device { get; set; }
        public ServerConfig Local { get; set; }
        public string Address { get; set; }

        public void Init()
        {
            LoadInit();
        }

        public override void ViewAppeared()
        {
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
            Summaries = _authService.Get(AppUserId);
        }
        private async void Refresh()
        {
            _dialogService.ShowWait($"Downloading,Please wait...");
            var remoteData = await _userSyncService.DownloadSummary(Address, AppUserId);
            _dialogService.HideWait();
            if (null != remoteData)
            {
                var summaries = remoteData;
                _authService.SaveDownloaded(AppUserId,summaries);
                Summaries = _authService.Get(AppUserId);
            }
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