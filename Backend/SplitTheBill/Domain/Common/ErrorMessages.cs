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
        public const string NotAFriend = "You are not friends";
    }

    public static class Group
    {
        public const string CreateRequestPrefix = "Create group request has validation errors";
        public const string EmptyName = "Group name should not be empty";
        public const string ReceiverIsNotFriend = "You can not invite a user who is not your friend";
        public const string NotAMember = "You are not a member of this group";
        public const string ReceiverIsAlreadyMember = "User is already a member of the group";
        public const string ReceiverHasAnInvitation = "User is already invited to join the group";
        public const string NotInvited = "You don't have an invitation to this group";
        public const string AlreadyMember = "You are already a member of this group";
        public const string NotAMemberMultiple = "One or more users are not members of the group";
    }

    public static class API
    {
        public const string TokenNotFound = "Token not found";
        public const string InvalidToken = "Invalid token";
    }

    public static class Finances
    {
        public const string CreateRequestPrefix = "Create entry request has validation errors";
        public const string NoExpenseLines = "No expense lines were provided";
        public const string LineWithNegativeAmount = "Expense lines can't contain negative amounts";
        public const string PayerIsDebtor = "Payer can't be a debtor";
        public const string InvalidUserFilter = "Entry expenses can't include yourself as other user";
        public const string GetExpensesRequestPrefix = "Get expenses request has validation errors";
    }
}