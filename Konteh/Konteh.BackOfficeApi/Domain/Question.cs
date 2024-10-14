using Konteh.BackOfficeApi.Domain.Enumerations;

namespace Konteh.BackOfficeApi.Domain
{
    public class Question
    {
        public long Id { get; set; }
        public String Text { get; set; }

        public String Category { get; set; }

        public QuestionType Type { get; set; }

        public Question() { }
        public Question(long id, string text, string category, QuestionType type)
        {
            Id = id;
            Text = text;
            Category = category;
            Type = type;
        }

    }
}
