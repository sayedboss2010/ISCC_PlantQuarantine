using PlantQuar.DTO.DTO.Import.Constrains;
using PlantQuar.DTO.HelperClasses;
using PlantQuar.WEB.App_Start;
using PlantQuar.WEB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlantQuar.WEB.Areas.Im_Constrains.Controllers
{
    public class Im_ItemsConstrainsController : BaseController
    {
        // GET: Im_Constrains/Im_ItemsConstrains
        public ActionResult Im_ItemsConstrains()
        {
            return View();
        }
        public JsonResult ListItemsRows(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (Session["ImDB_ItemConstrainRows"] != null)
            {
                var dbConstrain = Session["ImDB_ItemConstrainRows"] as ImCustomCountryConstrain;
                if (dbConstrain != null && dbConstrain.items != null)
                {
                    var itemIndex = int.Parse(Session["ImItemConRows_Index"].ToString());

                    List<ImCustomItemConstrain_Rows> items = new List<ImCustomItemConstrain_Rows>();

                    foreach (var item in dbConstrain.items.ItemConstrain_Rows)
                    {
                        item.index = itemIndex;
                        itemIndex++;
                    }
                    items.AddRange(dbConstrain.items.ItemConstrain_Rows);

                    Session["ImItemConRows_Index"] = itemIndex;
                    Session["ImDB_ItemConstrainRows"] = null;
                    Session["ImItemsConstrain_Rows"] = items;
                }
            }

            var itemsList = Session["ImItemsConstrain_Rows"] as List<ImCustomItemConstrain_Rows>;

            return Json(new { Result = "OK", Records = itemsList.OrderBy(p => p.index), TotalRecordCount = itemsList.Count });
        }
        public JsonResult CreateItemsRows(ImCustomItemConstrain_Rows model)
        {
            model.index = int.Parse(Session["ImItemConRows_Index"].ToString());
            var itemsList = Session["ImItemsConstrain_Rows"] as List<ImCustomItemConstrain_Rows>;

            var itemRepeat = itemsList.Where(p => (p.Id == model.Id)).Count();

            if (itemRepeat == 0)
            {
                itemsList.Add(model);
                Session["ImItemsConstrain_Rows"] = itemsList;

                Session["ImItemConRows_Index"] = int.Parse(Session["ImItemConRows_Index"].ToString()) + 1;
                return Json(new { Result = "OK", Record = itemsList.OrderBy(p => p.index), TotalRecordCount = itemsList.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdateItemsRows(ImCustomItemConstrain_Rows model)
        {
            var itemsList = Session["ImItemsConstrain_Rows"] as List<ImCustomItemConstrain_Rows>;

            var itemRepeat = itemsList.Where(p => p.Id == model.Id && p.index != model.index).Count();

            if (itemRepeat == 0)
            {
                ImCustomItemConstrain_Rows update = itemsList.Find(p => p.index == model.index || p.Id == model.Id);
                model.countryConstraintext_Id = update.countryConstraintext_Id;
                itemsList.Remove(update);
                itemsList.Add(model);

                Session["ImItemsConstrain_Rows"] = itemsList;

                return Json(new { Result = "OK", Record = itemsList.OrderBy(p => p.index) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeleteItemsRows(int index)
        {
            var itemsList = Session["ImItemsConstrain_Rows"] as List<ImCustomItemConstrain_Rows>;

            ImCustomItemConstrain_Rows delete = itemsList.Find(p => p.index == index);
            itemsList.Remove(delete);
            Session["ImItemsConstrain_Rows"] = itemsList;

            if (delete.Id > 0)
            {
                DeleteParameters obj = new DeleteParameters();

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;

                var deleteList = Session["ImDeletedConstrain"] as List<DeleteParameters>;
                deleteList.Add(obj);

                Session["ImDeletedConstrain"] = deleteList;
            }

            return Json(new { Result = "OK", Record = itemsList.OrderBy(p => p.index) });
        }

        public JsonResult ListItems_ports(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            if (Session["ImDB_ItemConstrainPorts"] != null)
            {
                var dbConstrain = Session["ImDB_ItemConstrainPorts"] as ImCustomCountryConstrain;
                if (dbConstrain != null && dbConstrain.items != null)
                {
                    var itemIndextre = int.Parse(Session["ImItemConRows_IndexPorts"].ToString());

                    List<ImCustomItemConstrain_ArrivalPorts> Ports = new List<ImCustomItemConstrain_ArrivalPorts>();

                    foreach (var item in dbConstrain.items.ItemConstrain_ArrivalPorts)
                    {
                        item.index = itemIndextre;
                        itemIndextre++;
                    }
                    Ports.AddRange(dbConstrain.items.ItemConstrain_ArrivalPorts);

                    Session["ImItemConRows_IndexPorts"] = itemIndextre;
                    Session["ImDB_ItemConstrainPorts"] = null;
                    Session["ImItemsConstrain_Ports"] = Ports;
                }
            }

            var itemsListPorts = Session["ImItemsConstrain_Ports"] as List<ImCustomItemConstrain_ArrivalPorts>;

            return Json(new { Result = "OK", Records = itemsListPorts.OrderBy(p => p.index), TotalRecordCount = itemsListPorts.Count });
        }
        public JsonResult CreateItems_ports(ImCustomItemConstrain_ArrivalPorts model)
        {
            model.index = int.Parse(Session["ImItemConRows_IndexPorts"].ToString());


            var itemsListtr = Session["ImItemsConstrain_Ports"] as List<ImCustomItemConstrain_ArrivalPorts>;

            var itemRepeattr = itemsListtr.Where(p => p.PortnationalID == model.PortnationalID).Count();

            if (itemRepeattr == 0)
            {
                itemsListtr.Add(model);
                Session["ImItemsConstrain_Ports"] = itemsListtr;

                Session["ImItemConRows_IndexPorts"] = int.Parse(Session["ImItemConRows_IndexPorts"].ToString()) + 1;
                return Json(new { Result = "OK", Record = itemsListtr.OrderBy(p => p.index), TotalRecordCount = itemsListtr.Count });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult UpdateItems_ports(ImCustomItemConstrain_ArrivalPorts model)
        {
            var itemsListtr = Session["ImItemsConstrain_Ports"] as List<ImCustomItemConstrain_ArrivalPorts>;

            var itemRepeattr = itemsListtr.Where(p => p.PortnationalID == model.PortnationalID && p.index != model.index).Count();

            if (itemRepeattr == 0)
            {
                ImCustomItemConstrain_ArrivalPorts update = itemsListtr.Find(p => p.index == model.index || p.Id == model.Id);
                model.ArrivalConstrain_ID = update.ArrivalConstrain_ID;
                itemsListtr.Remove(update);
                itemsListtr.Add(model);

                Session["ImItemsConstrain_Ports"] = itemsListtr;

                return Json(new { Result = "OK", Record = itemsListtr.OrderBy(p => p.index) });
            }
            else
            {
                return Json(new { Result = "Error", Message = Enums.GetEnumDescription<Enums.Error>(Enums.Error.RepeatedData) });
            }
        }
        public JsonResult DeleteItems_ports(int index)
        {
            var itemsList = Session["ImItemsConstrain_Ports"] as List<ImCustomItemConstrain_ArrivalPorts>;

            ImCustomItemConstrain_ArrivalPorts delete = itemsList.Find(p => p.index == index);
            itemsList.Remove(delete);
            Session["ImItemsConstrain_Ports"] = itemsList;

            if (delete.Id > 0)
            {
                DeleteParameters obj = new DeleteParameters();

                User_Session Current = User_Session.GetInstance;
                obj.Userid = (short)Session["UserId"];
                obj._DateNow = DateTime.Now;

                var deleteList = Session["ImDeletedConstrain"] as List<DeleteParameters>;
                deleteList.Add(obj);

                Session["ImDeletedConstrain"] = deleteList;
            }

            return Json(new { Result = "OK", Record = itemsList.OrderBy(p => p.index) });
        }
    }
}