using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlantQuar.DAL;
using PlantQuar.WEB.Controllers;

namespace PlantQuar.WEB.Areas.DE_Item_Data.Controllers
{
    public class ItemCategoriesTestController : BaseController
    {
        private PlantQuarantineEntities db = new PlantQuarantineEntities();

        // GET: DE_Item_Data/ItemCategoriesTest
        public ActionResult Index()
        {
            var itemCategories = db.ItemCategories.Where(a=>a.User_Deletion_Date==null).Include(i => i.Company_National).Include(i => i.Item).Include(i => i.ItemCategories_Group).Include(i => i.ItemCategories_Type1);
            return View(itemCategories.ToList());
        }

        // GET: DE_Item_Data/ItemCategoriesTest/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company_ID = new SelectList(db.Company_National, "ID", "Name_Ar", itemCategory.Company_ID);
            ViewBag.Item_ID = new SelectList(db.Items.Where(a => a.User_Deletion_Id == null), "ID", "Name_Ar", itemCategory.Item_ID);
            ViewBag.groupLst = new SelectList(db.Groups, "ID", "Name_Ar", itemCategory.Item.Group_ID);
            ViewBag.secClassLst = new SelectList(db.SecondaryClassifications, "ID", "Name_Ar", itemCategory.Item.Group.SecClass_ID);
            ViewBag.mainClassLst = new SelectList(db.MainCalssifications, "ID", "Name_Ar", itemCategory.Item.Group.SecondaryClassification.MainClass_ID);
            ViewBag.itemTypeLst = new SelectList(db.Item_Type, "ID", "Name_Ar", itemCategory.Item.Item_Type_ID).ToList();
            ViewBag.Scientific_Name = db.Items.Where(a => a.ID == itemCategory.Item_ID).Select(a => a.Scientific_Name).FirstOrDefault();
            ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group.Where(a => a.IsActive == true && a.User_Deletion_Id == null), "ID", "Name_Ar", itemCategory.ItemCategories_Group_ID);
            //ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group, "ID", "Name_En", itemCategory.ItemCategories_Group_ID);
            ViewBag.ItemCategories_Type = new SelectList(db.ItemCategories_Type, "ID", "Name_Ar", itemCategory.ItemCategories_Type);
            //ViewBag.Company_ID = db.Company_National.Where(a => a.ID == itemCategory.Company_ID).Select(b => b.Name_Ar).FirstOrDefault();
            //ViewBag.Item_ID = db.Items.Where(a => a.ID == itemCategory.Item_ID ).Select(b => b.Name_Ar).FirstOrDefault();
            //ViewBag.ItemCategories_Group_ID = db.ItemCategories_Group.Where(a => a.ID == itemCategory.ItemCategories_Group_ID).Select(b => b.Name_En).FirstOrDefault();
            //ViewBag.ItemCategories_Type = db.ItemCategories_Type.Where(a => a.ID == itemCategory.ItemCategories_Type).Select(b => b.Name_Ar).FirstOrDefault();
            //ViewBag.ItemCategories_Type = db.ItemCategories_Type.Where(a => a.ID == itemCategory.ItemCategories_Type).Select(b => b.Name_Ar).FirstOrDefault();
            //ViewBag.groupLst = db.Groups.Where(a => a.ID == itemCategory.Item.Group_ID).Select(b => b.Name_Ar).FirstOrDefault();
            //ViewBag.secClassLst = db.SecondaryClassifications.Where(a => a.ID == itemCategory.Item.Group.SecClass_ID).Select(b => b.Name_Ar).FirstOrDefault();
            //ViewBag.mainClassLst = db.MainCalssifications.Where(a => a.ID == itemCategory.Item.Group.SecondaryClassification.MainClass_ID).Select(b => b.Name_Ar).FirstOrDefault();
            //ViewBag.itemTypeLst = db.Item_Type.Where(a => a.Id == itemCategory.Item.Item_Type_ID).Select(a => a.Name_Ar).FirstOrDefault();
            //ViewBag.Scientific_Name = db.Items.Where(a => a.ID == itemCategory.Item_ID).Select(a => a.Scientific_Name).FirstOrDefault();
            //ViewBag.ItemCategories_Group_ID = db.ItemCategories_Group.Where(a => a.ID == itemCategory.ItemCategories_Group_ID&& a.IsActive == true && a.User_Deletion_Id == null).Select(a => a.Name_Ar).FirstOrDefault();

            return View(itemCategory);
        }

    
        // GET: DE_Item_Data/ItemCategoriesTest/Create
        public ActionResult Create()
        {

            Fill_DroupDwenList();
           
            return View();
        }

        private void Fill_DroupDwenList()
        {
            ViewBag.Company_ID = new SelectList(db.Company_National.Where(a => a.IsActive == true && a.User_Deletion_Id == null), "ID", "Name_Ar");
            ViewBag.Item_ID = new SelectList(db.Items.Where(a =>  a.User_Deletion_Id == null), "ID", "Name_Ar");
            ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group.Where(a => a.IsActive == true && a.User_Deletion_Id == null), "ID", "Name_En");
            ViewBag.ItemCategories_Type = new SelectList(db.ItemCategories_Type.Where(a => a.IsActive == true && a.User_Deletion_Id == null), "ID", "Name_Ar");
        }

        // POST: DE_Item_Data/ItemCategoriesTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCategory itemCategory)
        {
            long ID = db.Database.SqlQuery<long>("SELECT NEXT VALUE FOR dbo.ItemCategories_SEQ").Single(); 
            itemCategory.ID = ID;
            itemCategory.User_Creation_Id = (short)Session["UserId"];
            itemCategory.User_Creation_Date = DateTime.Now;
            //if (ModelState.IsValid)
            //{



                db.ItemCategories.Add(itemCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            
            ViewBag.Company_ID = new SelectList(db.Company_National, "ID", "Name_Ar", itemCategory.Company_ID);
            ViewBag.Item_ID = new SelectList(db.Items.Where(a => a.User_Deletion_Id == null), "ID", "Name_Ar", itemCategory.Item_ID);
            ViewBag.groupLst = new SelectList(db.Groups, "ID", "Name_Ar", itemCategory.Item.Group_ID);
            ViewBag.secClassLst = new SelectList(db.SecondaryClassifications, "ID", "Name_Ar", itemCategory.Item.Group.SecClass_ID);
            ViewBag.mainClassLst = new SelectList(db.MainCalssifications, "ID", "Name_Ar", itemCategory.Item.Group.SecondaryClassification.MainClass_ID);
            ViewBag.itemTypeLst = new SelectList(db.Item_Type, "ID", "Name_Ar", itemCategory.Item.Item_Type_ID).ToList();
            ViewBag.Scientific_Name = db.Items.Where(a => a.ID == itemCategory.Item_ID).Select(a => a.Scientific_Name).FirstOrDefault();
            ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group.Where(a => a.IsActive == true && a.User_Deletion_Id == null), "ID", "Name_Ar", itemCategory.ItemCategories_Group_ID);
            //ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group, "ID", "Name_En", itemCategory.ItemCategories_Group_ID);
            ViewBag.ItemCategories_Type = new SelectList(db.ItemCategories_Type, "ID", "Name_Ar", itemCategory.ItemCategories_Type);
            //ViewBag.Company_ID = new SelectList(db.Company_National, "ID", "Name_Ar", itemCategory.Company_ID);
            //ViewBag.Item_ID = new SelectList(db.Items, "ID", "Name_Ar", itemCategory.Item_ID);
            //ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group, "ID", "Name_En", itemCategory.ItemCategories_Group_ID);
            //ViewBag.ItemCategories_Type = new SelectList(db.ItemCategories_Type, "ID", "Name_Ar", itemCategory.ItemCategories_Type);
         //  }
        return View(itemCategory);
        }

        // GET: DE_Item_Data/ItemCategoriesTest/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company_ID = new SelectList(db.Company_National, "ID", "Name_Ar", itemCategory.Company_ID);
            ViewBag.Item_ID = new SelectList(db.Items.Where(a => a.User_Deletion_Id == null), "ID", "Name_Ar", itemCategory.Item_ID);
            ViewBag.groupLst = new SelectList(db.Groups, "ID", "Name_Ar", itemCategory.Item.Group_ID);
            ViewBag.secClassLst = new SelectList(db.SecondaryClassifications, "ID", "Name_Ar", itemCategory.Item.Group.SecClass_ID);
            ViewBag.mainClassLst = new SelectList(db.MainCalssifications, "ID", "Name_Ar", itemCategory.Item.Group.SecondaryClassification.MainClass_ID);
            ViewBag.itemTypeLst = new SelectList(db.Item_Type, "ID", "Name_Ar", itemCategory.Item.Item_Type_ID).ToList(); 
            ViewBag.Scientific_Name = db.Items.Where(a=>a.ID== itemCategory.Item_ID).Select(a=>a.Scientific_Name).FirstOrDefault();
            ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group.Where(a => a.IsActive == true && a.User_Deletion_Id == null), "ID", "Name_Ar", itemCategory.ItemCategories_Group_ID);
            //ViewBag.ItemCategories_Group_ID = new SelectList(db.ItemCategories_Group, "ID", "Name_En", itemCategory.ItemCategories_Group_ID);
            ViewBag.ItemCategories_Type = new SelectList(db.ItemCategories_Type, "ID", "Name_Ar", itemCategory.ItemCategories_Type);
            return View(itemCategory);
        }

        // POST: DE_Item_Data/ItemCategoriesTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemCategory itemCategory)
        {
            var userId = (short)Session["UserId"];
            //if (ModelState.IsValid)
            //{
                itemCategory.User_Creation_Date = DateTime.Now;
                itemCategory.User_Updation_Id = userId;
                db.Entry(itemCategory).State = EntityState.Modified;

                db.SaveChanges();
          
            return RedirectToAction("Index");
        }

        // GET: DE_Item_Data/ItemCategoriesTest/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            if (itemCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Company_ID = db.Company_National.Where(a => a.ID == itemCategory.Company_ID).Select(b => b.Name_Ar).FirstOrDefault();
            ViewBag.Item_ID = db.Items.Where(a => a.ID == itemCategory.Item_ID).Select(b => b.Name_Ar).FirstOrDefault();
            ViewBag.ItemCategories_Group_ID = db.ItemCategories_Group.Where(a => a.ID == itemCategory.ItemCategories_Group_ID).Select(b => b.Name_En).FirstOrDefault();
            ViewBag.ItemCategories_Type = db.ItemCategories_Type.Where(a => a.ID == itemCategory.ItemCategories_Type).Select(b => b.Name_Ar).FirstOrDefault();
            ViewBag.ItemCategories_Type = db.ItemCategories_Type.Where(a => a.ID == itemCategory.ItemCategories_Type).Select(b => b.Name_Ar).FirstOrDefault();
            ViewBag.groupLst = db.Groups.Where(a => a.ID == itemCategory.Item.Group_ID).Select(b => b.Name_Ar).FirstOrDefault();
            ViewBag.secClassLst = db.SecondaryClassifications.Where(a => a.ID == itemCategory.Item.Group.SecClass_ID).Select(b => b.Name_Ar).FirstOrDefault();
            ViewBag.mainClassLst = db.MainCalssifications.Where(a => a.ID == itemCategory.Item.Group.SecondaryClassification.MainClass_ID).Select(b => b.Name_Ar).FirstOrDefault();
            ViewBag.itemTypeLst = db.Item_Type.Where(a => a.Id == itemCategory.Item.Item_Type_ID).Select(a => a.Name_Ar).FirstOrDefault();
            ViewBag.Scientific_Name = db.Items.Where(a => a.ID == itemCategory.Item_ID).Select(a => a.Scientific_Name).FirstOrDefault();
            ViewBag.ItemCategories_Group_ID = db.ItemCategories_Group.Where(a => a.ID == itemCategory.ItemCategories_Group_ID && a.IsActive == true && a.User_Deletion_Id == null).Select(a => a.Name_Ar).FirstOrDefault();

            return View(itemCategory);
        }

        // POST: DE_Item_Data/ItemCategoriesTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            var userId = (short)Session["UserId"];
            ItemCategory itemCategory = db.ItemCategories.Find(id);
            itemCategory.User_Deletion_Date = DateTime.Now;
            itemCategory.User_Deletion_Id = userId;
            db.Entry(itemCategory).State = EntityState.Modified;
            //  db.ItemCategories.Remove(itemCategory);
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
