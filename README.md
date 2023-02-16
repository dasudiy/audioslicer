# Audio Slicer

This is a simple command-line tool that can be used to split a video file into multiple audio files based on the timestamps provided in an SRT subtitle file. It also generates a corresponding list file with the text from the subtitle.

## Requirements
- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)
- [FFmpeg](https://ffmpeg.org/)

## Usage
```shell
dotnet VideoToSpeechConverter.dll input-video.mp4 subtitle-file.srt output-directory/
```
- `input-video.mp4`: The path to the input video file.
- `subtitle-file.srt`: The path to the SRT subtitle file.
- `output-directory/`: The directory where the output audio files and list file will be saved.
 

## Output

This tool generates two types of output:
1. Audio files: The audio files are saved in the specified output directory with a naming convention of `audio-{timestamp}.wav`, where `{timestamp}` is the timestamp of the subtitle entry in seconds. Each audio file contains the audio from the input video corresponding to the subtitle entry.
2. List file: The list file contains the text from the subtitle file along with the corresponding audio file name. The list file is saved in the specified output directory with the name `list.txt`.

## Example
Suppose you have an input video `my-video.mp4` and an SRT subtitle file `my-subtitle.srt`. You want to split the input video into audio files and generate a list file in the `output/` directory. Run the following command:

```shell
dotnet VideoToSpeechConverter.dll my-video.mp4 my-subtitle.srt output/
```

The tool will generate multiple audio files in the output/ directory, such as audio-10.3.wav, audio-12.5.wav, audio-14.2.wav, etc. It will also generate a list file list.txt in the output/ directory, which will contain the text of the subtitle along with the corresponding audio file names.

## Credits

This tool uses the following open-source libraries:

- [FFmpeg](https://ffmpeg.org/)
- [SubtitlesParser](https://github.com/AlexPoint/SubtitlesParser)