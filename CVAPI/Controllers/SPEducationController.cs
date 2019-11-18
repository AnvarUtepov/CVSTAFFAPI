using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVCore.Entities;
using CVCore.Services;
using CVCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVAPI.Controllers
{   
    [Authorize]
    public class SPEducationController : BaseApiController
    {
        private readonly ISPEducationService _spEducationService;
        public SPEducationController(ISPEducationService spEducationService)
        {
            _spEducationService=spEducationService;
        }
        [HttpPost]
        public async Task<IActionResult> GetList([FromBody] TableMetaData tableMetaData)
        {
            var result = await this._spEducationService.GetSPEducationViewList(tableMetaData);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetItemById(int Id)
        {
            var result = this._spEducationService.GetById(Id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> AddUpdateItem([FromBody] SPEducation item)
        {
            var result = await this._spEducationService.SaveUpdateSPEducation(item);
            return Ok(result);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var result = await this._spEducationService.DeleteSPEducation(Id);
            return Ok(result);
        }
    }
}