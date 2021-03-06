﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THOK.WebUtil;
using Microsoft.Practices.Unity;
using THOK.Wms.Bll.Interfaces;
using THOK.Security;
using Wms.Security;

namespace WMS.Controllers.Wms.WMS
{
    [TokenAclAuthorize]
    [SystemEventLog]
    public class InventoryWorkController : Controller
    {
        //
        // GET: /InventoryWork/
        [Dependency]
        public IWMSBillMasterService BillMasterService { get; set; }

        public ActionResult Index(string moduleID)
        {
            ViewBag.hasSearch = true;
            ViewBag.hasTask = true;
            ViewBag.ModuleID = moduleID;
            return View();
        }
        //作业函数
        public ActionResult Task(string BillNo)
        {
            string userName = this.GetCookieValue("userid");
            bool bResult = BillMasterService.InventoryTask (BillNo, userName);
            string msg = bResult ? "作业成功" : "作业失败";
            var just = new
            {
                success = msg
            };
            return Json(just, "text/html", JsonRequestBehavior.AllowGet);
        }

    }
}
