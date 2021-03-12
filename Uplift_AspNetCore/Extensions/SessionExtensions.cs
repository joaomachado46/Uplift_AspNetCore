using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Uplift_AspNetCore.Extensions
{
    public static class SessionExtensions
    {
        //COLOCA O VALOR NA KEY
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //VAI BUSCAR OS VALORES QUE JA ESTAO NA KEY
        public static T GetObject<T>(this ISession session, string Key)
        {
            var value = session.GetString(Key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
