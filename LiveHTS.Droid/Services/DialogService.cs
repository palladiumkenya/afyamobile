﻿using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using LiveHTS.Presentation.Interfaces;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace LiveHTS.Droid.Services
{
    public class DialogService:IDialogService
    {
        private readonly IUserDialogs _userDialogs;

        public DialogService()
        {
            _userDialogs = Mvx.Resolve<IUserDialogs>();
        }

        public void Alert(string message, string title="LiveHTS", string okbtnText="Ok")
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetIcon(Resource.Drawable.Icon);
            adb.SetPositiveButton(okbtnText, (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }

        public void ConfirmExit()
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle("Exit");
            adb.SetMessage("Are you sure you want to Quit ?");
            adb.SetIcon(Resource.Drawable.Icon);
            adb.SetPositiveButton("Yes", (sender, args) => {act.FinishAffinity();});
            adb.SetNegativeButton("No", (sender, args) => { /* some logic */ });
            adb.Create().Show();
        }

        public async  Task<bool> ConfirmAction(string message,string title = "LiveHTS", string yesbtnText = "Yes", string nobtnText = "No")
        {
            var destroy = await _userDialogs.ConfirmAsync(new ConfirmConfig
            {
                Title =title,
                Message = message,
                OkText = yesbtnText,
                CancelText = nobtnText
            });
            return destroy;
        }
    }
}