using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEEntity;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;
using System.Linq.Expressions;
using System.Collections;
using System.Data.Linq.SqlClient;
using EMEWE.Common;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class SystemOperate : Form
    {
        public SystemOperate()
        {
            InitializeComponent();
        }

      
        public MainFrom mf;//主窗体
        Expression<Func<View_LogInfo_Dictionary, bool>> expr = null;
        PageControl page = new PageControl();
        //得到日志的编号的字段
        //Expression<Func<View_LogInfo_Dictionary, int> P = n => Common.GetInt(n.Log_ID.Value.ToString());
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            this.lvwUserList.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            expr = n => n.Log_ID != null;
            LoadData();
            mf = new MainFrom();
            typeBing();
        }
        private void typeBing()
        {
            cmb_type.DataSource = DictionaryDAL.GetValueStateDictionary("日志类型");
            cmb_type.DisplayMember = "Dictionary_Name";
            cmb_type.ValueMember = "Dictionary_ID";
            if (cmb_type.DataSource!=null)
            {
                cmb_type.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                
                this.lvwUserList.DataSource = null;
                lvwUserList.DataSource = page.BindBoundControl<View_LogInfo_Dictionary>("", txtCurrentPage2, lblPageCount2, expr, "Log_ID Log_Time desc");
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("日记管理 LoadData()" + ex.Message.ToString());
            }

        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void btnxSearch_Click(object sender, EventArgs e)
        {
            if (btnxSearch.Enabled)
            {
                btnxSearch.Enabled = false;
                btnxSearchWhere();
                btnxSearch.Enabled = true;

            }
        }
        /// <summary>
        /// 搜索条件
        /// </summary>
        /// 
        private void btnxSearchWhere()
        {
           
            try
            {
                int i = 0;
                string str = "";
                //得到查询的条件
                string name = txtOperName.Text.Trim();
                string beginTime = this.dtiBegin.Text.Trim();
                string endTime = this.dtiEnd.Text.Trim();
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "

                //if (expr == null )
                //{
                    expr = PredicateExtensionses.True<View_LogInfo_Dictionary>();
                //}
                if (name != "")//操作人
                {
                    expr = expr.And(n =>SqlMethods.Like( n.Log_Name,"%"+txtOperName.Text.Trim()+"%"));
                      
             
                    i++;
                }
                if (beginTime != "")//开始时间
                {
                    expr = expr.And(n => n.Log_Time >= Common.GetDateTime(begin));
            
                    i++;
                }
                if (endTime != "")//结束时间
                {
                    expr = expr.And(n => n.Log_Time <= Common.GetDateTime(end));
                
                    i++;
                }
                if (beginTime != "" && endTime != "")
                {
                    if (Common.GetDateTime(beginTime) > Common.GetDateTime(endTime))
                    {
                        MessageBox.Show("查询开始时间不能大于结束时间！", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        dtiBegin.Text = "";
                        dtiEnd.Text = "";
                        return;
                    }

                }
                if (cmb_type.SelectedValue != null)
                {
                    int stateID = Converter.ToInt(cmb_type.SelectedValue.ToString());
                    if (stateID > 0)
                    {
                        Dictionary dicEntity = cmb_type.SelectedItem as Dictionary;
                        if (dicEntity.Dictionary_Value == "全部")
                        {
                            expr = n => n.Log_ID != null;
                        }
                        else
                        {
                            expr = expr.And(n => n.Log_Dictionary_ID == Common.GetInt(cmb_type.SelectedValue.ToString()));
                        }
                    }


                    i++;
                }
                
                if (i == 0)
                {
                    expr = n => n.Log_ID != null;
                }
            }
            catch(Exception ex)
            {
                Common.WriteTextLog("SystemOperate.btnxSearchWhere()" + ex.Message.ToString());
            }
            finally
            {
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                LoadData();
            }

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemOperate_Load(object sender, EventArgs e)
        {
            InitUser();
        }
       
      
        /// <summary>
        /// 分页控件响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            lvwUserList.DataSource = page.BindBoundControl<View_LogInfo_Dictionary>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr);
        }
        /// <summary>
        /// 设置每页显示最大条数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData();
        }

       
        /// <summary>
        /// 菜单分页响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            lvwUserList.DataSource = page.BindBoundControl<View_LogInfo_Dictionary>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr, "Log_ID Log_Time desc");
        }

        #region  DataTimePicker控件的自定义显示  和 KeyPress事件
        /// <summary>
        /// DataTimePicker控件的文本操作方法
        /// </summary>
        private void DateTimeBeginISNull()
        {
            this.dtiBegin.Format = DateTimePickerFormat.Custom;
            this.dtiBegin.CustomFormat = " ";
        }
        private void DateTimeEndIsNull()
        {
            this.dtiEnd.Format = DateTimePickerFormat.Custom;
            this.dtiEnd.CustomFormat = " ";
        }
        /// <summary>
        /// 将“DataTiemPicker”控件赋值为null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
         private void dtiBegin_ValueChanged(object sender, EventArgs e)
        {
            this.dtiBegin.Format = DateTimePickerFormat.Custom;
            this.dtiBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
           
        }
        private void dtiEnd_ValueChanged(object sender, EventArgs e)
        {
            this.dtiEnd.Format = DateTimePickerFormat.Custom;
            this.dtiEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        /// <summary>
        /// “DataTiemPicker”控件的键盘事件 KeyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtiBegin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                DateTimeBeginISNull();
            }
        }
        private void dtiEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            DateTimePicker dtp = sender as DateTimePicker;
            if (e.KeyChar == '\b')
            {
                DateTimeEndIsNull();
            }
        }
        #endregion

    }
}
