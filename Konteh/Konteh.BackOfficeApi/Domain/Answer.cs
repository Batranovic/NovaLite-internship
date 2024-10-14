namespace Konteh.BackOfficeApi.Domain
{
    public class Answer
    {
        public long Id { get; set; }
        public String Text { get; set; }
        public Boolean IsCorrect { get; set; }

        public Answer() { }

        public Answer(string text, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}
