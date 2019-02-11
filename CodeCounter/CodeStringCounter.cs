using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeCounter {
    public static class CodeStringCounter {
        private static readonly char[] CharsToTrim = {' ', '\t'};
        private static readonly List<string> Comments = new List<string>();
        
        public static void Configure(string filepath) {
            string[] lines;
            try {
                lines = File.ReadAllLines(filepath);
            }
            catch (FileNotFoundException e) {
                Comments.Clear();
                return;
            }
            
            lines = lines.Select( s => s.TrimStart(CharsToTrim) ).ToArray();
            Comments.Clear();
            Comments.AddRange(lines);
        }

        public static int CountStrings(string pattern) {
            var curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            return ScanDirectory(curDir, pattern);
        }

        private static int ScanDirectory(DirectoryInfo currentDir, string pattern) {
            return currentDir.GetDirectories().Sum(dir => ScanDirectory(dir, pattern)) +
                   currentDir.GetFiles(pattern).Select(ScanFile).Sum();
        }

        private static int ScanFile(FileInfo file) {
            StreamReader lineReader;
            try {
                lineReader = new StreamReader(file.Open(FileMode.Open));
            }
            catch (IOException) {
                return 0;
            }
            var count = 0;
            while (true) {
                var line = lineReader.ReadLine();
                if (line == null) break;
                
                line = line.TrimStart(CharsToTrim);
                if (line.Equals("")) continue;

                if (Comments.Any(line.StartsWith)) {
                    continue;
                }
                
                count++;
            }
            return count;
        }
    }
}