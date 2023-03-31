
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

    public static bool WriteNewPassword(string password)
    {
        var parser = new FileIniDataParser();
        IniData data = parser.ReadFile("settings.ini");
        data["Database"]["Password"] = password;
        try
        {
            parser.WriteFile("settings.ini", data);
            return true;
        }
        catch (Exception)
        {
            LogModule.WriteErrorMessageToLog("Configuration error: Couldn't write new password to ini file!");
            return false;
        }
    }
}