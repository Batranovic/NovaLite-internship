using Konteh.Domain.Enumerations;

namespace Konteh.Domain;

public class Question
{
    public long Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public QuestionCategory Category { get; set; }
    public QuestionType Type { get; set; }
    public List<Answer> Answers { get; set; } = [];
    public bool IsDeleted { get; set; } = false;

    internal bool IsCorrect(IEnumerable<long> sumittedAnswerIds)
    {
        if (Type == QuestionType.RadioButton)
        {
            var correctAnswerId = Answers.Single(x => x.IsCorrect).Id;
            return correctAnswerId == sumittedAnswerIds.Single();
        }
        if (Type == QuestionType.CheckBox)
        {
            var correctAnswerIds = Answers.Where(x => x.IsCorrect).Select(x => x.Id).Order();
            return sumittedAnswerIds.Order().SequenceEqual(correctAnswerIds);
        }
        return false;
    }
}
