using Konteh.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Konteh.Infrastructure.Configurations;

class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasDiscriminator(x => x.Type)
            .HasValue<RadioButtonQuestion>(Domain.Enumerations.QuestionType.RadioButton)
            .HasValue<CheckBoxQuestion>(Domain.Enumerations.QuestionType.CheckBox);
    }
}
