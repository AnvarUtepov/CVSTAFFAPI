using CVCore.Entities;
using CVCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CVCore.Services
{
    public interface ISPEducationService
    {
        Task<QueryResult<SPEducation>> GetSPEducationViewList(TableMetaData tableMetaData);
        SPEducation GetById(int Id);
        Task<int> SaveUpdateSPEducation(SPEducation item);
        Task<int> DeleteSPEducation(int Id);
        Task<List<SelectItem>> GetSPEducationSelectItems();
    }
}