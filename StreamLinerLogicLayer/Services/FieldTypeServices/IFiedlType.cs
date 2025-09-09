using StreamLinerEntitiesLayer.Entities;

namespace StreamLinerLogicLayer.Services.FieldTypeServices
{

    public interface IFiedlType
    {
        Task<FieldType> GetTypesById(int id);
        Task<List<FieldType>> GetAlltypes();
    }
}
