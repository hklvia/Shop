using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MODEL;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string dh = CreateNumber(1);
            //Console.WriteLine(dh);
            var aa= (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            var bb= DateTime.Now.ToString("yyyyMMddHHmmssfffff");
        }

        //private static string CreateNumber(int type)
        //{
        //    ShopEntities entities = new ShopEntities();
        //    //1-商品销售订单
        //    //Random random = new Random();
        //    int currentNumber = 1;
        //    var number = entities.Number.Where(x => x.Type == type).FirstOrDefault();
        //    if (number != null)
        //    {
        //        currentNumber = number.CurrentNumber.Value + 1;
        //        number.CurrentNumber = currentNumber;
        //        entities.SaveChanges();//执行update
        //    }
        //    else
        //    {
        //        Number number1 = new Number();
        //        number.Type = type;
        //        number.CurrentNumber = currentNumber;
        //        entities.Number.Add(number1);
        //        entities.SaveChanges();
        //    }
        //    string dh = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + new Random().Next(10000, 999999) + currentNumber.ToString().PadLeft(5, '0');
        //    return dh;
        //}
    }
}
