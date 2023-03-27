using IniParser.Model;
using LiteDB;

namespace BtCS_Library.Modules.Static;

public static class StorageModule
{
    public static LiteDatabase GetStorage()
    {
        string connectionString = GetConnectionStringFromConfigurationFile();
        if (connectionString == null) throw new ArgumentNullException(nameof(connectionString));
        LogModule.WriteInformationMessageToLog($"Reading database from: {connectionString}");
        return new LiteDatabase(connectionString);
    }

    private static string GetConnectionStringFromConfigurationFile()
    {
        IniData data = ConfigurationModule.GetConfiguration();
        string filePath = Path.Combine(Environment.CurrentDirectory, data["Database"]["Filename"]);
        return $"Filename={filePath};Password={data["Database"]["Password"]}";
    }

    public static string GetStoragePath()
    {
        IniData data = ConfigurationModule.GetConfiguration();
        return Path.Combine(Environment.CurrentDirectory, data["Database"]["Filename"]);
    }
}