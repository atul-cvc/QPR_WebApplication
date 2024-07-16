using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QPR_Application.Models.DTO.Request;
using QPR_Application.Models.Entities;
using QPR_Application.Repository;

namespace QPR_Application.Controllers
{
    //[Authorize]
    public class ManageQPRController : Controller
    {
        private readonly ILogger<ManageQPRController> _logger;
        private readonly IManageQprRepo _manageQprRepo;
        private readonly IHttpContextAccessor _httpContext;
        private qpr QPR;

        public ManageQPRController(ILogger<ManageQPRController> logger, IManageQprRepo manageQprRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _manageQprRepo = manageQprRepo;
            _httpContext = httpContext;
        }

        public async Task<IActionResult> Index()
        {
            var qprList = await _manageQprRepo.GetAllQprs();
            return View(qprList);
        }

        //public IActionResult Search()
        //{
        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchById search)
        {
            return RedirectToAction("EditQpr", new { id = search.Id });
        }

        public IActionResult EditQpr(string id)
        {
            QPR = _manageQprRepo.GetQPRByRef(id);
            return View(QPR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditQpr(qpr qpr)
        {
            try
            {
                _manageQprRepo.UpdateQpr(qpr);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { }
            return View();
        }

        public IActionResult Details(string id)
        {
            var qpr = _manageQprRepo.GetQPRByRef(id);
            return View(qpr);
        }


        // POST: ManageQPR/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long? id, [Bind("userid,orgname,orgcode,qtrreport,qtryear,contactnumberoffice,mobilenumber,emailid,fulltime,parttime,createdate,referencenumber,ip,finalsubmit,finalsubmitdate")] qpr qpr)
        //{
        //if (id != qpr.referencenumber)
        //{
        //    return NotFound();
        //}

        //if (ModelState.IsValid)
        //{
        //    try
        //    {
        //        _context.Update(qpr);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!qprExists(qpr.referencenumber))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
        //    return View(qpr);
        //}

        // POST: ManageQPR/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long? id)
        //{
        //    //if (_context.qpr == null)
        //    //{
        //    //    return Problem("Entity set 'QPRContext.qpr'  is null.");
        //    //}
        //    //var qpr = await _context.qpr.FindAsync(id);
        //    //if (qpr != null)
        //    //{
        //    //    _context.qpr.Remove(qpr);
        //    //}

        //    //await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
