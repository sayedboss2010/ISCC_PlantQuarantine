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
    public class Station_Emp_DataResultController : Controller
    {
        private PlantQuarantineEntities db = new PlantQuarantineEntities();

        // GET: Test_mvc/Station_Emp_Data_Result
        public ActionResult Index()
        {
            return View(db.Station_Emp_Data_Result.ToList());
        }

        // GET: Test_mvc/Station_Emp_Data_Result/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station_Emp_Data_Result station_Emp_Data_Result = db.Station_Emp_Data_Result.Find(id);
            if (station_Emp_Data_Result == null)
            {
                return HttpNotFound();
            }
            return View(station_Emp_Data_Result);
        }

        // GET: Test_mvc/Station_Emp_Data_Result/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test_mvc/Station_Emp_Data_Result/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Company_Name,Station_Name,StationCode,Gov_Name,Date_From,Date_To,Emp_Id,FullName,Outlet_Name")] Station_Emp_Data_Result station_Emp_Data_Result)
        {
            if (ModelState.IsValid)
            {
                db.Station_Emp_Data_Result.Add(station_Emp_Data_Result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(station_Emp_Data_Result);
        }

        // GET: Test_mvc/Station_Emp_Data_Result/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station_Emp_Data_Result station_Emp_Data_Result = db.Station_Emp_Data_Result.Find(id);
            if (station_Emp_Data_Result == null)
            {
                return HttpNotFound();
            }
            return View(station_Emp_Data_Result);
        }

        // POST: Test_mvc/Station_Emp_Data_Result/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Company_Name,Station_Name,StationCode,Gov_Name,Date_From,Date_To,Emp_Id,FullName,Outlet_Name")] Station_Emp_Data_Result station_Emp_Data_Result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(station_Emp_Data_Result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(station_Emp_Data_Result);
        }

        // GET: Test_mvc/Station_Emp_Data_Result/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station_Emp_Data_Result station_Emp_Data_Result = db.Station_Emp_Data_Result.Find(id);
            if (station_Emp_Data_Result == null)
            {
                return HttpNotFound();
            }
            return View(station_Emp_Data_Result);
        }

        // POST: Test_mvc/Station_Emp_Data_Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Station_Emp_Data_Result station_Emp_Data_Result = db.Station_Emp_Data_Result.Find(id);
            db.Station_Emp_Data_Result.Remove(station_Emp_Data_Result);
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
