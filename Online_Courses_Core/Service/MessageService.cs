using AutoMapper;
using Online_Courses_Core.Models;
using Online_Courses_Core.Repository;
using Online_Courses_Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Courses_Core.Service
{
    public class MessageService
    {
        private MessageRepository _repo;
        public MessageService(MessageRepository repo)
        {
            _repo = repo;
        }

        public List<MessageDto> GetAll()
        {
            var messages = _repo.GetAll();
            var messagesModels = Mapper.Map<List<Message>, List<MessageDto>>(messages);
            return messagesModels;
        }

        public MessageDto GetByID(int messageID)
        {
            var messageEntity = _repo.GetByID(messageID);
            var messageModel = Mapper.Map<Message, MessageDto>(messageEntity);
            return messageModel;
        }

        public MessageDto GetByCustomId(int customMessageID)
        {
            var messageEntity = _repo.GetByCustomMessageID(customMessageID);
            var messageModel = Mapper.Map<Message, MessageDto>(messageEntity);
            return messageModel;
        }

        public int Add(MessageDto messageDto)
        {
            var messageEntity = Mapper.Map<MessageDto, Message>(messageDto);
            var addMessageResult = _repo.Add(messageEntity);
            return addMessageResult;
        }

        public int Update(MessageDto messageDto)
        {
            var messageEntity = Mapper.Map<MessageDto, Message>(messageDto);
            var updateMessageResult = _repo.Update(messageEntity);
            return updateMessageResult;
        }

        public int Delete(int messageID)
        {
            var deleteResult = _repo.Delete(messageID);
            return deleteResult;
        }
    }
}
