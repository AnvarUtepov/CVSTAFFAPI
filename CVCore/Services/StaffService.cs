using CVCore.Entities;
using CVCore.Extensions;
using CVCore.Interfaces;
using CVCore.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CVCore.Services
{
    public class StaffService:IStaffService
    {
        private readonly IRepository _context;        
        public StaffService(
            IRepository context)
        {
            _context = context;            
        }

        public async Task<QueryResult<Staff>> GetStaffViewList(TableMetaData tableMetaData)
        {
            QueryResult<Staff> result = new QueryResult<Staff>();
            var query = _context.Staffs.Where(x=>x.Active)
                .AsQueryable();

            //filters            
            if (tableMetaData.filters != null)
            {
                foreach (string itemKey in tableMetaData.filters.Keys)
                {                    
                    string value = tableMetaData.filters[itemKey];
                        switch (itemKey)
                        {
                            case "fio":
                                query = query.Where(x => x.FIO.Contains(value));
                                break;
                            case "Phone":
                                query = query.Where(x => x.Phone.Contains(value));
                                break;
                        }                    
                }
            }
            //sort            
            var columnsMap = new Dictionary<string, Expression<Func<Staff, object>>>()
            {
                ["fio"] = v => v.FIO,
            };
            //По умолчанию
            query = query.ApplyOrdering(tableMetaData, "fio", columnsMap);

            result.TotalItems = await query.CountAsync();

            List<Staff> datas = await query
                .Skip(tableMetaData.first.Value)
                .Take(tableMetaData.rows.Value).ToListAsync();

            result.Items = datas;
            return result;
        }
        public Staff GetById(int Id){
            return  _context.Staffs.Find(Id);
        }
        public async Task<int> SaveUpdateStaff(Staff item){
            if(item.Id==0){
                _context.Staffs.Add(item);
            }
            else
            {
                _context.Staffs.Update(item);
            }
            return await _context.SaveChangesAsync();            
        }
        public async Task<int> DeleteStaff(int Id){
           var item= _context.Staffs.Find(Id);
           item.Active=false;
           _context.Staffs.Update(item);
           return await _context.SaveChangesAsync();            
        }
    }
}
