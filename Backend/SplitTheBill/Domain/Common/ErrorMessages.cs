namespace Domain.Common;

public static class ErrorMessages
{
    public static class User
    {
        public const string IncorrectEmailOrPassword = "Email or password is incorrect";
        public const string InvalidEmail = "Invalid email address";
        public const string PasswordMismatch = "Passwords do not match";
        public const string NotFound = "User not found";
        public const string GetListRequestPrefix = "Get user list request has validation errors";
        public const string UpdateRequestPrefix = "Update user profile request has validation errors";
        public const string EmailAlreadyExists = "Profile with this email already exists";
        public const string UsernameAlreadyExists = "Profile with this username already exists";
        public const string RegistrationRequestPrefix = "Registration request has validation errors";

        public static string MinimumUsernameLength(int minimumLength) => $"Username has to contain at least {minimumLength} characters";
        public static string MinimumPasswordLength(int minimumLength) => $"Password has to contain at least {minimumLength} characters";
        public static string MinimumSearchLength(int minimumLength) => $"Search value has to contain at least {minimumLength} characters";
    }

    public static class Friends
    {
        public const string AlreadyFriends = "You are already friends";
        public const string RequestSent = "Friend request is already sent";
        public const string RequestReceived = "You have already received an invitation from this user";
        public const string RequestDoesNotExist = "Friend request does not exist";
    }

    public static class Group
    {
        public const string CreateRequestPrefix = "Create group request has validation errors";
        public const string EmptyName = "Group name should not be empty";
    }

    public static class API
    {
        public const string TokenNotFound = "Token not found";
        public const string InvalidToken = "Invalid token";
    }
}