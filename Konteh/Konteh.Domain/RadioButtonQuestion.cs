using Konteh.Domain.Enumerations;

namespace Konteh.Domain;
public class RadioButtonQuestion : Question
{
    public override QuestionType Type { get => QuestionType.RadioButton; set { } }

    internal override bool IsCorrect(IEnumerable<long> submittedAnswerIds)
    {
        if (!submittedAnswerIds.Any())
            return false;
        var correctAnswerId = Answers.SingleOrDefault(x => x.IsCorrect && !x.IsDeleted)?.Id;
        return correctAnswerId == submittedAnswerIds.Single();
    }
}
