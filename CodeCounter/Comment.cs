
namespace CodeCounter {
    public class Comment {
        public bool IsMultiline { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public Comment(bool isMultiline, string start, string end) {
            IsMultiline = isMultiline;
            Start = start;
            End = end;
        }
    }
}