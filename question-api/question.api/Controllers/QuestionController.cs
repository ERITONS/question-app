using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using question.domain;
using question.service;

namespace question.api.Controllers
{
    [Route("/questions")]
    [ApiController]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _service;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        // GET: Question
        [HttpGet]
        public IActionResult GetQuestionByQuery([FromQueryAttribute] string filter, [FromQueryAttribute] int offset, [FromQueryAttribute] int limit)
        {

            var list = _service.GetQuestion(filter, offset, limit);
            string result = JsonConvert.SerializeObject(list);
            return Ok(result);
        }

        // GET: Question/5
        [HttpGet("{Id}")]
        public IActionResult GetQuestionByID(int Id)
        {
            var list = _service.GetQuestion(Id);
            string result = JsonConvert.SerializeObject(list);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(QuestionDTO questionDto)
        {
            var question = _mapper.Map<Question>(questionDto);

            _service.PostQuestion(question);
            string result = JsonConvert.SerializeObject(question);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put(Question question)
        {
            _service.PutQuestion(question);
            string result = JsonConvert.SerializeObject(question);
            return Ok(result);
        }

    }
}