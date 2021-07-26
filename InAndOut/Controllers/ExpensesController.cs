using InAndOut.Data;
using InAndOut.Models;
using InAndOut.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ExpensesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Expenses> objectList = _db.Expenses;

            foreach (var obj in objectList)
            {
                obj.ExpenseType = _db.ExpenseType.FirstOrDefault(u => u.Id == obj.ExpenseTypeId);                
            }

            objectList.OrderByDescending(x => x.ExpenseTypeId);

            return View(objectList);
        }

        //Get-Create
        public IActionResult Create()
        {
            //IEnumerable<SelectListItem> TypeDropDown = _db.ExpenseType.Select(i => new SelectListItem
            //{
            //    Text = i.ExpenseTypeName,
            //    Value = i.Id.ToString()
            //});

            //ViewBag.TypeDropDown = TypeDropDown;

            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expenses = new Expenses(),
                TypeDropDown = _db.ExpenseType.Select(i => new SelectListItem
                {
                    Text = i.ExpenseTypeName,
                    Value = i.Id.ToString()
                })
            };

            return View(expenseVM);
        }

        //POST-Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                //obj.ExpenseTypeId = 1;
                _db.Expenses.Add(obj.Expenses);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET-Delete
        public IActionResult Delete(int? id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expenses = new Expenses(),
                TypeDropDown = _db.ExpenseType.Select(i => new SelectListItem
                {
                    Text = i.ExpenseTypeName,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            expenseVM.Expenses = _db.Expenses.Find(id);
            if (expenseVM.Expenses == null)
            {
                return NotFound();
            }

            return View(expenseVM);
        }

        //POST-Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(ExpenseVM obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            _db.Expenses.Remove(obj.Expenses);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET-Update
        public IActionResult Update(int? id)
        {
            ExpenseVM expenseVM = new ExpenseVM()
            {
                Expenses = new Expenses(),
                TypeDropDown = _db.ExpenseType.Select(i => new SelectListItem
                {
                    Text = i.ExpenseTypeName,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return NotFound();
            }

            expenseVM.Expenses = _db.Expenses.Find(id);
            if (expenseVM.Expenses == null)
            {
                return NotFound();
            }

            return View(expenseVM);
        }

        //POST-Update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ExpenseVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Expenses.Update(obj.Expenses);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
