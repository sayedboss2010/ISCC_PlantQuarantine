using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlantQuar.DAL;

namespace PlantQuar.WEB.Areas.Test_mvc.Controllers
{
    public class OutletsController : Controller
    {
        private PlantQuarantineEntities db = new PlantQuarantineEntities();

        // GET: Test_mvc/Outlets
        public ActionResult Index()
        {
            var outlets = db.Outlets.Include(o => o.A_SystemCode).Include(o => o.General_Admin).Include(o => o.PortNational);
            return View(outlets.ToList());
        }

        // GET: Test_mvc/Outlets/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // GET: Test_mvc/Outlets/Create
        public ActionResult Create()
        {
            ViewBag.IsExport = new SelectList(db.A_SystemCode, "Id", "ValueName");
            ViewBag.GrAdmin_ID = new SelectList(db.General_Admin, "ID", "Ar_Name");
            ViewBag.PortNational_ID = new SelectList(db.PortNationals, "ID", "Name_Ar");
            return View();
        }

        // POST: Test_mvc/Outlets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GrAdmin_ID,Ar_Name,En_Name,Address_Ar,Address_En,Supervisor_ID,IsActive,IsExport,User_Updation_Id,User_Updation_Date,User_Deletion_Id,User_Deletion_Date,User_Creation_Id,User_Creation_Date,ID_HR,PortNational_ID")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Outlets.Add(outlet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IsExport = new SelectList(db.A_SystemCode, "Id", "ValueName", outlet.IsExport);
            ViewBag.GrAdmin_ID = new SelectList(db.General_Admin, "ID", "Ar_Name", outlet.GrAdmin_ID);
            ViewBag.PortNational_ID = new SelectList(db.PortNationals, "ID", "Name_Ar", outlet.PortNational_ID);
            return View(outlet);
        }

        // GET: Test_mvc/Outlets/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            ViewBag.IsExport = new SelectList(db.A_SystemCode, "Id", "ValueName", outlet.IsExport);
            ViewBag.GrAdmin_ID = new SelectList(db.General_Admin, "ID", "Ar_Name", outlet.GrAdmin_ID);
            ViewBag.PortNational_ID = new SelectList(db.PortNationals, "ID", "Name_Ar", outlet.PortNational_ID);
            return View(outlet);
        }

        // POST: Test_mvc/Outlets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GrAdmin_ID,Ar_Name,En_Name,Address_Ar,Address_En,Supervisor_ID,IsActive,IsExport,User_Updation_Id,User_Updation_Date,User_Deletion_Id,User_Deletion_Date,User_Creation_Id,User_Creation_Date,ID_HR,PortNational_ID")] Outlet outlet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outlet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IsExport = new SelectList(db.A_SystemCode, "Id", "ValueName", outlet.IsExport);
            ViewBag.GrAdmin_ID = new SelectList(db.General_Admin, "ID", "Ar_Name", outlet.GrAdmin_ID);
            ViewBag.PortNational_ID = new SelectList(db.PortNationals, "ID", "Name_Ar", outlet.PortNational_ID);
            return View(outlet);
        }

        // GET: Test_mvc/Outlets/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outlet outlet = db.Outlets.Find(id);
            if (outlet == null)
            {
                return HttpNotFound();
            }
            return View(outlet);
        }

        // POST: Test_mvc/Outlets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Outlet outlet = db.Outlets.Find(id);
            db.Outlets.Remove(outlet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
