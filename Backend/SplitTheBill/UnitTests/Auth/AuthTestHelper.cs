using Application.Authorization.Registration;

namespace UnitTests.Auth;

public static class AuthTestHelper
{
    public static void FillDataWithSamePassword(this RegisterCommand @this)
    {
        @this.Email = "email@email.com";
        @this.Password = "password";
        @this.RepeatPassword = @this.Password;
    }

    public static UserSettings UserSettings => new() { MinPasswordLength = 6 };
}