using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TestTask.Models;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private readonly AppointmentContext _context;

        public AppointmentController(AppointmentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            DateTime localDate = DateTime.Now;
            try
            {
                return Json(_context.Appointment.Where(x => x.Dateendofactuality > localDate).ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]Appointment  item)
        {
            if (item != null)
            {
                try
                {
                    Appointment apointment = new Appointment { DateCreate = item.DateCreate, Dateendofactuality = item.Dateendofactuality,
                        Description = item.Description, Category_id = item.Category_id};

                    var insertedItem = _context.Add<Appointment>(apointment);
                    _context.SaveChanges();
                    return Json(insertedItem.Entity);
                }
                catch(Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpPut, Route("Update")]
        public JsonResult Update([FromBody]Appointment item)
        {
            if (item != null)
            {
                try
                {
                    var appointmentForUpdate = _context.Appointment.Where(x => x.Id == item.Id).FirstOrDefault();
                    appointmentForUpdate.DateCreate = item.DateCreate;
                    appointmentForUpdate.Dateendofactuality = item.Dateendofactuality;
                    appointmentForUpdate.Description = item.Description;
                    appointmentForUpdate.Category_id = item.Category_id;

                    _context.SaveChanges();

                    return Json(appointmentForUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] Appointment item)
        {
            if (item != null)
            {
                try
                {
                    _context.Appointment.Remove(item);
                    _context.SaveChanges();

                    return Json(item);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }
    }


}