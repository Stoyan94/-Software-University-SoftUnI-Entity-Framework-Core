namespace Boardgames.Data.Models
{
    public static class DataConstraints
    {
        // BoardGame
        public const byte BoardGameNameMinLenght = 10;
        public const byte BoardGameNameMaxLenght = 20;
        public const double BoardGameRatingMinRange = 1.00;
        public const double BoardGameRatingMaxRange = 10.00;
        public const int BoardGameYearPublishedMinRange = 2018;
        public const int BoardGameYearPublishedMaxRange = 2023;
    }
}
