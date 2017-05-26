using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaRecipientsConverter : IObjectConverter<InstaRecipients, InstaRecipientsResponse>
    {
        public InstaRecipientsResponse SourceObject { get; set; }

        public InstaRecipients Convert()
        {
            var recipients = new InstaRecipients();

            return recipients;
        }
    }
}