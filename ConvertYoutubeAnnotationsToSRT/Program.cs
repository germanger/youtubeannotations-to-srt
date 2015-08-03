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
		static void Main (string[] args)
		{
			if (args.Length == 0) {
				Console.WriteLine ("usage: ann2srt input.xml > output.srt");
				Console.WriteLine ("Converts YouTube's XML annotations to SRT files VLC (and others) will accept as subtitles");
				return;
			}

			// The parsed subtitles
			List<SubtitleLine> subtitles = new List<SubtitleLine> ();

			// Open document
			XDocument doc = null;

			try {
				using (StreamReader oReader = new StreamReader (args [0], Encoding.UTF8)) {
					doc = XDocument.Load (oReader);
				}
			} catch (System.IO.FileNotFoundException e) {
				Console.WriteLine ("Couldn't find the file.");
				Console.WriteLine (e);
				return;
			} catch (Exception e) {
				Console.WriteLine ("Error reading the file. Full exception:");
				Console.WriteLine (e);
				return;
			}

			// Fetch all <annotation>'s
			var annotations = from anns in doc.Descendants ("annotation")
			                    select anns;

			// Iterate over them, parsing them
			foreach (var annotation in annotations) {
				if (annotation.Element ("TEXT") == null)
					continue;

				SubtitleLine subtitleLine = new SubtitleLine ();

				subtitleLine.Text = annotation.Element ("TEXT").Value.Replace ("\n", "");
				subtitleLine.StartTime = FormatTime (annotation.Element ("segment").Element ("movingRegion").Descendants ("rectRegion").ToList () [0].Attribute ("t").Value);
				subtitleLine.EndTime = FormatTime (annotation.Element ("segment").Element ("movingRegion").Descendants ("rectRegion").ToList () [1].Attribute ("t").Value);

				subtitles.Add (subtitleLine);
			}

			// Output SRT file
			string output = "";
			int subtitleNumber = 1;

			foreach (SubtitleLine subtitleLine in subtitles.OrderBy(s => s.StartTime)) {
				output = output + subtitleNumber++;
				output = output + "\n" + subtitleLine.StartTime + " --> " + subtitleLine.EndTime;
				output = output + "\n" + subtitleLine.Text;
				output = output + "\n\n";
			}

			// Show output
			Console.WriteLine (output);
		}

		private static string FormatTime (string timecode)
		{
			timecode = timecode.Replace (".", ",");

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
