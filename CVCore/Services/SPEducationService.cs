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
    public class SPEducationService:ISPEducationService
    {
 private readonly IRepository _context;        
        public SPEducationService(
            IRepository context)
        {
            _context = context;            
        }

        public async Task<QueryResult<SPEducation>> GetSPEducationViewList(TableMetaData tableMetaData)
        {
            QueryResult<SPEducation> result = new QueryResult<SPEducation>();
            var query = _context.SPEducations.Where(x=>x.Active)
                .AsQueryable();

            //filters            
            if (tableMetaData.filters != null)
            {
                foreach (string itemKey in tableMetaData.filters.Keys)
                {                    
                    string value = tableMetaData.filters[itemKey];
                        switch (itemKey)
                        {
                            case "nameRu":
                                query = _context.SPEducations.Where(x => x.NameRu.Contains(value));
                                break;
                            case "nameUz":
                                query = _context.SPEducations.Where(x => x.NameUz.Contains(value));
                                break;
                        }                    
                }
            }
            //sort            
            var columnsMap = new Dictionary<string, Expression<Func<SPEducation, object>>>()
            {
                ["nameRu"] = v => v.NameRu,
            };
            //По умолчанию
            query = query.ApplyOrdering(tableMetaData, "nameRu", columnsMap);

            result.TotalItems = await query.CountAsync();

            List<SPEducation> datas = await query
                .Skip(tableMetaData.first.Value)
                .Take(tableMetaData.rows.Value).ToListAsync();

            result.Items = datas;
            return result;
        }
        public SPEducation GetById(int Id){
            return  _context.SPEducations.Find(Id);
        }
        public async Task<int> SaveUpdateSPEducation(SPEducation item){
            if(item.Id==0){
                _context.SPEducations.Add(item);
            }
            else
            {
                _context.SPEducations.Update(item);
            }
            return await _context.SaveChangesAsync();            
        }
        public async Task<int> DeleteSPEducation(int Id){
           var item= _context.SPEducations.Find(Id);
           item.Active=false;
           _context.SPEducations.Update(item);
           return await _context.SaveChangesAsync();            
        }
    }
}
