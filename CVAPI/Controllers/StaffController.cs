using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVCore.Entities;
using CVCore.Services;
using CVCore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CVAPI.Controllers
{   
    public class StaffController : BaseApiController
    {
        private readonly IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService=staffService;
        }
        [HttpPost]
        public async Task<IActionResult> GetList([FromBody] TableMetaData tableMetaData)
        {
            var result = await this._staffService.GetStaffViewList(tableMetaData);
            return Ok(result);
        }
        [HttpGet]
        public IActionResult GetItemById(int Id)
        {
            var result = this._staffService.GetById(Id);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> AddUpdateItem([FromBody] Staff item)
        {
            var result = await this._staffService.SaveUpdateStaff(item);
            return Ok(result);
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteItem(int Id)
        {
            var result = await this._staffService.DeleteStaff(Id);
            return Ok(result);
        }
    }
}