namespace Footballers.Data
{
    public static class DataConstraints
    {
        // Footballer
        public const byte FootballerNameMinLenght = 2;
        public const byte FootballerNameMaxLenght = 40;


        // Team
        public const byte TeamNameMinLenght = 3;
        public const byte TeamNameMaxLenght = 40;
        public const byte TeamNationalityMinLenght = 2;
        public const byte TeamNationalityMaxLenght = 40;

        public const string RegexTeamNamePattern = @"^[a-zA-Z0-9 .-]+$";

        // Coach
        public const byte CoachNameMinLenght = 2;
        public const byte CoachNameMaxLenght = 40;
    }
}
