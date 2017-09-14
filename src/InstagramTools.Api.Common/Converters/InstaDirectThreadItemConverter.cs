using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaDirectThreadItemConverter : IObjectConverter<InstaDirectInboxItem, InstaDirectInboxItemResponse>
    {
        public InstaDirectInboxItemResponse SourceObject { get; set; }

        public InstaDirectInboxItem Convert()
        {
            var threadItem = new InstaDirectInboxItem
            {
                ClientContext = this.SourceObject.ClientContext,
                ItemId = this.SourceObject.ItemId
            };
            switch (this.SourceObject.ItemType)
            {
                case "text":
                    threadItem.ItemType = InstaDirectThreadItemType.Text;
                    break;
                case "media_share":
                    threadItem.ItemType = InstaDirectThreadItemType.MediaShare;
                    break;
            }
            threadItem.Text = this.SourceObject.Text;
            threadItem.TimeStamp = DateTimeHelper.UnixTimestampMilisecondsToDateTime(this.SourceObject.TimeStamp);
            threadItem.UserId = this.SourceObject.UserId;
            if (this.SourceObject.MediaShare == null) return threadItem;
            var converter = ConvertersFabric.GetSingleMediaConverter(this.SourceObject.MediaShare);
            threadItem.MediaShare = converter.Convert();
            return threadItem;
        }
    }
}