using MODEL;
using System.Runtime.Remoting.Messaging;

namespace DAL
{
    public class DbContextFactory
    {
        public static ShopEntities GetEntities()
        {
            var obj = CallContext.GetData("dbContext");  //从容器中获取数据
            if (obj == null)
            {
                //创建一个上下文对象，并将其放到线程容器中
                ShopEntities entities = new ShopEntities();
                CallContext.SetData("dbContext", entities);
                return entities;
            }

            return (ShopEntities)obj;
        }
    }
}