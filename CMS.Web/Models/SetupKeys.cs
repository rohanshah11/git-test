using CMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Models
{
    public class SetupKeys
    {
        public static List<string> getKeys()
        {
            return new List<string>()
            {
                getOrganisationNameKey,
                getPhoneNumberKey,
                getPanNumberKey,
                getAddressKey,
                getEmailKey,
                getEmailPasswordKey,
                getEmailHostKey,
                getEmailPortKey,
                getFacebookKey,
                getInstaKey,
                getTwitterKey,
                getCompanyShortDescriptionKey,
                getLocationKey,
                getYoutubeKey,
                getOpeningHours,
                getDays,
                getHours,
                getQrCodeKey,
            };
        }
        public static string getOrganisationNameKey { get; } = "Organization Name";
        public static string getQrCodeKey { get; } = "QRCODE";
        public static string getPhoneNumberKey { get; } = "Phone Number";
        public static string getPanNumberKey { get; } = "PAN Number";
        public static string getAddressKey { get; } = "Address";
        public static string getEmailKey { get; } = "Email Address";
        public static string getEmailPasswordKey { get; } = "Email Password";
        public static string getEmailHostKey { get; } = "Email Host";
        public static string getEmailPortKey { get; } = "Email Port";
        public static string getFacebookKey { get; } = "Facebook Url";
        public static string getInstaKey { get; } = "Instagram Url";
        public static string getTwitterKey { get; } = "Twitter Url";
        public static string getCompanyShortDescriptionKey { get; } = "Company Description";
        public static string getLocationKey { get; } = "Location";
        public static string getYoutubeKey { get; } = "Youtube Url";

        public static string getOpeningHours { get; } = "Opening Hours";
        public static string getDays { get; } = "Days";
        public static string getHours { get; } = "Hours";



    }
}
