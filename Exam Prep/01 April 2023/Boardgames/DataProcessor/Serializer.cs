namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.DataProcessor.ExportDto;
    using Boardgames.Utilities;
    using Newtonsoft.Json;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRootName = "Creators";

            var creatorsWithBoardGames = context.Creators
                .Where(cb => cb.Boardgames.Any())
                .Select(creatorDto => new CreatorExportDto()
                {
                    CreatorName = creatorDto.FirstName + " " + creatorDto.LastName,
                    BoardgamesCount = creatorDto.Boardgames.Count(),
                    Boardgames = creatorDto.Boardgames.Select(bgDto => new BoardgamesCreatorExporttDto()
                    {
                        BoardgameName = bgDto.Name,
                        BoardgameYearPublished = bgDto.YearPublished
                    })
                    .OrderBy(bgDto => bgDto.BoardgameName)
                    .ToArray()
                })
                .OrderByDescending(cb => cb.BoardgamesCount)
                .ThenBy(cb => cb.CreatorName)
                .ToArray();
                

            return xmlHelper.Serialize(creatorsWithBoardGames, xmlRootName);
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var creatorsWithBoardGames = context.Sellers
              .Where(sb => sb.BoardgamesSellers
              .Any(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating))
              .ToArray()
              .Select(s => new
              {
                  s.Name,
                  s.Website,
                  Boardgames = s.BoardgamesSellers
                    .Where(sb => sb.Boardgame.YearPublished >= year && sb.Boardgame.Rating <= rating)
                    .ToArray()
                    .OrderByDescending(sb => sb.Boardgame.Rating)
                    .ThenBy(sb => sb.Boardgame.Name)
                    .Select(sb => new
                    {
                        Name = sb.Boardgame.Name,
                        Rating = sb.Boardgame.Rating,
                        Mechanics = sb.Boardgame.Mechanics,
                        Category = sb.Boardgame.CategoryType.ToString()
                    })
                    .ToArray()
              })
              .OrderByDescending(bg => bg.Boardgames.Count())
              .ThenBy(s => s.Name)
              .Take(5)
              .ToArray();

            return JsonConvert.SerializeObject(creatorsWithBoardGames, Formatting.Indented);
        }
    }
}