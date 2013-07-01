﻿using System.Web.Mvc;
using System.Web.Routing;
using System.Text;
using Microsoft.Practices.Unity;
using THOK.WebUtil;
using THOK.Authority.Bll.Interfaces;
using System;
using THOK.Wms.Bll.Interfaces;
using THOK.Wms.DbModel;

namespace WMS.Controllers.Wms.WMS
{
    public class FormulaController : Controller
    {
        //
        // GET: /Formula/

        [Dependency]
        public IWMSFormulaService FormulaService { get; set; }

        // GET: /Formula/
        public ActionResult Index(string moduleID)
        {
            ViewBag.hasSearch = true;
            ViewBag.hasAdd = true;
            ViewBag.hasEdit = true;
            ViewBag.hasDelete = true;
            ViewBag.hasPrint = true;
            ViewBag.hasHelp = true;
            ViewBag.ModuleID = moduleID;
            return View();
        }

        // GET: /Formula/Details/
        [HttpPost]
        public ActionResult Details(int page, int rows, FormCollection collection)
        {
            string BTYPE_NAME = collection["BTYPE_NAME"] ?? "";
            string BILL_TYPE = collection["BILL_TYPE"] ?? "";
            string TASK_LEVEL = collection["TASK_LEVEL"] ?? "";
            string Memo = collection["MEMO"] ?? "";
            string TARGET_CODE = collection["TARGET_CODE"] ?? "";
            var users = FormulaService.GetDetails(page, rows, BTYPE_NAME, BILL_TYPE, TASK_LEVEL, Memo, TARGET_CODE);
            return Json(users, "text", JsonRequestBehavior.AllowGet);
        }
        // GET: /Formula/Details/
        public ActionResult SubDetails(int page, int rows, string FORMULA_CODE)
        {
            var users = FormulaService.GetSubDetails(page, rows, FORMULA_CODE);
            return Json(users, "text", JsonRequestBehavior.AllowGet);
        }

        // POST: /Formula/Create/
        [HttpPost]
        public ActionResult Create(WMS_FORMULA_MASTER master,object detail)
        {
            bool bResult = FormulaService.Add(master, detail);
            string msg = bResult ? "新增成功" : "新增失败";
            return Json(JsonMessageHelper.getJsonMessage(bResult, msg, null), "text", JsonRequestBehavior.AllowGet);
        }
        // POST: /Formula/Create/
        [HttpPost]
        public ActionResult Edit(WMS_FORMULA_MASTER master, object detail)
        {
            bool bResult = FormulaService.Edit(master, detail);
            string msg = bResult ? "修改成功" : "修改失败";
            return Json(JsonMessageHelper.getJsonMessage(bResult, msg, null), "text", JsonRequestBehavior.AllowGet);
        }

        // POST: /Formula/Delete/
        [HttpPost]
        public ActionResult Delete(string FORMULA_CODE)
        {
            bool bResult = FormulaService.Delete(FORMULA_CODE);
            string msg = bResult ? "删除成功" : "删除失败";
            return Json(JsonMessageHelper.getJsonMessage(bResult, msg, null), "text", JsonRequestBehavior.AllowGet);
        }

        // POST: /Formula/GetFormulaCode/

        public ActionResult GetFormulaCode(System.DateTime dtime, string FORMULA_CODE)
        {
            string userName = this.GetCookieValue("username");
            var FormulaInfo = FormulaService.GetFormulaCode(userName, dtime, FORMULA_CODE);

            return Json(FormulaInfo, "text", JsonRequestBehavior.AllowGet);
        }
    }
}
