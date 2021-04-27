using question.domain;
using question.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace question.service
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;
        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public void PostQuestion(Question question)
        {
            question.PublishedAt = DateTime.Now;
            _repository.PostQuestion(question);
        }
        public void PutQuestion(Question question)
        {
            _repository.PutQuestion(question);
        }

        public Question GetQuestion(int  questionID)
        {
           return _repository.GetQuestion(questionID);
        }

        public List<Question> GetQuestion(string _filter, int? offset, int? _limit)
        {
            return _repository.GetQuestion( _filter, offset, _limit);
        }
    }
}
