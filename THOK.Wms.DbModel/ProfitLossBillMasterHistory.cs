﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace THOK.Wms.DbModel
{
   public class ProfitLossBillMasterHistory
    {
       public ProfitLossBillMasterHistory()
        {
            this.ProfitLossBillDetailHistorys = new List<ProfitLossBillDetailHistory>();
        }
        public string BillNo { get; set; }
        public DateTime BillDate { get; set; }
        public string BillTypeCode { get; set; }
        public string CheckBillNo { get; set; }
        public string WarehouseCode { get; set; }
        public Guid OperatePersonID { get; set; }
        public string Status { get; set; }
        public Guid? VerifyPersonID { get; set; }
        public DateTime? VerifyDate { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public DateTime UpdateTime { get; set; }
        public string LockTag { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<ProfitLossBillDetailHistory> ProfitLossBillDetailHistorys { get; set; }
    }
}
