using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEQUALITY.HelpClass;
using EMEWEEntity;
using EMEWEDAL;
using System.Collections;
using EMEWE.Common;
using System.Linq.Expressions;
using System.Data.Linq.SqlClient;
using EMEWEQUALITY.SystemAdmin;
using System.Threading;
using EMEWEQUALITY.NewAdd;
using EMEWEQUALITY.LWU9WebReference;
using System.Threading.Tasks;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class OneQCAdmin : Form
    {
        public OneQCAdmin()
        {
            InitializeComponent();
        }


        /// 排序字段及方式[以空格格开]
        /// </summary>
        private string orderbywhere = "QCInfo_ID desc";
        /// <summary>

        Expression<Func<View_QCInfo, bool>> expr = null;
        Expression<Func<View_QCInfo, int>> P = N => N.QCInfo_ID;
        //Expression<Func<View_QCInfo, bool>> p = null;
        PageControl page = new PageControl();
        public MainFrom mf;//主窗体
        PrintDemo pd = new PrintDemo();//打印类

        ArrayList list = new ArrayList();
        /// <summary>
        /// 选中行列表
        /// </summary>
        List<int> ids = new List<int>();
        /// <summary>
        /// 选中lvwUserList中的QCInfo_ID列表数据
        /// </summary>
        List<int> listrecordID = new List<int>();
        int UpdateQCInfo_ID = 0;//质检编号【修改时用】
        int iQcInfoID = 0;
        int iQCRetest_ID = 0;
        int iDRAW_EXAM_INTERFACE_ID;
        /// <summary>
        /// 保存按钮是否忙碌
        /// </summary>
        bool ISBtnSaveBusy = false;
        /// <summary>
        /// 计算按钮是否忙碌
        /// </summary>
        bool ISbtnCalculateAfterBusy = false;
        /// <summary>
        /// 是否取消发送
        /// </summary>
        bool istishi = false;


        /// <summary>
        /// 生明无返回值的委托
        /// </summary>
        private delegate void AsynUpdateUI();

        /// <summary>
        /// 引用正在加载
        /// </summary>
        public Form_Loading form_loading = null;


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OneQCAdmin_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

            InitUser();
            // ISUpdateTrue();
        }
        /// <summary>
        /// 初始化时间
        /// </summary>
        private void TimeEndIsNull()
        {
            DateTime Time = DateTime.Now.AddDays(0);
            txtbeginTime.Value = Time;
            txtendTime.Value = Convert.ToDateTime(DateTime.Now.AddDays(1));
        }
        private void ISUpdateTrue()
        {
            if (btn_DetermineUpdate.Enabled == true && btn_DetermineUpdate.Visible == true)
            {
                Enabledbool(true);
            }
        }

        /// <summary>
        /// 绑定重量检测状态下拉列表框数据
        /// </summary>
        private void BindCboxState()
        {
            try
            {
                List<Dictionary> list = DictionaryDAL.GetValueStateDictionary("质检状态");
                //Dictionary dic=new Dictionary();
                //dic.Dictionary_Name="";
                //list.Add(dic);
                cbxQCInfoState.DataSource = DictionaryDAL.GetValueStateDictionary("状态");
                this.cbxQCInfoState.DisplayMember = "Dictionary_Name";
                cbxQCInfoState.ValueMember = "Dictionary_ID";
                if (cbxQCInfoState.DataSource != null)
                {
                    cbxQCInfoState.SelectedIndex = 0;
                }

                cbxQCInfoState.DataSource = list;
                cbxQCInfoState.DisplayMember = "Dictionary_Name";
                cbxQCInfoState.ValueMember = "Dictionary_ID";
                if (cbxQCInfoState.DataSource != null)
                {
                    cbxQCInfoState.SelectedIndex = 1;
                }
                List<Dictionary> list2 = DictionaryDAL.GetValueStateDictionary("质检状态");
                cbxWaitQCInfoState.DataSource = list2;
                this.cbxWaitQCInfoState.DisplayMember = "Dictionary_Name";
                cbxWaitQCInfoState.ValueMember = "Dictionary_ID";
                if (cbxWaitQCInfoState.DataSource != null)
                {

                    cbxWaitQCInfoState.SelectedIndex = 1;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("加载下拉框数据异常", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
        /// <summary>
        /// 加载用户
        /// </summary>
        private void InitUser()
        {
            this.lvwUserList.AutoGenerateColumns = false;//设置只显示列表控件绑定的列
            mf = new MainFrom();
            //comb_PageMaxCount.SelectedText = "10";
            //---------------2012-1-10---李灵--------------
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();

            //-------------------------


            ids.Clear();
            expr = null;
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            BindCboxState();
            TimeEndIsNull();
            GetDgvWaitQCSeacher();
            LoadData("");

        }
        //判断是否退货
        /// <summary>
        /// 
        /// </summary>
        private void YesNoMethod()
        {
            bool rbool = true;
            try
            {

                string strDEGRADE_OUTTHROWS_PERCT = "";  //降级退货杂纸标准  
                string strDEGRADE_MATERIAL_PERCT = ""; //降级退货杂质标准
                string strDEGRADE_MOISTURE_PERCT = ""; //降级退货水份标准  

                if (lvwUserList.SelectedRows[0].Cells["DEGRADE_OUTTHROWS_PERCT"] != null)
                {
                    strDEGRADE_OUTTHROWS_PERCT = lvwUserList.SelectedRows[0].Cells["DEGRADE_OUTTHROWS_PERCT"].Value.ToString();//降级退货杂纸标准 
                }
                if (lvwUserList.SelectedRows[0].Cells["DEGRADE_MATERIAL_PERCT"] != null)
                {
                    strDEGRADE_MATERIAL_PERCT = lvwUserList.SelectedRows[0].Cells["DEGRADE_MATERIAL_PERCT"].Value.ToString(); //降级退货杂质标准
                }
                if (lvwUserList.SelectedRows[0].Cells["DEGRADE_MOISTURE_PERCT"] != null)
                {
                    strDEGRADE_MOISTURE_PERCT = lvwUserList.SelectedRows[0].Cells["DEGRADE_MOISTURE_PERCT"].Value.ToString(); //降级退货水份标准 
                }


                if (lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_PER_SAMPLE"].Value != null)//平均水分：
                {
                    int a = Common.GetInt(lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_PER_SAMPLE"].Value.ToString());
                    int b = Common.GetInt(strDEGRADE_MOISTURE_PERCT);
                    if (Common.GetInt(lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_PER_SAMPLE"].Value.ToString()) > Common.GetInt(strDEGRADE_MOISTURE_PERCT))
                    {
                        rbool = false;
                    }
                    else
                    {
                        if (lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_SCALE"].Value != null)//杂质比例：
                        {

                            if (Common.GetInt(lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_SCALE"].Value.ToString()) > Common.GetInt(strDEGRADE_MATERIAL_PERCT))
                            {
                                rbool = false;
                            }
                            else
                            {
                                if (lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value != null)//杂纸比例：
                                {
                                    if (Common.GetInt(lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value.ToString()) > Common.GetInt(strDEGRADE_OUTTHROWS_PERCT))
                                    {
                                        rbool = false;
                                    }

                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("一检管理 YesNoMethod()" + ex.Message.ToString());
            }
            Common.rboolBT = rbool;
        }

        /// <summary>
        /// 批量打印方法
        /// </summary>
        /// <param name="max">打印的条数</param>
        private void OpenForm1(int max)
        {
            progressBar1.Maximum = max;
            progressBar1.Minimum = 0;
            int numcount = 0;
            if (max > 0)
            {
                panel2.Visible = true;
                progressBar1.Enabled = true;
                progressBar1.Visible = true;
                for (int i = 0; i < lvwUserList.RowCount; i++)
                {
                    if (exitdayin)
                    {
                        break;
                    }
                    try
                    {
                        string trueRows = lvwUserList.Rows[i].Cells[0].Value.ToString();
                        if (trueRows == "True")
                        {
                            numcount++;
                            WordPringOut word = new WordPringOut(lvwUserList.Rows[i].Cells[1].Value.ToString(), winWordControl1);
                            progressBar1.Value = numcount;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            panel2.Visible = false;
            progressBar1.Value = 0;
        }
        /// <summary>
        /// 预览打印
        /// </summary>
        /// <param name="str"></param>
        private void OpenForm1(string str)
        {
            Form1 frm = new Form1();
            frm.Show();
        }
        /// <summary>
        ///  批量打印
        /// </summary>
        /// <param name="num">打印的条数</param>
        public void tslPrintPreview(int num)
        {
            try
            {
                if (MessageBox.Show("确定要批量打印吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (Common.intQCInfo_ID <= 0)
                    {
                        MessageBox.Show("选中行的质检编号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    OpenForm1(num);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("打印预览异常" + ex.Message.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Common.WriteTextLog("一检管理 tslPrintPreview()" + ex.Message.ToString());
            }

        }
        /// <summary>
        /// 打印预览
        /// </summary>
        public void tslPrintPreview(string str)
        {
            try
            {
                if (MessageBox.Show("确定要打印吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (Common.intQCInfo_ID <= 0)
                    {
                        MessageBox.Show("选中行的质检编号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    OpenForm1("");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("打印预览异常" + ex.Message.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Common.WriteTextLog("一检管理 tslPrintPreview()" + ex.Message.ToString());
            }

        }


        /// <summary>
        /// 菜单栏加载数据
        /// </summary>
        private async void LoadData(string strName)
        {
            //form_loading = new Form_Loading();
            //form_loading.ShowLoading(this);
            //form_loading.Text = "数据加载中...";
            await GetWeightData(strName);
            //form_loading.CloseLoading(this);
        }

      

        private async Task GetWeightData(string strName)
        {
            await Task.Factory.StartNew(() =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new AsynUpdateUI(delegate ()
                    {
                        try
                        {
                            lvwUserList.DataSource = null;
                            #region 2012-11-18测试
                           // GetDgvWaitQCSeacher();
                            #endregion

                            lvwUserList.DataSource = page.BindBoundControl<View_QCInfo>(strName, txtCurrentPage2, lblPageCount2, expr, orderbywhere);
                            listrecordID.Clear();
                            ids.Clear();
                        }
                        catch (Exception ex)
                        {
                            Common.WriteTextLog("一检管理费 LoadData()" + ex.Message.ToString());
                        }

                    }));
                }
            });
        }



        /// <summary>
        /// 修改
        /// </summary>
        private void tsb_Updateclick()
        {
            if (lvwUserList.SelectedRows.Count > 0)//是否选中行
            {
                if (lvwUserList.SelectedRows.Count > 1)//是否选择多行
                {
                    MessageBox.Show("只能选择一行", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    if (lvwUserList.SelectedRows[0].Cells["QCInfo_ID"].Value == null)
                    {
                        return;//质检编号
                    }
                    UpdateQCInfo_ID = Common.GetInt(lvwUserList.SelectedRows[0].Cells["QCInfo_ID"].Value.ToString());
                    if (isHandAdd)
                    {
                        handAddBind();
                        isHandAdd = false;


                    }
                    else
                    {
                        UpdateMethodSee();
                        Enabledbool(true);
                    }
                }
            }
            else
            {
                MessageBox.Show("请选中要修改的行", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// 手动录入绑定数据
        /// </summary>
        private void handAddBind()
        {
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_PER_SAMPLE"].Value != null)//平均水分：
            {
                txtQCInfo_MOIST_PER_SAMPLE.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_PER_SAMPLE"].Value.ToString();
                txtQCInfo_MOIST_PER_SAMPLE.Enabled = false;
            }
            else
            {
                txtQCInfo_MOIST_PER_SAMPLE.Enabled = true;
            }



            if (lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_WEIGHT"].Value != null)//杂质重量：
            {
                txtQCInfo_MATERIAL_WEIGHT.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_WEIGHT"].Value.ToString();
                txtQCInfo_MATERIAL_WEIGHT.Enabled = false;
            }
            else
            {
                txtQCInfo_MATERIAL_WEIGHT.Enabled = true;
            }
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_SCALE"].Value != null)//杂质比例：
            {
                txtQCInfo_MATERIAL_SCALE.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_SCALE"].Value.ToString();
                txtQCInfo_MATERIAL_SCALE.Enabled = false;
            }
            else
            {
                txtQCInfo_MATERIAL_SCALE.Enabled = true;
            }

            if (lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_WEIGHT"].Value != null)//杂纸重量：
            {
                txtQCInfo_PAPER_WEIGHT.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_WEIGHT"].Value.ToString();
                txtQCInfo_PAPER_WEIGHT.Enabled = false;
            }
            else
            {
                txtQCInfo_PAPER_WEIGHT.Enabled = isHandAdd == true;
            }
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value != null)//杂纸比例：
            {
                txtQCInfo_PAPER_SCALE.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value.ToString();
                txtQCInfo_PAPER_SCALE.Enabled = false;
            }
            else
            {
                txtQCInfo_PAPER_WEIGHT.Enabled = true;
            }

            if (lvwUserList.SelectedRows[0].Cells["QCInfo_BAGWeight"].Value != null)//抽样总重：
            {
                txtQCInfo_BAGWeight.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_BAGWeight"].Value.ToString();
                txtQCInfo_BAGWeight.Enabled = false;
            }
            else
            {
                txtQCInfo_BAGWeight.Enabled = true;
            }

            txtQCInfo_REMARK.Enabled = false;
            //if (lvwUserList.SelectedRows[0].Cells["QCInfo_UPDATE_REASON"].Value != null)//修改原因：
            //{
            //    txtQCInfo_REMARK.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_UPDATE_REASON"].Value.ToString();
            //    txtQCInfo_REMARK.Enabled =   false;
            //}
            //else
            //{
            //    txtQCInfo_REMARK.Enabled = true;
            //}
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_EXAMINER"].Value != null)//水分检测员
            {
                txtQCInfo_MOIST_EXAMINER.SelectedText = lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_EXAMINER"].Value.ToString();
                txtQCInfo_MOIST_EXAMINER.Enabled = false;
            }
            else
            {
                txtQCInfo_MOIST_EXAMINER.Enabled = true;
            }
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value != null)//杂纸比例
            {
                txtQCInfo_PAPER_SCALE.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value.ToString();
                txtQCInfo_PAPER_SCALE.Enabled = false;
            }
            else
            {
                txtQCInfo_PAPER_SCALE.Enabled = true;
            }
        }
        /// <summary>
        /// 修改质检信息查看
        /// </summary>
        private void UpdateMethodSee()
        {
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_PER_SAMPLE"].Value != null)//平均水分：
            {
                txtQCInfo_MOIST_PER_SAMPLE.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_MOIST_PER_SAMPLE"].Value.ToString();
            }

            if (lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_WEIGHT"].Value != null)//杂质重量：
            {
                txtQCInfo_MATERIAL_WEIGHT.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_WEIGHT"].Value.ToString();
            }
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_SCALE"].Value != null)//杂质比例：
            {
                txtQCInfo_MATERIAL_SCALE.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_MATERIAL_SCALE"].Value.ToString();
            }

            if (lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_WEIGHT"].Value != null)//杂纸重量：
            {
                txtQCInfo_PAPER_WEIGHT.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_WEIGHT"].Value.ToString();
            }
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value != null)//杂纸比例：
            {
                txtQCInfo_PAPER_SCALE.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_PAPER_SCALE"].Value.ToString();
            }

            if (lvwUserList.SelectedRows[0].Cells["QCInfo_BAGWeight"].Value != null)//抽样总重：
            {
                txtQCInfo_BAGWeight.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_BAGWeight"].Value.ToString();
            }
            if (lvwUserList.SelectedRows[0].Cells["QCInfo_UPDATE_REASON"].Value != null)//修改原因：
            {
                txtQCInfo_REMARK.Text = lvwUserList.SelectedRows[0].Cells["QCInfo_UPDATE_REASON"].Value.ToString();
            }

        }
        /// <summary>
        /// 修改质检信息
        /// </summary>
        private void UpdateMethod()
        {
            try
            {
                if (string.IsNullOrEmpty(txtQCInfo_REMARK.Text.Trim()))
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "提示", "修改原因不能为空", txtQCInfo_REMARK, this);
                    return;
                }
                Expression<Func<QCInfo, bool>> p = n => n.QCInfo_ID == UpdateQCInfo_ID;
                Action<QCInfo> ap = n =>
                {
                    //cbxQCInfo_MOIST_EXAMINER.Text 
                    n.QCInfo_MOIST_PER_SAMPLE = Common.GetDecimal(txtQCInfo_MOIST_PER_SAMPLE.Text.Trim());//平均水分：

                    n.QCInfo_MATERIAL_WEIGHT = Common.GetDecimal(txtQCInfo_MATERIAL_WEIGHT.Text.Trim());//杂质重量：
                    n.QCInfo_MATERIAL_SCALE = Common.GetDecimal(txtQCInfo_MATERIAL_SCALE.Text.Trim());//杂质比例：

                    n.QCInfo_PAPER_WEIGHT = Common.GetDecimal(txtQCInfo_PAPER_WEIGHT.Text.Trim());//杂纸重量：
                    n.QCInfo_PAPER_SCALE = Common.GetDecimal(txtQCInfo_PAPER_SCALE.Text.Trim());//杂纸比例：
                    n.QCInfo_BAGWeight = Common.GetDecimal(txtQCInfo_BAGWeight.Text.Trim());//平均水分：
                    n.QCInfo_UPDATE_REASON = txtQCInfo_REMARK.Text.Trim();//修改原因：
                    n.QCInfo_UPDATE_TIME = LinQBaseDao.getDate();//修改时间：
                    n.QCInfo_UPDATE_NAME = Common.GetInt(Common.USERID);//修改人：
                    #region 2012-11-18 新增水分检测人修改
                    if (!string.IsNullOrEmpty(txtQCInfo_MOIST_EXAMINER.Text))//如果水分检测人不为空修改
                    {
                        n.QCInfo_UnpackBefore_MOIST_EXAMINER = txtQCInfo_MOIST_EXAMINER.Text;
                        n.QCInfo_UnpackBack_MOIST_EXAMINER = txtQCInfo_MOIST_EXAMINER.Text;
                        n.QCInfo_MOIST_EXAMINER = txtQCInfo_MOIST_EXAMINER.Text;
                    }
                    #endregion
                };
                if (QCInfoDAL.Update(p, ap))
                {
                    string Log_Content = String.Format("修改内容：平均水分:{0} 拆包后平均水分:{1} 拆包后水分点数:{2} 杂质重量:{3} 杂质比例:{4} 拆包前平均水分:{5} 杂纸重量:{6} 杂纸比例:{7} 拆包前水分点数:{8} 平均水分:{9}", txtQCInfo_MOIST_PER_SAMPLE.Text.Trim(), txtQCInfo_MATERIAL_WEIGHT.Text.Trim(), txtQCInfo_MATERIAL_SCALE.Text.Trim(), txtQCInfo_PAPER_WEIGHT.Text.Trim(), txtQCInfo_PAPER_SCALE.Text.Trim(), txtQCInfo_BAGWeight.Text.Trim());
                    Common.WriteLogData("修改", Log_Content, Common.USERNAME);
                    MessageBox.Show("修改成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("修改失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("OneQCAdmin.UpdateMethod()" + ex.Message.ToString());
            }
            finally
            {
                Enabledbool(false);
                btn_DetermineUpdate.Enabled = true;
                #region 2012-11-20屏蔽
                //page = new PageControl();
                //page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();

                //LoadData("");
                #endregion

            }
        }
        private void Enabledbool(bool rbool)
        {
            txtQCInfo_MOIST_EXAMINER.Enabled = rbool;//检测人
            btn_DetermineUpdate.Visible = rbool;
            txtQCInfo_MOIST_PER_SAMPLE.Enabled = rbool;//平均水分：
            txtQCInfo_MATERIAL_WEIGHT.Enabled = rbool;// 杂质重量
            txtQCInfo_MATERIAL_SCALE.Enabled = rbool;// 杂质比例：
            txtQCInfo_PAPER_WEIGHT.Enabled = rbool;//杂纸重量：
            txtQCInfo_PAPER_SCALE.Enabled = rbool;//杂纸比例：
            txtQCInfo_BAGWeight.Enabled = rbool;//抽样总重：
            txtQCInfo_REMARK.Enabled = rbool;
            if (rbool)
            {
                label9.Text = "修改原因：";
            }
            else
            {
                label9.Text = "备注：";
            }
        }
        #region 删除 2012-08-10 周意
        /// <summary>
        /// 删除质检信息，删除条件多选框选择删除可以删除多条
        /// 删除时包括删除单条记录和总记录，质检登记信息，并日志记录删除操作
        /// </summary>
        public void DelQcInfo()
        {

        }
        #endregion
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click()
        {
            for (int i = 0; i < lvwUserList.Rows.Count; i++)
            {
                lvwUserList.Rows[i].Cells["xz"].Value = true;
                lvwUserList.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < this.lvwUserList.Rows.Count; i++)
            {
                lvwUserList.Rows[i].Cells["xz"].Value = false;
                lvwUserList.Rows[i].Selected = false;
            }
        }


        bool isHandAdd = false;
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                if (e.ClickedItem.Name == "tslSelectAll") // 全选
                {
                    tslSelectAll_Click();
                    return;
                }
                else if (e.ClickedItem.Name == "tslNotSelect") // 取消全选
                {
                    tslNotSelect_Click();
                    return;
                }
                else if (e.ClickedItem.Name == "tsb_Update")//修改
                {
                    tsb_HandAdd.Enabled = false;
                    tsb_Updateclick();

                    tsb_HandAdd.Enabled = true;

                    btnCalculateAfter.Visible = false;
                    btnSave.Visible = false;
                    btn_DetermineUpdate.Visible = true;
                    btn_DetermineUpdate.Enabled = true;
                }
                else if (e.ClickedItem.Name == "tsb_HandAdd")//手动录入数据，用在现场出现异常的情况下用会原始检测方式最终结果手动录入系统
                {
                    isHandAdd = true;
                    tsb_HandAdd.Enabled = false;
                    tsb_Updateclick();

                    tsb_HandAdd.Enabled = true;

                    btnCalculateAfter.Visible = false;
                    btnSave.Visible = false;
                    btn_DetermineUpdate.Visible = true;
                    btn_DetermineUpdate.Enabled = true;
                }
                //else  if (e.ClickedItem.Name == "tsbDel")//删除
                //{
                //    tsbDel.Enabled = false;
                //    //删除语句
                //    DelQcInfo();
                //    tsbDel.Enabled = true;
                //}
                else if (e.ClickedItem.Name == "tbtn_DaYin")//打印
                {

                    int counts = 0;//记录选中的个数
                    for (int i = 0; i < lvwUserList.RowCount; i++)
                    {
                        try
                        {
                            string trueRows = lvwUserList.Rows[i].Cells[0].Value.ToString();
                            if (trueRows == "True")
                            {
                                counts++;
                            }
                        }
                        catch
                        {
                        }
                    }
                    if (counts > 0)
                    {

                        Common.rbool = true;
                        tslPrintPreview(counts);
                    }
                    else
                    {
                        MessageBox.Show("请勾选要打印的内容信息", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }


                }
                //查看详情
                else if (e.ClickedItem.Name == "tslblQcRecordInfo")
                {
                    if (iQcInfoID > 0)
                    {
                        frmQcRecordInfo frm = new frmQcRecordInfo();
                        frm.Owner = this;
                        frm.iqcinfoid = iQcInfoID;
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("选中行的质检编号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }//发送接收表
                else if (e.ClickedItem.Name == "tslabSend")
                {
                    tslabSend_Click();


                }
                //手动修改
                else if (e.ClickedItem.Name == "bslabUpdateQC")
                {
                    if (iQcInfoID > 0)
                    {
                        frmUpdateQC frm = new frmUpdateQC();
                        frm.Owner = this;
                        frm.iQcInfoID = iQcInfoID;
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("选中行的质检编号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else if (e.ClickedItem.Name == "tsb_PrintTemplateSetUp")//打印模板设置
                {
                    if (iQcInfoID > 0)
                    {
                        Common.rbool = false;
                        PrintTemplateSetUpForm ptsuf = new PrintTemplateSetUpForm();
                        ptsuf.Owner = this;
                        ptsuf.iQcInfoID = iQcInfoID;
                        mf.ShowChildForm(ptsuf);
                    }
                    else
                    {
                        MessageBox.Show("选中行的质检编号不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                else
                {
                    LoadData(e.ClickedItem.Name);
                }
                if (e.ClickedItem.Name == "tbtSendSMS")//发送短信
                {


                    if (iQcInfoID > 0)
                    {
                        SMSManualSendForm ssf = new SMSManualSendForm(iQcInfoID);
                        ssf.Show();

                    }
                    else
                    {
                        MessageBox.Show("请选择一条质检信息！");
                    }

                }


                //else if (e.ClickedItem.Name == "tbtn_TuiHuo")
                //{
                //    YesNo_RECV_RMA_METHOD();
                //}
                //else
                //{

                //    LoadData(e.ClickedItem.Name);
                //    //lvwUserList.DataSource = page.BindBoundControl<View_QCInfo>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, expr, orderbywhere);

                //    //lvwUserList.DataSource = fy.BindBoundControl<View_QCInfo>(e.ClickedItem.Name, txtCurrentPage2, lblPageCount2, p);
                //}





            }
            catch (Exception ex)
            {
                Common.WriteTextLog("OneQCAdmin.bdnInfo_ItemClicked()" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// 显示当前页
        /// </summary>
        private void GetloadGrid()
        {

            // expr = null;
            //page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            page.rbool = false;
            LoadData("");
            btnQCOneInfo_Click(null, null);
        }

        /// <summary>
        /// 设置每页显示最大条数事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"><
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            //fy.PageMaxCount = PageMaxCount;
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            LoadData("");
        }
        /// <summary>
        /// 搜索 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQCOneInfo_Click(object sender, EventArgs e)
        {
            ids.Clear();
            expr = null;
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            GetDgvWaitQCSeacher();
            LoadData("");
            Common.WriteLogData("查询", "一检信息查询", Common.USERNAME); //操作日志
        }
        private void GetDgvWaitQCSeacher()
        {
            try
            {
                int i = 0;
                if (expr == null)
                {
                    expr = PredicateExtensionses.True<View_QCInfo>();
                }
                if (!string.IsNullOrEmpty(txtWaitPO_NO.Text.Trim()))//采购单
                {
                    //expr = expr.And(n => n.PO_NO == txtWaitPO_NO.Text.Trim().ToUpper());
                    expr = expr.And(n => SqlMethods.Like(n.PO_NO, "%" + txtWaitPO_NO.Text.Trim() + "%"));
                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitSHIPMENT_NO.Text.Trim()))//送货单
                {
                    //expr = expr.And(n => n.SHIPMENT_NO == txtWaitSHIPMENT_NO.Text.Trim().ToUpper());
                    expr = expr.And(n => SqlMethods.Like(n.SHIPMENT_NO, "%" + txtWaitSHIPMENT_NO.Text.Trim() + "%"));
                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitCarNo.Text.Trim()))//车牌号
                {
                    //expr = expr.And(n => n.CNTR_NO == txtWaitCarNo.Text.Trim().ToUpper());
                    expr = expr.And(n => SqlMethods.Like(n.CNTR_NO, "%" + txtWaitCarNo.Text.Trim() + "%"));

                    i++;
                }
                if (!string.IsNullOrEmpty(txtWaitWEIGHTTICKETNO.Text.Trim()))//磅单号
                {
                    //expr = expr.And(n => n.WEIGHT_TICKET_NO == txtWaitWEIGHTTICKETNO.Text.Trim().ToUpper());
                    expr = expr.And(n => SqlMethods.Like(n.WEIGHT_TICKET_NO, "%" + txtWaitWEIGHTTICKETNO.Text.Trim() + "%"));
                    i++;
                }
                if (cbxWaitQCInfoState.SelectedValue != null)//质检状态
                {
                    int stateID = Converter.ToInt(cbxWaitQCInfoState.SelectedValue.ToString());
                    if (stateID > 0)
                    {
                        expr = expr.And(n => (n.Dictionary_ID == stateID));

                        i++;
                    }
                }

                if (!string.IsNullOrEmpty(cbbxYesNoUpdate.Text))//是否修改
                {
                    if (cbbxYesNoUpdate.Text.Trim() == "是")
                    {
                        expr = expr.And(n => n.QCInfo_UPDATE_NAME != null);
                        i++;
                    }
                    else if (cbbxYesNoUpdate.Text.Trim() == "否")
                    {
                        expr = expr.And(n => n.QCInfo_UPDATE_NAME == null);
                        i++;
                    }
                }

                //if (chb_YesNoUpdate.Checked)//是否修改
                //{
                //    expr = expr.And(n => n.QCInfo_UPDATE_NAME != null);
                //    i++;
                //}
                //else
                //{
                //    expr = expr.And(n => n.QCInfo_UPDATE_NAME == null);
                //    i++;
                //}

                string beginTime = txtbeginTime.Value.ToString("yyyy-MM -dd ");
                string endTime = txtendTime.Value.ToString("yyyy-MM -dd ");
                string begin = beginTime;  // + " 00:00:00"
                string end = endTime;  // + "23:59:59 "
                if (!string.IsNullOrEmpty(begin))//开始时间
                {
                    expr = expr.And(n => n.QCInfo_TIME > Common.GetDateTime(begin));
                    i++;
                }
                if (!string.IsNullOrEmpty(end))//完成时间
                {
                    expr = expr.And(n => n.QCInfo_TIME < Common.GetDateTime(end));
                    i++;
                }
                if (!string.IsNullOrEmpty(cbbxISSend.Text))//是否发送接口表
                {
                    expr = expr.And(n => n.QCInfo_ISSend == cbbxISSend.Text.Trim());
                    i++;
                }


                //if (expr == null)
                //{
                //    expr = (Expression<Func<View_QCInfo, bool>>)PredicateExtensionses.True<View_QCInfo>();
                //}
                //var i = 0;
                //if (cbxWaitQCInfoState.SelectedValue != null)
                //{
                //    int stateID = Converter.ToInt(cbxWaitQCInfoState.SelectedValue.ToString());
                //    if (stateID > 0)
                //    {
                //        expr = expr.And(c => (c.Dictionary_ID == stateID));
                //        i++;
                //    }
                //}

                //if (!string.IsNullOrEmpty(txtWaitWEIGHTTICKETNO.Text))
                //{
                //    expr = expr.And(n => n.WEIGHT_TICKET_NO == txtWaitWEIGHTTICKETNO.Text.Trim().ToUpper() || n.WEIGHT_TICKET_NO == txtWaitWEIGHTTICKETNO.Text.Trim().ToLower());

                //    i++;
                //}
                //if (txtWaitCarNo.Text.Trim() != "")
                //{
                //    expr = expr.And(n => n.CNTR_NO == txtWaitCarNo.Text.Trim().ToUpper() || n.CNTR_NO == txtWaitCarNo.Text.Trim().ToLower());
                //    i++;
                //}
                //if (!string.IsNullOrEmpty(txtWaitPO_NO.Text))
                //{
                //    expr = expr.And(n => n.PO_NO == txtWaitPO_NO.Text.Trim().ToUpper() || n.PO_NO == txtWaitPO_NO.Text.Trim().ToLower());
                //    i++;
                //}
                //if (!string.IsNullOrEmpty(txtWaitSHIPMENT_NO.Text))
                //{
                //    expr = expr.And(n => n.SHIPMENT_NO == txtWaitSHIPMENT_NO.Text.Trim().ToUpper() || n.SHIPMENT_NO == txtWaitSHIPMENT_NO.Text.Trim().ToLower());
                //    i++;
                //}
                if (i == 0)
                {
                    expr = null;
                }
                //LoadData();
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("OneQCAdmin.GetDgvWaitQCSeacher异常:" + ex.Message.ToString());
            }
        }
        //private void GetDgvWaitQCSeacher()
        //{
        //    try
        //    {
        //        var i = 0;
        //        if (cbxWaitQCInfoState.SelectedValue != null)
        //        {
        //            int stateID = Converter.ToInt(cbxWaitQCInfoState.SelectedValue.ToString());
        //            if (stateID > 0)
        //            {
        //                p += n => n.Dictionary_ID == stateID;
        //                i++;
        //            }
        //        }

        //        if (!string.IsNullOrEmpty(txtWaitWEIGHTTICKETNO.Text))
        //        {
        //            p += n => n.WEIGHT_TICKET_NO == txtWaitWEIGHTTICKETNO.Text.Trim().ToUpper() || n.WEIGHT_TICKET_NO == txtWaitWEIGHTTICKETNO.Text.Trim().ToLower();

        //            i++;
        //        }
        //        if (txtWaitCarNo.Text.Trim() != "")
        //        {
        //            p += n => n.CNTR_NO == txtWaitCarNo.Text.Trim().ToUpper() || n.CNTR_NO == txtWaitCarNo.Text.Trim().ToLower();
        //            i++;
        //        }
        //        if (!string.IsNullOrEmpty(txtWaitPO_NO.Text))
        //        {
        //            p += n => n.PO_NO == txtWaitPO_NO.Text.Trim().ToUpper() || n.PO_NO == txtWaitPO_NO.Text.Trim().ToLower();
        //            i++;
        //        }
        //        if (!string.IsNullOrEmpty(txtWaitSHIPMENT_NO.Text))
        //        {
        //            p += n => n.SHIPMENT_NO == txtWaitSHIPMENT_NO.Text.Trim().ToUpper() || n.SHIPMENT_NO == txtWaitSHIPMENT_NO.Text.Trim().ToLower();
        //            i++;
        //        }
        //        if (i == 0)
        //        {
        //            p = null;
        //        }
        //        LoadData();
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.WriteTextLog("OneQCAdmin.GetDgvWaitQCSeacher异常:" + ex.Message.ToString());
        //    }
        //}
        /// <summary>
        /// 退货
        /// </summary>
        private void YesNo_RECV_RMA_METHOD()
        {
            try
            {
                if (lvwUserList.SelectedRows.Count > 0)//选中退货
                {
                    if (MessageBox.Show("确定要退货吗？", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {

                        //选中数量
                        int count = lvwUserList.SelectedRows.Count;
                        string id = "";
                        int j = 0;
                        //遍历
                        for (int i = 0; i < count; i++)
                        {

                            Expression<Func<DRAW_EXAM_INTERFACE, bool>> fun = n => n.DRAW_EXAM_INTERFACE_ID.ToString() == lvwUserList.SelectedRows[i].Cells["DRAW_EXAM_INTERFACE_ID"].Value.ToString();
                            Action<DRAW_EXAM_INTERFACE> action = n =>
                            {
                                n.RECV_RMA_METHOD = "RMA";
                            };
                            if (!DRAW_EXAM_INTERFACEDAL.Update(fun, action))
                            {
                                i++;
                            }
                        }
                        if (j == 0)
                        {
                            MessageBox.Show("成功退货", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("退货失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                }
                else//没有选中
                {
                    MessageBox.Show("请选择要要退货的行！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("退货失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                #region 屏蔽
                ////------------2012-1-10李灵---------------------------------------------
                //page = new PageControl();
                //page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();

                ////-------------------------
                //LoadData("");
                #endregion
                GetloadGrid();
            }
        }






        /// <summary>
        /// 计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCalculateAfter_Click(object sender, EventArgs e)
        {
            if (btnCalculateAfter.Enabled)
            {
                btnCalculateAfter.Enabled = false;
                if (string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text.Trim()))
                {
                    #region 2012-10-19修改
                    if (MessageBox.Show("是否输入杂纸比例！", "计算提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        txtQCInfo_PAPER_SCALE.Focus();
                        btnCalculateAfter.Enabled = true;
                        return;
                    }
                    #endregion
                    //mf.ShowToolTip(ToolTipIcon.Info, "计算提示", "请输入杂纸比例！", txtQCInfo_PAPER_SCALE, this);
                    //btnCalculateAfter.Enabled = true;
                    //return;
                }
                try
                {
                    if (iQcInfoID != 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        #region 平均水分
                        string strzavg = "";//水分平均数
                        List<QCRecordAVGEnitiy> list = QCRecordDAL.GetQCRecord_RESULT_AVERAGE(iQcInfoID, 0, "");
                        foreach (QCRecordAVGEnitiy qc in list)
                        {
                            string stravg = GetData.GetStringMath(qc.Avg.ToString(), 2);
                            switch (qc.TestItemName)
                            {
                                #region 水分平均数
                                //case "拆包后水分检测":
                                //    if (EMEWE.Common.Converter.ToInt(txtQCInfo_UnpackBack_MOIST_COUNT.Text) == qc.Count)
                                //    {
                                //        txtQCInfo_UnpackBack_MOIST_PER_SAMPLE.Text = stravg;
                                //        txtQCInfo_UnpackBack_MOIST_COUNT.Text = qc.Count.ToString();
                                //    }
                                //    break;
                                //case "拆包前水分检测":
                                //    if (EMEWE.Common.Converter.ToInt(txtQCInfo_UnpackBefore_MOIST_PER_COUNT.Text) == qc.Count)
                                //    {
                                //        txtQCInfo_UnpackBefore_MOIST_PER_COUNT.Text = qc.Count.ToString();
                                //        txtQCInfo_UnpackBefore_MOIST_PER_SAMPLE.Text = stravg;
                                //    }
                                //    break;
                                case "水分检测":
                                    strzavg = stravg;
                                    break;
                                #endregion
                            }
                        }
                        // 平均水分 如果拆包前水分和拆包后水分不为空才填充平均水分
                        //if (!string.IsNullOrEmpty(txtQCInfo_UnpackBefore_MOIST_PER_SAMPLE.Text) && !string.IsNullOrEmpty(txtQCInfo_UnpackBack_MOIST_PER_SAMPLE.Text))
                        //{
                        //    txtQCInfo_MOIST_PER_SAMPLE.Text = strzavg;

                        //}
                        //else
                        //{
                        //    sb.Append("水分平均值不能计算，请检查水分检测模块水分点数是否已检测完成。");
                        //}
                        #endregion

                        #region 重量计算
                        List<QCRecordAVGEnitiy> list2 = QCRecordDAL.GetQCRecord_RESULT_SUM(iQcInfoID, iQCRetest_ID, "重量检测"); foreach (QCRecordAVGEnitiy qc in list2)
                        {
                            string stravg = GetData.GetStringMath(qc.Avg.ToString(), 2);
                            switch (qc.TestItemName)
                            {

                                #region 重量平均数
                                case "废纸包重"://抽样总重量是废纸包总数等于计算结果中总数量
                                    int ibag = Converter.ToInt(lblQCInfo_PumpingPackets.Text);
                                    if (ibag == qc.Count)
                                    {
                                        txtQCInfo_BAGWeight.Text = stravg;
                                    }
                                    break;
                                case "杂质":
                                    txtQCInfo_MATERIAL_WEIGHT.Text = stravg;
                                    break;
                                case "杂纸":
                                    txtQCInfo_PAPER_WEIGHT.Text = stravg;
                                    break;

                                #endregion

                            }
                        }
                        if (!string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text))
                        {

                            //杂质比例 如果杂质重量和废纸包总重量不为空时计算
                            if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text))
                            {
                                txtQCInfo_MATERIAL_SCALE.Text = GetData.GetStringMathAndPercentage((Converter.ToDouble(txtQCInfo_MATERIAL_WEIGHT.Text) / Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                            }
                            else
                            {
                                sb.Append("杂质比例不能计算，请检查重量检测模块杂质是否检测完成。");
                            }
                            #region 2012-10-29 杂纸比例和重量计算的时候有杂纸过磅,就通过计算,没有的话直接存储,输入的杂纸比例,如果即有输入比例,又有过磅,那么按照过磅来计算.一般不会出现这样的问题

                            //杂纸比例 如果杂纸重量和废纸包总重量不为空时计算
                            if (!string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text))
                            {
                                txtQCInfo_PAPER_SCALE.Text = GetData.GetStringMathAndPercentage((Converter.ToDouble(txtQCInfo_PAPER_WEIGHT.Text) / Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                            }
                            else
                            {
                                //杂纸重量 如果杂纸比例和废纸包总重量不为空时就算杂纸重量 特殊要求
                                if (!string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text))
                                {
                                    txtQCInfo_PAPER_WEIGHT.Text = GetData.GetStringMath((Convert.ToDouble(txtQCInfo_PAPER_SCALE.Text) / 100 * Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                                }
                                else
                                {
                                    sb.Append("杂纸重量不能计算，请检查重量检测模块杂纸比例是否填写。");
                                }
                            }
                            #endregion

                        }
                        else
                        {
                            sb.Append("废纸包重量不能计算，请检查重量检测模块废纸包是否已检测完成。");
                        }
                        #region 暂时注释 未检测杂纸重量
                        ////杂纸重量 如果杂纸比例和废纸包总重量不为空时就散杂纸重量 特殊要求 2012-10-29
                        //if (!string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text) )
                        //{
                        //    txtQCInfo_PAPER_WEIGHT.Text = GetData.GetStringMath((Convert.ToDouble(txtQCInfo_PAPER_SCALE.Text) / 100 * Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                        //}
                        //else
                        //{
                        //    sb.Append("杂纸重量不能计算，请检查重量检测模块杂纸比例是否填写。");
                        //}
                        ////杂纸比例 如果杂纸重量和废纸包总重量不为空时计算 2012-1-5
                        //if (!string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text) && !string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text))
                        //{
                        //    txtQCInfo_PAPER_SCALE.Text = GetData.GetStringMathAndPercentage((Converter.ToDouble(txtQCInfo_PAPER_WEIGHT.Text) / Converter.ToDouble(txtQCInfo_BAGWeight.Text)).ToString(), 2);
                        //}
                        #endregion
                        #endregion

                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            MessageBox.Show(sb.ToString(), "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Common.WriteTextLog("OneQCAdmin.btnCalculateAfter_Click" + ex.Message);
                }
                btnCalculateAfter.Enabled = true;

            }
        }

        //计算和保存按钮打开
        private void ISOpenSave()
        {
            btnCalculateAfter.Enabled = true;
            btnSave.Enabled = true;

        }
        private void CloseButton()
        {
            btnSave.Enabled = false;
            btnCalculateAfter.Enabled = false;
        }
        private void lvwUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btn_DetermineUpdate.Visible = false;
                btn_DetermineUpdate.Enabled = false;
                Enabledbool(false);
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    //ids.Clear();
                    //listrecordID.Clear();


                    //绑定显示是否有异常，接口表是否有收货退货标识  20130630
                    //DataTable RECV_RMA_METHODDT = LinQBaseDao.Query("select * from QCInfo,DRAW_EXAM_INTERFACE where QCInfo_DRAW_EXAM_INTERFACE_ID=DRAW_EXAM_INTERFACE_ID and QCInfo_ID=" + lvwUserList.SelectedRows[0].Cells["QCInfo_ID"].Value).Tables[0];

                    //if (RECV_RMA_METHODDT.Rows.Count > 0)
                    //{
                    //    if (RECV_RMA_METHODDT.Rows.Count > 0)
                    //    {
                    //        string strRECV_RMA_METHOD = RECV_RMA_METHODDT.Rows[0]["RECV_RMA_METHOD"].ToString();

                    //    }
                    //}

                    //是否有未处理的异常，有则不能保存
                    DataTable UnusualDT = LinQBaseDao.Query("select * from Unusual,QCInfo where Unusual_QCInfo_ID=QCInfo_ID and Unusual_State='未处理' and QCInfo_ID=" + lvwUserList.SelectedRows[0].Cells["QCInfo_ID"].Value).Tables[0];

                    if (UnusualDT.Rows.Count > 0)
                    {
                        mf.ShowToolTip(ToolTipIcon.Info, "质检异常提示", "车牌号：" + lvwUserList.SelectedRows[0].Cells["CNTR_NO"].Value + "有质检异常未处理，请等待处理完成后再操作！", txtQCInfo_REMARK, this);
                        btnSave.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }


                    Common.SetSelectionBackColor(lvwUserList);
                    int a = e.RowIndex;

                    if (lvwUserList.Rows[a].Cells["QCInfo_MATERIALWeightWay"].Value != null)
                    {
                        lblQCInfo_MATERIALWeightWay.Text = lvwUserList.Rows[a].Cells["QCInfo_MATERIALWeightWay"].Value.ToString();

                    }
                    else
                    {
                        lblQCInfo_MATERIALWeightWay.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_PAPERWeightWay"].Value != null)
                    {
                        lblQCInfo_PAPERWeightWay.Text = lvwUserList.Rows[a].Cells["QCInfo_PAPERWeightWay"].Value.ToString();

                    }
                    else
                    {
                        lblQCInfo_PAPERWeightWay.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["DRAW_EXAM_INTERFACE_ID"].Value != null)
                    {
                        iDRAW_EXAM_INTERFACE_ID = Common.GetInt(lvwUserList.Rows[a].Cells["DRAW_EXAM_INTERFACE_ID"].Value.ToString());
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_ID"].Value != null)
                    {
                        iQcInfoID = EMEWE.Common.Converter.ToInt(lvwUserList.Rows[a].Cells["QCInfo_ID"].Value.ToString());
                        Common.intQCInfo_ID = iQcInfoID;
                        YesNoMethod();
                    }

                    if (lvwUserList.Rows[a].Cells["QCInfo_MOIST_PER_SAMPLE"].Value != null)
                        txtQCInfo_MOIST_PER_SAMPLE.Text = lvwUserList.Rows[a].Cells["QCInfo_MOIST_PER_SAMPLE"].Value.ToString();
                    else
                    {
                        txtQCInfo_MOIST_PER_SAMPLE.Text = "";
                    }

                    txtQCInfo_REMARK.Enabled = false;
                    if (lvwUserList.Rows[a].Cells["QCInfo_Dictionary_ID"].Value != null)
                    {
                        if (cbxQCInfoState.DataSource != null)
                        {
                            cbxQCInfoState.SelectedValue = EMEWE.Common.Converter.ToInt(lvwUserList.Rows[a].Cells["QCInfo_Dictionary_ID"].Value.ToString());
                            Dictionary dic = cbxQCInfoState.SelectedItem as Dictionary;
                            if (dic.Dictionary_Name == "终止质检")
                            {
                                txtQCInfo_REMARK.Enabled = true;
                            }
                            else if (dic.Dictionary_Name == "质检中")
                            {
                                ISOpenSave();
                            }
                            lblCurrentQcState.Text = dic.Dictionary_Name;
                        }
                    }
                    if (lvwUserList.Rows[a].Cells["CNTR_NO"].Value != null)
                    {
                        lblCNTR_NO.Text = lvwUserList.Rows[a].Cells["CNTR_NO"].Value.ToString();
                        //  txtWaitCarNo.Text = lvwUserList.Rows[a].Cells["CNTR_NO"].Value.ToString();
                    }
                    else
                    {
                        lblCNTR_NO.Text = "";
                        txtWaitCarNo.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["SHIPMENT_NO"].Value != null)
                    {
                        lblSHIPMENT_NO.Text = lvwUserList.Rows[a].Cells["SHIPMENT_NO"].Value.ToString();
                        //txtWaitSHIPMENT_NO.Text = lvwUserList.Rows[a].Cells["SHIPMENT_NO"].Value.ToString();
                    }
                    else
                    {
                        lblSHIPMENT_NO.Text = "";
                        txtWaitSHIPMENT_NO.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["PO_NO"].Value != null)
                    {
                        lblPO_NO.Text = lvwUserList.Rows[a].Cells["PO_NO"].Value.ToString();
                        //  txtWaitPO_NO.Text = lvwUserList.Rows[a].Cells["PO_NO"].Value.ToString();
                    }
                    else
                    {
                        lblPO_NO.Text = "";
                        txtWaitPO_NO.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["PROD_ID"].Value != null)
                    {
                        lblPROD_ID.Text = lvwUserList.Rows[a].Cells["PROD_ID"].Value.ToString();
                    }
                    else
                    {
                        lblPROD_ID.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["DEGRADE_MOISTURE_PERCT"].Value != null)
                        lblDEGRADE_MOISTURE_PERCT.Text = lvwUserList.Rows[a].Cells["DEGRADE_MOISTURE_PERCT"].Value.ToString();
                    else
                        lblDEGRADE_MOISTURE_PERCT.Text = "";
                    if (lvwUserList.Rows[a].Cells["NO_OF_BALES"].Value != null)
                        lblNO_OF_BALES.Text = lvwUserList.Rows[a].Cells["NO_OF_BALES"].Value.ToString();
                    else lblNO_OF_BALES.Text = "";
                    if (lvwUserList.Rows[a].Cells["WEIGHT_TICKET_NO"].Value != null)
                    {
                        lblWEIGHT_TICKET_NO.Text = lvwUserList.Rows[a].Cells["WEIGHT_TICKET_NO"].Value.ToString();
                        // txtWaitWEIGHTTICKETNO.Text = lvwUserList.Rows[a].Cells["WEIGHT_TICKET_NO"].Value.ToString();
                    }
                    else
                    {
                        lblWEIGHT_TICKET_NO.Text = "";
                        // txtWaitWEIGHTTICKETNO.Text = "";
                    }

                    if (lvwUserList.Rows[a].Cells["QCInfo_PumpingPackets"].Value != null)
                        lblQCInfo_PumpingPackets.Text = lvwUserList.Rows[a].Cells["QCInfo_PumpingPackets"].Value.ToString();
                    else lblQCInfo_PumpingPackets.Text = "";

                    if (lvwUserList.Rows[a].Cells["QCInfo_MATERIAL_WEIGHT"].Value != null)
                    {
                        txtQCInfo_MATERIAL_WEIGHT.Text = lvwUserList.Rows[a].Cells["QCInfo_MATERIAL_WEIGHT"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_MATERIAL_WEIGHT.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_MATERIAL_SCALE"].Value != null)
                    {
                        txtQCInfo_MATERIAL_SCALE.Text = lvwUserList.Rows[a].Cells["QCInfo_MATERIAL_SCALE"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_MATERIAL_SCALE.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_PAPER_WEIGHT"].Value != null)
                    {
                        txtQCInfo_PAPER_WEIGHT.Text = lvwUserList.Rows[a].Cells["QCInfo_PAPER_WEIGHT"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_PAPER_WEIGHT.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_PAPER_SCALE"].Value != null)
                    {
                        txtQCInfo_PAPER_SCALE.Text = lvwUserList.Rows[a].Cells["QCInfo_PAPER_SCALE"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_PAPER_SCALE.Text = "";
                    }

                    if (lvwUserList.Rows[a].Cells["QCInfo_BAGWeight"].Value != null)
                    {
                        txtQCInfo_BAGWeight.Text = lvwUserList.Rows[a].Cells["QCInfo_BAGWeight"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_BAGWeight.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_REMARK"].Value != null)
                    {
                        txtQCInfo_REMARK.Text = lvwUserList.Rows[a].Cells["QCInfo_REMARK"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_REMARK.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_MATERIAL_EXAMINER"].Value != null)
                    {
                        lblQCInfo_MATERIAL_EXAMINER.Text = lvwUserList.Rows[a].Cells["QCInfo_MATERIAL_EXAMINER"].Value.ToString();
                    }
                    else
                    {
                        lblQCInfo_MATERIAL_EXAMINER.Text = "";
                    }

                    #region 2012-11-18 修改为输入框
                    if (lvwUserList.Rows[a].Cells["QCInfo_MOIST_EXAMINER"].Value != null)
                    {
                        txtQCInfo_MOIST_EXAMINER.Text = lvwUserList.Rows[a].Cells["QCInfo_MOIST_EXAMINER"].Value.ToString();
                        //  lblQCInfo_MOIST_EXAMINER.Text = lvwUserList.Rows[a].Cells["QCInfo_MOIST_EXAMINER"].Value.ToString();
                    }
                    else
                    {
                        txtQCInfo_MOIST_EXAMINER.Text = "";
                        //lblQCInfo_MOIST_EXAMINER.Text = "";
                    }
                    if (lvwUserList.Rows[a].Cells["QCInfo_ISSend"].Value.ToString() == "是")
                    {
                        //  btnSave.Enabled = false;
                    }
                    else
                    {
                        btnSave.Enabled = true;
                    }
                    #endregion
                    if (!string.IsNullOrEmpty(txtQCInfo_MOIST_PER_SAMPLE.Text.Trim()) && !string.IsNullOrEmpty(txtQCInfo_MATERIAL_SCALE.Text.Trim()) && !string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text.Trim()))
                    {
                        Expression<Func<Unusualstandard, bool>> fun = n => Convert.ToDecimal(n.Unusualstandard_DEGRADE_MOISTURE_PERCT) >= Convert.ToDecimal(txtQCInfo_MOIST_PER_SAMPLE.Text);
                        fun = fun.And(n => Convert.ToDecimal(n.Unusualstandard_DEGRADE_MATERIAL_PERCT) >= Convert.ToDecimal(txtQCInfo_MATERIAL_SCALE.Text));
                        fun = fun.And(n => Convert.ToDecimal(n.DEGRADE_OUTTHROWS_PERCT) >= Convert.ToDecimal(txtQCInfo_PAPER_SCALE.Text));
                        IEnumerable<Unusualstandard> unusualstandard = UnusualstandardDAL.Query(fun).OrderBy(n=> n.Unusualstandard_DEGRADE_MATERIAL_PERCT);

                        if (unusualstandard != null)
                        {
                            lblResults.Text = unusualstandard.First().Unusualstandard_PROD;
                            if (!lblResults.Text.Equals(lblPROD_ID.Text))
                            {
                                lblResults.ForeColor = Color.Red;
                            }
                            else
                            {
                                lblResults.ForeColor = Color.Black;
                            }
                        }

                        // bool b = ADDUnusual(iQcInfoID, "杂质", Convert.ToDouble(txtQCInfo_MATERIAL_SCALE.Text.Trim()), "Unusualstandard_DEGRADE_MATERIAL_PERCT");
                    }


                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("OneQCAdmin.lvwUserList_CellContentClick（）" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Enabled)
            {
                btnSave.Enabled = false;
                //if (!ISBtnSaveBusy)
                //{
                //    ISBtnSaveBusy = true;
                baocun();
                Common.WriteLogData("新增", "一检管理新增bynSave_Click()", Common.USERNAME); //操作日志
                //    ISBtnSaveBusy = false;
                //}
            }
        }
        private void baocun()
        {
            string strRECV_RMA_METHOD = "";
            bool ISFinish = false;//是否完成
            string RECV_RMA_METHOD = "";//收获/退货

            try
            {
                if (iQcInfoID > 0 && iDRAW_EXAM_INTERFACE_ID > 0)
                {
                    QCInfo oldqcInfo = QCInfoDAL.GetQCInfo(iQcInfoID);
                    QCInfo qcinfo = oldqcInfo;

                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_WEIGHT.Text))
                        qcinfo.QCInfo_MATERIAL_WEIGHT = Converter.ToDecimal(txtQCInfo_MATERIAL_WEIGHT.Text.Trim(), 0);//杂质重量
                    if (!string.IsNullOrEmpty(txtQCInfo_MATERIAL_SCALE.Text))
                        qcinfo.QCInfo_MATERIAL_SCALE = Converter.ToDecimal(txtQCInfo_MATERIAL_SCALE.Text.Trim(), 0);//杂质比例
                    if (!string.IsNullOrEmpty(txtQCInfo_PAPER_WEIGHT.Text))
                        qcinfo.QCInfo_PAPER_WEIGHT = Converter.ToDecimal(txtQCInfo_PAPER_WEIGHT.Text.Trim(), 0);//杂纸重量
                    if (!string.IsNullOrEmpty(txtQCInfo_PAPER_SCALE.Text))
                        qcinfo.QCInfo_PAPER_SCALE = Converter.ToDecimal(txtQCInfo_PAPER_SCALE.Text.Trim(), 0);//杂纸比例
                    if (!string.IsNullOrEmpty(txtQCInfo_MOIST_PER_SAMPLE.Text))
                        qcinfo.QCInfo_MOIST_PER_SAMPLE = Converter.ToDecimal(txtQCInfo_MOIST_PER_SAMPLE.Text.Trim(), 0);//水分值（%）
                    if (!string.IsNullOrEmpty(txtQCInfo_BAGWeight.Text))
                        qcinfo.QCInfo_BAGWeight = Converter.ToDecimal(txtQCInfo_BAGWeight.Text.Trim(), 0);//抽样总重

                    if (cbxQCInfoState.SelectedIndex > -1)
                    {
                        int itemId = Converter.ToInt(cbxQCInfoState.SelectedValue.ToString());
                        if (itemId > 0)
                        {
                            Dictionary dic = cbxQCInfoState.SelectedItem as Dictionary;
                            if (dic.Dictionary_Value == "终止质检")
                            {
                                if (string.IsNullOrEmpty(txtQCInfo_REMARK.Text))
                                {
                                    mf.ShowToolTip(ToolTipIcon.Info, "保存提示", "请填写终止质检原因！", txtQCInfo_REMARK, this); btnSave.Enabled = true;
                                    return;
                                }
                                else
                                {
                                    qcinfo.QCInfo_REMARK = txtQCInfo_REMARK.Text.Trim();
                                }
                            }
                            else if (dic.Dictionary_Value == "质检中" || dic.Dictionary_Value == "完成质检")//改为质检中
                            {
                                #region 新增验证
                                string strMessage = "";
                                if (qcinfo.QCInfo_BAGWeight <= 0 || qcinfo.QCInfo_BAGWeight == null || qcinfo.QCInfo_MATERIAL_WEIGHT <= 0 || qcinfo.QCInfo_MATERIAL_WEIGHT == null || qcinfo.QCInfo_MATERIAL_SCALE <= 0 || qcinfo.QCInfo_MATERIAL_SCALE == null || qcinfo.QCInfo_PAPER_WEIGHT <= 0 || qcinfo.QCInfo_PAPER_WEIGHT == null || qcinfo.QCInfo_PAPER_SCALE <= 0 || qcinfo.QCInfo_PAPER_SCALE == null)
                                {
                                    strMessage += "重量质检未完成,";
                                }

                                if (qcinfo.QCInfo_MOIST_PER_SAMPLE <= 0 || qcinfo.QCInfo_MOIST_PER_SAMPLE == null)
                                {
                                    //liermao begin modify 20161119
                                    //org：strMessage += "水分质检未完成,";
                                    var result = MessageBox.Show("水分质检未完成，是否提交数据", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (result == System.Windows.Forms.DialogResult.Cancel)
                                    {
                                        strMessage += "水分质检未完成,";
                                    }
                                    //liermao end modify 
                                }



                                //是否有未处理的异常，有则不能保存
                                DataTable UnusualDT = LinQBaseDao.Query("select * from Unusual,QCInfo where Unusual_QCInfo_ID=QCInfo_ID and Unusual_State='未处理' and QCInfo_ID=" + iQcInfoID).Tables[0];

                                if (UnusualDT.Rows.Count > 0)
                                {
                                    mf.ShowToolTip(ToolTipIcon.Info, "保存提示", "该车辆质检异常未处理，请等待处理完成后再保存！！", txtQCInfo_REMARK, this);
                                    return;
                                }

                                #region //收货退货判断
                                //保存前先判断是否有过异常， 是否有收货退货标识
                                DataTable RECV_RMA_METHODDT = LinQBaseDao.Query("select * from QCInfo where   QCInfo_ID=" + iQcInfoID).Tables[0];
                                RECV_RMA_METHOD = comboBox1.Text;
                                if (RECV_RMA_METHODDT.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(RECV_RMA_METHODDT.Rows[0]["QCInfo_RECV_RMA_METHOD"].ToString()))
                                    {
                                        RECV_RMA_METHOD = RECV_RMA_METHODDT.Rows[0]["QCInfo_RECV_RMA_METHOD"].ToString();
                                    }
                                }
                                if (!string.IsNullOrEmpty(RECV_RMA_METHOD) && comboBox1.Text != RECV_RMA_METHOD)
                                {
                                    DialogResult dr = MessageBox.Show("该车辆异常处理结果为" + RECV_RMA_METHOD + ",不能进行" + comboBox1.Text + "！");
                                    if (dr == DialogResult.OK)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }

                                if (RECV_RMA_METHOD == "退货")//退货
                                {
                                    strRECV_RMA_METHOD = "退货";

                                    //加上是否退货还包判断

                                    DataTable RMAdt = LinQBaseDao.Query("select * from QCRecord where QCRecord_QCInfo_ID=" + iQcInfoID + " and QCRecord_TestItems_ID =(select TestItems_ID from TestItems where TestItems_NAME='退货还包')").Tables[0];
                                    if (RMAdt.Rows.Count == 0)
                                    {
                                        DialogResult dr = MessageBox.Show("退货车辆 " + lblCNTR_NO.Text + "还未退货还包，是否完成质检？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                        if (dr == DialogResult.No)
                                        {
                                            return;
                                        }
                                    }
                                }
                                else if (RECV_RMA_METHOD == "收货" || RECV_RMA_METHOD == "" || RECV_RMA_METHOD == null)//收货
                                {
                                    strRECV_RMA_METHOD = "收货";
                                }

                                #endregion

                                if (!string.IsNullOrEmpty(strMessage))
                                {
                                    if (MessageBox.Show(strMessage + "是否完成质检？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        ISFinish = true;
                                        qcinfo.QCIInfo_EndTime = LinQBaseDao.getDate();

                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                #endregion
                            }
                            qcinfo.QCInfo_Dictionary_ID = itemId;
                            qcinfo.QCIInfo_EndTime = LinQBaseDao.getDate();
                            qcinfo.QCInfo_RECV_RMA_METHOD = strRECV_RMA_METHOD == "" ? "收货" : strRECV_RMA_METHOD;
                            DataTable Dictionary_IDDT = LinQBaseDao.Query("select Dictionary_ID from Dictionary where Dictionary_Name='完成质检'").Tables[0];

                            if (Dictionary_IDDT.Rows.Count > 0)
                            {
                                qcinfo.QCInfo_Dictionary_ID = Convert.ToInt32(Dictionary_IDDT.Rows[0][0]);
                            }
                        }
                    }
                    bool bUpdateUser = QCInfoDAL.UpdateInfo(qcinfo);



                    CloseButton();
                    StringBuilder sbMessage = new StringBuilder();
                    sbMessage.Append("质检数据成功保存!");



                    Expression<Func<View_QCInfo, bool>> Funqcinfo = n => n.QCInfo_ID == iQcInfoID;
                    View_QCInfo view_QCInfo_InTerface = QCInfoDAL.Single(Funqcinfo);

                    //发送数据到U9  13620046554

                    string sqls = "select QCInfo_PumpingPackets from View_QCInfo  where QCInfo_ID='" + iQcInfoID + "'";
                    DataTable tb = LinQBaseDao.Query(sqls).Tables[0];
                    int count = 0;
                    if (tb.Rows.Count > 0)
                    {
                        string str = tb.Rows[0]["QCInfo_PumpingPackets"].ToString();
                        if (str != "")
                        {
                            try
                            {
                                count = int.Parse(str);
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    object CountObj = count;//LinQBaseDao.GetSingle("select count(*) from QCRecord where QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='废纸包重') and QCRecord_UPDATE_TIME is NULL and QCRecord_QCInfo_ID=" + iQcInfoID);

                    #region  测试过程中不和U9交互 暂时屏蔽

                    string sql = "select U9Bool from dbo.U9Start where U9Name='一检管理'";
                    bool isbool = bool.Parse(LinQBaseDao.Query(sql).Tables[0].Rows[0]["U9Bool"].ToString());
                    if (isbool)
                    {
                        int DRAW_EXAM_INTERFACE_ID = (int)view_QCInfo_InTerface.DRAW_EXAM_INTERFACE_ID;
                        string strs = U9Class.updataIsU9(DRAW_EXAM_INTERFACE_ID, iQcInfoID.ToString());
                        if (strs == "失败")
                        {
                            MessageBox.Show("发送数据到U9失败！");
                            return;
                        }
                    }

                    //Expression<Func<DRAW_EXAM_INTERFACE, bool>> funDRAW_EXAM_INTERFACE = n => n.DRAW_EXAM_INTERFACE_ID == iDRAW_EXAM_INTERFACE_ID;

                    //if (isIdebarJoinU9(DRAW_EXAM_INTERFACEDAL.Query(funDRAW_EXAM_INTERFACE).FirstOrDefault().PROD_ID))
                    //{

                    //    DataTable issendDT = LinQBaseDao.Query("select issend from RegisterLoosePaperDistribution where R_DRAW_EXAM_INTERFACE_ID=" + view_QCInfo_InTerface.DRAW_EXAM_INTERFACE_ID).Tables[0];
                    //    if (issendDT.Rows.Count > 0)
                    //    {
                    //        object str = issendDT.Rows[0][0];
                    //        bool b = Convert.ToBoolean(str);
                    //        if (!b)
                    //        {
                    //            bool isSend = U9Class.sendDate(Convert.ToInt32(CountObj), Convert.ToDouble(view_QCInfo_InTerface.QCInfo_BAGWeight), view_QCInfo_InTerface.ExtensionField2, view_QCInfo_InTerface.QCInfo_ID.ToString(), view_QCInfo_InTerface.SHIPMENT_NO, view_QCInfo_InTerface.DepartmentCode);


                    //            if (isSend)
                    //            {
                    //                //发送成功修改标识
                    //                int result = LinQBaseDao.ExecuteSql("update dbo.RegisterLoosePaperDistribution set  issend=1 where R_DRAW_EXAM_INTERFACE_ID=" + view_QCInfo_InTerface.DRAW_EXAM_INTERFACE_ID);

                    //            }
                    //            else
                    //            {

                    //                MessageBox.Show("发送数据到U9失败！");
                    //                return;
                    //            }

                    //        }
                    //    }
                    //}
                    #endregion
                    //判断是否发送到接口表
                    if (strRECV_RMA_METHOD == "退货")
                    {
                        //如果退货 先删除水分和重量接口表的数据，以免先收货发送数据过去后 又改成退货。DTS不要退货的数据

                        LinQBaseDao.Query("delete  dbo.MATERIAL_QC_INTERFACE where SHIPMENT_NO='" + view_QCInfo_InTerface.SHIPMENT_NO + "'");
                        LinQBaseDao.Query("delete  dbo.OCC_MOIST_INTERFACE where SHIPMENT_NO='" + view_QCInfo_InTerface.SHIPMENT_NO + "'");
                        LinQBaseDao.Query("update QCInfo set QCInfo_ISSend=null,QCInfo_SendTime=null where QCInfo_id=" + iQcInfoID);
                    }
                    else
                    {
                        //收货就发送到接口表
                        sbMessage.Append(InsertInterface(iQcInfoID));
                    }


                    tslPrintPreview("");//打印
                    iQcInfoID = 0;//清除质检编号
                    iDRAW_EXAM_INTERFACE_ID = 0;//抽包编号

                    GetloadGrid();

                    MessageBox.Show(sbMessage.ToString(), "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    mf.ShowToolTip(ToolTipIcon.Info, "保存提示", "请选择数据列表中的一行数据再保存！", lvwUserList, this);
                    return;
                }

            }
            catch (Exception ex)
            {
                Common.WriteTextLog("OneQCAdmin.baocun()异常" + ex.Message.ToString());
            }

        }

        public bool BatchSaving()
        {
            int countnum = 0;
            for (int i = 0; i < lvwUserList.RowCount; i++)
            {
                try
                {
                    string trueRows = lvwUserList.Rows[i].Cells[0].Value.ToString();
                    if (trueRows == "True")
                    {
                        try
                        {
                            string RECV_RMA_METHOD = lvwUserList.Rows[i].Cells["QCInfo_RECV_RMA_METHOD"].Value.ToString();
                            if (RECV_RMA_METHOD == "")
                            {
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            return false;
                        }
                        countnum++;
                    }
                }
                catch
                {
                }
            }
            if (countnum > 0)
            {
                progressBar1.Visible = true;
                int numcount = 0;
                StringBuilder sbMessage = new StringBuilder();
                #region
                for (int i = 0; i < lvwUserList.RowCount; i++)
                {
                    try
                    {
                        string trueRows = lvwUserList.Rows[i].Cells[0].Value.ToString();
                        if (trueRows == "True")
                        {
                            string strRECV_RMA_METHOD = "";
                            bool ISFinish = false;//是否完成
                            string RECV_RMA_METHOD = "";//收获/退货
                            try
                            {
                                int qcinfi_id = int.Parse(lvwUserList.Rows[i].Cells["QCInfo_ID"].Value.ToString());
                                try
                                {
                                    RECV_RMA_METHOD = lvwUserList.Rows[i].Cells["QCInfo_RECV_RMA_METHOD"].Value.ToString();
                                    if (RECV_RMA_METHOD == "")
                                    {
                                        break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    return false;
                                }
                                QCInfo oldqcInfo = QCInfoDAL.GetQCInfo(qcinfi_id);
                                QCInfo qcinfo = oldqcInfo;
                                #region 新增验证
                                string strMessage = "";
                                if (qcinfo.QCInfo_BAGWeight <= 0 || qcinfo.QCInfo_BAGWeight == null || qcinfo.QCInfo_MATERIAL_WEIGHT <= 0 || qcinfo.QCInfo_MATERIAL_WEIGHT == null || qcinfo.QCInfo_MATERIAL_SCALE <= 0 || qcinfo.QCInfo_MATERIAL_SCALE == null || qcinfo.QCInfo_PAPER_WEIGHT <= 0 || qcinfo.QCInfo_PAPER_WEIGHT == null || qcinfo.QCInfo_PAPER_SCALE <= 0 || qcinfo.QCInfo_PAPER_SCALE == null)
                                {
                                    strMessage += "重量质检未完成,";
                                }
                                if (qcinfo.QCInfo_MOIST_PER_SAMPLE <= 0 || qcinfo.QCInfo_MOIST_PER_SAMPLE == null)
                                {
                                    //liermao begin modify 20161119
                                    //strMessage += "水分质检未完成,";
                                    var result = MessageBox.Show("水分质检未完成，是否提交数据", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                                    if (result == System.Windows.Forms.DialogResult.Cancel)
                                    {
                                        strMessage += "水分质检未完成,";
                                    }
                                    //liermao end modify
                                }

                                //是否有未处理的异常，有则不能保存
                                DataTable UnusualDT = LinQBaseDao.Query("select * from Unusual,QCInfo where Unusual_QCInfo_ID=QCInfo_ID and Unusual_State='未处理' and QCInfo_ID=" + qcinfo.QCInfo_ID).Tables[0];
                                if (UnusualDT.Rows.Count > 0)
                                {
                                    progressBar1.Visible = false;
                                    mf.ShowToolTip(ToolTipIcon.Info, "保存提示", "该车辆质检异常未处理，请等待处理完成后再保存！！", txtQCInfo_REMARK, this);
                                    return false;
                                }
                                //保存前先判断是否有过异常， 是否有收货退货标识
                                DataTable RECV_RMA_METHODDT = LinQBaseDao.Query("select * from QCInfo where   QCInfo_ID=" + qcinfo.QCInfo_ID).Tables[0];

                                if (RECV_RMA_METHODDT.Rows.Count > 0)
                                {
                                    try
                                    {
                                        if (RECV_RMA_METHODDT.Rows[0]["QCInfo_RECV_RMA_METHOD"].ToString() != "")
                                        {
                                            RECV_RMA_METHOD = RECV_RMA_METHODDT.Rows[0]["QCInfo_RECV_RMA_METHOD"].ToString();
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (RECV_RMA_METHOD == "退货")//退货
                                {
                                    strRECV_RMA_METHOD = "退货";
                                }
                                if (RECV_RMA_METHOD == "收货" || RECV_RMA_METHOD == "" || RECV_RMA_METHOD == null)//收货
                                {
                                    strRECV_RMA_METHOD = "收货";
                                }
                                #endregion
                                qcinfo.QCIInfo_EndTime = LinQBaseDao.getDate();
                                qcinfo.QCInfo_RECV_RMA_METHOD = strRECV_RMA_METHOD == "" ? "收货" : strRECV_RMA_METHOD;
                                DataTable Dictionary_IDDT = LinQBaseDao.Query("select Dictionary_ID from Dictionary where Dictionary_Name='完成质检'").Tables[0];

                                if (Dictionary_IDDT.Rows.Count > 0)
                                {
                                    qcinfo.QCInfo_Dictionary_ID = Convert.ToInt32(Dictionary_IDDT.Rows[0][0]);
                                }
                                if (!string.IsNullOrEmpty(strMessage))
                                {
                                    if (MessageBox.Show("质检编号：" + qcinfo.QCInfo_ID + "\n" + strMessage + "是否完成质检？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        ISFinish = true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                bool bUpdateUser = QCInfoDAL.UpdateInfo(qcinfo);
                                sbMessage.Append(qcinfo.QCInfo_ID + "质检数据成功保存!");
                                Expression<Func<View_QCInfo, bool>> Funqcinfo = n => n.QCInfo_ID == qcinfo.QCInfo_ID;
                                View_QCInfo view_QCInfo_InTerface = QCInfoDAL.Single(Funqcinfo);
                                //发送数据到U9  13620046554
                                object CountObj = LinQBaseDao.GetSingle("select count(*) from QCRecord where QCRecord_TestItems_ID=(select TestItems_ID from TestItems where TestItems_NAME='废纸包重') and QCRecord_QCInfo_ID=" + qcinfo.QCInfo_ID);
                                #region  测试过程中不和U9交互 暂时屏蔽
                                Expression<Func<DRAW_EXAM_INTERFACE, bool>> funDRAW_EXAM_INTERFACE = n => n.DRAW_EXAM_INTERFACE_ID == iDRAW_EXAM_INTERFACE_ID;
                                if (isIdebarJoinU9(DRAW_EXAM_INTERFACEDAL.Query(funDRAW_EXAM_INTERFACE).FirstOrDefault().PROD_ID))
                                {
                                    DataTable issendDT = LinQBaseDao.Query("select issend from RegisterLoosePaperDistribution where R_DRAW_EXAM_INTERFACE_ID=" + view_QCInfo_InTerface.DRAW_EXAM_INTERFACE_ID).Tables[0];
                                    if (issendDT.Rows.Count > 0)
                                    {
                                        object str = issendDT.Rows[0][0];
                                        bool b = Convert.ToBoolean(str);
                                        if (!b)
                                        {
                                            bool isSend = U9Class.sendDate(Convert.ToInt32(CountObj), Convert.ToDouble(view_QCInfo_InTerface.QCInfo_BAGWeight), view_QCInfo_InTerface.ExtensionField2, view_QCInfo_InTerface.QCInfo_ID.ToString(), view_QCInfo_InTerface.SHIPMENT_NO, view_QCInfo_InTerface.DepartmentCode);
                                            if (isSend)
                                            {
                                                //发送成功修改标识
                                                int result = LinQBaseDao.ExecuteSql("update dbo.RegisterLoosePaperDistribution set  issend=1 where R_DRAW_EXAM_INTERFACE_ID=" + view_QCInfo_InTerface.DRAW_EXAM_INTERFACE_ID);
                                            }
                                            else
                                            {
                                                progressBar1.Visible = false;
                                                MessageBox.Show("发送数据到U9失败！");
                                                return false;
                                            }
                                        }
                                    }
                                }
                                #endregion
                                //判断是否发送到接口表
                                if (strRECV_RMA_METHOD == "退货")
                                {
                                    //如果退货 先删除水分和重量接口表的数据，以免先收货发送数据过去后 又改成退货。DTS不要退货的数据

                                    LinQBaseDao.Query("delete  dbo.MATERIAL_QC_INTERFACE where SHIPMENT_NO='" + view_QCInfo_InTerface.SHIPMENT_NO + "'");
                                    LinQBaseDao.Query("delete  dbo.OCC_MOIST_INTERFACE where SHIPMENT_NO='" + view_QCInfo_InTerface.SHIPMENT_NO + "'");
                                    LinQBaseDao.Query("update QCInfo set QCInfo_ISSend=null,QCInfo_SendTime=null where QCInfo_id=" + qcinfo.QCInfo_ID);
                                }
                                else
                                {
                                    //收货就发送到接口表
                                    sbMessage.Append(InsertInterface(qcinfo.QCInfo_ID));
                                }
                            }
                            catch (Exception ex)
                            {
                                Common.WriteTextLog("OneQCAdmin.baocun()异常" + ex.Message.ToString());
                            }
                        }
                    }
                    catch
                    {
                    }
                    numcount++;
                    progressBar1.Value = numcount;
                }
                MessageBox.Show(sbMessage.ToString(), "保存提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                #endregion
            }



            return true;
        }

        /// <summary>
        /// 是否需要和U9交互
        /// </summary>
        /// <param name="PROD_ID"></param>
        private bool isIdebarJoinU9(string PROD_ID)
        {

            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            List<idebarJoinU9> ijList = dc.idebarJoinU9.ToList();

            foreach (idebarJoinU9 item in ijList)
            {
                if (item.idebarJoinU9_PROD_ID == PROD_ID)
                {
                    return false;
                }
            }

            return true;
        }


        #region 20130105  杨再飞   加入重复发送接口表判断和修改
        /// <summary>
        /// 将水分和重量结果插入接口表
        /// 修改;2012-08-10 判断是否算出水分拆包前水分>0和拆包后水分平均值>0和杂质比例>=0、杂物比例>=0、抽样废纸包总重>0 如果不是这样不发送
        /// </summary>
        /// <param name="iQcID">质检编号</param>
        /// <returns></returns>
        private string InsertInterface(int iQcID)
        {
            //iQcInfoID
            StringBuilder sbMessage = new StringBuilder();
            Expression<Func<View_QCInfo, bool>> Funqcinfo = n => n.QCInfo_ID == iQcID;
            int rint = 0;
            bool isupdate = false;//未过数是否修改
            istishi = false;//是否提示
            View_QCInfo view_QCInfo_InTerface = QCInfoDAL.Single(Funqcinfo);
            if (view_QCInfo_InTerface.QCInfo_MOIST_PER_SAMPLE != null && view_QCInfo_InTerface.QCInfo_BAGWeight != null && view_QCInfo_InTerface.QCInfo_RECV_RMA_METHOD == "收货")
            {

                if (view_QCInfo_InTerface.QCInfo_MOIST_PER_SAMPLE > 0)
                {

                    Expression<Func<OCC_MOIST_INTERFACE, bool>> Occmoist = n => n.WEIGHT_TICKET_NO == view_QCInfo_InTerface.WEIGHT_TICKET_NO && n.CNTR_NO == view_QCInfo_InTerface.CNTR_NO;
                    IEnumerable<OCC_MOIST_INTERFACE> occ_erface = OCC_MOIST_INTERFACEDAL.Query(Occmoist);
                    if (occ_erface.Count() > 0)
                    {
                        foreach (var occ in occ_erface)
                        {
                            if (string.IsNullOrEmpty(occ.TRANS_TO_DTS_FLAG) && string.IsNullOrEmpty(occ.TRANS_TO_DTS_DTTM.ToString()))
                            {
                                if (MessageBox.Show("质检编号为：" + view_QCInfo_InTerface.QCInfo_ID + "已发送至接口表，是否替换？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    isupdate = true;
                                    Action<OCC_MOIST_INTERFACE> ap = s =>
                                    {
                                        decimal wateValue = 0;

                                        if (view_QCInfo_InTerface.QCInfo_MOIST_PER_SAMPLE != null)
                                        {
                                            wateValue = Convert.ToDecimal(view_QCInfo_InTerface.QCInfo_MOIST_PER_SAMPLE);//水分百分比
                                        }
                                        s.REF_NO = "";//REF_NO抽包
                                        s.CNTR_NO = view_QCInfo_InTerface.CNTR_NO;
                                        s.COMPANY_ID = view_QCInfo_InTerface.COMPANY_ID;
                                        s.MOIST_EXAMINER = view_QCInfo_InTerface.QCInfo_MOIST_EXAMINER;
                                        s.MOIST_PER_SAMPLE = view_QCInfo_InTerface.QCInfo_MOIST_PER_SAMPLE;
                                        s.PO_NO = view_QCInfo_InTerface.PO_NO;
                                        s.PRE_DRY_WT = 100;
                                        s.POST_DRY_WT = 100 * (1 - wateValue / 100);
                                        s.PROD_ID = view_QCInfo_InTerface.PROD_ID;
                                        s.QC_RPT_NO = view_QCInfo_InTerface.QC_PRINTNUM;//化验单
                                        s.REF_NO = view_QCInfo_InTerface.REF_NO;
                                        s.SHIPMENT_NO = view_QCInfo_InTerface.SHIPMENT_NO;

                                        s.TTL_BALES = view_QCInfo_InTerface.NO_OF_BALES;
                                        s.TTL_SAMPLE = view_QCInfo_InTerface.QCInfo_PumpingPackets;
                                        s.VERSION_NO = "1";
                                        s.WEIGHT_TICKET_NO = view_QCInfo_InTerface.WEIGHT_TICKET_NO;
                                    };
                                    OCC_MOIST_INTERFACEDAL.Update(Occmoist, ap);
                                }
                                else
                                {
                                    istishi = true;
                                }
                            }
                            else
                            {
                                sbMessage.Append("已发送至接口表,并且已经过数DTS！");
                            }
                        }
                    }
                    else
                    {
                        bool rboolSetAndInsertOCC_MOIST_INTERFACE = OCC_MOIST_INTERFACEDAL.SetAndInsertOCC_MOIST_INTERFACE(view_QCInfo_InTerface, out rint);
                        if (!rboolSetAndInsertOCC_MOIST_INTERFACE)
                        {
                            sbMessage.Append("将水分检测信息插入接口表失败，请与管理员联系！ ");
                        }
                    }

                }
                if (view_QCInfo_InTerface.QCInfo_BAGWeight != null && view_QCInfo_InTerface.QCInfo_MATERIAL_SCALE != null && view_QCInfo_InTerface.QCInfo_PAPER_SCALE != null)
                {
                    if (view_QCInfo_InTerface.QCInfo_BAGWeight.Value > 0 && view_QCInfo_InTerface.QCInfo_MATERIAL_SCALE.Value >= 0 && view_QCInfo_InTerface.QCInfo_PAPER_SCALE.Value >= 0)
                    {
                        Expression<Func<MATERIAL_QC_INTERFACE, bool>> Occmoist = n => n.WEIGHT_TICKET_NO == view_QCInfo_InTerface.WEIGHT_TICKET_NO && n.CNTR_NO == view_QCInfo_InTerface.CNTR_NO;
                        IEnumerable<MATERIAL_QC_INTERFACE> material_qc = MATERIAL_QC_INTERFACEDAL.Query(Occmoist);
                        if (material_qc.Count() > 0)
                        {
                            if (isupdate)
                            {
                                Action<MATERIAL_QC_INTERFACE> ap = v =>
                                {
                                    v.CNTR_NO = view_QCInfo_InTerface.CNTR_NO;
                                    v.COMPANY_ID = view_QCInfo_InTerface.COMPANY_ID;
                                    v.NET_WEIGHT = view_QCInfo_InTerface.QCInfo_BAGWeight;//重量
                                    v.NET_WEIGHT_UOM = "KG";//重量单位
                                    v.OTHER_MATERIAL = view_QCInfo_InTerface.QCInfo_MATERIAL_WEIGHT;	//暂时将杂质重量 设置为其它杂质(kg)	
                                    v.OTHER_OUTTHROW = view_QCInfo_InTerface.QCInfo_PAPER_WEIGHT;//暂时将杂纸重量 其它杂纸 (kg)	
                                    v.PO_NO = view_QCInfo_InTerface.PO_NO;
                                    v.PROD_EXAMINER = view_QCInfo_InTerface.QCInfo_PAPER_EXAMINER;
                                    v.PROD_ID = view_QCInfo_InTerface.PROD_ID;
                                    v.QC_RPT_NO = view_QCInfo_InTerface.QC_PRINTNUM;//化验单(打印编号)
                                    v.REF_NO = view_QCInfo_InTerface.REF_NO;
                                    v.SHIPMENT_NO = view_QCInfo_InTerface.SHIPMENT_NO;
                                    v.TTL_BALES = view_QCInfo_InTerface.NO_OF_BALES;//总包数
                                    v.TTL_SAMPLE = view_QCInfo_InTerface.QCInfo_PumpingPackets;//总抽样数
                                    v.VERSION_NO = "1";
                                    v.WEIGHT = view_QCInfo_InTerface.QCInfo_BAGWeight;//抽样捆重量
                                    v.WEIGHT_TICKET_NO = view_QCInfo_InTerface.WEIGHT_TICKET_NO;

                                };
                                MATERIAL_QC_INTERFACEDAL.Update(Occmoist, ap);
                            }
                        }
                        else
                        {
                            bool rboolSetAndInsertMATERIAL_QC_INTERFACE = MATERIAL_QC_INTERFACEDAL.SetAndInsertMATERIAL_QC_INTERFACE(view_QCInfo_InTerface, out rint);
                            if (!rboolSetAndInsertMATERIAL_QC_INTERFACE)
                            {
                                sbMessage.Append("将重量检测信息插入接口表失败，请与管理员联系！ ");
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(sbMessage.ToString()))
                {
                    #region 修改发送QcInfo发送状态 2012-07-10
                    try
                    {
                        Expression<Func<QCInfo, bool>> p = n => n.QCInfo_ID == iQcID;
                        Action<QCInfo> ap = n =>
                        {

                            n.QCInfo_ISSend = true;//平均水分：
                            n.QCInfo_SendTime = LinQBaseDao.getDate();
                        };
                        QCInfoDAL.Update(p, ap);

                    }
                    catch (Exception ex)
                    {
                        Common.WriteTextLog("将水分和重量结果插入接口表更改质检表状态异常：" + ex.Message);
                        //  mf.ShowToolTip(ToolTipIcon.Info, "系统提示", "发送成功，但更改发送状态失败！", lvwUserList, this);
                    }
                    #endregion
                }
            }
            else
            {
                sbMessage.Append("质检编号：" + view_QCInfo_InTerface.QCInfo_ID + "，数据不完整！ ");
            }
            return sbMessage.ToString();
        }
        #endregion
        #region      20130105  旧版本方法
        /// <summary>
        /// 将水分和重量结果插入接口表
        /// 修改;2012-08-10 判断是否算出水分拆包前水分>0和拆包后水分平均值>0和杂质比例>=0、杂物比例>=0、抽样废纸包总重>0 如果不是这样不发送
        /// </summary>
        /// <param name="iQcID">质检编号</param>
        /// <returns></returns>
        //private string InsertInterface(int iQcID)
        //{//iQcInfoID
        //    StringBuilder sbMessage = new StringBuilder();
        //    Expression<Func<View_QCInfo, bool>> Funqcinfo = n => n.QCInfo_ID == iQcID;
        //    int rint = 0;
        //    View_QCInfo view_QCInfo_InTerface = QCInfoDAL.Single(Funqcinfo);
        //    if (view_QCInfo_InTerface.QCInfo_UnpackBefore_MOIST_PER_SAMPLE != null && view_QCInfo_InTerface.QCInfo_UnpackBack_MOIST_PER_SAMPLE != null)
        //    {
        //        if (view_QCInfo_InTerface.QCInfo_UnpackBefore_MOIST_PER_SAMPLE.Value > 0 && view_QCInfo_InTerface.QCInfo_UnpackBack_MOIST_PER_SAMPLE.Value > 0)
        //        {
        //            bool rboolSetAndInsertOCC_MOIST_INTERFACE = OCC_MOIST_INTERFACEDAL.SetAndInsertOCC_MOIST_INTERFACE(view_QCInfo_InTerface, out rint);
        //            if (!rboolSetAndInsertOCC_MOIST_INTERFACE)
        //            {
        //                sbMessage.Append("将水分检测信息插入接口表失败，请与管理员联系！ ");
        //            }
        //        }
        //        if (view_QCInfo_InTerface.QCInfo_BAGWeight != null && view_QCInfo_InTerface.QCInfo_MATERIAL_SCALE != null && view_QCInfo_InTerface.QCInfo_PAPER_SCALE != null)
        //        {
        //            if (view_QCInfo_InTerface.QCInfo_BAGWeight.Value > 0 && view_QCInfo_InTerface.QCInfo_MATERIAL_SCALE.Value >= 0 && view_QCInfo_InTerface.QCInfo_PAPER_SCALE.Value >= 0)
        //            {
        //                bool rboolSetAndInsertMATERIAL_QC_INTERFACE = MATERIAL_QC_INTERFACEDAL.SetAndInsertMATERIAL_QC_INTERFACE(view_QCInfo_InTerface, out rint);
        //                if (!rboolSetAndInsertMATERIAL_QC_INTERFACE)
        //                {
        //                    sbMessage.Append("将重量检测信息插入接口表失败，请与管理员联系！ ");
        //                }
        //            }
        //        }
        //    }


        //    if (string.IsNullOrEmpty(sbMessage.ToString()))
        //    {
        //        #region 修改发送QcInfo发送状态 2012-07-10
        //        try
        //        {
        //            Expression<Func<QCInfo, bool>> p = n => n.QCInfo_ID == iQcID;
        //            Action<QCInfo> ap = n =>
        //            {

        //                n.QCInfo_ISSend = true;//平均水分：
        //                n.QCInfo_SendTime = LinQBaseDao.getDate();
        //            };
        //            QCInfoDAL.Update(p, ap);

        //        }
        //        catch (Exception ex)
        //        {
        //            Common.WriteTextLog("将水分和重量结果插入接口表更改质检表状态异常：" + ex.Message);
        //            //  mf.ShowToolTip(ToolTipIcon.Info, "系统提示", "发送成功，但更改发送状态失败！", lvwUserList, this);
        //        }
        //        #endregion
        //    }
        //    return sbMessage.ToString();
        //}
        #endregion
        private void cbxQCInfoState_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxQCInfoState.SelectedIndex > 0)
            {
                int itemId = Converter.ToInt(cbxQCInfoState.SelectedValue.ToString());
                if (itemId > -1)
                {
                    Dictionary dic = cbxQCInfoState.SelectedItem as Dictionary;
                    if (dic.Dictionary_Value == "终止质检")
                    {
                        txtQCInfo_REMARK.Enabled = true;
                    }
                    else
                    {
                        txtQCInfo_REMARK.Enabled = false;
                    }

                    #region 验证是否可以完成质检
                    #endregion
                }
            }
        }
        /// <summary>
        /// 发送检测重量和水分结果到接口表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslabSend_Click()
        {
            //iQcInfoID
            int count = 0;//计数器计算选中的个数
            for (int i = 0; i < lvwUserList.RowCount; i++)
            {
                try
                {
                    string trueRows = lvwUserList.Rows[i].Cells[0].Value.ToString();
                    if (trueRows == "True")
                    {
                        count++;
                    }
                }
                catch (Exception)
                {

                }
            }
            if (count > 0)
            {
                if (MessageBox.Show("是否发送结果到接口表？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    BatchSaving();
                }
            }
            else
            {
                MessageBox.Show("请选中数据在发送到结果表", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// "手动修改质检信息"按钮的单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bslabUpdateQC_Click(object sender, EventArgs e)
        {
        }

        private void dtp_endTime_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void btn_DetermineUpdate_Click(object sender, EventArgs e)
        {
            if (btn_DetermineUpdate.Enabled)
            {
                btn_DetermineUpdate.Enabled = false;
                UpdateMethod();
                btn_DetermineUpdate.Enabled = true;
                int ID = this.lvwUserList.SelectedRows.Count;
                if (ID > 0)
                {
                    btnCalculateAfter.Visible = true;
                    btnSave.Visible = true;
                }
            }
            //GetloadGrid();
            btnQCOneInfo_Click(null, null);
            btn_DetermineUpdate.Visible = false;
            btn_DetermineUpdate.Enabled = false;
            //  LoadData("");
        }


        /// <summary>
        /// 根据是否发送到接口表改变颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwUserList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                if (lvwUserList.RowCount > 0)
                {
                    for (int i = 0; i < lvwUserList.RowCount; i++)
                    {
                        if (lvwUserList.Rows[i].Cells["QCInfo_ISSend"] != null)
                        {
                            string str = lvwUserList.Rows[i].Cells["QCInfo_ISSend"].Value.ToString().Trim();
                            if (!string.IsNullOrEmpty(lvwUserList.Rows[i].Cells["QCInfo_ISSend"].Value.ToString().Trim()) && lvwUserList.Rows[i].Cells["QCInfo_ISSend"].Value.ToString().Trim() == "是")
                            {
                                lvwUserList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Lime;
                            }
                            else
                            {
                                lvwUserList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gold;
                            }
                        }
                        else
                        {
                            lvwUserList.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Gold;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("是否发送接口表颜色变化处理异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 单击单元格内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwUserList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btn_DetermineUpdate.Visible = false;
                btn_DetermineUpdate.Enabled = false;
                Enabledbool(false);
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    Common.SetSelectionBackColor(lvwUserList);

                    if ((bool)lvwUserList.Rows[e.RowIndex].Cells["xz"].EditedFormattedValue != false)
                    {
                        lvwUserList.Rows[e.RowIndex].Cells["xz"].Value = false;
                        lvwUserList.Rows[e.RowIndex].Selected = false;
                        ids.Remove(e.RowIndex);
                        if (lvwUserList.Rows[e.RowIndex].Cells["QCInfo_ID"].Value != null)
                        {
                            ids.Remove(e.RowIndex);
                            int qcrid = EMEWE.Common.Converter.ToInt(lvwUserList.Rows[e.RowIndex].Cells["QCInfo_ID"].Value.ToString());
                            listrecordID.Remove(qcrid);
                        }
                    }
                    else
                    {
                        lvwUserList.Rows[e.RowIndex].Cells["xz"].Value = true;
                        lvwUserList.Rows[e.RowIndex].Selected = true;
                        ids.Add(e.RowIndex);
                        if (lvwUserList.Rows[e.RowIndex].Cells["QCInfo_ID"].Value != null)
                        {
                            ids.Add(e.RowIndex);
                            int qcrid = EMEWE.Common.Converter.ToInt(lvwUserList.Rows[e.RowIndex].Cells["QCInfo_ID"].Value.ToString());
                            listrecordID.Remove(qcrid);
                            listrecordID.Add(qcrid);

                        }
                    }
                }
                //选中多行
                if (ids.Count > 0)
                {
                    for (int i = 0; i < ids.Count; i++)
                    {

                        if (i > lvwUserList.Rows.Count)
                        {
                            lvwUserList.Rows[ids[i]].Selected = true;
                            lvwUserList.Rows[ids[i]].Cells["xz"].Value = true;
                        }

                    }
                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// 双击显示图片信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvwUserList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                btn_DetermineUpdate.Visible = false;
                btn_DetermineUpdate.Enabled = false;
                ImageForm igf = new ImageForm(lvwUserList.SelectedRows[0].Cells["QCInfo_ID"].Value.ToString());
                igf.Show();
            }
            catch (Exception)
            {

            }
        }

        private void txtWaitPO_NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('\r'))
            {
                ids.Clear();
                expr = null;
                page = new PageControl();
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                GetDgvWaitQCSeacher();
                LoadData("");
                Common.WriteLogData("查询", "一检信息查询", Common.USERNAME); //操作日志
            }
        }
        bool exitdayin = false;
        private void button1_Click(object sender, EventArgs e)
        {
            exitdayin = true;
        }
    }
}