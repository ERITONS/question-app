using Microsoft.EntityFrameworkCore;
using question.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace question.model
{

    public class QuestionRepository : IQuestionRepository
    {
        private QuestionContext _context;
        public QuestionRepository(QuestionContext contex)
        {
            _context = contex;
        }

        public void PostQuestion(Question question)
        {
            _context.Questions.Add(question);
            _context.SaveChanges();
        }

        public void PutQuestion(Question _question)
        {
            var questionID = _question.QuestionId;

            var existingQuestion = _context.Questions
                .Where(p => p.QuestionId == questionID)
                .Include(p => p.Choices)
                .SingleOrDefault();

            this.ProcessUpdateQuestionAndChoices(existingQuestion, _question, questionID);
        }
        public Question GetQuestion(int idQuestion)
        {
            return _context.Questions.Include(q => q.Choices).Where(x => x.QuestionId == idQuestion).FirstOrDefault();
        }

        public List<Question> GetQuestion(string _filter, int? _offset, int? _limit)
        {
            var filterON = !String.IsNullOrEmpty(_filter);
            var skipON = !(_offset == null || _offset == 0);
            var limitON = !(_limit == null || _limit == 0);

            if (filterON && skipON && limitON)
            {
                return _context.Questions.Include(q => q.Choices).Where(x => x.QuestionName.ToLower().Contains(_filter.ToLower()) || x.Choices
                  .Any(c => c.ChoiceName.ToLower().Contains(_filter.ToLower())))
                    .Skip((int)_offset).Take((int)_limit).ToList();
            }
            else if (filterON && skipON)
            {
                return _context.Questions.Include(q => q.Choices).Where(x => x.QuestionName.ToLower().Contains(_filter.ToLower()) || x.Choices
                  .Any(c => c.ChoiceName.ToLower().Contains(_filter.ToLower())))
                    .Skip((int)_offset).ToList();
            }
            else if (filterON && limitON)
            {
                return _context.Questions.Include(q => q.Choices).Where(x => x.QuestionName.ToLower().Contains(_filter.ToLower()) || x.Choices
                  .Any(c => c.ChoiceName.ToLower().Contains(_filter.ToLower())))
                    .Take((int)_limit).ToList();

            }
            else if (filterON)
            {
                return _context.Questions.Include(m => m.Choices).Where(x => x.QuestionName.ToLower().Contains(_filter.ToLower()) || x.Choices
                  .Any(c => c.ChoiceName.ToLower().Contains(_filter.ToLower()))).ToList();
            }
            else if (skipON && limitON)
            {
                return _context.Questions.Include(q => q.Choices).Skip((int)_offset).Take((int)_limit).ToList();
            }
            else if (skipON)
            {
                return _context.Questions.Include(q => q.Choices).Skip((int)_offset).ToList();
            }
            else if (limitON)
            {
                return _context.Questions.Include(q => q.Choices).Take((int)_limit).ToList();
            }

            return _context.Questions.Include(q => q.Choices).ToList();
        }

        public void ProcessUpdateQuestionAndChoices(Question existingQuestion, Question _question, int questionID)
        {
            if (existingQuestion != null)
            {
                // Update Question
                UpdateQuestion(existingQuestion, _question);

                // Delete Choice
                DeleteChoice(existingQuestion, questionID);

                // Update and Insert Choice
                foreach (var choiceModel in _question.Choices.ToList())
                {
                    var existingChoice = _context.Choices
                        .Where(c => c.Question.QuestionId == questionID
                        && c.ChoiceName == choiceModel.ChoiceName && c.ChoiceId != default(int))
                        .SingleOrDefault();

                    if (existingChoice != null)
                    {
                        // Update Choice
                        UpdateChoice(existingChoice, choiceModel);
                    }
                    else
                    {
                        // Insert Choice
                        var choice = AddChoice(choiceModel);
                        existingQuestion.Choices.Add(choice);
                        _context.SaveChanges();
                    }
                }
              
            }
        }

        public void UpdateQuestion(Question existingQuestion, Question _question)
        {
            _context.Entry(existingQuestion).CurrentValues.SetValues(_question);
            _context.SaveChanges();
        }
        public void DeleteChoice(Question existingQuestion, int questionID)
        {
            foreach (var choice in existingQuestion.Choices.ToList())
            {
                if (!existingQuestion.Choices
                    .Any(c => c.Question.QuestionId == questionID && c.ChoiceName == choice.ChoiceName))
                    _context.Choices.Remove(choice);
            }

            _context.SaveChanges();
        }

        public Choice AddChoice(Choice choiceModel)
        {
            var choice = new Choice
            {
                ChoiceName = choiceModel.ChoiceName,
                Votes = choiceModel.Votes
            };
            return choice;
        }

        public void UpdateChoice(Choice existingChoice, Choice choiceModel)
        {
            choiceModel.ChoiceId = existingChoice.ChoiceId;
            _context.Entry(existingChoice).CurrentValues.SetValues(choiceModel);
            _context.SaveChanges();
        }

    }
}
