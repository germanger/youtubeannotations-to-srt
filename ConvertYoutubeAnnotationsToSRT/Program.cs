using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConvertYoutubeAnnotationsToSRT
{
    class Program
    {
        static void Main(string[] args)
        {
            // The parsed subtitles
            List<SubtitleLine> subtitles = new List<SubtitleLine>();

            // Open document
            XDocument doc = null;

            using (StreamReader oReader = new StreamReader("C:\\annotations.xml", Encoding.GetEncoding("ISO-8859-1")))
            {
                doc = XDocument.Load(oReader);
            }

            // Fetch all <annotation>'s
            var annotations = doc.Descendants("annotation");

            // Iterate over them, parsing them
            foreach (var annotation in annotations)
            {
                SubtitleLine subtitleLine = new SubtitleLine();

                subtitleLine.Text = annotation.Element("TEXT").Value.Replace("\n", "");
                subtitleLine.StartTime = FormatTime(annotation.Descendants("rectRegion").ToList()[0].Attribute("t").Value);
                subtitleLine.EndTime = FormatTime(annotation.Descendants("rectRegion").ToList()[1].Attribute("t").Value);

                subtitles.Add(subtitleLine);
            }

            // Output SRT file
            string output = "";
            int subtitleNumber = 1;

            foreach (SubtitleLine subtitleLine in subtitles.OrderBy(s => s.StartTime))
            {
                output = output + subtitleNumber++;
                output = output + "\n" + subtitleLine.StartTime + " --> " + subtitleLine.EndTime;
                output = output + "\n" + subtitleLine.Text;
                output = output + "\n\n";
            }

            // Show output
            Console.WriteLine(output);

            // Lets dont end the program yet
            Console.ReadLine();
        }

        private static string FormatTime(string timecode)
        {
            timecode = timecode.Replace(".", ",");

            if (timecode.Length == 9)
                return "00:" + timecode;

            if (timecode.Length == 8)
                return "00:0" + timecode;

            if (timecode.Length == 6)
                return "00:00:" + timecode;

            return timecode;
        }
    }

    class SubtitleLine
    {
        public string Text { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
