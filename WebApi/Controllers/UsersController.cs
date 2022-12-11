using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataMoveoTask;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [RoutePrefix("Users")]
    public class UsersController : ApiController
    {
        [HttpPost]
        [Route("LoginUser")]
        public IHttpActionResult PostFindUser([FromBody] LoginDetailsDTO user)
        {
            try
            {
                moveoTaskDbContext db = new moveoTaskDbContext();
                if (db.tblUsers.Count() == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Sorry, there is no users in the DB");
                }
                tblUsers checkMentor = db.tblUsers.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password && u.Type == "M");
                UserDTO userToReturn = new UserDTO();
                userToReturn.FullName = checkMentor.FullName;
                userToReturn.UserId = checkMentor.UserId;
                userToReturn.Email = checkMentor.Email;
                userToReturn.Type = checkMentor.Type;

                if (checkMentor != null)
                {
                    return Content(HttpStatusCode.OK, userToReturn);
                }
                else
                {
                    tblUsers checkStudent = db.tblUsers.SingleOrDefault(s => s.Email == user.Email && s.Password == user.Password && s.Type == "S");
                    if (checkStudent != null)
                    {
                        return Content(HttpStatusCode.OK, userToReturn);
                    }
                }
                return Content(HttpStatusCode.NotFound, $"Sorry, there is no user with this login details");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Something wrong! " + ex.Message);
            }
        }

        [HttpPost]
        [Route("LoginStudentUser")]
        public IHttpActionResult PostFindStudentUser([FromBody] StudentLoginDetailsDTO studentDetails)
        {
            try
            {
                moveoTaskDbContext db = new moveoTaskDbContext();
                if (db.tblUsers.Count() == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Sorry, there is no users in the DB");
                }

                tblSessins checkStudent = db.tblSessins.SingleOrDefault(s => s.SessionId == studentDetails.Uuid && s.UserId == studentDetails.UserId && s.tblUsers.Password == studentDetails.Password);
                if (checkStudent != null)
                {
                    return Content(HttpStatusCode.OK, "approved student");
                }
                return Content(HttpStatusCode.NotFound, $"Sorry, there is no student with this login details");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Something wrong! " + ex.Message);
            }
        }


        [HttpGet]
        [Route("getAllStudentsUsers")]
        public IHttpActionResult GetAllStudentsUsers()
        {
            try
            {
                moveoTaskDbContext db = new moveoTaskDbContext();
                if (db.tblUsers.Count() == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Sorry, there is no users in the DB");
                }
                List<UserDTO> StudentUsers = new List<UserDTO>();
                if (db.tblUsers != null)
                {
                    foreach (tblUsers user in db.tblUsers)
                    {
                        if (user.Type == "S")
                        {
                            UserDTO user_dto = new UserDTO();
                            user_dto.UserId = user.UserId;
                            user_dto.FullName = user.FullName;
                            user_dto.Email = user.Email;
                            user_dto.Type = user.Type;
                            StudentUsers.Add(user_dto);
                        }
                    }
                }
                if (StudentUsers.Count > 0)
                {
                    return Content(HttpStatusCode.OK, StudentUsers);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Sorry there are no correct Users in the system!");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Sorry something wrong! " + ex.Message);
            }
        }

    }
}