using question.domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace question.model
{
    public interface IQuestionRepository
    {
        void PostQuestion(Question question);
        void PutQuestion(Question question);
        Question GetQuestion(int idQuestion);
        List<Question> GetQuestion(string filter, int? _skip, int? _limit);
    }
}
