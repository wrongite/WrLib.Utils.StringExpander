namespace WrLib.Utils.StringExpander.Core
{
    public interface IStringExpander
    {
        string Resolve(string keyName);
        string Expand(string s);
    }
}
