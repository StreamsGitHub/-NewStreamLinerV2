
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;


namespace StreamLinerLogicLayer.Services.FieldTypeServices
{
    public class FieldTypeService : IFiedlType
    {
        private readonly IGenericRepository<FieldType> _fieldtype;
        public FieldTypeService(IGenericRepository<FieldType> fieldtype)
        {
            _fieldtype = fieldtype;
        }
        public Task<List<FieldType>> GetAlltypes()
        {
          var fieldTypes = _fieldtype.GetAllAsync().Result.ToList();
            return Task.FromResult(fieldTypes);
        }

        public Task<FieldType> GetTypesById(int id)
        {

            var fieldType = _fieldtype.GetByIdAsync(id).Result;
            if (fieldType == null)
            {
                throw new Exception("Field type not found");
            }
            return  Task.FromResult(fieldType);

        }

       
    }
}
