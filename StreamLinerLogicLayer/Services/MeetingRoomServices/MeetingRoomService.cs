using AutoMapper;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerRepositoryLayer.IRepositories;
using StreamLinerViewModelLayer.ModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StreamLinerLogicLayer.Services.MeetingRoomServices
{
    public class MeetingRoomService : IMeetingRoom
    {
        private readonly IGenericRepository<MeetingRoom> _MeetingRoomRepository;
        private readonly IMapper _mapper;
        public MeetingRoomService(IGenericRepository<MeetingRoom> MeetingRoomRepository, IMapper mapper)
        {
            _MeetingRoomRepository = MeetingRoomRepository;
            _mapper = mapper;

        }
        public async Task<bool> AddMeetingRoom(MeetingRoomDTO meetingroomDTO)
        {
            try
            {

                MeetingRoom meetingroom = _mapper.Map<MeetingRoom>(meetingroomDTO);
               // Field field = _mapper.Map<Field>(fieldDTO);

                await _MeetingRoomRepository.AddAsync(meetingroom);
                await _MeetingRoomRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in Add Meeting Room : {ex.Message}");
                return false;
            }
        }

        public async  Task DeleteMeetingRoom(int id)
        {
            try
            {
                var meetingRoomDB = _MeetingRoomRepository.GetByIdAsync(id).Result;
                if (meetingRoomDB == null) throw new Exception("Entity not found");

                _MeetingRoomRepository.Delete(meetingRoomDB);
                await _MeetingRoomRepository.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting field: {ex.Message}");

            }
        }

        public Task<List<MeetingRoomDTO>> GetAllMeetingRooms()
        {
            var Meetingrooms = _MeetingRoomRepository.GetAllAsync().Result.ToList();
            if (Meetingrooms == null || Meetingrooms.Count == 0)
            {
               
            }

            var Meetingslist = _mapper.Map<List<MeetingRoomDTO>>(Meetingrooms);
            return Task.FromResult(Meetingslist);
        }

        public Task<MeetingRoomDTO> GetMeetingRoomById(int id)
        {
            var meetingRoom = _MeetingRoomRepository.GetByIdAsync(id).Result;
        
            if (meetingRoom == null)
                return null;
            MeetingRoomDTO meetingRoomDTO = _mapper.Map<MeetingRoomDTO>(meetingRoom);

            return Task.FromResult(meetingRoomDTO);
        }

        public async  Task<bool> UpdateMeetingRoom(MeetingRoomDTO meetingroomDTO)
        {
            try
            {


                MeetingRoom meetingroom = _mapper.Map<MeetingRoom>(meetingroomDTO);

                _MeetingRoomRepository.Update(meetingroom);
                  _MeetingRoomRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in Update Meeting Room: {ex.Message}");
                return false;
            }
        }
    }
}
