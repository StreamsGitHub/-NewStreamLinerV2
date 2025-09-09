using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;
using System.Linq.Expressions;


namespace StreamLinerLogicLayer.Services.FieldServices
{
    public class FieldService : IField
    {
        private readonly IGenericRepository<Field> _FieldRepository;
        private readonly IMapper _mapper;
        
        public FieldService(IGenericRepository<Field> FiledRepository, IMapper mapper  )
        {
            _FieldRepository = FiledRepository;
            _mapper = mapper;
             
        }

        public async Task<bool> AddField(FieldDTO fieldDTO)
        {

            try
            {

                Field field = _mapper.Map<Field>(fieldDTO);


                await _FieldRepository.AddAsync(field);
                await _FieldRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in AddField: {ex.Message}");
                return false;
            }
        }




      

        public Task<List<FieldDTO>> GetAllField()
        {
            var Fields = _FieldRepository.GetAllIncludingAsync(new Expression<Func<Field, object>>[] { x => x.Type }).Result.ToList();
            if (Fields == null || Fields.Count == 0)
            {
               // throw new Exception("Entity not found");
            }

            var Fieldslist = _mapper.Map<List<FieldDTO>>(Fields);
            return Task.FromResult(Fieldslist);
        }

        public Task<FieldDTO> GetFieldById(int id)
        {
            var field = _FieldRepository.GetByIdAsync(id).Result;
            //  var field = _FieldRepository.GetByIdExpressAsync(id, new Expression<Func<Field, object>>[] { x => x.Type }).Result;

            if (field == null)
                return null;
            FieldDTO fielddto = _mapper.Map<FieldDTO>(field);

            return Task.FromResult(fielddto);



        }

        public async Task<bool> UpdateField(FieldDTO fieldDTO)
        {
            try
            {


                Field field = _mapper.Map<Field>(fieldDTO);

                _FieldRepository.Update(field);
                await _FieldRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in Updatefields: {ex.Message}");
                return false;
            }
        }

        public async Task DeleteField(int id)
        {
     
            try
            {
                var FieldDB =  _FieldRepository.GetByIdAsync(id).Result;
                if (FieldDB == null) throw new Exception("Entity not found");

                 _FieldRepository.Delete(FieldDB);
                await _FieldRepository.SaveChangesAsync(); 


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting field: {ex.Message}");

            }

        }

       


    }
}
