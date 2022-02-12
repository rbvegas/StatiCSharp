using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace StatiCsharp
{
    internal static class MarkdownFactory
    {
        /// <summary>
        /// Parses the meta data (yaml) of a markdown file.
        /// </summary>
        /// <param name="path">Path to the markdown file.</param>
        /// <returns>A Dictionary<string, string> with the parsed meta data.</returns>
        public static Dictionary<string, string> ParseMetaData(string path)
        {
            Dictionary<string, string> metaData = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(path);
            List<int> yamlMarker = YamlMarkers(lines);

            // Add entries between yaml markers in dict
            for (int i = yamlMarker[0] + 1; i < yamlMarker[1]; i++)
            {
                int indexOfColon = lines[i].IndexOf(':');
                string key = lines[i].Substring(0, indexOfColon).ToLower().Trim();
                string value = lines[i].Substring(indexOfColon + 1).ToLower().Trim();
                metaData.Add(key, value);
            }



            return metaData;
        }

        public static string SliceMetaData(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<int> yamlMarker = YamlMarkers(lines);
            string[] linesWithoutMetaData = new ArraySegment<string>(lines, yamlMarker[1] + 1, lines.Length - yamlMarker[1]-1).ToArray();
            string content = String.Join("\n", linesWithoutMetaData);
            return content;
        }

        private static List<int> YamlMarkers(string[] lines)
        {
            List<int> marker = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "---")
                {
                    marker.Add(i);
                }
            }
            return marker;
        }
    }
}
