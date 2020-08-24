using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        //单例模式
        //private static Singleton instance = null;
        //private static readonly object padlock = new object();
        //Singleton()
        //{
        //}
        //public static Singleton Instance
        //{
        //    get
        //    {
        //        lock (padlock)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new Singleton();
        //            }
        //            return instance;
        //        }
        //    }
        //}
        //单例模式2
        //private static readonly Singleton instance = new Singleton();
        //static Singleton()
        //{
        //}
        //private Singleton()
        //{
        //}
        //public static Singleton Instance
        //{
        //    get
        //    {
        //        return instance;
        //    }
        //}

        static void Main(string[] args)
        {
            //c#实现异步编程
            //1、通过线程
            //声明一个线程
            //Thread thread = new Thread(F1);
            //thread.Start();//启动一个线程
                           //线程阻塞
                           //thread.Join();//主线程会等待thread线程执行完毕后才会继续往下执行

            // 2 task
            //Task task1 = Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine("aaa");
            //    }
            //    Thread.Sleep(3000);
            //});

            //Task task2 = Task.Run(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine("bbb");
            //    }
            //    Thread.Sleep(3000);
            //});

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("111");
            //}

            //Console.WriteLine(task.Result);//遇到task.Result 会产生一个阻塞，等待异步任务执行结束后才会继续往下执行
            //Task<string> ts = 
            //F1();
            //Console.WriteLine(ts.Result);
            //for (int i = 0; i < 50; i++)
            //{
            //    Console.WriteLine("1");
            //}

            for (int i = 1; i <= 30; i++)
            {
                Console.WriteLine(F(i));
            }
            Console.ReadLine();
        }

        static int F(int n)
        {
            if (n == 1 || n == 2)
            {
                return 1;
            }
            return F(n - 2) + F(n - 1);
        }

        //async：标识该方法内部至少会有一个异步任务
        //async修饰的方法内必须至少包含一个await运算符，await后一般跟一个异步任务
        //async修饰方法的返回值：void、task、task<>
        //void、task不用返回值
        //task<T>:直接返回T类型的值就可以
        public async static void F1()
        {

            //主线程执行
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("a");
            }
            //遇到await之后会从线程池中取一个线程执行await之后的所有代码
            await Task.Run(() =>
            {

                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("b");
                }
            });
            await Task.Run(() =>
             {
                 for (int i = 0; i < 20; i++)
                 {
                     Console.WriteLine("c");
                 }
             });
            //await也会产生一个阻塞，等待await之后的异步任务执行结束之后才会继续往下执行

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("d");
            }
            //return "hello world";
        }
    }
}
