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

        public async Task<QueryResult<StaffListViewModel>> GetStaffViewList(TableMetaData tableMetaData)
        {
            QueryResult<StaffListViewModel> result = new QueryResult<StaffListViewModel>();
            var query = _context.Staffs                
                .Include(x=>x.Educations)
                    .ThenInclude(x=>x.SPEducation)
                .Include(x=>x.SPNation)
                .Where(x=>x.Active)
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
                            case "phone":
                                query = query.Where(x => x.Phone.Contains(value));
                                break;
                            case "email":
                                query = query.Where(x => x.Email.Contains(value));
                                break;
                        }                    
                }
            }
            //sort            
            var columnsMap = new Dictionary<string, Expression<Func<Staff, object>>>()
            {
                ["id"] = v => v.Id,
                ["fio"] = v => v.FIO,
                ["phone"] = v => v.Phone,
                ["email"] = v => v.Email,
            };
            //По умолчанию
            query = query.ApplyOrdering(tableMetaData, "fio", columnsMap);

            result.TotalItems = await query.CountAsync();

            List<Staff> datas = await query
                .Skip(tableMetaData.first.Value)
                .Take(tableMetaData.rows.Value)
                .ToListAsync();

            result.Items = datas.Select(x=>new StaffListViewModel(){ 
                    Id=x.Id,
                    FIO=x.FIO,
                    BirthDate=x.BirthDate.ToString("dd.MM.yyyy")+" г.",
                    Educations=x.Educations
                        .Select(e=>$"{e.YearOfDone} - {e.Place}, {e.SPEducation.NameRu}")
                        .ToArray(),
                    SPNation=x.SPNation.NameRu,
                    Phone=x.Phone,
                    Email=x.Email
                }).ToList();;
            return result;
        }
        public StaffViewModel GetById(int Id){
            return  _context.Staffs
                .Include(x=>x.Educations)
                .Include(x=>x.Jobs)
                .Select(x=>new StaffViewModel{ 
                    Id=x.Id,
                    FIO=x.FIO,
                    BirthDate=x.BirthDate.ToString("MM/dd/yyyy"),
                    SPNationId=x.SPNationId,
                    Phone=x.Phone,
                    Email=x.Email,
                    Educations=x.Educations.ToList(),
                    Jobs=x.Jobs.ToList()
                 })
                .FirstOrDefault(x=>x.Id==Id);
        }
        public async Task<int> SaveUpdateStaff(Staff item){
            //adding new educations
            var newEducations=item.Educations.Where(x=>x.StaffId==0).ToList();
            foreach (var itemEducation in newEducations)
            {
                item.Educations.Remove(itemEducation);
                itemEducation.StaffId=item.Id;
                itemEducation.Id=0;
                item.Educations.Add(itemEducation);
            }            
            //adding new jobs
            var newJobs=item.Jobs.Where(x=>x.StaffId==0).ToList();
            foreach (var itemJob in newJobs)
            {
                item.Jobs.Remove(itemJob);
                itemJob.StaffId=item.Id;
                itemJob.Id=0;
                item.Jobs.Add(itemJob);
            }
            
            if(item.Id==0){

                _context.Staffs.Add(item);
            }
            else
            {     
                //removing deleted educations
                var existingEducationsIds=item.Educations.Select(x=>x.Id).ToArray();
                var removingEducations=_context.Educations
                    .Where(x=>x.StaffId==item.Id && !existingEducationsIds.Contains(x.Id))
                    .ToList();
                _context.Educations.RemoveRange(removingEducations);
                //removing deleted jobs
                var existingJobsIds=item.Jobs.Select(x=>x.Id).ToArray();
                var removingJobs=_context.Jobs
                    .Where(x=>x.StaffId==item.Id && !existingJobsIds.Contains(x.Id))
                    .ToList();
                _context.Jobs.RemoveRange(removingJobs);

                _context.Staffs.Attach(item);
                //_context.Educations.Attach(item.Educations.First(x=>x.Id==2));
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
