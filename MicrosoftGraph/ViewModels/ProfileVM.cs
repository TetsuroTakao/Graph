﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MicrosoftGraph
{
    public class Content:INotifyPropertyChanged
    {
        public Content()
        {
            ContentType = ContentTypes.Profile;
            var except = new string[] { "Undefined", "Goo", "Rakten", "Blogger", "Mixi", "Ameba", "Tumbler", "Reddit", "Yahoo", "Pintarest", "OWAccount" };
            ProviderBase tempprovider = null;
            string providerName = string.Empty;
            foreach (ProviderBase.ProviderTypes s in Enum.GetValues(typeof(ProviderBase.ProviderTypes)))
            {
                if (except.Where(i => i == s.ToString()).Count() > 0) continue;
                switch (s)
                {
                    case ProviderBase.ProviderTypes.FaceBook:
                        tempprovider = new Facebook();
                        break;
                    case ProviderBase.ProviderTypes.MicrosoftGraph:
                        tempprovider = new MSGraph();
                        break;
                    default:
                        tempprovider = new Facebook();
                        break;
                }
                providerName = Enum.GetName(typeof(ProviderBase.ProviderTypes), s);
                tempprovider.Caption = providerName;
                tempprovider.SetImage(providerName);
                ProviderSelectionList.Add(tempprovider);
            }
            PropertyChanged += Content_PropertyChanged;
        }

        private void Content_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, e);
        }

        public List<ProviderBase> ProviderSelectionList = new List<ProviderBase>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ContentTypes ContentType { get; set; }
        public ProviderBase CurrentProvider { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string ID { get; set; }
    }
}
