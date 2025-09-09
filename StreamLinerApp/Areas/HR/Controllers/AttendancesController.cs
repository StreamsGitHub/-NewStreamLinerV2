using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StreamLinerDataLayer.Data;
using StreamLinerEntitiesLayer.HREntities;
using System.Security.Claims;
using StreamLinerViewModelLayer.HRViewModel;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using Humanizer;
//using UAParser;

namespace StreamLinerApp.Areas.HR.Controllers;
[Area("HR")]
[Authorize]
//[Authorize(Roles = "PowerUser")]
public class AttendancesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    string ControllerName = "Attendances  ";
    string AppName = "HR Apps";
    public AttendancesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        // DateTime today = DateTime.Now;

        var list = await _context.HRAttend
             .Where(a => a.CompanyId == user.CompanyId && a.CheckDate == DateTime.Today)
             .Include(a => a.Partner)
             .OrderByDescending(h => h.HRShiftAttendId)
             .ToListAsync();

        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;

        var applicationDbContext = await _context.UploadFile
            .Where(a => a.CompanyId == user.CompanyId).OrderByDescending(d => d.UploadFileId)
             .ToListAsync();
        return View(applicationDbContext);
    }


    [HttpGet]
    public async Task<IActionResult> Upload(string? id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        ViewData["ex"] = id;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload([Bind("DocumentName,DocumentFile")] UploadFileViewModel uploadFileViewModel)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        if (ModelState.IsValid)
        {
            string fileurlpath = "";

            if (uploadFileViewModel.DocumentFile != null)
            {
                string folder = "Upload/UploadFiles/";
                folder += Guid.NewGuid().ToString() + "-" + uploadFileViewModel.DocumentFile.FileName;
                fileurlpath = folder;
                string serverfolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await uploadFileViewModel.DocumentFile.CopyToAsync(new FileStream(serverfolder, FileMode.Create));

                string extension = Path.GetExtension(serverfolder);
                UploadFile emp = new UploadFile
                {
                    DocumentName = uploadFileViewModel.DocumentName,
                    DocumentFile = fileurlpath,
                    Extension = extension,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    DeletedDate = DateTime.Now,
                    CreateId = user.PartnerId,
                    UpdateId = user.PartnerId,
                    DeleteId = user.PartnerId,
                    Active = true,
                    Deleted = false,
                    Updated = false,
                    Version = 1,
                    CompanyId = user.CompanyId,
                };
                _context.Add(emp);
                await _context.SaveChangesAsync();
                bool results = true;
                var list = new List<FingerPrint>();
                using (var stream = new MemoryStream())
                {
                    await uploadFileViewModel.DocumentFile.CopyToAsync(stream);
                    try
                    {
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowcount = worksheet.Dimension.Rows;
                            for (int row = 2; row < rowcount + 1 ; row++)
                            {
                                string? datetimestring = null;
                                string? ClockIn = null;
                                string? ClockOut = null;
                                DateTime AttendDate = new DateTime();
                                try
                                {
                                    datetimestring = worksheet.Cells[row, 4].Value.ToString().Trim();

                                    // AttendDate = Convert.ToDateTime(datetimestring);
                                    if (datetimestring != null)
                                    {
                                        int lenstring = datetimestring.Length;
                                        int firstslash = datetimestring.IndexOf("/");

                                        string month = datetimestring.Substring(0, firstslash);
                                        string fromexcel2 = datetimestring.Substring(firstslash + 1);
                                        int scondslash = fromexcel2.IndexOf("/");
                                        string day = datetimestring.Substring(firstslash + 1, datetimestring.Substring(firstslash + 1).IndexOf("/"));
                                        string year = fromexcel2.Substring(scondslash + 1, 4);

                                        int datelenth = day.Length + month.Length + 6;
                                        string stringtime = datetimestring.Substring(datelenth, datetimestring.Length - datelenth).Trim();
                                        DateTime timeOnly = Convert.ToDateTime(stringtime);


                                        AttendDate = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day), timeOnly.Hour, timeOnly.Minute, timeOnly.Second);
                                        //string d = day.ToString() + "/" + month.ToString() + "/" + year.ToString() + stringtime;
                                        //AttendDate = Convert.ToDateTime(d);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    return RedirectToRoute(new { action = "Upload", id = ex.Message });
                                }





                                //if (datetimestring != null)
                                //{
                                //    int lenstring = datetimestring.Length;
                                //    int firstslash = datetimestring.IndexOf("/");

                                //    string month = datetimestring.Substring(0, firstslash);
                                //    string fromexcel2 = datetimestring.Substring(firstslash + 1);
                                //    int scondslash = fromexcel2.IndexOf("/");
                                //    string day = datetimestring.Substring(firstslash + 1, datetimestring.Substring(firstslash + 1).IndexOf("/"));
                                //    string year = fromexcel2.Substring(scondslash + 1, 4);


                                //}



                                //DateTime AttendDate = new DateTime();
                                //DateTime attendTime = new DateTime();
                                //DateTime leaveTime = new DateTime();
                                //bool isattend=false;
                                //bool isleave = false;



                                //if (ClockIn != null && ClockIn != "")
                                //{
                                //    try
                                //    {
                                //        attendTime = Convert.ToDateTime(ClockIn);
                                //        isattend = true;
                                //    }
                                //    catch (Exception)
                                //    {
                                //        throw;
                                //    }

                                //}
                                //if (ClockOut != null && ClockOut != "")
                                //{
                                //    try
                                //    {
                                //        leaveTime = Convert.ToDateTime(ClockOut);
                                //        isleave =true;
                                //    }
                                //    catch (Exception)
                                //    {

                                //        throw;
                                //    }


                                FingerPrint record = new FingerPrint()
                                {
                                    ACNo = worksheet.Cells[row, 1].Value.ToString().Trim(),
                                    Number = worksheet.Cells[row, 2].Value.ToString().Trim(),
                                    Name = worksheet.Cells[row, 3].Value.ToString().Trim(),
                                    DateAndTime = AttendDate,
                                    Status = worksheet.Cells[row, 5].Value.ToString().Trim(),
                                    NewState = worksheet.Cells[row, 6].Value.ToString().Trim(),
                                    Exception = worksheet.Cells[row, 7].Value.ToString().Trim(),
                                    Operation = worksheet.Cells[row, 8].Value.ToString().Trim(),
                                    UploadFileId = emp.UploadFileId,
                                    IconUpload = "fa fa-file-excel",
                                    CreateId = user.PartnerId,
                                    UpdateId = user.PartnerId,
                                    DeleteId = user.PartnerId,
                                    CreatedDate = DateTime.Now,
                                    UpdatedDate = DateTime.Now,
                                    DeletedDate = DateTime.Now,
                                    Active = true,
                                    CompanyId = user.CompanyId,
                                    Deleted = false,
                                    Updated = false,
                                    Version = 1,

                                };

                                _context.Add(record);
                                await _context.SaveChangesAsync();
                               
                               var partner = await _context.Partner
                                    .Where(p => p.CompanyId == user.CompanyId && p.FingerPrintId == Convert.ToInt32(record.ACNo)).FirstOrDefaultAsync();

                                                                    // DateTime attendDate, DateTime checkIn, int companyId, int partnerId, int hRShiftAttendId, string icon, int CheckType
                                results = await CheckInAttendFromFile(AttendDate.Date, AttendDate, user.CompanyId, partner.PartnerId, partner.HRShiftAttendId, record.IconUpload, 6);



                                //If Status => c /in Or out then Check IN / OUT
                                // Get Emp = Number
                                // DateAndTime

                                //int FingerPrintNumber = Convert.ToInt32(record.ACNo);
                                //var employeemodel = await _context.Partner
                                //    .Where(e => e.FingerPrintId == FingerPrintNumber && e.CompanyId == user.CompanyId)
                                //    .FirstOrDefaultAsync();

                                //string date1 = record.CheckIn.Replace(" ã", "");
                                //string date2 = record.CheckOut.Replace(" ã", "");
                                //DateTime datetime = Convert.ToDateTime(record.Date);

                                //DateTime justdate = new DateTime(datetime.Year, datetime.Month, datetime.Day);
                                //DateTime justtime = new DateTime(datetime.Hour, datetime.Minute, datetime.Second);



                                //int shiftId = employeemodel.HRShiftAttendId;
                                //var shift = await _context.HRShiftAttend.FindAsync(shiftId);


                                //if (record.CheckIn.ToLower().Contains("in"))
                                //{

                                //    CheckInViewModel checkInViewModel = new CheckInViewModel();
                                //    checkInViewModel.EmployeeId = employeemodel.PartnerId;
                                //    checkInViewModel.CheckIn = datetime;


                                //    TimeSpan startShift = shift.ShiftStart;
                                //    10:30                  10 * 60 = 600 + 30 = 630
                                //    int startstart = startShift.Hours * 60 + startShift.Minutes;
                                //    11:15                         11 * 60 = 660 + 15 = 675
                                //    int attendtime = checkInViewModel.CheckIn.Hour * 60 + checkInViewModel.CheckIn.Minute;

                                //    int LateMin = MinToInt(attendtime, startstart);
                                //    int penaltyint = await PenaltyRole(LateMin);

                                //    string startmnthcode = Convert.ToDateTime(checkInViewModel.CheckIn).ToString("yy")
                                //         + Convert.ToDateTime(checkInViewModel.CheckIn).ToString("MM");

                                //    HRAttendances hRAttendancesin = new HRAttendances
                                //    {
                                //        PartnerId = employeemodel.PartnerId,
                                //        CheckIn = checkInViewModel.CheckIn,
                                //        PenaltyMin = penaltyint,
                                //        HRShiftAttendId = employeemodel.HRShiftAttendId,
                                //        MonthCode = startmnthcode,
                                //    };


                                //    hRAttendancesin.CreateId = uid;
                                //    hRAttendancesin.CreatedDate = DateTime.Now;
                                //    hRAttendancesin.CompanyId = user.CompanyId;
                                //    hRAttendancesin.Active = true;
                                //    _context.Add(hRAttendancesin);
                                //    await _context.SaveChangesAsync();

                                //}
                                //else if (record.CheckOut.ToLower().Contains("out"))
                                //{
                                //    CheckOutViewModel checkOutViewModel = new CheckOutViewModel();
                                //    checkOutViewModel.EmployeeId = employeemodel.PartnerId;
                                //    checkOutViewModel.CheckOut = datetime;

                                //    var employeeattend = await _context.HRAttendance
                                //        .Where(a => a.PartnerId == checkOutViewModel.EmployeeId && a.CheckIn.Date == checkOutViewModel.CheckOut.Date)
                                //        .Include(h => h.HRShiftAttend)
                                //        .FirstOrDefaultAsync();
                                //    if (employeeattend != null)
                                //    {
                                //        int shiftId = employeemodel.HRShiftAttendId;
                                //        var shift2 = await _context.HRShiftAttend.FindAsync(shiftId);
                                //        TimeSpan endShift = shift2.ShiftEnd;

                                //        convert shift end and checkout to min
                                //        int end = endShift.Hours * 60 + endShift.Minutes;
                                //        int checkout = checkOutViewModel.CheckOut.Hour * 60 + checkOutViewModel.CheckOut.Minute;

                                //        call method &takes value(checkin and end)
                                //        int PenaltyMin = CheckInToOut(checkout, end);
                                //        call method PenaltyRole
                                //        int penaltyint2 = await PenaltyRole(PenaltyMin);

                                //        employeeattend.PenaltyMin += penaltyint2;
                                //        employeeattend.CheckOut = checkOutViewModel.CheckOut;
                                //        employeeattend.UpdateId = uid;
                                //        employeeattend.UpdatedDate = DateTime.Now;
                                //        employeeattend.Active = true;

                                //        try
                                //        {
                                //            _context.Update(employeeattend);
                                //            await _context.SaveChangesAsync();
                                //        }
                                //        catch (Exception)
                                //        {

                                //            throw;
                                //        }
                                //    }


                                //}

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return RedirectToRoute(new { action = "Upload", id = ex.Message + " - " + results.ToString() });
                    }

                }
            }
        }
        return RedirectToAction(nameof(All));
    }


    [HttpGet]
    public async Task<IActionResult> UploadedFile(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;

        var modellist = await _context.FingerPrint.Where(f => f.UploadFileId == id).ToListAsync();

        return View(modellist);

    }

    // close day  GetMonthCodeViewModel

    [HttpGet]
    public async Task<IActionResult> Closeday()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);

        List<HRAttend> attendlist = await _context.HRAttend
            .Where(a => a.CheckDate == DateTime.Today && a.CompanyId == user.CompanyId)
            .Include(a=>a.Partner)
            .ToListAsync();
        //var employees = await _context.Partner
        //    .Where(e => e.CompanyId == user.CompanyId && e.Active == true)
        //    .ToListAsync();
        //DateTime date = DateTime.Today;
        //string startmnthcode = Convert.ToDateTime(date).ToString("yy")
        //                      + Convert.ToDateTime(date).ToString("MM");

        // await _context.Database.ExecuteSqlAsync($"Delete [iconncco_Sharekite].[HRAttend] Where [CheckDate] =  {date} and [CompanyId] = {user.CompanyId} ");


        //List<HRAttend> attendlist = new List<HRAttend>();

        //foreach (var item in employees)
        //{
        //    int shiftId = item.HRShiftAttendId;
        //    var shift = await _context.HRShiftAttend.FindAsync(shiftId);

        //    HRAttend attendmodel = new HRAttend()
        //    {
        //        MonthCode = startmnthcode,
        //        CheckDate = date,
        //        PartnerId = item.PartnerId,
        //        HRShiftAttendId = shiftId,
        //    };

        //    var hrattendacnceIn = await _context.HRAttendance
        //        .Where(a => a.AttendDate == date && a.PartnerId == item.PartnerId)
        //        .OrderBy(a => a.CheckIn)
        //        .FirstOrDefaultAsync();

        //    var hrattendacnceOut = await _context.HRAttendance
        //        .Where(a => a.AttendDate == date && a.PartnerId == item.PartnerId)
        //        .OrderByDescending(a => a.CheckIn)
        //        .FirstOrDefaultAsync();

        //    var fingerprintin = await _context.FingerPrint
        //       .Where(a => a.DateAndTime.Value.Date == date && a.ACNo == item.FingerPrintId.ToString())
        //       .OrderBy(a => a.DateAndTime)
        //       .FirstOrDefaultAsync();

        //    var fingerprintout = await _context.FingerPrint
        //       .Where(a => a.DateAndTime.Value.Date == date && a.ACNo == item.FingerPrintId.ToString())
        //       .OrderByDescending(a => a.DateAndTime)
        //       .FirstOrDefaultAsync();

        //    //---------------- IN --------------

        //    if (hrattendacnceIn != null && fingerprintin != null)
        //    {
        //        if (hrattendacnceIn.CheckIn > fingerprintin.DateAndTime)
        //        {
        //            attendmodel.CheckIn = (DateTime)fingerprintin.DateAndTime;
        //            attendmodel.CheckTypeIn = 5;

        //        }
        //        else
        //        {
        //            attendmodel.CheckIn = hrattendacnceIn.CheckIn;
        //            attendmodel.CheckTypeIn = 6;

        //        }
        //    }
        //    else if (hrattendacnceIn != null)
        //    {
        //        attendmodel.CheckIn = hrattendacnceIn.CheckIn;
        //        attendmodel.CheckTypeIn = 6;
        //    }
        //    else if (fingerprintin != null)
        //    {
        //        attendmodel.CheckIn = (DateTime)fingerprintin.DateAndTime;
        //        attendmodel.CheckTypeIn = 5;
        //    }
        //    else if (hrattendacnceIn == null && fingerprintin == null)
        //    {
        //        attendmodel.CheckTypeIn = 0;
        //    }

        //    //---------------- OUT --------------
        //    if (hrattendacnceOut != null && fingerprintout != null)
        //    {
        //        if (hrattendacnceOut.CheckIn > fingerprintout.DateAndTime)
        //        {
        //            attendmodel.CheckOut = (DateTime)fingerprintout.DateAndTime;
        //            attendmodel.CheckTypeOut = 5;

        //        }
        //        else
        //        {
        //            attendmodel.CheckTypeOut = 6;
        //            attendmodel.CheckOut = hrattendacnceOut.CheckIn;

        //        }
        //    }
        //    else if (hrattendacnceOut != null)
        //    {
        //        attendmodel.CheckTypeOut = 6;
        //        attendmodel.CheckOut = hrattendacnceOut.CheckIn;
        //    }
        //    else if (fingerprintout != null)
        //    {
        //        attendmodel.CheckOut = (DateTime)fingerprintout.DateAndTime;
        //        attendmodel.CheckTypeOut = 5;
        //    }

        //    attendmodel.CompanyId = user.CompanyId;

        //    _context.Add(attendmodel);
        //    await _context.SaveChangesAsync();

        //    attendlist.Add(attendmodel);
        //}

        //ViewData["date"] = DateTime.Today.ToString("yyyy-MM-dd");
        return View(attendlist);
    }

    [HttpPost]
    public async Task<IActionResult> Closeday([Bind("date")] GetMonthCodeViewModel model)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        List<HRAttend> attendlist = await _context.HRAttend
          .Where(a => a.CheckDate == model.date && a.CompanyId == user.CompanyId)
           .Include(a => a.Partner)
          .ToListAsync();
        //var employees = await _context.Partner
        //    .Where(e => e.CompanyId == user.CompanyId && e.Active == true)
        //    .ToListAsync();

        //string startmnthcode = Convert.ToDateTime(model.date).ToString("yy")
        //                    + Convert.ToDateTime(model.date).ToString("MM");

        //await _context.Database.ExecuteSqlAsync($"Delete [iconncco_Sharekite].[HRAttend] Where [CheckDate] =  {model.date} and [CompanyId] = {user.CompanyId} ");


        //List<HRAttend> attendlist = new List<HRAttend>();

        //foreach (var item in employees)
        //{
        //    int shiftId = item.HRShiftAttendId;
        //    var shift = await _context.HRShiftAttend.FindAsync(shiftId);

        //    HRAttend attendmodel = new HRAttend()
        //    {
        //        MonthCode = startmnthcode,
        //        CheckDate = model.date,
        //        PartnerId = item.PartnerId,
        //        HRShiftAttendId = shiftId,
        //    };

        //    var hrattendacnceIn = await _context.HRAttendance
        //        .Where(a => a.AttendDate == model.date && a.PartnerId == item.PartnerId)
        //        .OrderBy(a => a.CheckIn)
        //        .FirstOrDefaultAsync();

        //    var hrattendacnceOut = await _context.HRAttendance
        //      .Where(a => a.AttendDate == model.date && a.PartnerId == item.PartnerId)
        //      .OrderByDescending(a => a.CheckIn)
        //      .FirstOrDefaultAsync();


        //    var fingerprintin = await _context.FingerPrint
        //      .Where(a => a.DateAndTime.Value.Date == model.date && a.ACNo == item.FingerPrintId.ToString())
        //      .OrderBy(a => a.DateAndTime)
        //      .FirstOrDefaultAsync();

        //    var fingerprintout = await _context.FingerPrint
        //      .Where(a => a.DateAndTime.Value.Date == model.date && a.ACNo == item.FingerPrintId.ToString())
        //      .OrderByDescending(a => a.DateAndTime)
        //      .FirstOrDefaultAsync();

        //    //---------------- IN --------------

        //    if (hrattendacnceIn != null && fingerprintin != null)
        //    {
        //        if (hrattendacnceIn.CheckIn > fingerprintin.DateAndTime)
        //        {
        //            attendmodel.CheckIn = (DateTime)fingerprintin.DateAndTime;
        //            attendmodel.CheckTypeIn = 5;

        //        }
        //        else
        //        {
        //            attendmodel.CheckIn = hrattendacnceIn.CheckIn;
        //            attendmodel.CheckTypeIn = 6;

        //        }
        //    }
        //    else if (hrattendacnceIn != null)
        //    {
        //        attendmodel.CheckIn = hrattendacnceIn.CheckIn;
        //        attendmodel.CheckTypeIn = 6;
        //    }
        //    else if (fingerprintin != null)
        //    {
        //        attendmodel.CheckIn = (DateTime)fingerprintin.DateAndTime;
        //        attendmodel.CheckTypeIn = 5;
        //    }
        //    else if (hrattendacnceIn == null && fingerprintin == null)
        //    {
        //        attendmodel.CheckTypeIn = 0;
        //    }

        //    //---------------- OUT --------------
        //    if (hrattendacnceOut != null && fingerprintout != null)
        //    {
        //        if (hrattendacnceOut.CheckIn > fingerprintout.DateAndTime)
        //        {
        //            attendmodel.CheckOut = (DateTime)fingerprintout.DateAndTime;
        //            attendmodel.CheckTypeOut = 5;

        //        }
        //        else
        //        {
        //            attendmodel.CheckTypeOut = 6;
        //            attendmodel.CheckOut = hrattendacnceOut.CheckIn;

        //        }
        //    }
        //    else if (hrattendacnceOut != null)
        //    {
        //        attendmodel.CheckTypeOut = 6;
        //        attendmodel.CheckOut = hrattendacnceOut.CheckIn;
        //    }
        //    else if (fingerprintout != null)
        //    {
        //        attendmodel.CheckOut = (DateTime)fingerprintout.DateAndTime;
        //        attendmodel.CheckTypeOut = 5;
        //    }


        //    attendmodel.CompanyId = user.CompanyId;

        //    _context.Add(attendmodel);
        //    await _context.SaveChangesAsync();

        //    attendlist.Add(attendmodel);
        //}

        //ViewData["date"] = model.date.ToString("yyyy-MM-dd");
        return View(attendlist);
    }

    [HttpGet]
    public async Task<IActionResult> UserDays()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ViewUserDays([Bind("PartnerId,ToDate,FromDate")] UserDaysViewModel model)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName");

        List<HRAttend> attendlist = await _context.HRAttend
            .Where(a => a.CheckDate >= model.FromDate && a.CheckDate <= model.ToDate && a.CompanyId == user.CompanyId && a.PartnerId == model.PartnerId)
            .Include(a => a.Partner)
            .ToListAsync();
     
        return View(attendlist);
    }



    // ربط كل موظف ب ID 

    public int MinToInt(int AttendTime, int ShiftStart)
    {
        int LateMin = AttendTime - ShiftStart;
        return LateMin;
    }
    private async Task<int> PenaltyRole(int LateMin)
    {
        int result = 0;
        var model = await _context.HRAttendRole.Where(e => e.LateMin <= LateMin)
        .OrderByDescending(e => e.LateMin).FirstOrDefaultAsync();
        if (model != null)
        {
            result = model.PenaltyMin;
        }

        return result;

    }


    // GET: Attendances/CheckIn
    public async Task<IActionResult> CheckIn()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName");
        CheckInViewModel model = new CheckInViewModel();
        model.HRAttendances = await _context.HRAttendance
            .Where(a => a.CheckIn.Date == DateTime.Now.Date && a.CheckType == 1)
            .Include(a => a.Partner)
            .ToListAsync();
        return View(model);
    }

    // POST: Attendances/CheckIn
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckIn([Bind("EmployeeId,CheckIn")] CheckInViewModel checkInViewModel)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        if (ModelState.IsValid)
        {
            var employeemodel = await _context.Partner.FindAsync(checkInViewModel.EmployeeId);
            int shiftId = employeemodel.HRShiftAttendId;
            var shift = await _context.HRShiftAttend.FindAsync(shiftId);
            TimeSpan startShift = shift.ShiftStart;
            ////     10:30                  10*60 = 600 +   30 = 630 
            //int startstart = startShift.Hours * 60 + startShift.Minutes;
            ////      11:15                         11*60=660 + 15 = 675            
            //int attendtime = checkInViewModel.CheckIn.Hour * 60 + checkInViewModel.CheckIn.Minute;

            //int LateMin = MinToInt(attendtime, startstart);
            //int penaltyint = await PenaltyRole(LateMin);
            string startmnthcode = Convert.ToDateTime(checkInViewModel.CheckIn).ToString("yy")
                                 + Convert.ToDateTime(checkInViewModel.CheckIn).ToString("MM");

            HRAttendances hRAttendances = new HRAttendances
            {
                PartnerId = employeemodel.PartnerId,
                AttendDate = checkInViewModel.CheckIn.Date,
                CheckIn = checkInViewModel.CheckIn,
                PenaltyMin = 0,
                HRShiftAttendId = shift.HRShiftAttendId,
                MonthCode = startmnthcode,
                CheckType = 1,
            };
            hRAttendances.CompanyId = employeemodel.CompanyId;
            hRAttendances.icon = "fa fa-laptop";
            hRAttendances.CreateId = uid;
            hRAttendances.CreatedDate = DateTime.Now;
            hRAttendances.Active = true;
            _context.Add(hRAttendances);
            await _context.SaveChangesAsync();

            bool results = await CheckInAttendFromWeb(hRAttendances.AttendDate, hRAttendances.CheckIn, hRAttendances.CompanyId, hRAttendances.PartnerId, hRAttendances.HRShiftAttendId, hRAttendances.icon, hRAttendances.CheckType);

            return RedirectToAction(nameof(CheckIn));
        }
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", checkInViewModel.EmployeeId);
        return View(checkInViewModel);
    }


    public int CheckInToOut(int CheckOut, int ShiftEnd)
    {

        int PenaltyMin = ShiftEnd - CheckOut;
        return PenaltyMin;
    }

    // GET: Attendances/CheckOut
    public async Task<IActionResult> CheckOut()
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName");
        CheckOutViewModel model = new CheckOutViewModel();
        model.HRAttendances = await _context.HRAttendance
            .Where(a => a.CheckIn.Date == DateTime.Now.Date && a.CheckType == 2)
            .Include(a => a.Partner)
            .ToListAsync();

        return View(model);
    }

    // POST: Attendances/CheckOut
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut([Bind("EmployeeId,CheckOut")] CheckOutViewModel checkOutViewModel)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        if (ModelState.IsValid)
        {
            var employeemodel = await _context.Partner.FindAsync(checkOutViewModel.EmployeeId);
            //var employeeattend =
            //    await _context.HRAttendance.Where(a => a.PartnerId == checkOutViewModel.EmployeeId
            //    && a.CheckIn.Date == checkOutViewModel.CheckOut.Date && a.CompanyId == user.CompanyId)
            //    .FirstOrDefaultAsync();

            ////  attend من ال shiftid هجيب ال 
            int shiftId = employeemodel.HRShiftAttendId;
            var shift = await _context.HRShiftAttend.FindAsync(shiftId);
            TimeSpan endShift = shift.ShiftEnd;

            //// convert shift end and checkout to min 
            //int end = endShift.Hours * 60 + endShift.Minutes;
            //int checkout = checkOutViewModel.CheckOut.Hour * 60 + checkOutViewModel.CheckOut.Minute;

            //// call method & takes value(checkin and end)
            //int PenaltyMin = CheckInToOut(checkout, end);
            ////call method PenaltyRole
            //int penaltyint = await PenaltyRole(PenaltyMin);

            //employeeattend.PenaltyMin += penaltyint;
            //employeeattend.UpdateId = uid;
            //employeeattend.UpdatedDate = DateTime.Now;
            //employeeattend.Active = true;

            //_context.HRAttendance.Update(employeeattend);
            string startmnthcode = Convert.ToDateTime(checkOutViewModel.CheckOut).ToString("yy")
                                + Convert.ToDateTime(checkOutViewModel.CheckOut).ToString("MM");
            HRAttendances hRAttendances = new HRAttendances
            {
                PartnerId = employeemodel.PartnerId,
                AttendDate = checkOutViewModel.CheckOut.Date,
                CheckIn = checkOutViewModel.CheckOut,
                PenaltyMin = 0,
                HRShiftAttendId = shift.HRShiftAttendId,
                MonthCode = startmnthcode,
                CheckType = 2,
            };
            hRAttendances.CompanyId = employeemodel.CompanyId;
            hRAttendances.icon = "fa fa-laptop";
            hRAttendances.CreateId = uid;
            hRAttendances.CreatedDate = DateTime.Now;
            hRAttendances.Active = true;
            _context.Add(hRAttendances);
            await _context.SaveChangesAsync();


            bool results = await CheckOutAttendFromWeb(hRAttendances.AttendDate, hRAttendances.CheckIn, hRAttendances.CompanyId, hRAttendances.PartnerId, hRAttendances.HRShiftAttendId, hRAttendances.icon, hRAttendances.CheckType);



            return RedirectToAction(nameof(CheckOut));
        }
        ViewData["PartnerId"] = new SelectList(_context.Partner.Where(p => p.CompanyId == user.CompanyId), "PartnerId", "FullName", checkOutViewModel.EmployeeId);

        return View(checkOutViewModel);
    }


    private async Task<bool> CheckInAttendFromWeb(DateTime attendDate, DateTime checkIn, int companyId, int partnerId, int hRShiftAttendId, string icon, int CheckType)
    {
        bool result = false;
        var attendday = await _context.HRAttend
            .Where(a => a.CompanyId == companyId && a.PartnerId == partnerId && a.CheckDate == attendDate)
            .FirstOrDefaultAsync();

        if (attendday == null)
        {
            HRAttend attendmodel = new HRAttend()
            {
                //  MonthCode = startmnthcode,
                CheckDate = attendDate,
                PartnerId = partnerId,
                HRShiftAttendId = hRShiftAttendId,
                IconIn = icon,
                CheckTypeIn = CheckType,
                CheckIn = checkIn,
                Active = true,
                CheckTypeOut = 0,
                Penaltyin = 0,
                CompanyId = companyId,

            };

            var shift = await _context.HRShiftAttend.FindAsync(hRShiftAttendId);
            TimeSpan startShift = shift.ShiftStart;
            ////     10:30                  10*60 = 600 +   30 = 630 
            int startstart = startShift.Hours * 60 + startShift.Minutes;
            ////      11:15                         11*60=660 + 15 = 675            
            int attendtime = checkIn.Hour * 60 + checkIn.Minute;

            int LateMin = MinToInt(attendtime, startstart);
            int penaltyint = await PenaltyRole(LateMin);

            attendmodel.Penaltyin = penaltyint;

            _context.Add(attendmodel);
            await _context.SaveChangesAsync();

            result = true;
        }
        else
        {
            if (attendday.CheckIn > checkIn)
            {

                //  MonthCode = startmnthcode,
                attendday.CheckDate = attendDate;
                attendday.PartnerId = partnerId;
                attendday.HRShiftAttendId = hRShiftAttendId;
                attendday.IconIn = icon;
                attendday.CheckTypeIn = CheckType;
                attendday.CheckIn = checkIn;
                attendday.Active = true;
                attendday.CheckTypeOut = 0;

                var shift = await _context.HRShiftAttend.FindAsync(hRShiftAttendId);
                TimeSpan startShift = shift.ShiftStart;
                ////     10:30                  10*60 = 600 +   30 = 630 
                int startstart = startShift.Hours * 60 + startShift.Minutes;
                ////      11:15                         11*60=660 + 15 = 675            
                int attendtime = checkIn.Hour * 60 + checkIn.Minute;

                int LateMin = MinToInt(attendtime, startstart);
                int penaltyint = await PenaltyRole(LateMin);

                attendday.Penaltyin = penaltyint;

                _context.Update(attendday);
                await _context.SaveChangesAsync();

                result = true;
            }
        }

        return result;
    }

    private async Task<bool> CheckOutAttendFromWeb(DateTime attendDate, DateTime checkIn, int companyId, int partnerId, int hRShiftAttendId, string icon, int CheckType)
    {
        bool result = false;
        var attendday = await _context.HRAttend
            .Where(a => a.CompanyId == companyId && a.PartnerId == partnerId && a.CheckDate == attendDate)
            .FirstOrDefaultAsync();

        if (attendday == null)
        {
            HRAttend attendmodel = new HRAttend()
            {
                //  MonthCode = startmnthcode,
                CheckDate = attendDate,
                PartnerId = partnerId,
                HRShiftAttendId = hRShiftAttendId,
                IconOut = icon,
                CheckTypeIn = 0,
                Active = true,
                CheckTypeOut = CheckType,
                CheckOut = checkIn,
                Penaltyin = 0,
                CompanyId = companyId,
            };

            var shift = await _context.HRShiftAttend.FindAsync(hRShiftAttendId);
            TimeSpan endShift = shift.ShiftEnd;

            // convert shift end and checkout to min 
            int end = endShift.Hours * 60 + endShift.Minutes;
            int checkout = checkIn.Hour * 60 + checkIn.Minute;

            // call method & takes value(checkin and end)
            int PenaltyMin = CheckInToOut(checkout, end);
            //call method PenaltyRole
            int penaltyint = await PenaltyRole(PenaltyMin);

            attendmodel.PenaltyOut = penaltyint;

            _context.Add(attendmodel);
            await _context.SaveChangesAsync();

            result = true;
        }
        else
        {
            if (attendday.CheckOut < checkIn || attendday.CheckTypeOut == 0)
            {
                //  MonthCode = startmnthcode,

                attendday.IconOut = icon;
                attendday.CheckTypeOut = CheckType;
                attendday.CheckOut = checkIn;

                var shift = await _context.HRShiftAttend.FindAsync(hRShiftAttendId);
                TimeSpan endShift = shift.ShiftEnd;

                // convert shift end and checkout to min 
                int end = endShift.Hours * 60 + endShift.Minutes;
                int checkout = checkIn.Hour * 60 + checkIn.Minute;

                // call method & takes value(checkin and end)
                int PenaltyMin = CheckInToOut(checkout, end);
                //call method PenaltyRole
                int penaltyint = await PenaltyRole(PenaltyMin);

                attendday.PenaltyOut = penaltyint;

                _context.Update(attendday);
                await _context.SaveChangesAsync();

                result = true;
            }
           
        }

        return result;
    }


    private async Task<bool> CheckInAttendFromFile(DateTime attendDate, DateTime checkIn, int companyId, int partnerId, int hRShiftAttendId, string icon, int CheckType)
    {
        bool result = false;
        var attendday = await _context.HRAttend
            .Where(a => a.CompanyId == companyId && a.PartnerId == partnerId && a.CheckDate == attendDate.Date )
            .FirstOrDefaultAsync();

        if (attendday == null)
        {
            HRAttend attendmodel = new HRAttend()
            {
                //  MonthCode = startmnthcode,
                CheckDate = attendDate,
                PartnerId = partnerId,
                HRShiftAttendId = hRShiftAttendId,
                IconIn = icon,
                CheckTypeIn = CheckType,
                CheckIn = checkIn,
                Active = true,
                CheckTypeOut = 0,
                Penaltyin = 0,
                CompanyId= companyId,
            };

            var shift = await _context.HRShiftAttend.FindAsync(hRShiftAttendId);
            TimeSpan startShift = shift.ShiftStart;
            ////     10:30                  10*60 = 600 +   30 = 630 
            int startstart = startShift.Hours * 60 + startShift.Minutes;
            ////      11:15                         11*60=660 + 15 = 675            
            int attendtime = checkIn.Hour * 60 + checkIn.Minute;

            int LateMin = MinToInt(attendtime, startstart);
            int penaltyint = await PenaltyRole(LateMin);

            attendmodel.Penaltyin = penaltyint;

            _context.Add(attendmodel);
            await _context.SaveChangesAsync();

            result = true;
        }
        else
        {
            if (attendday.CheckIn > checkIn)
            {

                //  MonthCode = startmnthcode,
                attendday.CheckDate = attendDate;
                attendday.PartnerId = partnerId;
                attendday.HRShiftAttendId = hRShiftAttendId;
                attendday.IconIn = icon;
                attendday.CheckTypeIn = CheckType;
                attendday.CheckIn = checkIn;
                attendday.Active = true;
                attendday.CheckTypeOut = 0;

                var shift = await _context.HRShiftAttend.FindAsync(hRShiftAttendId);
                TimeSpan startShift = shift.ShiftStart;
                ////     10:30                  10*60 = 600 +   30 = 630 
                int startstart = startShift.Hours * 60 + startShift.Minutes;
                ////      11:15                         11*60=660 + 15 = 675            
                int attendtime = checkIn.Hour * 60 + checkIn.Minute;

                int LateMin = MinToInt(attendtime, startstart);
                int penaltyint = await PenaltyRole(LateMin);

                attendday.Penaltyin = penaltyint;

                _context.Update(attendday);
                await _context.SaveChangesAsync();

                result = true;
            }
            else if (attendday.CheckOut < checkIn  )
            {
                //  MonthCode = startmnthcode,

                attendday.IconOut = icon;
                attendday.CheckTypeOut = CheckType;
                attendday.CheckOut = checkIn;

                var shift = await _context.HRShiftAttend.FindAsync(hRShiftAttendId);
                TimeSpan endShift = shift.ShiftEnd;

                // convert shift end and checkout to min 
                int end = endShift.Hours * 60 + endShift.Minutes;
                int checkout = checkIn.Hour * 60 + checkIn.Minute;

                // call method & takes value(checkin and end)
                int PenaltyMin = CheckInToOut(checkout, end);
                //call method PenaltyRole
                int penaltyint = await PenaltyRole(PenaltyMin);

                attendday.PenaltyOut = penaltyint;

                _context.Update(attendday);
                await _context.SaveChangesAsync();

                result = true;
            }
        }

        return result;
    }


    //  location
    // GET: Attendances/location / 1
    public async Task<IActionResult> location(int id)
    {
        ViewData["ControllerName"] = ControllerName;
        ViewData["AppName"] = AppName;
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int uid = Convert.ToInt32(userId);
        var user = await _context.Users.FindAsync(uid);
        var attend = await _context.HRAttendance.FindAsync(id);
        string INLatitude = attend.INLatitude.ToString();
        string INLongitude = attend.INLongitude.ToString();

        ViewData["INLatitude"] = attend.INLatitude.ToString();
        ViewData["INLongitude"] = attend.INLongitude.ToString();
        // https://maps.google.com/maps?q=30.0722732,31.2758584&hl=es&z=14&amp;output=embed
        ViewData["src"] = "https://maps.google.com/maps?q=" + INLongitude.ToString() + "," + INLatitude.ToString() + "&hl=es&z=14&amp";
        ViewData["href"] = "https://www.google.com/maps/place/" + INLatitude + "+," + INLongitude;
        return View(attend);
    }





}
