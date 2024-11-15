﻿namespace PublicTools.Constants;

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

        public const string SellerId = "3";
        public const string SellerName = "Seller";
        public const string SellerNormalizedName = "SELLER";
        public const string SellerConcurrencyStamp = "3";

        public const string BuyerId = "4";
        public const string BuyerName = "Buyer";
        public const string BuyerNormalizedName = "BUYER";
        public const string BuyerConcurrencyStamp = "4";
    }

    public static class GodAdminUsers
    {
        public const string ShahbaziUserId = "1";
        public const string ShahbaziFirstName = "Amir";
        public const string ShahbaziLastName = "Shahbazi";
        public const string ShahbaziNationalId = "0440000000";
        public const string ShahbaziUserName = "GodAdmin";
        public const string ShahbaziPassword = "Aa@12345";
        public const string ShahbaziCellPhone = "09120000000";
        public const string ShahbaziEmail = "Shahbazi.amh@gmail.com";
    }
}
