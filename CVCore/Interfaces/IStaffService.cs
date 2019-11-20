using CVCore.Entities;
using CVCore.ViewModels;
using System.Threading.Tasks;

namespace CVCore.Services
{
    public interface IStaffService
    {
        Task<QueryResult<StaffListViewModel>> GetStaffViewList(TableMetaData tableMetaData);
        StaffViewModel GetById(int Id);
        Task<int> SaveUpdateStaff(Staff item);
        Task<int> DeleteStaff(int Id);
    }
}