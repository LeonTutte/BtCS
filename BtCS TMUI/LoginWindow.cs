using Terminal.Gui;

namespace BtCS_TMUI;

public class LoginWindow : Window
{
    private TextField usernameText;

    public LoginWindow()
    {
        Title = "BtCs TMUI (Ctrl+Q to quit)";

        // Create input components and labels
        var usernameLabel = new Label () { 
            Text = "Username:" 
        };

        usernameText = new TextField ("") {
            // Position text field adjacent to the label
            X = Pos.Right (usernameLabel) + 1,

            // Fill remaining horizontal space
            Width = Dim.Fill (),
        };

        var passwordLabel = new Label () {
            Text = "Password:",
            X = Pos.Left (usernameLabel),
            Y = Pos.Bottom (usernameLabel) + 1
        };

        var passwordText = new TextField ("") {
            Secret = true,
            // align with the text box above
            X = Pos.Left (usernameText),
            Y = Pos.Top (passwordLabel),
            Width = Dim.Fill (),
        };

        // Create login button
        var btnLogin = new Button () {
            Text = "Login",
            Y = Pos.Bottom(passwordLabel) + 1,
            // center the login button horizontally
            X = Pos.Center (),
            IsDefault = true,
        };

        // When login button is clicked display a message popup
        btnLogin.Clicked += () => {
            if (usernameText.Text == "admin" && passwordText.Text == "password") {
                MessageBox.Query ("Logging In", "Login Successful", "Ok");
                Application.RequestStop ();
            } else {
                MessageBox.ErrorQuery ("Logging In", "Incorrect username or password", "Ok");
                var createUser = MessageBox.Query ("Message", $"Do you want to create the user {usernameText.Text}?", "Yes", "No");
                if (createUser == 0)
                {
                    MessageBox.Query ("Creating user", $"Created the user {usernameText.Text}", "Ok");
                }
                else
                {
                    MessageBox.Query ("Aborted", "No user was created", "Ok");
                }
            }
        };

        // Add the views to the Window
        Add (usernameLabel, usernameText, passwordLabel, passwordText, btnLogin);
    }
}