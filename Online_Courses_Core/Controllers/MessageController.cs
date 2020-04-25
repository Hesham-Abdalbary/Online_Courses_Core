using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Online_Courses_Core.Service;
using Online_Courses_Core.ViewModels;

namespace Online_Courses_Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private MessageService _messageService;
        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<MessageDto>> Get()
        {
            return _messageService.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<MessageDto> Get(int id)
        {
            return _messageService.GetByID(id);
        }

        // GET api/values/5
        [HttpGet("usermessage/{customId}")]
        public ActionResult<MessageDto> GetByCustomId(int customId)
        {
            return _messageService.GetByCustomId(customId);
        }

        // POST api/values
        [HttpPost]
        public ActionResult<int> Post([FromBody]MessageDto model)
        {
            return _messageService.Add(model);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<int> Put([FromBody]MessageDto model, int id)
        {
            model.MessageID = id;
            return _messageService.Update(model);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<int> Delete(int id)
        {
            return _messageService.Delete(id);
        }
    }
}