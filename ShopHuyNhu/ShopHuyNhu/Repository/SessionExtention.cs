using Newtonsoft.Json;
using ShopHuyNhu.Controllers;
using ShopHuyNhu.Models;
using System.Runtime.CompilerServices;

namespace ShopHuyNhu.Repository
{
    public static class SessionExtention
    {
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
           
        }
        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null ? default(T) : JsonConvert.DeserializeObject<T>(sessionData);
        }
       
    }
}
