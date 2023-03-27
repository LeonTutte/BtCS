using IniParser;
using IniParser.Model;

namespace BtCS_Library.Modules.Static;

public static class ConfigurationModule
{
    public static IniData GetConfiguration()
    {
        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile("settings.ini");
        return data;
    }
}