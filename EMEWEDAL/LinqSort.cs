using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace EMEWEDAL
{
    public static class LinqSort
    {
        /// <summary>
        /// Linq动态排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">要排序的数据源</param>
        /// <param name="value">排序依据（加空格）排序方式</param>
        /// <returns>IOrderedQueryable</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string value)
        {
            string[] arr = value.Split(' ');
            string Name = arr[1].ToUpper() == "DESC" ? "OrderByDescending" : "OrderBy";
            return ApplyOrder<T>(source, arr[0], Name);
        }
        /// <summary>
        /// Linq动态排序再排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="source">要排序的数据源</param>
        /// <param name="value">排序依据（加空格）排序方式</param>
        /// <returns>IOrderedQueryable</returns>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string value)
        {
            string[] arr = value.Split(' ');
            string Name = arr[1].ToUpper() == "DESC" ? "ThenByDescending" : "ThenBy";
            return ApplyOrder<T>(source, arr[0], Name);
        }
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "a");
            PropertyInfo pi = type.GetProperty(property);
            Expression expr = Expression.Property(arg, pi);
            type = pi.PropertyType;
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            object result = typeof(Queryable).GetMethods().Single(
            a => a.Name == methodName
            && a.IsGenericMethodDefinition
            && a.GetGenericArguments().Length == 2
            && a.GetParameters().Length == 2).MakeGenericMethod(typeof(T), type).Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;


        }

        //private void dd() {
        //ParameterExpression param = Expression.Parameter(typeof(Customer), "c");
        //Expression left = Expression.Property(param, typeof(Customer).GetProperty("City"));
        //Expression right = Expression.Constant("London");
        //Expression filter = Expression.Equal(left, right);
        //Expression pred = Expression.Lambda(filter, param);
        //IQueryable custs = db.Customers;
        //Expression expr = Expression.Call(typeof(Queryable), "Where", new Type[] { typeof(Customer) }, Expression.Constant(custs), pred);
        //expr = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeof(Customer), typeof(string) }, custs.Expression, Expression.Lambda(Expression.Property(param, "ContactName"), param));
        //IQueryable<Customer> query = db.Customers.AsQueryable().Provider.CreateQuery<Customer>(expr);
        //}

        public static Func<T, Tkey> DynamicLambda<T, Tkey>(string propertyName)
        {

            ParameterExpression p = Expression.Parameter(typeof(T), "p");
            Expression body = Expression.Property(p, typeof(T).GetProperty(propertyName));

            var lambda = Expression.Lambda<Func<T, Tkey>>(body, p);

            return lambda.Compile();
            //调用
            //List<Employee> list = new List<Employee>();
            //list.Add(new Employee() { Name = "张三", Age = 21, Salary = 1800f, Job = "UI" });
            //list.Add(new Employee() { Name = "李四", Age = 25, Salary = 2000f, Job = "DBA" });
            //list.Add(new Employee() { Name = "王五", Age = 24, Salary = 2000f, Job = "UI" });
            //list.Add(new Employee() { Name = "李九", Age = 31, Salary = 2900f, Job = "DBA" });
            //list.Add(new Employee() { Name = "张一", Age = 21, Salary = 2100f, Job = "UI" });
            //list.Add(new Employee() { Name = "王三", Age = 32, Salary = 2100f, Job = "DBA" });

            ////按Age排序
            //list.OrderBy(DynamicLambda<Employee, int>("Age"));
            ////按Salary排序
            //// list.OrderBy(DynamicLambda<Employee,float>("Salary"));

            //list.ForEach(e => Console.WriteLine(e.Name + "\t" + e.Age + "\t" + e.Salary));



            //var property = typeof(TSource).GetProperty(item);             
            //var parameter = Expression.Parameter(typeof(TSource), "p");        
            //var propertyAccess = Expression.MakeMemberAccess(parameter, property);      
            //var orderByExp = Expression.Lambda(propertyAccess, parameter);          
            //if (message.Sort[item] == PageSortDirection.Ascending)//判断是升序还是降序 如果是应用到搜索条件 那么就是 判断应该用= > < like 还是别的什么         
            //{                 
            //    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { typeof(TSource), property.PropertyType }, list.Expression, Expression.Quote(orderByExp));      
            //    list = list.Provider.CreateQuery<TSource>(resultExp);            
            //}              
            //else                {               
            //    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { typeof(TSource), property.PropertyType }, list.Expression, Expression.Quote(orderByExp));                    
            //list = list.Provider.CreateQuery<TSource>(resultExp);             
            //}
        }


    }
}