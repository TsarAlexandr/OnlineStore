﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebDev_MainLab
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static void SetNull(this ISession session, string key)
        {
            session.Remove(key);
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            if (value != null)
            {
                var res = JsonConvert.DeserializeObject<T>(value);
                return res;
            }
            return default(T);//value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
