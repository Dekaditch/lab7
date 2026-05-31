namespace lab1.Models
{
    public class EditAction
    {
        public string Text { get; set; }
        public int SelectionStart { get; set; }
        public int SelectionLength { get; set; }
        public ActionType ActionType { get; set; }
    }
}