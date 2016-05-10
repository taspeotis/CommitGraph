using System.ComponentModel.Composition;
using System.Configuration;
using CommitGraph.Attributes;
using CommitGraph.Infrastructure;
using CommitGraph.Interfaces;
using HockeyApp;

[assembly: EagerInstantiation(typeof(ITelemetryService))]

namespace CommitGraph.Services
{
    [Export(typeof(ITelemetryService))]
    internal sealed class TelemetryService : ITelemetryService
    {
        private const string HockeyAppId = "1f7ebd6596344dd6b3ee415fae42478a";

        private static readonly TelemetryServiceSettings Settings = new TelemetryServiceSettings();

        public TelemetryService()
        {
            if (Enabled)
                EnableTelemetry();
        }

        public string EmailAddress
        {
            get { return Settings.EmailAddress; }
            set { Settings.EmailAddress = value; }
        }

        public bool Enabled
        {
            get { return Settings.Enabled; }
            set { Settings.Enabled = value; }
        }

        public string UserName
        {
            get { return Settings.UserName; }
            set { Settings.UserName = value; }
        }

        private async void EnableTelemetry()
        {
            var hockeyClient = HockeyClient.Current;

            hockeyClient.Configure(HockeyAppId);

            try
            {
                hockeyClient.UpdateContactInfo(UserName, EmailAddress);

                await hockeyClient.SendCrashesAsync();

                // It's not clear that "shutdownActions" is ever called...
                // https://github.com/bitstadium/HockeySDK-Windows/issues/39
                await hockeyClient.CheckForUpdatesAsync(true, () => true);
            }
            catch
            {
                // https://github.com/bitstadium/HockeySDK-Windows/issues/9
            }
        }

        private sealed class TelemetryServiceSettings : Settings
        {
            [DefaultSettingValue("True"), UserScopedSetting]
            public bool Enabled
            {
                get { return Get<bool>(nameof(Enabled)); }
                set { SetImmediate(nameof(Enabled), value); }
            }

            [UserScopedSetting]
            public string EmailAddress
            {
                get { return Get<string>(nameof(EmailAddress)); }
                set { SetImmediate(nameof(EmailAddress), value); }
            }

            [UserScopedSetting]
            public string UserName
            {
                get { return Get<string>(nameof(UserName)); }
                set { SetImmediate(nameof(UserName), value); }
            }
        }
    }
}