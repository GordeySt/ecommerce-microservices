namespace Identity.Application.Common
{
    public static class UnauthorizedExceptionMessageConstants
    {
        public const string InvalidEmailMessage = "Invalid email";
        public const string EmailNotConfirmedMessage = "Email not confirmed";
        public const string InvalidPasswordMessage = "Invalid password";
    }

    public static class NotFoundExceptionMessageConstants
    {
        public const string NotFoundRoleMessage = "Role not found";
        public const string NotFoundUserMessage = "User not found";
    }

    public static class BadRequestExceptionMessageConstants
    {
        public const string ProblemVerifyingEmailMessage = "Problem verifying email address";
        public const string ProblemResetingPasswordMessage = "Problem reseting password";
        public const string UserIsNotInRoleMessage = "User is not in role";
        public const string UserIsInRoleMessage = "User is already in this role";
        public const string RoleAlreadyExistsMessage = "This role already exists";
    }

    public static class ExceptionMessageConstants
    {
        public const string ExistedEmailMessage = "Email already exists";
        public const string InvalidTokenMessage = "Invalid token was recieved";
    }
}
