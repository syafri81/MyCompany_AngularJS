using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyCompany.Models
{
    public class ExpendDetail: View_Expend
    {
        public string FakturDate { get; set; }
        public List<View_ExpendDetail> Details { get; set; }
    }
}