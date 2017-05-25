using InstagramTools.Api.Classes.Models;
using InstagramTools.Api.Classes.ResponseWrappers;

namespace InstagramTools.Api.Converters
{
    internal class InstaRecipientsConverter : IObjectConverter<InstaRecipients, InstaRecipientsResponse>
    {
        public InstaRecipientsResponse SourceObject { get; set; }

        public InstaRecipients Convert()
        {
            var recipients = new InstaRecipients();

            return recipients;
        }
    }
}