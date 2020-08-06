using DAL;
using IBLL;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NumberBLL : BaseBLL<Number, NumberDAL>, INumberBLL
    {
        /// <summary>
        ///生成订单编号
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string CreateNumber(int type)
        {
            //ShopEntities entities = new ShopEntities();
            //1-商品销售订单
            int currentNumber = 1;
            var number = dal.Search(x => x.Type == type).FirstOrDefault();
            if (number != null)
            {
                currentNumber = number.CurrentNumber.Value + 1;
                number.CurrentNumber = currentNumber;
                SaveChange();//执行update
            }
            else
            {
                Number number1 = new Number();
                number.Type = type;
                number.CurrentNumber = currentNumber;
                dal.Add(number1);
                SaveChange();
            }
            string dh = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + new Random().Next(10000, 999999) + currentNumber.ToString().PadLeft(5, '0');
            return dh;
        }
    }
}
