using Microsoft.AspNetCore.Http;
using System;

namespace CRM.Domain.Abstract
{
    public class BaseRepository
    {
        private string _userid;

        public string UserContext { get; set; }

        public class DatetimeRange
        {
            public DateTime? dBegin { get; set; }
            public DateTime? dEnd { get; set; }

            public DatetimeRange(string sRange)
            {
                dBegin = null; dEnd = null;
                try
                {
                    int index = sRange.IndexOf(',');
                    string s = sRange.Substring(0, index).Trim();
                    string e = sRange.Substring(index + 1).Trim();
                    if (s != "") dBegin = DateTime.Parse(s);
                    if (e != "") dEnd = DateTime.Parse(e);
                    //if is only date part, increase one day on the end date
                    if (e != "" && e.Length < 11) dEnd.GetValueOrDefault().AddDays(1);
                }
                catch 
                {

                }
            }
        }

        public void SetCreatedSignature(IEntity item)
        {
            _userid = UserContext;
            if (_userid!=null) item.CreatedByID = _userid;
            item.CreatedTime = DateTime.Now;
            if (_userid != null) item.ModifiedByID = _userid;
            item.ModifiedTime = DateTime.Now;
        }

        public void SetModifiedSignature(IEntity item)
        {
            _userid = UserContext;
            if (_userid != null) item.ModifiedByID = _userid;
            item.ModifiedTime = DateTime.Now;
        }
    }
}

