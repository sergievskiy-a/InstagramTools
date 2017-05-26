using Newtonsoft.Json;

namespace InstagramTools.Api.Common.Models.ResponseWrappers
{
    public class DeleteResponse : BaseResponse.BaseStatusResponse
    {
        [JsonProperty("did_delete")]
        public bool IsDeleted { get; set; }
    }
}
