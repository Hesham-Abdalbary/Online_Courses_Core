using Microsoft.EntityFrameworkCore;
using Online_Courses_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Repository
{
    public class MessageRepository
    {
        Context _context;
        public MessageRepository(Context context)
        {
            _context = context;
        }

        public List<Message> GetAll()
        {
            var messages = _context.Messages.Include( m => m.MessageStatus).ToList();
            return messages;
        }

        public Message GetByCustomMessageID(int customMessageId)
        {
            var messages = _context.Messages.Include(m => m.MessageStatus).Where(c => c.CustomMessageID == customMessageId).FirstOrDefault();
            return messages;
        }

        public Message GetByID(int messageId)
        {
            var messages = _context.Messages.Include(m => m.MessageStatus).Where(c => c.MessageID == messageId).FirstOrDefault();
            return messages;
        }

        public int Add(Message message)
        {
            _context.Messages.Add(message);
            var addMessageResult = _context.SaveChanges();
            return addMessageResult;
        }

        public int Update(Message message)
        {
            _context.Messages.Update(message);
            return _context.SaveChanges();
        }

        public int Delete(int messageId)
        {
            _context.Messages.Remove(new Message() { MessageID = messageId });
            return _context.SaveChanges();
        }
    }
}
