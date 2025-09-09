using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerLogicLayer.Services.MeetingRoomServices
{
    public interface IMeetingRoom
    {
        Task<MeetingRoomDTO> GetMeetingRoomById(int id);
        Task<List<MeetingRoomDTO>> GetAllMeetingRooms();
        Task<bool> AddMeetingRoom(MeetingRoomDTO meetingDTO);
        Task<bool> UpdateMeetingRoom(MeetingRoomDTO meetingDTO);
        Task DeleteMeetingRoom(int id);
    }
}
