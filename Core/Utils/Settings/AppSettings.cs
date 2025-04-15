using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utils.Settings
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public OverPassApiSettings OverPassApiSettings { get; set; }
        public CountrySettings CountrySettings { get; set; }
        public CampagneFolderSettings CampagneFolderSettings { get; set; }
        public GooglePlacesSettings GooglePlacesSettings { get; set; }
        public string PwdCryptKey { get; set; }
    }
}
