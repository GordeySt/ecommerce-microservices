namespace Identity.Application.Common
{
    public static class NotFoundExceptionMessageConstants
    {
        public const string NotFoundItemMessage = "Item not found";
        public const string NotFoundRoleMessage = "Role not found";
        public const string NotFoundUserMessage = "User not found";
    }

    public static class BadRequestExceptionMessageConstants
    {
        public const string ProblemVerifyingEmailMessage = "Problem verifying email address";
        public const string UserIsNotInRoleMessage = "User is not in role";
        public const string UserIsInRoleMessage = "User is already in this role";
        public const string RoleAlreadyExistsMessage = "This role already exists";
    }

    public static class ExceptionMessageConstants
    {
        public const string ExistedEmailMessage = "Email already exists";
        public const string InvalidTokenMessage = "Invalid token was recieved";
        public const string ProblemDeletingItemMessage = "Problem deleting item";
    }
}
