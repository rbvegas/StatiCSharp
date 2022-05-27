using static System.Console;

namespace StatiCsharp
{
    /// <summary>
    /// Privides usefull methods to work with markdown files and data.
    /// </summary>
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

            // Add meta data from md-file between yaml markers in dict, if there are any.
            if (yamlMarker.Count > 0)
            {
                try
                {
                    for (int i = yamlMarker[0] + 1; i < yamlMarker[1]; i++)
                    {
                        int indexOfColon = lines[i].IndexOf(':');
                        string key = lines[i].Substring(0, indexOfColon).ToLower().Trim();
                        string value = lines[i].Substring(indexOfColon + 1).Trim();
                        metaData.Add(key, value);
                    }
                }
                catch
                {
                    WriteLine($"No meta data found in {path}\nCheck out https://github.com/rolandbraun-dev/StatiCsharp/blob/develop/Documentation/HowTo/content-template.md for a site template.");
                }
            }
            return metaData;
        }

        /// <summary>
        /// Parses the content of the markdownfile, while slicing the meta data.
        /// </summary>
        /// <param name="path">Path to the markdown file.</param>
        /// <returns>A string with the parsed content.</returns>
        public static string ParseContent(string path)
        {
            string[] lines = File.ReadAllLines(path);
            List<int> yamlMarker = YamlMarkers(lines);
            if (yamlMarker.Count == 0) // No meta data available
            {
                return String.Join("\n", lines);
            }
            string[] linesWithoutMetaData = new ArraySegment<string>(lines, yamlMarker[1] + 1, lines.Length - yamlMarker[1] - 1).ToArray();
            string content = String.Join("\n", linesWithoutMetaData);
            return content;
        }

        private static List<int> YamlMarkers(string[] lines)
        {
            List<int> marker = new List<int>();

            if (lines[0] != "---")
            {
                return marker;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "---")
                {
                    marker.Add(i);
                }

                if (marker.Count == 2)
                {
                    break;
                }
            }
            return marker;
        }
    }
}
