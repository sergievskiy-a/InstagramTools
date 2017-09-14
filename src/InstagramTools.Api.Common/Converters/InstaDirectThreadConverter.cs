using System.Collections.Generic;
using InstagramTools.Api.Common.Helpers;
using InstagramTools.Api.Common.Models.Models;
using InstagramTools.Api.Common.Models.ResponseWrappers;

namespace InstagramTools.Api.Common.Converters
{
    public class InstaDirectThreadConverter : IObjectConverter<InstaDirectInboxThread, InstaDirectInboxThreadResponse>
    {
        public InstaDirectInboxThreadResponse SourceObject { get; set; }

        public InstaDirectInboxThread Convert()
        {
            var thread = new InstaDirectInboxThread();
            thread.Canonical = this.SourceObject.Canonical;
            thread.HasNewer = this.SourceObject.HasNewer;
            thread.HasOlder = this.SourceObject.HasOlder;
            thread.IsSpam = this.SourceObject.IsSpam;
            thread.Muted = this.SourceObject.Muted;
            thread.Named = this.SourceObject.Named;
            thread.Pending = this.SourceObject.Pending;
            thread.VieweId = this.SourceObject.VieweId;
            thread.LastActivity = DateTimeHelper.UnixTimestampMilisecondsToDateTime(this.SourceObject.LastActivity);
            thread.ThreadId = this.SourceObject.ThreadId;
            thread.OldestCursor = thread.OldestCursor;
            thread.ThreadType = this.SourceObject.ThreadType;
            thread.Title = this.SourceObject.Title;
            if (this.SourceObject.Inviter != null)
            {
                var userConverter = ConvertersFabric.GetUserConverter(this.SourceObject.Inviter);
                thread.Inviter = userConverter.Convert();
            }

            if (this.SourceObject.Items != null && this.SourceObject.Items.Count > 0)
            {
                thread.Items = new List<InstaDirectInboxItem>();
                foreach (var item in this.SourceObject.Items)
                {
                    var converter = ConvertersFabric.GetDirectThreadItemConverter(item);
                    thread.Items.Add(converter.Convert());
                }
            }

            if (this.SourceObject.Users != null && this.SourceObject.Users.Count > 0)
            {
                thread.Users = new InstaUserList();
                foreach (var user in this.SourceObject.Users)
                {
                    var converter = ConvertersFabric.GetUserConverter(user);
                    thread.Users.Add(converter.Convert());
                }
            }

            return thread;
        }
    }
}