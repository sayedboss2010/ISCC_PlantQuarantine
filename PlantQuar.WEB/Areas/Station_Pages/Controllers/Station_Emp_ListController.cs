using PlantQuar.DAL;
using PlantQuar.DTO.DTO.Station_Pages;
using PlantQuar.WEB.Controllers;
using Privilages.DAL;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Station_Pages.Controllers
{
    public class Station_Emp_ListController : BaseController
    {
        PlantQuarantineEntities db = new PlantQuarantineEntities();
        dbPrivilageEntities db_Prv = new dbPrivilageEntities();

        // GET: Station_Pages/Station_Emp_List
        public ActionResult Index()
        {
            var station_Emp = (from s in db.Stations
                               //join sa in db.Station_Accreditation on s.ID equals sa.Station_ID
                               //  join sc in db.StationCompanies on sa.ID equals sc.Station_Accreditation_ID

                               //join sc in db.StationCompanies on sa.ID equals sc.Station_Accreditation_ID

                               join cn in db.Company_National on new { a = (long)s.Company_ID, b = (int)s.User_Type_Id } equals new { a = cn.ID, b = 6 } into cn1
                               from cn in cn1.DefaultIfEmpty()
                               join po in db.Public_Organization on new { a = (long)s.Company_ID, b = (int)s.User_Type_Id } equals new { a = po.ID, b = 7 } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in db.People on new { a = (long)s.Company_ID, b = (int)s.User_Type_Id } equals new { a = pr.ID, b = 8 } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join g in db.Governates on s.Gov_Id equals g.ID
                               where s.StationCode != null
                               select new Station_Emp_DTO
                               {
                                   Id = s.ID,
                                   Company_ID = s.Company_ID,
                                   Company_Type_Id = s.User_Type_Id,
                                   Company_Name = s.User_Type_Id == 6 ? cn.Name_Ar :
                                                   s.User_Type_Id == 7 ? po.Name_Ar :
                                                   s.User_Type_Id == 8 ? pr.Name : "",
                                   Station_Name = s.Ar_Name,
                                   Station_Code = s.StationCode,
                                   Govern_Name = g.Ar_Name,
                                   Station_Id = s.ID

                               }).Distinct().ToList();

            //station_Emp = (from se in station_Emp
            //               join u in db_Prv.PR_User on se.Emp_Id equals u.Id
            //               join o in db.Outlets on u.Outlet_ID equals o.ID_HR
            //               select new Station_Emp_DTO
            //               {
            //                   Id = se.Id,
            //                   Company_Name = se.Company_Name,
            //                   Station_Name = se.Station_Name,
            //                   Station_Code = se.Station_Code,
            //                   Govern_Name = se.Govern_Name,
            //                   Date_From = se.Date_From,
            //                   Date_To = se.Date_To,
            //                   FullName=u.FullName,
            //                   OutletName=o.Ar_Name
            //               }).ToList();

            //data.Insert(0, new CustomOptionLongId() { DisplayText = "----------", Value = null });


            //var station_Emp = db.Station_Emp.Where(a=>a.IsActive==true).Include(s => s.Station);
            //var Emp_ID = db.Station_Emp.Where(a=>a.IsActive==true).Include(s => s.Station);
            return View(station_Emp);
        }

        // GET: Station_Pages/Station_Emp_List/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Station_Emp station_Emp = db.Station_Emp.Find(id);
            if (station_Emp == null)
            {
                return HttpNotFound();
            }
            return View(station_Emp);
        }

        // GET: Station_Pages/Station_Emp_List/Create
        public ActionResult Create()
        {
            ViewBag.Station_Id = new SelectList(db.Stations, "ID", "Ar_Name");
            ViewBag.Emp_Id = new SelectList(db_Prv.PR_User.Where(a => a.LoginName != null && a.Password != null), "Id", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Station_Emp station_Emp)
        {
            long id = db.Database.SqlQuery<long>("SELECT NEXT VALUE FOR dbo.Station_Emp_seq").Single();
            station_Emp.Id = id;
            station_Emp.User_Creation_Date = DateTime.Now;
            station_Emp.User_Creation_Id = (short)Session["UserId"];
            if (ModelState.IsValid)
            {
                db.Station_Emp.Add(station_Emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Station_Id = new SelectList(db.Stations, "ID", "Ar_Name", station_Emp.Station_Id);
            return View(station_Emp);
        }


        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var station_Emp = (from s in db.Stations
                               //join sa in db.Station_Accreditation on s.ID equals sa.Station_ID
                               //join sc in db.StationCompanies on sa.ID equals sc.Station_Accreditation_ID

                               //join sc in db.StationCompanies on sa.ID equals sc.Station_Accreditation_ID

                               join cn in db.Company_National on new { a = (long)s.Company_ID, b = (int)s.User_Type_Id } equals new { a = cn.ID, b = 6 } into cn1
                               from cn in cn1.DefaultIfEmpty()
                               join po in db.Public_Organization on new { a = (long)s.Company_ID, b = (int)s.User_Type_Id } equals new { a = po.ID, b = 7 } into po1
                               from po in po1.DefaultIfEmpty()
                               join pr in db.People on new { a = (long)s.Company_ID, b = (int)s.User_Type_Id } equals new { a = pr.ID, b = 8 } into pr1
                               from pr in pr1.DefaultIfEmpty()

                               join g in db.Governates on s.Gov_Id equals g.ID
                               join se in db.Station_Emp on s.ID equals se.Station_Id
                               where s.ID == id //&&se.Date_From>=DateTime.Now 
                               && se.IsActive == true
                               && se.User_Deletion_Id == null
                               select new Station_Emp_DTO
                               {
                                   Station_Emp_ID = se.Id,
                                   Station_Id = s.ID,
                                   //Company_Type_Id = sc.Company_Type_Id,
                                   Company_Name = s.User_Type_Id == 6 ? cn.Name_Ar :
                                                   s.User_Type_Id == 7 ? po.Name_Ar :
                                                   s.User_Type_Id == 8 ? pr.Name : "",
                                   Station_Name = s.Ar_Name,
                                   Station_Code = s.StationCode,
                                   Govern_Name = g.Ar_Name,
                                   Emp_Id = se.Emp_Id,
                                   Date_From = se.Date_From,
                                   Date_To = se.Date_To,
                               }).Distinct().ToList();

            station_Emp = (from se in station_Emp
                           join u in db_Prv.PR_User on se.Emp_Id equals u.Id
                           join o in db.Outlets on u.Outlet_ID equals o.ID_HR
                           select new Station_Emp_DTO
                           {
                               Id = se.Id,
                               Station_Emp_ID = se.Station_Emp_ID,
                               Station_Id = se.Station_Id,
                               Company_Name = se.Company_Name,
                               Station_Name = se.Station_Name,
                               Station_Code = se.Station_Code,
                               Govern_Name = se.Govern_Name,
                               Date_From = se.Date_From,
                               Date_To = se.Date_To,
                               FullName = u.FullName,
                               OutletName = o.Ar_Name
                           }).Distinct().ToList();




            if (station_Emp == null)
            {
                return HttpNotFound();
            }
            ViewBag.Station_Id = new SelectList(db.Stations, "ID", "Ar_Name", id);
            ViewBag.Emp_Id = new SelectList(db_Prv.PR_User.Where(a => a.LoginName != null && a.Password != null), "Id", "FullName");
            return View(station_Emp);
        }

        // GET: Station_Pages/Station_Emp_List/Edit/5
        //public ActionResult Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var station_Emp = (from s in db.Stations
        //                       join sa in db.Station_Accreditation on s.ID equals sa.Station_ID
        //                       //join sc in db.StationCompanies on sa.ID equals sc.Station_Accreditation_ID 

        //                       join c in db.Company_National on s.Company_ID equals c.ID

        //                       join g in db.Governates on s.Gov_Id equals g.ID
        //                       join se in db.Station_Emp on s.ID equals se.Station_Id
        //                       where s.ID == id //&&se.Date_From>=DateTime.Now 
        //                       &&se.IsActive==true
        //                       &&se.User_Deletion_Id==null
        //                       select new Station_Emp_DTO
        //                       {
        //                           Station_Emp_ID = se.Id,
        //                           Station_Id=s.ID,
        //                           Company_Name = c.Name_Ar,
        //                           Station_Name = s.Ar_Name,
        //                           Station_Code = s.StationCode,
        //                           Govern_Name = g.Ar_Name,
        //                           Emp_Id = se.Emp_Id,
        //                           Date_From = se.Date_From,
        //                           Date_To = se.Date_To,
        //                       }).ToList();

        //    station_Emp = (from se in station_Emp
        //                   join u in db_Prv.PR_User on se.Emp_Id equals u.Id
        //                   join o in db.Outlets on u.Outlet_ID equals o.ID_HR
        //                   select new Station_Emp_DTO
        //                   {
        //                       Id = se.Id,
        //                       Station_Emp_ID = se.Station_Emp_ID,
        //                       Station_Id = se.Station_Id,
        //                       Company_Name = se.Company_Name,
        //                       Station_Name = se.Station_Name,
        //                       Station_Code = se.Station_Code,
        //                       Govern_Name = se.Govern_Name,
        //                       Date_From = se.Date_From,
        //                       Date_To = se.Date_To,
        //                       FullName = u.FullName,
        //                       OutletName = o.Ar_Name
        //                   }).ToList();




        //    if (station_Emp == null)
        //    {
        //        return HttpNotFound();
        //    }           
        //    ViewBag.Station_Id = new SelectList(db.Stations, "ID", "Ar_Name", id);
        //    ViewBag.Emp_Id = new SelectList(db_Prv.PR_User.Where(a => a.LoginName != null && a.Password != null), "Id", "FullName");
        //    return View(station_Emp);
        //}

        // POST: Station_Pages/Station_Emp_List/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Station_Emp station_Emp)
        {
            var _Station_Id = station_Emp.Id;
            //var _Emp_Check = db.Station_Emp.Where(a => a.Emp_Id == station_Emp.Emp_Id
            //&& a.IsActive == true 
            //&& a.User_Deletion_Id == null
            ////&& ((station_Emp.Date_From >= a.Date_From || station_Emp.Date_From <= a.Date_To)
            ////|| (station_Emp.Date_To >= a.Date_From || station_Emp.Date_To <= a.Date_To))
            //).Count();


            //if (_Emp_Check > 0)
            //{
            //   // station_Emp.Messige_Error 
            //    ViewBag.Error = "الموظف موجود مسبقا";
            //    Station_Emp_DTO station_Emp_DTO = new Station_Emp_DTO();
            //    station_Emp_DTO.Messige_Error = "الموظف موجود مسبقا";
            //    return RedirectToAction("Edit", new { id = _Station_Id });
            //}
            //else
            //{
            station_Emp.Station_Id = station_Emp.Id;
            long id = db.Database.SqlQuery<long>("SELECT NEXT VALUE FOR dbo.Station_Emp_seq").Single();
            station_Emp.Id = id;
            station_Emp.User_Creation_Date = DateTime.Now;
            station_Emp.User_Creation_Id = (short)Session["UserId"];
            station_Emp.IsActive = true;
            if (ModelState.IsValid)
            {
                db.Station_Emp.Add(station_Emp);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Edit", new { id = _Station_Id });
            }

            //} 
            ViewBag.Station_Id = new SelectList(db.Stations, "ID", "Ar_Name", station_Emp.Station_Id);
            //return View(station_Emp);

            return RedirectToAction("Edit", new { id = _Station_Id });
        }

        // GET: Station_Pages/Station_Emp_List/Delete/5
        public ActionResult Delete(long? id)
        {
            Station_Emp station_Emp = db.Station_Emp.Find(id);
            //db.Station_Emp.Remove(station_Emp);
            station_Emp.IsActive = false;
            station_Emp.User_Deletion_Id = null;
            station_Emp.User_Deletion_Date = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = station_Emp.Station_Id });
            //return RedirectToAction("Index");
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Station_Emp station_Emp = db.Station_Emp.Find(id);
            //if (station_Emp == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(station_Emp);
        }

        // POST: Station_Pages/Station_Emp_List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Station_Emp station_Emp = db.Station_Emp.Find(id);
            db.Station_Emp.Remove(station_Emp);
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
