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
using Newtonsoft.Json;

namespace CVCore.Services
{
    public class SPNationService:ISPNationService
    {
        private readonly IRepository _context;        
        public SPNationService(
            IRepository context)
        {
            _context = context;            
        }

        public async Task<QueryResult<SPNation>> GetSPNationViewList(TableMetaData tableMetaData)
        {
            QueryResult<SPNation> result = new QueryResult<SPNation>();
            var query = _context.SPNations.Where(x=>x.Active)
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
                                query = _context.SPNations.Where(x => x.NameRu.Contains(value));
                                break;
                            case "nameUz":
                                query = _context.SPNations.Where(x => x.NameUz.Contains(value));
                                break;
                        }                    
                }
            }
            //sort            
            var columnsMap = new Dictionary<string, Expression<Func<SPNation, object>>>()
            {
                ["nameRu"] = v => v.NameRu,
            };
            //По умолчанию
            query = query.ApplyOrdering(tableMetaData, "nameRu", columnsMap);

            result.TotalItems = await query.CountAsync();

            List<SPNation> datas = await query.Skip(tableMetaData.first.Value).Take(tableMetaData.rows.Value).ToListAsync();

            result.Items = datas;
            return result;
        }
        public SPNation GetById(int Id){
            return  _context.SPNations.Find(Id);
        }
        public async Task<int> SaveUpdateSPNation(SPNation item){
            if(item.Id==0){
                _context.SPNations.Add(item);
            }
            else
            {
                _context.SPNations.Update(item);
            }
            return await _context.SaveChangesAsync();            
        }
        public async Task<int> DeleteSPNation(int Id){
           var item= _context.SPNations.Find(Id);
           item.Active=false;
           _context.SPNations.Update(item);
           return await _context.SaveChangesAsync();            
        }

    }
}
