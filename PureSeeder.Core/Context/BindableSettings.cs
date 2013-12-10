﻿using System;
using System.ComponentModel;
using PureSeeder.Core.Settings;

namespace PureSeeder.Core.Context
{
    public class BindableSettings : BindableBase
    {
        private readonly SeederUserSettings _settings;

        public BindableSettings(SeederUserSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("settings");
            //settings.Reset();
            //settings.Upgrade();
            _settings = settings;
            DirtySettings = false;
            this.PropertyChanged += SettingChanged;
            Servers.ServerChanged += SettingChanged;
        }

        private void SettingChanged(object sender, EventArgs e)
        {
            var propertyChangedEventArgs = e as PropertyChangedEventArgs;
            if (propertyChangedEventArgs != null)
            {
                if (propertyChangedEventArgs.PropertyName == "DirtySettings")
                    return;
            }

            //_settings.Save();
            DirtySettings = true;
        }

        public bool SaveSettings()
        {
            _settings.Save();
            DirtySettings = false;
            return true;
        }

        private bool _dirtySettings;
        public bool DirtySettings
        {
            get { return _dirtySettings; }
            set { SetField(ref _dirtySettings, value); }
        }


        public string Username
        {
            get { return _settings.Username; }
            set { SetProperty(_settings, value, x => x.Username); }
        }

        public bool EnableGameHangProtection
        {
            get { return _settings.EnableGameHangProtection; }
            set { SetProperty(_settings, value, x => x.EnableGameHangProtection); }
        }

        public bool EnableLogging
        {
            get { return _settings.EnableLogging; }
            set { SetProperty(_settings, value, x => x.EnableLogging); }
        }

        public Servers Servers
        {
            get { return _settings.Servers; }
            set { SetProperty(_settings, value, x => x.Servers); }
        }

        public int CurrentServer
        {
            get { return _settings.CurrentServer; }
            set { SetProperty(_settings, value, x => x.CurrentServer); }
        }
    }
}