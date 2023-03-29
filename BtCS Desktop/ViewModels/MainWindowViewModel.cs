using System;
using System.IO;
using System.Threading.Tasks;
using BtCS_Library.Modules.Helper.Storage;
using BtCS_Library.Modules.Static;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiteDB;

namespace BtCS_Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _userName;
    [ObservableProperty]
    private string? _userPassword;
    [ObservableProperty]
    private int? _userId;

    private LiteDatabase? _storage;

    [RelayCommand]
    async Task Login()
    {
        if (string.IsNullOrWhiteSpace(UserName)) return;
        if (string.IsNullOrWhiteSpace(UserPassword)) return;

        _storage = StorageModule.GetStorage();
        if (!File.Exists(StorageModule.GetStoragePath())) return;
        if (!AuthenticateOnStorage()) return;
        // TODO: Add routing to next page
    }
    
    [RelayCommand]
    void Exit()
    {
        Environment.Exit(0);
    }

    private bool AuthenticateOnStorage()
    {
        UserDataHelper userDataHelper = new UserDataHelper(_storage);
        var passwordHash = CryptoModule.CreateSHA512(UserPassword);
        var UserExists = userDataHelper.CheckIfUserExists(UserName);
        if (!UserExists)
        {
            // TODO: Prompt for account creation
            return false;
        }
        UserId = userDataHelper.GetUserIdByLabel(UserName);
        if (userDataHelper.GetRecordById((int)UserId).Password != passwordHash) return false;
        // TODO: Implement failure prompt
        return true;
    }
}