using Konteh.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Konteh.Infrastructure.Repositories
{
    public class ExamQuestionRepository : BaseRepository<ExamQuestion>
    {
        public ExamQuestionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
