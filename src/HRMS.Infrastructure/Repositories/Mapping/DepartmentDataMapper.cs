using HRMS.Domain.Entities;
using System.Data.Common;
namespace HRMS.Infrastructure.Repositories.Mapping
{
    public class DepartmentDataMapper
    {
        public static Department MapReaderToDepartment(DbDataReader reader)
        {
            return new Department
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}