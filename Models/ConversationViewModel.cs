using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whisper.Models
{
    public class ConversationViewModel
    {
        public Conversations Conversation { get; set; }
        public IEnumerable<Messages> Messages { get; set; }

        public Users OtherUser { get; set; }
    }
}