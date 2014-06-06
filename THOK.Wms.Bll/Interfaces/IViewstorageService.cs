using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using THOK.Wms.DbModel;

namespace THOK.Wms.Bll.Interfaces
{
    public interface IViewstorageService : IService<VIEW_STORAGE >
    {
        //库存查询
        object GetDetails(int page, int rows, Dictionary<string, string> paramers);
        //获取所有包含该等级的入库单的库存。
        object GetSubDetails(int page, int rows, string Gradecode);
    }
}
