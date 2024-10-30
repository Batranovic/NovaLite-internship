using Konteh.Domain.Enumerations;

namespace Konteh.Domain;
public class CheckBoxQuestion : Question
{
    public override QuestionType Type { get => QuestionType.CheckBox; set { } }

    internal override bool IsCorrect(IEnumerable<long> submittedAnswerIds)
    {
        if (!submittedAnswerIds.Any())
            return false;
        var correctAnswerIds = Answers.Where(x => x.IsCorrect).Select(x => x.Id).Order();
        return submittedAnswerIds.Order().SequenceEqual(correctAnswerIds);
    }

}
