using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;
using System;

namespace StreamLinerLogicLayer.Services.MetaDataTemplateServices
{
    public class MetaDataTemplateService : IMetaDataTemplateService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<MetaDataTemplateField> _metaDataTemplateField;

        public MetaDataTemplateService(ApplicationDbContext context, IGenericRepository<MetaDataTemplateField> metaDataTemplateField)
        {
            _context = context;
            _metaDataTemplateField = metaDataTemplateField;
        }

        public async Task<bool> AddField(MetaDataTemplateField model)
        {
            _metaDataTemplateField.AddAsync(model);
            await _metaDataTemplateField.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteField(int id, int MetaDataTemplateId)
        {
            MetaDataTemplateField model =await _metaDataTemplateField
                .GetFindAsync(m=>m.FieldId == id && m.MetaDataTemplateId == MetaDataTemplateId);
            _metaDataTemplateField.Delete(model);
            await _metaDataTemplateField.SaveChangesAsync();
            return true;
        }

        

        public async Task<List<Field>> GetFieldsByTemplateIdAsync(int templateId)
        {
            return await _context.MetaDataTemplateFields
                .Where(mtf => mtf.MetaDataTemplateId == templateId)
                .Include(mtf => mtf.Field)
                .Select(mtf => mtf.Field)
                .ToListAsync();
        }

      
    }
}
