using question.domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace question.service
{
    public interface IQuestionService
    {
        void PostQuestion(Question question);
        void PutQuestion(Question question);
        Question GetQuestion(int questionID);
        List<Question> GetQuestion(string _filter, int? offset, int? _limit);
    }
}
