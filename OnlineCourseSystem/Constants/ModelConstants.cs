namespace OnlineCourseSystem.Constants
{
    public static class ModelConstants
    {
        #region Roles Related
        public static class Roles
        {
            public static string Admin = "Admin";
            public static string Teacher = "Teacher";
            public static string Student = "Student";
            public static string AdminorTeacher = Admin + "," + Teacher;
        }
        #endregion

        #region Modal Related
        public enum ModalType
        {
            Default, Primary, Warning, Danger
        }

        public enum ModalSize
        {
            Small, Medium, Default, Large, ExtraLarge
        }
        #endregion

        #region Contact Info
        public static class ContactInfo
        {
            public static string PhoneNo = "+4478580 98656";
            public static string Email = "passmrcpch@passmrcpch.com";
        }
        #endregion

        #region FileTypes Related
        public static class FileTypes
        {
            public static string Image = "Image";
            public static string Pdf = "Pdf";
            public static string Video = "Video";
        }
        #endregion
    }
}
