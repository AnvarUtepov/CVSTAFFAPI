using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVAPI.Controllers
{    
    public class MetaController : BaseApiController
    {
        public ActionResult<string> Version()
        {
            var assembly = typeof(Startup).Assembly;
            //var creationDate = System.IO.File.GetCreationTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            return Ok(new {version});
        }
        [HttpGet("/info")]
        public ActionResult<string> Info()
        {            
            var assembly = typeof(Startup).Assembly;
            var creationDate = System.IO.File.GetCreationTime(assembly.Location);
            var version = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
            return Ok($"Version: {version}, Last Updated: {creationDate}");
        }
        [HttpGet]
        public object GetTime()
        {
            return DateTime.UtcNow;
        }
        [HttpGet("/error")]
        public ActionResult<string> Error()
        {
            throw new Exception();            
        }
    }
}