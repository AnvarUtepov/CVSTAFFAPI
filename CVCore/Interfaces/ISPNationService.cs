using CVCore.Entities;
using CVCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CVCore.Interfaces
{
    public interface ISPNationService
    {
        Task<QueryResult<SPNation>> GetSPNationViewList(TableMetaData tableMetaData);
        SPNation GetById(int Id);
        Task<int> SaveUpdateSPNation(SPNation item);
        Task<int> DeleteSPNation(int Id);
    }
}
