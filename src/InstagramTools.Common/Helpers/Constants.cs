namespace InstagramTools.Common.Helpers
{
	public static class Constants
	{
		public static class ClaimTypes
		{
			public const string UserId = "userId";
			public const string UserName = "userName";
            public const string Password = "password";
            public const string RoleId = "roleId";
			public const string RoleName = "roleName";
			public const string Permission = "permission";
		}

		public static class Roles
		{
			public const string Guest = "guest";
			public const string User = "user";
			public const string Moderator = "moderator";
			public const string Admin = "admin";
		}

		public static class Policies
		{
			public const string UserMinimum = "UserMinimum";
			public const string ModeratorMinimum = "ModeratorMinimum";
			public const string AdminOnly = "AdminOnly";
			public const string Over18 = "Over18";
		}
	}
	
}