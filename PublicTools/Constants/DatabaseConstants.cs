namespace PublicTools.Constants;

public static class DatabaseConstants
{
    public static class Schemas
    {
        public const string Sale = "Sale";
        public const string UserManagement = "UserManagement";
    }

    public static class DefaultRoles
    {
        public const string GodAdminId = "1";
        public const string GodAdminName = "GodAdmin";
        public const string GodAdminNormalizedName = "GODADMIN";
        public const string GodAdminConcurrencyStamp = "1";

        public const string AdminId = "2";
        public const string AdminName = "Admin";
        public const string AdminNormalizedName = "ADMIN";
        public const string AdminConcurrencyStamp = "2";
    }

    public static class GodAdminUsers
    {
        public const string ShahbaziUserId = "1";
        public const string ShahbaziFirstName = "Amir";
        public const string ShahbaziLastName = "Shahbazi";
        public const string ShahbaziNationalId = "0440000000";
        public const string ShahbaziUserName = "a.Shahbazi";
        public const string ShahbaziPassword = "Amir12345";
        public const string ShahbaziCellPhone = "09120000000";
    }
}
