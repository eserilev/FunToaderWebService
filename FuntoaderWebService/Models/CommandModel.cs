using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FuntoaderWebService.Models
{
    public abstract class CommandModel
    {
        public CommandType commandType { get; private set; }
        public MessageModel message { get; set; }
        public const string fieldDelimeter = ":";

        public CommandModel(CommandType type)
        {
            commandType = type;
        }

        public abstract void CreateMessage(string value);

        
    }

    public enum CommandType
    {
        COLOR,
        VIDEO,
        AUDIO,
        IMAGE
    }
}