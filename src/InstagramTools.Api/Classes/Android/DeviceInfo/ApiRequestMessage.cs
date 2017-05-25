﻿using System;
using InstagramTools.Api.API;
using InstagramTools.Api.Helpers;
using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.Android.DeviceInfo
{
    public class ApiRequestMessage
    {
        public string phone_id { get; set; }
        public string username { get; set; }
        public Guid guid { get; set; }
        public string device_id { get; set; }
        public string password { get; set; }
        public string login_attempt_count { get; set; } = "0";

        internal string GetMessageString()
        {
            return JsonConvert.SerializeObject(this);
        }

        internal string GenerateSignature()
        {
            return CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                JsonConvert.SerializeObject(this));
        }

        internal bool IsEmpty()
        {
            if (string.IsNullOrEmpty(phone_id)) return true;
            if (string.IsNullOrEmpty(device_id)) return true;
            if (Guid.Empty == guid) return true;
            return false;
        }

        internal static string GenerateDeviceId()
        {
            var hashedGuid = CryptoHelper.CalculateMd5(Guid.NewGuid().ToString());
            return $"android-{hashedGuid.Substring(0, 16)}";
        }

        internal static string GenerateUploadId()
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            var uploadId = (long) timeSpan.TotalSeconds;
            return uploadId.ToString();
        }
    }
}