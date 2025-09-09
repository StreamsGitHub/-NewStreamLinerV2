using StreamLinerEntitiesLayer.Entities;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Services.MetaDataTemplateServices
{
    public interface IMetaDataTemplateService
    {
        Task<List<Field>> GetFieldsByTemplateIdAsync(int templateId);
        Task<bool> AddField(MetaDataTemplateField model);
        Task<bool> DeleteField(int  id, int MetaDataTemplateId);

    }
}
