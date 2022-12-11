using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.DTO
{
    public class StudentLoginDetailsDTO
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Uuid { get; set; }
    }
}