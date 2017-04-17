using CRM.Domain.Concrete;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CRM.WebUI.Models
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            string a = JsonConvert.SerializeObject(value);
            session.SetString(key, a);
            session.CommitAsync();
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) :
                                  JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class QuerySettingViewModel
    {
        public List<QuerySetting> search;
        public List<QuerySetting> sort;
        public int offset;

        public QuerySettingViewModel()
        {
            search = new List<QuerySetting>();
            sort = new List<QuerySetting>();
            offset = 0;
        }
    }
}
