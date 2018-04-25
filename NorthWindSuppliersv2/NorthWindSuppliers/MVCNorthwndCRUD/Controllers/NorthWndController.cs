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
                    TempData["Message"] = ex.Data["Message"];
                }
            }
            return View(mappedItems);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SupplierPO from)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Update(SupplierPO from, int Id)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
    }
}