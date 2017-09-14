using System;
using InstagramTools.Api.Common.Helpers;
using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.Android.DeviceInfo
{
    public class ApiRequestMessage
    {
        public string phone_id { get; set; }
        public string username { get; set; }
        public Guid guid { get; set; }
        public string device_id { get; set; }
        public string password { get; set; }
        public string login_attempt_count { get; set; } = "0";

        public string GetMessageString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string GenerateSignature()
        {
            return CryptoHelper.CalculateHash(InstaApiConstants.IG_SIGNATURE_KEY,
                JsonConvert.SerializeObject(this));
        }

        public bool IsEmpty()
        {
            if (string.IsNullOrEmpty(this.phone_id)) return true;
            if (string.IsNullOrEmpty(this.device_id)) return true;
            if (Guid.Empty == this.guid) return true;
            return false;
        }

        public static string GenerateDeviceId()
        {
            var hashedGuid = CryptoHelper.CalculateMd5(Guid.NewGuid().ToString());
            return $"android-{hashedGuid.Substring(0, 16)}";
        }

        public static string GenerateUploadId()
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            var uploadId = (long) timeSpan.TotalSeconds;
            return uploadId.ToString();
        }
    }
}