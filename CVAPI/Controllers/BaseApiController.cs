﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}