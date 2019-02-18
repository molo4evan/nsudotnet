using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeCounter {
    public static class CodeStringCounter {
        private static readonly char[] CharsToTrim = {' ', '\t'};
        private static readonly List<Comment> Comments = new List<Comment>();
        
        public static void Configure(string filepath) {
            string[] lines;
            try {
                lines = File.ReadAllLines(filepath);
            }
            catch (FileNotFoundException) {
                Comments.Clear();
                return;
            }
            
            lines = lines.Select( s => s.TrimStart(CharsToTrim) ).ToArray();
            Comments.Clear();
            foreach (var line in lines) {
                var parts = line.Split(' ', '\t');
                switch (parts.Length) {
                    case 1:
                        Comments.Add(new Comment(false, parts[0], null));
                        break;
                    case 2:
                        Comments.Add(new Comment(true, parts[0], parts[1]));
                        break;
                    default:
                        throw new Exception("Incorrect format of comment");
                }
            }
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

                var pass = false;
                foreach (var comment in Comments) {
                    if (!line.StartsWith(comment.Start)) continue;
                    if (comment.IsMultiline) {
                        while (!line.Contains(comment.End)) {
                            line = lineReader.ReadLine();
                            if (line == null) {
                                pass = true;
                                break;
                            }
                        }
                        if (line == null) break;
                        if (line.EndsWith(comment.End)) {
                            pass = true;
                        }
                    } else {
                        pass = true;
                    }
                    break;
                }

                if (pass) {
                    continue;
                }
                
                count++;
            }
            return count;
        }
    }
}