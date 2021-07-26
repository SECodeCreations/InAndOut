using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InAndOut.Models.ViewModels
{
    public class ExpenseVM
    {
        public Expenses Expenses { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown { get; set; }
    }
}
