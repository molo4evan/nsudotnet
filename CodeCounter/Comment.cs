
namespace CodeCounter {
    public class Comment {
        public bool IsMultiline { get; private set; }

        public string Start { get; private set; }

        public string End { get; private set; }

        public Comment(bool isMultiline, string start, string end) {
            IsMultiline = isMultiline;
            Start = start;
            End = end;
        }
    }
}