﻿namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ImportDto;
    using Boardgames.Utilities;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            const string xmlRootName = "Creators";

            CreatorImportDto[] creatorsDtos= xmlHelper.Deserialize<CreatorImportDto[]>(xmlString, xmlRootName);

            List<Creator> creatorsToImport = new List<Creator>();

            foreach (var creatorDto in creatorsDtos)
            {
                if (!IsValid(creatorDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                
                Creator creator = new Creator()
                {
                    FirstName = creatorDto.FirstName,
                    LastName = creatorDto.LastName
                };

                foreach (var boardgameDto in creatorDto.Boardgame)
                {
                    if (!IsValid(boardgameDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Boardgame boardgame = new Boardgame()
                    {
                        Name = boardgameDto.Name,
                        Rating = boardgameDto.Rating,
                        YearPublished = boardgameDto.YearPublished,
                        CategoryType = (CategoryType)boardgameDto.CategoryType,
                        Mechanics = boardgameDto.Mechanics
                    };
                    creator.Boardgames.Add(boardgame);
                }

                creatorsToImport.Add(creator);
                sb.AppendLine(string.Format(SuccessfullyImportedCreator,creator.FirstName, creator.LastName, creator.Boardgames.Count));
            }

            context.Creators.AddRange(creatorsToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            SellersImportDto[] sellersImportDtos = JsonConvert.DeserializeObject<SellersImportDto[]>(jsonString)!;

            List<Seller> sellersToImport = new List<Seller>();

            int bc = 0;

            foreach (var sellerstDto in sellersImportDtos)
            {
                if (!IsValid(sellerstDto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Seller sellerToAdd = new Seller()
                {
                    Name = sellerstDto.Name,
                    Address = sellerstDto.Address,
                    Country = sellerstDto.Country,
                    Website = sellerstDto.Website
                };

                foreach (var boardgameId in sellerstDto.Boardgames.Distinct())
                {
                    Boardgame boardGameCount = context.Boardgames.Find(boardgameId);
                    if (boardGameCount == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    sellerToAdd.BoardgamesSellers.Add(new BoardgameSeller()
                    {
                        Boardgame = boardGameCount
                    });
                }

                bc += sellerToAdd.BoardgamesSellers.Count;
                sellersToImport.Add(sellerToAdd);
                sb.AppendLine(String.Format(SuccessfullyImportedSeller, sellerToAdd.Name, sellerToAdd.BoardgamesSellers.Count));
            }
            
            context.Sellers.AddRange(sellersToImport);
            context.SaveChanges();

            return sb.ToString();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
