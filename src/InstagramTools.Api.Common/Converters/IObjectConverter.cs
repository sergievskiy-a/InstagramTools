namespace InstagramTools.Api.Common.Converters
{
    public interface IObjectConverter<T, TT>
    {
        TT SourceObject { get; set; }
        T Convert();
    }
}