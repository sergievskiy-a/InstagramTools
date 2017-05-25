using Newtonsoft.Json;

namespace InstagramTools.Api.Classes.ResponseWrappers
{
    class DeleteResponse : BaseResponse.BaseStatusResponse
    {
        [JsonProperty("did_delete")]
        public bool IsDeleted { get; set; }
    }
}
