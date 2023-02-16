using System;
using System.IO;
using System.Threading.Tasks;

namespace AudioSlicer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Parse command line arguments
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: dotnet AudioSlicer.dll input-video.mp4 subtitle-file.srt output-directory/");
                return;
            }

            string inputVideoPath = args[0];
            string subtitleFilePath = args[1];
            string outputDirectoryPath = args[2];

            if (!File.Exists(inputVideoPath))
            {
                Console.WriteLine($"Input video file {inputVideoPath} does not exist.");
                return;
            }

            if (!File.Exists(subtitleFilePath))
            {
                Console.WriteLine($"Subtitle file {subtitleFilePath} does not exist.");
                return;
            }

            if (!Directory.Exists(outputDirectoryPath))
            {
                Console.WriteLine($"Output directory {outputDirectoryPath} does not exist.");
                return;
            }

            // Call the main application logic with the parsed arguments
            await AudioSlicerApp.ConvertVideoToSpeech(inputVideoPath, subtitleFilePath, outputDirectoryPath);
        }
    }
}
