﻿using NetEscapades.Configuration.Validation;

namespace Identity.API.Startup.Settings
{
    public class AppSettings : IValidatable
    {
        public DbSettings DbSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }

        public void Validate()
        {
            DbSettings.Validate();
            IdentitySettings.Validate();
        }
    }
}
