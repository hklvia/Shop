using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
    public class RedisHelper
    {
        static ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("192.168.137.111:6379,password=123456");
        static IDatabase db = conn.GetDatabase(0);

        public static bool Set(string key, int value, TimeSpan timeSpan)
        {
            //设置token的有效期为7天,openId也可以是数据库的id，唯一标识一个用户就可以
            return db.StringSet(key, value, timeSpan);
        }
        public static string Get(string key)
        {
            return db.StringGet(key);
        }
    }
}
