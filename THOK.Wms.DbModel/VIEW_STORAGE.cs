using System;
using System.Collections.Generic;

namespace THOK.Wms.DbModel
{
    public partial class VIEW_STORAGE
    {
        public string CELL_CODE { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_BARCODE { get; set; }
        public string BILL_NO { get; set; }
        public Nullable<decimal> REAL_WEIGHT { get; set; }
        public string GRADE_CODE { get; set; }
        public string ENGLISH_CODE { get; set; }
        public string USER_CODE { get; set; }
        public string GRADE_NAME { get; set; }
        public string MEMO { get; set; }

    }
}
