namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            var creatorsWithBoardGames = context.Creators
                .Where(b => b.Boardgames.Any() && b.Boardgames.Any(b => b.YearPublished >= b.Rating))
                .ToList();
            return null;
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var creatorsWithBoardGames = context.Sellers
              .Where(sb => sb.BoardgamesSellers.Any(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating))
              .ToArray();

            return null;
        }
    }
}