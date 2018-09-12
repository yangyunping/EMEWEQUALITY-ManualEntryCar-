﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEEntity;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections;

namespace EMEWEQUALITY.HelpClass
{
    public class FenYe
    {
        #region 分页内容
        private DataGridView dgv;
     /// <summary>
        /// 当前页数
     /// </summary>
        private ToolStripTextBox tstb;
        /// <summary>
        /// 显示总条数和每页条数
        /// </summary>
        private ToolStripLabel tsl;
        /// <summary>
        /// 每页显示行数
        /// </summary>
        int pageSize = 0;     
      /// <summary>
        /// 总记录数
      /// </summary>
        int nMax = 0;         
        /// <summary>
        /// 页数＝总记录数/每页显示行数
        /// </summary>
        int pageCount = 0;   
        /// <summary>
        /// 当前页号
        /// </summary>
        int pageCurrent = 0;   
        /// <summary>
        /// 当前记录行
        /// </summary>
        int nCurrent = 0;      

 
        #endregion
      

        bool rbool = true;
        public string PageMaxCount="10";




     
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">操作对象</typeparam>
        /// <param name="ClickedItemName">操作行为名 传入空值表示返回数据前10条</param>
        /// <param name="tstb">显示当前页数控件</param>
        /// <param name="tsl">显示总条数和每页条数的控件</param>
        /// <param name="value">排序字段</param>
        /// <param name="fun">查询条件<</param>
        /// <returns></returns>
        public IEnumerable<T> BindBoundControl<T>(string ClickedItemName, ToolStripTextBox tstb, ToolStripLabel tsl, Expression<Func<T, bool>> expr, string value) where T : class
        {
            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            try
            {
                pageSize = EMEWE.Common.Converter.ToInt(PageMaxCount.Replace("条", ""));//每页显示行数
                if (rbool)
                {
                    pageCurrent = 1;    //当前页数从1开始 
                }
                if (expr != null)
                {
                    nMax = dc.GetTable<T>().Where<T>(expr).Count();
                }
                else
                {
                    nMax = dc.GetTable<T>().Count();
                }
                pageCount = (nMax / pageSize);    //计算出总页数
                if ((nMax % pageSize) > 0) pageCount++;

                rbool = true;
                if (ClickedItemName != "")
                {
                    itemClicked<T>(ClickedItemName);
                }
                tsl.Text = " 共 " + pageCount.ToString() + " 页  共 " + nMax.ToString() + " 条";
                tstb.Text = Convert.ToString(pageCurrent);
                return OrderBy<T>(value, expr);
            }
            catch (Exception ex)
            {
              
                Common.WriteTextLog("分页FenYe.BindBoundControl异常：" + ex.Message);
                return null;
            }
        
        }
        /// <summary>
        /// 分页菜单事件响应处理
        /// </summary>
        /// <typeparam name="T">操作对象</typeparam>
        /// <param name="ClickedItemName">操作行为名</param>
        /// <param name="ie">数据源对象</param>
        public void itemClicked<T>(string ClickedItemName)
        {
            string stritemName = ClickedItemName;
            if (stritemName.Length > 2)
            {
                ClickedItemName = stritemName.Remove(stritemName.Length - 1);
            }
            if (ClickedItemName == "tslPreviousPage" || ClickedItemName == "bindingNavigatorMovePreviousItem")//上一页
            {

                if (pageCurrent <= 1)
                {
                    MessageBox.Show("已经是第一页，请点击“下一页”查看！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbool = false;
                    return;
                }
                else
                {
                    pageCurrent--;
                    nCurrent = pageSize * (pageCurrent - 1);
                }


            }
            if (ClickedItemName == "tslNextPage" || ClickedItemName == "bindingNavigatorMoveNextItem")//下一页
            {

                if (pageCurrent >= pageCount)
                {
                    MessageBox.Show("已经是最后一页，请点击“上一页”查看！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbool = false;
                    return;
                }
                else
                {
                    pageCurrent++;
                    nCurrent = pageSize * (pageCurrent - 1);
                }

            }
            if (ClickedItemName == "tslHomPage" || ClickedItemName == "bindingNavigatorMoveFirstItem")//首页
            {
                pageCurrent = 1;
                nCurrent = pageSize * (pageCurrent - 1);

            }
            if (ClickedItemName == "tslLastPage" || ClickedItemName == "bindingNavigatorMoveLastItem")//尾页
            {
                pageCurrent = pageCount;
                nCurrent = pageSize * (pageCurrent - 1);
            }
            rbool = false;

        }
        /// <summary>
        /// Linq动态排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="value">排序依据（加空格）排序方式</param>
        /// <returns>IOrderedQueryable</returns>
        public IEnumerable<T> OrderBy<T>(string value, Expression<Func<T, bool>> expr) where T : class
        {
            string[] arr = value.Split(' ');
            int arrleng = arr.Length;
            string Name = arr[arrleng - 1].ToUpper() == "DESC" ? "OrderByDescending" : "OrderBy";
            return ApplyOrder<T>(arr[0], Name, expr);
        }
        /// <summary>
        /// Linq动态排序再排序
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="value">排序依据（加空格）排序方式</param>
        /// <returns>IOrderedQueryable</returns>
        public IEnumerable<T> ThenBy<T>(string value, Expression<Func<T, bool>> expr) where T : class
        {
            string[] arr = value.Split(' ');
            int arrleng = arr.Length;
            string Name = arr[arrleng - 1].ToUpper() == "DESC" ? "OrderByDescending" : "OrderBy";
            return ApplyOrder<T>(arr[0], Name, expr);
        }
        private IEnumerable<T> ApplyOrder<T>(string property, string methodName, Expression<Func<T, bool>> expr1) where T : class
        {
            DCQUALITYDataContext db = new DCQUALITYDataContext();
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "a");
            PropertyInfo pi = type.GetProperty(property);
            Expression expr = Expression.Property(arg, pi);
            type = pi.PropertyType;
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);
            object result = typeof(Queryable).GetMethods().Single(a => a.Name == methodName && a.IsGenericMethodDefinition && a.GetGenericArguments().Length == 2 && a.GetParameters().Length == 2).MakeGenericMethod(typeof(T), type).Invoke(null, new object[] { expr1 != null ? db.GetTable<T>().Where<T>(expr1) : db.GetTable<T>(), lambda });
            if (expr1 != null)
            {
                return ((IOrderedQueryable<T>)result).Skip<T>(nCurrent).Take<T>(pageSize).ToList();
            }
            else
            {
                return ((IOrderedQueryable<T>)result).Skip<T>(nCurrent).Take<T>(pageSize).ToList();
            }
           
        }
    }
}

