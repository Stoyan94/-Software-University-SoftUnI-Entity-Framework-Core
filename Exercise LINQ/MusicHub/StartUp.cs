namespace MusicHub;

using System;
using System.Globalization;
using System.Text;
using Data;
using Initializer;

public class StartUp
{
    public static void Main()
    {
        MusicHubDbContext context =
            new MusicHubDbContext();

        DbInitializer.ResetDatabase(context);

        string result = ExportAlbumsInfo(context, 9);
        Console.WriteLine(result);
        //Test your solutions here
    }

    public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
    {
        StringBuilder output = new StringBuilder();

        var albumsInfo = context.Albums
            .Where(a => a.ProducerId.HasValue &&
                        a.ProducerId.Value == producerId)
            .ToArray()
            .OrderByDescending(a => a.Price)
            .Select(a => new
            {
                a.Name,
                ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                ProducerName = a.Producer.Name,
                Songs = a.Songs
                        .Select(s => new
                        {
                            SongName = s.Name,
                            Price = s.Price.ToString("f2"),
                            Writer = s.Writer.Name
                        })
                        .OrderByDescending(s => s.SongName)
                        .ThenBy(s => s.Writer)
                        .ToArray(),
                    AlbumPrice = a.Price.ToString("f2")
            })
            .ToArray();

        

        foreach (var album in albumsInfo)
        {
            output
                    .AppendLine($"-AlbumName: {album.Name}")
                    .AppendLine($"-ReleaseDate: {album.ReleaseDate}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine("-Songs:");

            int countSongs = 1;

            foreach (var song in album.Songs)
            {
                output                    
                    .AppendLine($"---#{countSongs}")
                    .AppendLine($"---SongName: {song.SongName}")
                    .AppendLine($"---Price: {song.Price}")
                    .AppendLine($"---Writer: {song.Writer}");

                countSongs++;
            }

            output.AppendLine($"-AlbumPrice: {album.AlbumPrice}");
        }

        return output.ToString().TrimEnd();
    }

    public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
    {
        throw new NotImplementedException();
    }
}
