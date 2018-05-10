using DataLayer;
using DataLayer.Models;
using MVCNorthwndCRUD.Mapping;
using MVCNorthwndCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using MVCNorthwndCRUD.Custom;
using System.IO;

namespace MVCNorthwndCRUD.Controllers
{
    public class NorthWndController : Controller
    {
        private SupplierDAO dataAccess;
        public NorthWndController()
        {
            string filePath = ConfigurationManager.AppSettings["logPath"];
            dataAccess = new SupplierDAO(connectionString, filePath);
        }
        //Dependencies
        
        private static string connectionString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        // GET: NorthWnd
        [HttpGet]
        public ActionResult Index()
        {
            List<SupplierPO> mappedItems = new List<SupplierPO>();
            try
            {
                List<SupplierDO> dataObjects = dataAccess.ViewAllSuppliers();
                mappedItems = SupplierMapper.MapDoToPO(dataObjects);
            }
            catch (Exception ex)
            {
                if (ex.Data.Contains("Message"))
                {
                    TempData["Message"] = "There aren't any suppliers left, you scared them all away!";
                }
            }
            return View(mappedItems);
        }

        [HttpGet]
        [SecurityFilter("RoleId", 1, 2, 3)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SupplierPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "NorthWnd");

            if (ModelState.IsValid)
            {
                try
                {
                    SupplierDO dataObject = SupplierMapper.MapPoToDO(form);
                    dataAccess.CreateNewSuppliers(dataObject);

                    TempData["Message"] = $"{form.ContactName} was created successfully.";
                }
                catch (Exception ex)
                { 
                    oResponse = View(form);
                }
            }
            else
            {
                oResponse = View(form);
            }

            return oResponse;
        }

        [HttpGet]
        [SecurityFilter("RoleId", 1, 2)]
        public ActionResult Update(int supplierID)
        {
            SupplierPO displayObject = new SupplierPO();
            ActionResult oResponse = View(displayObject);
            try
            {
                SupplierDO item = dataAccess.ViewSupplierAtID(supplierID);
                displayObject = SupplierMapper.MapDoToPO(item);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Supplier could not be updated at this time.";
            }
            return View(displayObject);
        }

        [HttpPost]
        public ActionResult Update(SupplierPO form)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "NorthWnd");

            if (ModelState.IsValid)
            {
                try
                {
                    SupplierDO dataObject = SupplierMapper.MapPoToDO(form);
                    dataAccess.UpdateSuppliers(dataObject);
                    TempData["Message"] = "Successfully updated supplier.";
                }
                catch (Exception ex)
                {
                    oResponse = View(form);
                }
            }
            else
            {
                oResponse = View(form);
            }

            return oResponse;
        }

        [HttpGet]
        public ActionResult Delete(int supplierID)
        {
            //Declaring local variables
            ActionResult oResponse = RedirectToAction("Index", "NorthWnd");
            try
            {
                dataAccess.DeleteSuppliers(supplierID);
                TempData["Message"] = "Supplier has been deleted.";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Supplier could not be deleted at this time.";

            }
            return oResponse;
        }

        [HttpGet]
        public ActionResult UploadFile()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase uploadedFile)
        {
            string pathToSaveTo = Path.Combine(Server.MapPath("/File/"), uploadedFile.FileName);
            uploadedFile.SaveAs(pathToSaveTo);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult DownloadFile()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DownloadFile(string name)
        {
            string fullPath = Path.Combine(Server.MapPath("/Files/"), name);
            FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            return File(stream, "application/octet-stream", name);
        }
    }
}