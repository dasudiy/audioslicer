using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpegCore;

namespace AudioSlicer
{
    class AudioSlicerApp
    {
        internal static async Task ConvertVideoToSpeech(string inputVideoPath, string subtitleFilePath, string outputDirectoryPath)
        {
            GlobalFFOptions.Configure(new FFOptions { BinaryFolder = @"C:\Users\asuka\OneDrive\Apps\Captura-Portable\ffmpeg-4.3.1-win64-shared\bin" });
            var dir = Directory.CreateDirectory(outputDirectoryPath);
            var srtParser = new SubtitlesParser.Classes.Parsers.SrtParser();
            using (var fs = new FileStream(subtitleFilePath, FileMode.Open))
            {
                var items = srtParser.ParseStream(fs, Encoding.UTF8);
                var split = 0;

                foreach (var item in items)
                {
                    if (!item.Lines.Any(t => t.Trim() == "[Music]") && item.EndTime - item.StartTime > 4000)
                    {
                        split++;
                        string outputFilePath = Path.Combine(dir.FullName, $"audio-{item.StartTime}.wav");

                        var info = $"{outputFilePath}|{string.Join(",", item.Lines)}";
                        System.Console.WriteLine(info);
                        await File.AppendAllTextAsync(Path.Combine(Path.GetDirectoryName(outputFilePath), "list.txt"), info + "\r\n");

                        // Execute the FFMpeg command to extract the range
                        await FFMpegArguments
                            .FromFileInput(inputVideoPath)
                            .OutputToFile(outputFilePath, false, options => options
                                .WithCustomArgument("-ac 1")
                                .WithAudioSamplingRate(22050)
                                .ForceFormat("wav")
                                .Seek(TimeSpan.FromMilliseconds(item.StartTime))
                                .WithDuration(TimeSpan.FromMilliseconds(item.EndTime - item.StartTime))
                            )
                            .ProcessAsynchronously();

                    }
                }
            }
        }
    }
}