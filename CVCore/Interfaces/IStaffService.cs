using CVCore.Entities;
using CVCore.ViewModels;
using System.Threading.Tasks;

namespace CVCore.Services
{
    public interface IStaffService
    {
        Task<QueryResult<Staff>> GetStaffViewList(TableMetaData tableMetaData);
        Staff GetById(int Id);
        Task<int> SaveUpdateStaff(Staff item);
        Task<int> DeleteStaff(int Id);
    }
}