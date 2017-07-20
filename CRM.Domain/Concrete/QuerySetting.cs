using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Domain.Concrete
{
    public class QuerySetting
    {
        public QuerySetting(string sField, string sValue)
        {
            Field = sField; Value = sValue;
        }
        public string Field { get; set; }
        public string Value { get; set; }
    }
}
