using Microsoft.AspNetCore.Mvc.Rendering;

namespace Test_Fidele.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Anonymous = "Anonymous";
        public static string UserAdded = "User added successfully.";
        public static string UserUpdated = "User updated successfully.";
        public static string UserDeleted = "User deleted successfully.";
        public static string UserExists = "User for selected date and time already exists.";
        public static string UserNotExists = "User not exists.";
        public static string SomethingWentWrong = "Something went wront, Please try again.";
        public static int success_code = 1;
        public static int failure_code = 0;
    }
}
