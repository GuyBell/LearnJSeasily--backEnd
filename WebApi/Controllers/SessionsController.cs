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
    [RoutePrefix("Sessins")]
    public class SessionsController : ApiController
    {
        [HttpPost]
        [Route("CreateSession")]
        public IHttpActionResult PostFindUser([FromBody] SessionDTO sessionDetails)
        {
            try
            {
                moveoTaskDbContext db = new moveoTaskDbContext();
                if (sessionDetails != null)
                {
                    tblSessins NewSessionToCreate = new tblSessins()
                    {
                        SessionId = sessionDetails.SessionId,
                        BlockId = sessionDetails.BlockId,
                        UserId = sessionDetails.UserId,
                        MentorId = sessionDetails.MentorId,
                    };
                    db.tblSessins.Add(NewSessionToCreate);
                    db.SaveChanges();
                    return Created(new Uri(Request.RequestUri.AbsoluteUri + "/" + NewSessionToCreate.MentorId), NewSessionToCreate);
                }
                return Content(HttpStatusCode.NotFound, "Sorry Invalid details!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Something wrong! " + ex.Message);
            }
        }


        [HttpGet]
        [Route("GetDetailsSession/{uuid}")]
        public IHttpActionResult GetDetailsSession(string uuid)
        {
            try
            {
                moveoTaskDbContext db = new moveoTaskDbContext();
                if (uuid != null)
                {
                    tblSessins session = db.tblSessins.SingleOrDefault(s => s.SessionId == uuid);
                    DetailsSessionToClientDTO detailesToClient = new DetailsSessionToClientDTO();
                    detailesToClient.BlockId = session.tblCodeBlock.BlockId;
                    detailesToClient.Title = session.tblCodeBlock.Title;
                    detailesToClient.Code = session.tblCodeBlock.Code;
                    detailesToClient.CorrectCode = session.tblCodeBlock.CorrectCode;
                    return Content(HttpStatusCode.OK, detailesToClient);
                }
                return Content(HttpStatusCode.NotFound, "Sorry Invalid details!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Sorry something wrong! " + ex.Message);
            }
        }
    }
}