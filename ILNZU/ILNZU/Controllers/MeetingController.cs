using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace ILNZU.Controllers
{
    public class MeetingController : Controller
    {
        [Authorize]
        public IActionResult Room(int? id)
        {
            return View(id);
        }
    }
}
