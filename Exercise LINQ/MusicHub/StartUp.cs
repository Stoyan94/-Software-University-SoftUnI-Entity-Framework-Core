namespace MusicHub;

using System;
using System.Globalization;
using System.Text;
using Data;
using Initializer;
using MusicHub.Data.Models;

public class StartUp
{
    public static void Main()
    {
        MusicHubDbContext context =
            new MusicHubDbContext();

        DbInitializer.ResetDatabase(context);

        string result = ExportSongsAboveDuration(context, 4);
        Console.WriteLine(result);

        //string result = ExportAlbumsInfo(context, 9);
        //Console.WriteLine(result);
        //Test your solutions here
    }

    public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
    {
        // Formatting of numbers in the .Select() gives you data ready to fill any ViewModel
        // This ViewModel can be passed to any View
        StringBuilder output = new StringBuilder();

        var albumsInfo = context.Albums
            .Where(a => a.ProducerId.HasValue &&
                        a.ProducerId.Value == producerId)
            .ToArray() // This is because of bug in EF => "cannot translate data" because is still IQueryable(connected to the base ) and need materilizeishon
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

    public static string ExportSongsAboveDuration(MusicHubDbContext context, int durationSeckonds)
    {
        StringBuilder output = new StringBuilder();

        var songsInfo = context.Songs
            .AsEnumerable()
            .Where(s => s.Duration.TotalSeconds > durationSeckonds)
            .Select(s => new
            {
                s.Name,
                Performers = s.SongPerformers
                    .Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
                    .OrderBy(p => p)
                    .ToArray(),
                WriterName = s.Writer.Name,
                AlbumProducer = s.Album!.Producer!.Name,
                Duration = s.Duration.ToString("c")
            })
            .OrderBy(s => s.Name)
            .ThenBy(s => s.WriterName)
            .ToArray();

        int songsCount = 1;

        foreach (var song in songsInfo)
        {

            output
                .AppendLine($"-Song #{songsCount}")
                .AppendLine($"---SongName: {song.Name}")
                .AppendLine($"---Writer: {song.WriterName}");

            foreach (var performer in song.Performers)
            {
                output
                    .AppendLine($"---Performer: {performer}");
            }

            output
                .AppendLine($"---AlbumProducer: {song.AlbumProducer}")
                .AppendLine($"---Duration: {song.Duration}");

            songsCount++;
        }

        return output.ToString().TrimEnd();
    }
}
