using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StreamLinerEntitiesLayer.Entities;
using StreamLinerLogicLayer.Services.MeetingRoomServices;
using StreamLinerViewModelLayer.ModelDTO;

namespace StreamLinerApp.Areas.scp.Controllers
{
    [Area("scp")]
    public class MeetingRoomController : Controller
    {
        private readonly IMeetingRoom _meeting;
       
        private readonly IMapper _mapper;
        string app = "Admin";
        string ctrl = " Meeting Rooms";
        public MeetingRoomController(IMeetingRoom meeting, IMapper mapper)
        {
            _meeting = meeting;
            _mapper = mapper;
           

        }
     
        public async Task<IActionResult> Index()
        {
            ViewData["application"] = app;
            ViewData["action"] = " ";
            ViewData["controller"] =ctrl;
           var meetings = await _meeting.GetAllMeetingRooms();
            return View(meetings);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["application"] = app;
            ViewData["action"] = " / New";
            ViewData["controller"] = ctrl;
       

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingRoomDTO meetingRoom)
        {

            if (ModelState.IsValid)
            {
                await _meeting.AddMeetingRoom(meetingRoom);
                return RedirectToAction("Index");
            }

           

            return View(meetingRoom);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();
            ViewData["application"] = app;
            ViewData["action"] = " / Edit";
            ViewData["controller"] = ctrl;
            var meetingRoom = await _meeting.GetMeetingRoomById(id);
           


            return View(meetingRoom);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MeetingRoomDTO meetingRoomDTO)
        {

            if (ModelState.IsValid)
            {



                var result = _meeting.UpdateMeetingRoom(meetingRoomDTO);

                return RedirectToAction("Index");

            }
            ModelState.AddModelError("", "Error updating field");
           
            return View(meetingRoomDTO);


        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();
            ViewData["application"] = app;
            ViewData["action"] = " / Delete";
            ViewData["controller"] = ctrl;
            var meetingroom = await _meeting.GetMeetingRoomById(id);
        

            return View(meetingroom);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _meeting.DeleteMeetingRoom(id);

            return RedirectToAction("Index");
        }
    }
}
