using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TestTask.Models;

namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryContext _context;

        public CategoryController(CategoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                return Json(_context.Category.ToList());
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost, Route("Add")]
        public JsonResult Insert([FromBody]Category item)
        {
            if (item != null)
            {
                try
                {
                    Category apointment = new Category
                    {
                        Name = item.Name,
                        Description = item.Description,
                    };

                    var insertedItem = _context.Add<Category>(apointment);
                    _context.SaveChanges();
                    return Json(insertedItem.Entity);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpPost, Route("Update")]
        public JsonResult Update([FromBody]Category item)
        {
            if (item != null)
            {
                try
                {
                    var categoryForUpdate = _context.Category.Where(x => x.Id == item.Id).FirstOrDefault();
                    categoryForUpdate.Name = item.Name;
                    categoryForUpdate.Description = item.Description;

                    _context.SaveChanges();

                    return Json(categoryForUpdate);
                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            return Json("Error");
        }

        [HttpDelete, Route("Delete")]
        public JsonResult Delete([FromBody] Category item)
        {
            if (item != null)
            {
                try
                {
                    _context.Category.Remove(item);
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
