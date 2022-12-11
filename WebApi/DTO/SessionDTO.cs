using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.DTO
{
    public class SessionDTO
    {
        public string SessionId { get; set; }
        public int BlockId { get; set; }
        public string UserId { get; set; }
        public string MentorId { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
    }
}