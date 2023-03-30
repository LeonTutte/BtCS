using BtCS_Library.Model.Storage;
using BtCS_Library.Modules.Helper.Storage;
using BtCS_Library.Modules.Static;
using Terminal.Gui;

namespace BtCS_TMUI;

public class LoginWindow : Window
{
    private readonly TextField usernameText;
    private string? _userName;
    private string? _userPassword;
    private int? _userId;

    public LoginWindow()
    {
        Title = "BtCS TMUI (Ctrl+Q to quit)";

        // Create input components and labels
        var usernameLabel = new Label
        {
            Text = "Username:"
        };

        usernameText = new TextField("")
        {
            // Position text field adjacent to the label
            X = Pos.Right(usernameLabel) + 1,

            // Fill remaining horizontal space
            Width = Dim.Fill()
        };

        var passwordLabel = new Label
        {
            Text = "Password:",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(usernameLabel) + 1
        };

        var passwordText = new TextField("")
        {
            Secret = true,
            // align with the text box above
            X = Pos.Left(usernameText),
            Y = Pos.Top(passwordLabel),
            Width = Dim.Fill()
        };

        var storageInfo = new Label
        {
            Text = $"Storage file: {StorageModule.GetStoragePath()}",
            X = Pos.Left(passwordLabel),
            Y = Pos.Bottom(passwordLabel) + 1
        };

        // Create login button
        var btnLogin = new Button
        {
            Text = "Login",
            Y = Pos.Bottom(storageInfo) + 1,
            // center the login button horizontally
            X = Pos.Center(),
            IsDefault = true
        };

        // Create exit button
        var btnExit = new Button
        {
            Text = "Exit",
            Y = Pos.Bottom(storageInfo) + 1,
            // center the exit button horizontally
            X = Pos.Right(btnLogin) + 2
        };

        // When login button is clicked display a message popup
        btnLogin.Clicked += () =>
        {
            _userName = usernameText.Text.ToString();
            _userPassword = passwordText.Text.ToString();
            
            if (string.IsNullOrWhiteSpace(_userName)) return;
            if (string.IsNullOrWhiteSpace(_userPassword)) return;
            if (!File.Exists(StorageModule.GetStoragePath()))
            {
                MessageBox.ErrorQuery("Missing file", "Couldn't find storage file", "Ok");
                var createStorage = MessageBox.Query("Message", $"Do you want to create a new storage?", "Yes", "No");
                if (createStorage == 0)
                {
                    StorageModule.GetStorage();
                    MessageBox.Query("Creating storage", $"Created the file at {StorageModule.GetStoragePath()}", "Ok");
                }
                else
                    MessageBox.Query("Aborted", "No file was created", "Ok");
                return;
            }
            if (AuthenticateOnStorage())
            {
                MessageBox.Query("Logging In", "Login Successful", "Ok");
                Application.RequestStop();
            }
            else
            {
                MessageBox.ErrorQuery("Logging In", "Incorrect username or password", "Ok");
                var createUser = MessageBox.Query("Message", $"Do you want to create the user {usernameText.Text}?", "Yes", "No");
                if (createUser == 0)
                {
                    var id  =  CreateUser();
                    MessageBox.Query("Creating user", $"Created the user {usernameText.Text} with Id {id}", "Ok");
                }
                else
                    MessageBox.Query("Aborted", "No user was created", "Ok");
            }
        };

        btnExit.Clicked += () => Application.RequestStop();

        // Add the views to the Window
        Add(usernameLabel, usernameText, passwordLabel, passwordText, storageInfo, btnLogin, btnExit);
    }
    
    private bool AuthenticateOnStorage()
    {
        UserDataHelper userDataHelper = new UserDataHelper(StorageModule.GetStorage());
        var passwordHash = CryptoModule.CreateSHA512(_userPassword);
        var UserExists = userDataHelper.CheckIfUserExists(_userName);
        if (!UserExists) return false;
        _userId = userDataHelper.GetUserIdByLabel(_userName);
        if (userDataHelper.GetRecordById((int)_userId).Password != passwordHash) return false;
        return true;
    }

    private int CreateUser()
    {
        UserDataHelper userDataHelper = new UserDataHelper(StorageModule.GetStorage());
        var passwordHash = CryptoModule.CreateSHA512(_userPassword);
        return userDataHelper.InsertRecord(new UserModel()
        {
            Label = _userName,
            Password = passwordHash
        });
    }
}