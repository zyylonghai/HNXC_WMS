using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;
using THOK.Wms.Bll.Interfaces;
using Microsoft.Practices.Unity;
using THOK.Wms.Dal.Interfaces;

namespace THOK.Wms.Bll.Service
{
    class ViewstorageService : ServiceBase<VIEW_STORAGE>,IViewstorageService 
    {

        protected override Type LogPrefix
        {
            get { return this.GetType();}
        }
        [Dependency]
        public IVIEWSTORAGERepository  ViewstorageRepository { get; set; }
        [Dependency]
        public IViewbillmastRepository  ViewbillmastRepository { get; set; }
        [Dependency]
        public ICMDCigaretteRepository CMDCigaretteRepository { get; set; }
        [Dependency]
        public IWMSFormulaMasterRepository FormulMasterRepository { get; set; }
        [Dependency]
        public IWMSFormulaDetailRepository FormulDetailRepository { get; set; }

        ////库存查询
        public object GetDetails(int page, int rows, Dictionary<string, string> paramers)
        {
            var storagequery = ViewstorageRepository.GetQueryable();
            var cigaret=CMDCigaretteRepository .GetQueryable ();
            var formulas=FormulMasterRepository .GetQueryable ();
            var formuladetail=FormulDetailRepository .GetQueryable ();
            if (paramers.Count > 0) {
                foreach (string fieldname in paramers.Keys) {
                    string fieldvalue = paramers[fieldname].ToString();
                    switch (fieldname) {
                        case "BILL_NO":storagequery= storagequery.Where(i => i.BILL_NO == fieldvalue); break;
                        case "GRADE_CODE": storagequery = storagequery.Where(i => i.GRADE_CODE == fieldvalue); break;
                        case "CIGARETTE_CODE":
                            var productcode = from a in cigaret
                                         join b in formulas on a.CIGARETTE_CODE equals b.CIGARETTE_CODE
                                         join c in formuladetail on b.FORMULA_CODE equals c.FORMULA_CODE
                                         where a.CIGARETTE_CODE == fieldvalue
                                         select new { 
                                             c.PRODUCT_CODE 
                                         };
                            storagequery = storagequery.Where(i => (from b in productcode select b.PRODUCT_CODE).Contains(i.PRODUCT_CODE));
                            break;
                        case "FORMULA_CODE":
                            var productcode2 = from a in formulas
                                              join b in formuladetail on a.FORMULA_CODE equals b.FORMULA_CODE
                                              where a.FORMULA_CODE == fieldvalue
                                              select new { 
                                                  b.PRODUCT_CODE 
                                              };
                            storagequery = storagequery.Where(i => (from b in productcode2 select b.PRODUCT_CODE).Contains(i.PRODUCT_CODE));
                            break;
                        default: break;
                    }
                }
            }
            var storage = from a in storagequery
                          group a by new {a.GRADE_CODE ,a.GRADE_NAME ,a.ENGLISH_CODE ,a.USER_CODE ,a.MEMO  } into g
                          select new { 
                              g.Key .GRADE_CODE ,
                              g.Key .GRADE_NAME ,
                              g.Key .ENGLISH_CODE ,
                              g.Key .USER_CODE ,
                              g.Key .MEMO ,
                              TOTALPACKAGE=g.Count (),
                              TOTALWEIGHT=g.Sum(i=>i.REAL_WEIGHT )
                          };
            storage = storage.OrderByDescending(i => i.GRADE_CODE);
            int total = storage.Count();
            storage = storage.Skip((page - 1) * rows).Take(rows);
            var temp = storage.ToArray().Select(i => i);
            return new { total, rows = temp };
        }

        // //获取所有包含该等级的入库单的库存。
        public object GetSubDetails(int page, int rows, string Gradecode)
        {
            var storagequery = ViewstorageRepository.GetQueryable();
            var billmast = ViewbillmastRepository.GetQueryable();
            var cigaret=CMDCigaretteRepository .GetQueryable ();
            var formula=FormulMasterRepository .GetQueryable ();
            var billnos = from a in storagequery
                          where a.GRADE_CODE == Gradecode
                          group a by new { a.BILL_NO,a.GRADE_CODE } into g
                          select new
                          {
                              g.Key,
                              TOTALPACKAGE = g.Count(),
                              TOTALWEIGHT = g.Sum(i => i.REAL_WEIGHT)
                          };
            var bilLstorage = from a in billmast
                              join b in billnos on a.BILL_NO  equals b.Key.BILL_NO
                              join c in cigaret on a.CIGARETTE_CODE equals c.CIGARETTE_CODE 
                              join f in formula on a.FORMULA_CODE equals f.FORMULA_CODE 
                              select new {
                                  GRADE_CODE=b.Key .GRADE_CODE ,
                                  BILL_NO= b.Key.BILL_NO,
                                  a.BILL_DATE ,
                                  a.CIGARETTE_CODE ,
                                  c.CIGARETTE_NAME ,
                                  a.FORMULA_CODE,
                                  f.FORMULA_NAME ,
                                  b.TOTALPACKAGE,
                                  b.TOTALWEIGHT
                              };
            bilLstorage = bilLstorage.OrderByDescending(i => i.BILL_NO );
            int total = bilLstorage.Count();
            bilLstorage = bilLstorage.Skip((page - 1) * rows).Take(rows);
            var temp = bilLstorage.ToArray().Select(i => new { 
                i.GRADE_CODE,
                i.BILL_NO ,
                BILL_DATE = i.BILL_DATE.ToString("yyyy-MM-dd HH"),
                i.CIGARETTE_NAME,
                i.FORMULA_NAME ,
                i.TOTALWEIGHT ,
                i.TOTALPACKAGE 
            });
            return new { total, rows = temp };
        }
    }
}
