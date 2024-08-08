namespace Footballers.DataProcessor
{
    using Data;
    using Newtonsoft.Json;
    using System.Globalization;

    public class Serializer
    {
        public static string ExportCoachesWithTheirFootballers(FootballersContext context)
        {
            throw new NotImplementedException();
        }

        public static string ExportTeamsWithMostFootballers(FootballersContext context, DateTime date)
        {
            var teamsFootballers = context
               .Teams
               .Where(t => t.TeamsFootballers.Any(tf => tf.Footballer.ContractStartDate >= date))
               .ToArray()
               .Select(t => new
               {
                   t.Name,
                   Footballers = t.TeamsFootballers
                       .Where(tf => tf.Footballer.ContractStartDate >= date)
                       .ToArray()
                       .OrderByDescending(tf => tf.Footballer.ContractEndDate)
                       .ThenBy(tf => tf.Footballer.Name)
                       .Select(tf => new
                       {
                           FootballerName = tf.Footballer.Name,
                           ContractStartDate = tf.Footballer.ContractStartDate.ToString("d", CultureInfo.InvariantCulture),
                           ContractEndDate = tf.Footballer.ContractEndDate.ToString("d", CultureInfo.InvariantCulture),
                           BestSkillType = tf.Footballer.BestSkillType.ToString(),
                           PositionType = tf.Footballer.PositionType.ToString()
                       })
                       .ToArray()
               })
               .OrderByDescending(t => t.Footballers.Length)
               .ThenBy(t => t.Name)
               .Take(5)
               .ToArray();

            return JsonConvert.SerializeObject(teamsFootballers, Formatting.Indented);
        }
    }
}
