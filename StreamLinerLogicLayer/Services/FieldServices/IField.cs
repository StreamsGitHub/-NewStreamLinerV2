using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Services.FieldServices
{
    public interface IField
    {
        Task<FieldDTO> GetFieldById(int id);
        Task<List<FieldDTO>> GetAllField();

        Task<bool> AddField(FieldDTO fieldDTO);
        Task<bool> UpdateField(FieldDTO fieldDTO);
        Task DeleteField(int id);

      

    }
}
