namespace EventMiMVC.Web.Common
{
    public static class EntityConstraint
    {
        public const byte EventNameMinLength = 5;
        public const byte EventNameMaxLength = 50;
        public const string EventNameLengthErrorMessage = "Event name must be between {0} and {0} characters long.";
        public const byte EventPlaceMinLength = 3;
        public const byte EventPlaceMaxLength = 75;
    }
}
    