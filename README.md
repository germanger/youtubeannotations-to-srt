# youtubeannotations-to-srt

This small tool (C# console application) lets to convert youtube annotations (XML) to SRT file.<br/>
Source code: `C#`

## Usage

1. Download youtube annotations as XML with this opensource tool: [youtube-copy-annotations](https://stefansundin.github.io/youtube-copy-annotations/) (you only need to use `Step 1: Get annotation data`)

 or you can directly get the annotations with this URL: `https://www.youtube.com/annotations_invideo?video_id=YOUR_VIDEO_ID`, just replace `YOUR_VIDEO_ID` with your real youtube video id (which is available in the URL of your video)

2. Convert the downloaded XML **using this project**:

    2.1. Compile project (with a C# compiler or using `Visual Studio`). You will get a `ConvertYoutubeAnnotationsToSRT.exe` (or you can [simply download it](http://www.qm.cl/germanvicencio/ConvertYoutubeAnnotationsToSRT.exe), if you trust me)

 2.2. Using `cmd`, run the `.exe` passing the XML file path as an argument (just type the path after the `.exe`, separated by one or more spaces).
 
 If you want to see the output in the console, just type:
 
       ConvertYoutubeAnnotationsToSRT.exe C:\annotations.xml
       
 If you want the output written to a file, type:
 
       ConvertYoutubeAnnotationsToSRT.exe C:\annotations.xml >> C:\output.srt
 
 **Argument alternative:** you can omit the XML argument, but then the file needs to be in `C:\annotations.xml` (with that exact name and location)
 

## TO-DOs:

1. Accept an argument that enables writing to a file instead of writing to `Console`'s output.
2. Maybe create a GUI, or a web application
3. Do more tests and check exceptions
