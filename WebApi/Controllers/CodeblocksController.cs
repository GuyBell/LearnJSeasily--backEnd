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
    [RoutePrefix("CodeBlocks")]
    public class CodeblocksController : ApiController
    {
        [HttpGet]
        [Route("GetAllCodeTitles")]
        public IHttpActionResult GetAllCodeTitles()
        {
            try
            {
                moveoTaskDbContext db = new moveoTaskDbContext();
                if (db.tblCodeBlock.Count() == 0)
                {
                    return Content(HttpStatusCode.NotFound, $"Sorry, there is no code bloks in the DB");
                }
                List<CodeBlockDTO> CodeTitles = new List<CodeBlockDTO>();
                if (db.tblCodeBlock != null)
                {
                    foreach (tblCodeBlock block in db.tblCodeBlock)
                    {
                        CodeBlockDTO title = new CodeBlockDTO();
                        title.BlockId = block.BlockId;
                        title.Title = block.Title;
                        CodeTitles.Add(title);
                    }
                    return Content(HttpStatusCode.OK, CodeTitles);
                }
                else
                {
                    return Content(HttpStatusCode.NotFound, "Sorry there are no correct codeblock in the system!");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Sorry something wrong! " + ex.Message);
            }
        }
    }
}