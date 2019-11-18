using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVCore.Entities;
using CVCore.Interfaces;
using CVCore.Services;
using CVCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVAPI.Controllers
{   
    [Authorize]
    public class SPNationController : BaseApiController
    {
        private readonly ISPNationService _spNationService;
        public SPNationController(ISPNationService spNationService)
        {
            _spNationService=spNationService;
        }
        [HttpPost]
        public async Task<IActionResult> GetList([FromBody] TableMetaData tableMetaData)
        {
            var result = await this._spNationService.GetSPNationViewList(tableMetaData);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetItemById(int Id)
        {
            var result = this._spNationService.GetById(Id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> AddUpdateItem([FromBody] SPNation item)
        {
            var result = await this._spNationService.SaveUpdateSPNation(item);
            return Ok(result);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var result = await this._spNationService.DeleteSPNation(Id);
            return Ok(result);
        }
    }
}