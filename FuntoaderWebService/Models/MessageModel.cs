using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FuntoaderWebService.Models
{
    public class MessageModel: EntityBase
    {
        public string messageContent { get; private set; }
        public byte[] messageBuffer { get; private set; }
        public DateTime time { get; set; }

        public MessageModel(string m)
        {
            messageContent = m;
            messageBuffer = Encoding.ASCII.GetBytes(m);
            time = DateTime.Now.ToUniversalTime();
        }

    }
}