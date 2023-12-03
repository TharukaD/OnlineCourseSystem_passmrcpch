namespace OnlineCourseSystem.Utility
{
    public static class HelperMethods
    {
        public static string ReturnServiceImagePath(string serviceImage)
        {
            string ImagePath = "/img/ServiceImages/default.jpg";
            if (!string.IsNullOrEmpty(serviceImage))
            {
                ImagePath = $"/img/ServiceImages/{serviceImage}";
            }

            return ImagePath;
        }

        public static string ReturnCourseImagePath(string courseImage)
        {
            string ImagePath = "/img/CourseImages/default.jpg";
            if (!string.IsNullOrEmpty(courseImage))
            {
                ImagePath = $"/img/CourseImages/{courseImage}";
            }

            return ImagePath;
        }

        public static string ReturnHomePageBannerSmallImagePath(string image)
        {
            string ImagePath = "/img/HomePageBannerSmallImage/default.jpg";
            if (!string.IsNullOrEmpty(image))
            {
                ImagePath = $"/img/HomePageBannerSmallImage/{image}";
            }

            return ImagePath;
        }

        public static string ReturnHomePageBannerLargeImagePath(string image)
        {
            string ImagePath = "/img/HomePageBannerLargeImage/default.jpg";
            if (!string.IsNullOrEmpty(image))
            {
                ImagePath = $"/img/HomePageBannerLargeImage/{image}";
            }

            return ImagePath;
        }

        public static string ReturnCounterRecordImagePath(string image)
        {
            string ImagePath = "/img/CounterRecordImages/default.jpg";
            if (!string.IsNullOrEmpty(image))
            {
                ImagePath = $"/img/CounterRecordImages/{image}";
            }

            return ImagePath;
        }

        public static string ReturnArticleImagePath(string articleImage)
        {
            string ImagePath = "/img/ArticleImages/default.jpg";
            if (!string.IsNullOrEmpty(articleImage))
            {
                ImagePath = $"/img/ArticleImages/{articleImage}";
            }

            return ImagePath;
        }

        public static string ToDateString(DateTime input)
        {
            return input.ToString("MM/dd/yyyy");
        }

        public static string ToDateTimeString(DateTime input)
        {
            return input.ToString("MM/dd/yyyy h:mm tt");
        }
    }
}
