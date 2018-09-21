using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEQUALITY.HelpClass;
using EMEWEDAL;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class InstrumentInfoForm : Form
    {
        public InstrumentInfoForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 实例化分页控件
        /// </summary>
        PageControl page = new PageControl();
        /// <summary>
        /// 总页数
        /// </summary>
        int zongyeshu = 0;
        private void InstrumentInfoForm_Load(object sender, EventArgs e)
        {
            //"水分检测员"
            cmbType.SelectedIndex = 0;
            CobUserName();
            BindCollection();
            this.txtCurrentPage1.Text = "1";
            BingList();
            this.dgvInstrumentInfo.AutoGenerateColumns = false;
        }

        /// <summary>
        /// 绑定采集端
        /// </summary>
        private void BindCollection()
        {
            try
            {
                //绑定客户端
                string sql = " select Client_ID,Client_NAME from dbo.ClientInfo ";
                DataTable dt = LinQBaseDao.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    cob_ClientName.DisplayMember = "Client_NAME";
                    cob_ClientName.ValueMember = "Client_ID";
                    cob_ClientName.DataSource = dt;
                    cmbCollection.SelectedIndex = 0;
                }
                //dt = LinQBaseDao.Query("select Collection_ID,Collection_Name from CollectionInfo").Tables[0];
                //if (dt.Rows.Count > 0)
                //{
                //    cmbCollection.DisplayMember = "Collection_Name";
                //    cmbCollection.ValueMember = "Collection_ID";
                //    cmbCollection.DataSource = dt;
                //    cmbCollection.SelectedIndex = 0;
                //}
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 绑定下拉列表框检测人
        /// </summary>
        /// <param name="name"></param>
        public void CobUserName()
        {
            string sql = "select UserID,UserName from UserInfo2 ";
            DataTable dt = LinQBaseDao.Query(sql).Tables[0];
            DataRow dr = dt.NewRow();
            dr["UserID"] = 0;
            dr["UserName"] = "";
            dt.Rows.InsertAt(dr, 0);
            cobInstrument_UserName.DisplayMember = "UserName";
            cobInstrument_UserName.ValueMember = "UserID";
            cobInstrument_UserName.DataSource = dt;
            cobInstrument_UserName.SelectedIndex = 0;
        }

        /// <summary>
        /// DataGridView添加列
        /// </summary>
        /// <param name="lname">显示列名</param>
        /// <param name="datename">绑定的数据库列的名称</param>
        ///  <param name="DataGridView1">添加的DataGridView表明</param>
        private void DataGridViewColumnADD(string lname, string datename, DataGridView DataGridView1)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.HeaderText = lname;//显示的列名
            column.Name = datename;//列名
            column.Width = 150;
            column.DataPropertyName = datename;//绑定的数据库列的名称

            DataGridView1.Columns.Add(column);

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BingList()
        {
            try
            {
                this.btnUpdate.Visible = false;
                this.btnqk.Visible = false;
                this.btnADD.Enabled = true;
                int pageNum = Convert.ToInt32(this.txtCurrentPage1.Text);
                int pagesize = Convert.ToInt32(tscbxPageSize1.SelectedItem.ToString());
                string sql = "select top (" + pagesize + ") Instrument_ID,(select Collection_Name from CollectionInfo where Collection_ID=Instrument_Collection_ID) as Collection_Name,(select UserName from UserInfo2 where UserId=Instrument_UserID) as UserName,(select Client_NAME from ClientInfo where Client_ID in(select Collection_Client_ID from CollectionInfo where Collection_ID=Instrument_Collection_ID)) as Client_NAME,Instrument_Name,case Instrument_Type when '0' then '水分检测员' else '保安部水分检测员' end as Instrument_Type from InstrumentInfo  where Instrument_ID not in (select top (" + ((pageNum - 1) * pagesize) + ") Instrument_ID from dbo.InstrumentInfo)";
                dgvInstrumentInfo.DataSource = LinQBaseDao.Query(sql).Tables[0];
                dgvInstrumentInfo.AllowUserToAddRows = false;//隐藏最后空白行
                sql = "select count(0) from InstrumentInfo";
                string sums = LinQBaseDao.GetSingle(sql).ToString();
                if (sums != "")
                {
                    zongyeshu = int.Parse(sums) / 10;
                    if (int.Parse(sums) % 10 != 0)
                    {
                        zongyeshu += 1;
                    }
                }
                lblPageCount2.Text = "共" + zongyeshu + "页";//把查询出来的总行数显示出来
                empty();
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 设置每页显示最大条数事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tscbxPageSize2_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = new PageControl();
            page.PageMaxCount = tscbxPageSize1.SelectedItem.ToString();
            BingList();
        }
        /// <summary>
        /// 添加仪表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnADD_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInstrument_name.Text.Trim() == "")
                {
                    MessageBox.Show("登记失败!仪表不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sql = "";
                DataTable dt = SelectName();
                //if (dt.Rows.Count <= 0)
                //{
                    if (cobInstrument_UserName.Text == "")
                    {
                        sql = "insert into InstrumentInfo(Instrument_Collection_ID,Instrument_Name,Instrument_Type) values( " + cmbCollection.SelectedValue + ",'" + txtInstrument_name.Text + "','" + cmbType.SelectedIndex + "')";
                    }
                    else
                    {
                        sql = "insert into InstrumentInfo(Instrument_Collection_ID,Instrument_Name,Instrument_UserID,Instrument_Type) values(" + cmbCollection.SelectedValue + ",'" + txtInstrument_name.Text + "'," + cobInstrument_UserName.SelectedValue + ",'" + cmbType.SelectedIndex + "')";
                    }
                    if (LinQBaseDao.ExecuteSql(sql) > 0)
                    {
                        MessageBox.Show("登记成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string strContent = this.txtInstrument_name.Text;
                        LogInfoDAL.loginfoadd("添加", "添加：" + strContent + "仪表登记的信息", Common.USERNAME);//添加日志
                        BingList();
                    }
                    else
                    {
                        MessageBox.Show("登记失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                //}
                //else
                //{
                //    MessageBox.Show("登记失败,该仪表或检测员已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// 查重
        /// </summary>
        /// <returns></returns>
        public DataTable SelectName()
        {
            string sql = "select * from dbo.InstrumentInfo where Instrument_Name='" + this.txtInstrument_name.Text + "'";
            return LinQBaseDao.Query(sql).Tables[0];
        }
        /// <summary>
        /// 分页响应事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tslHomPage1")//首页
            {
                txtCurrentPage1.Text = "1";
                BingList();
                return;
            }
            if (e.ClickedItem.Name == "tslPreviousPage1")//上一页
            {
                int yeshu = int.Parse(txtCurrentPage1.Text);
                if (yeshu > 1)
                {
                    txtCurrentPage1.Text = (yeshu - 1).ToString();
                }
                else
                {
                    MessageBox.Show("这已经第一页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                BingList();
                return;
            }
            if (e.ClickedItem.Name == "tslNextPage1")//下一页
            {
                int yeshu = int.Parse(txtCurrentPage1.Text);
                if (yeshu < zongyeshu)
                {
                    txtCurrentPage1.Text = (yeshu + 1).ToString();
                }
                else
                {
                    MessageBox.Show("这已经最后页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                BingList();
                return;
            }
            if (e.ClickedItem.Name == "tslLastPage1")//尾页
            {
                txtCurrentPage1.Text = zongyeshu.ToString();
                BingList();
                return;
            }
            if (e.ClickedItem.Name == "tsbtnUpdate")//修改
            {
                InstrumentUpdate();
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除
            {
                InstrumentYanzheng();
                BingList();
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect")//取消全选
            {
                for (int i = 0; i < dgvInstrumentInfo.Rows.Count; i++)
                {

                    dgvInstrumentInfo.Rows[i].Selected = false;
                }
                return;
            }
            if (e.ClickedItem.Name == "tslSelectAll")//全选
            {
                for (int i = 0; i < dgvInstrumentInfo.Rows.Count; i++)
                {

                    dgvInstrumentInfo.Rows[i].Selected = true;
                }
                return;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void InstrumentYanzheng()
        {
            string sql = null;
            try
            {
                if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.dgvInstrumentInfo.SelectedRows.Count > 0)//选中删除
                    {
                        bool r = false;
                        //选中数量
                        int count = dgvInstrumentInfo.SelectedRows.Count;
                        int cou = 0;
                        for (int i = 0; i < count; i++)
                        {
                            int id = Convert.ToInt32(dgvInstrumentInfo.SelectedRows[i].Cells["Instrument_ID"].Value.ToString());
                            sql = "delete from dbo.InstrumentInfo where Instrument_ID= " + id;
                            //受影响的行数
                            if (LinQBaseDao.ExecuteSql(sql) > 0)
                            {
                                string strContent = "：" + dgvInstrumentInfo.SelectedRows[i].Cells["Instrument_Name"].Value.ToString();
                                LogInfoDAL.loginfoadd("删除", "删除" + strContent + "仪表登记的信息", Common.USERNAME);//添加日志
                                cou++;
                            }
                        }
                        if (cou > 0)
                        {
                            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            BingList();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("请选择要删除的行！");
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        /// <summary>
        /// 清空
        /// </summary>
        public void empty()
        {
            this.txtInstrument_name.Tag = "";
            this.txtInstrument_name.Text = "";
            this.cobInstrument_UserName.SelectedIndex = 0;

        }

        /// <summary>
        /// 修改，为文本框赋值
        /// </summary>
        public void InstrumentUpdate()
        {
            try
            {
                this.btnUpdate.Visible = true;
                this.btnqk.Visible = true;
                this.btnADD.Enabled = false;
                this.btnUpdate.Enabled = true;
                this.btnqk.Enabled = true;
                this.btnADD.Enabled = false;
                if (this.dgvInstrumentInfo.SelectedRows.Count > 0)//选中删除
                {
                    this.txtInstrument_name.Tag = dgvInstrumentInfo.SelectedRows[0].Cells["Instrument_ID"].Value.ToString();
                    this.cob_ClientName.Text = dgvInstrumentInfo.SelectedRows[0].Cells["Client_NAME"].Value.ToString();
                    this.txtInstrument_name.Text = dgvInstrumentInfo.SelectedRows[0].Cells["Instrument_Name"].Value.ToString();
                    this.cobInstrument_UserName.Text = dgvInstrumentInfo.SelectedRows[0].Cells["UserName"].Value.ToString();
                    this.cmbCollection.Text = dgvInstrumentInfo.SelectedRows[0].Cells["Collection_Name"].Value.ToString();
                    this.cmbType.Text =dgvInstrumentInfo.SelectedRows[0].Cells["Instrument_Type"].Value.ToString();

                }
            }
            catch (Exception)
            {

            }
        }

        private void btnqk_Click(object sender, EventArgs e)
        {
            empty();
            btnUpdate.Enabled = false;
            btnADD.Enabled = true;
            btnqk.Enabled = false;
        }
        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtInstrument_name.Text.Trim() == "")
                {
                    MessageBox.Show("修改失败!仪表不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    string sql = "";
                    DataTable dt = SelectName();
                    if (dt.Rows.Count > 0 && dt.Rows[0]["Instrument_ID"].ToString() != this.txtInstrument_name.Tag.ToString())
                    {
                        MessageBox.Show("修改失败,该仪表已存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    sql = "Update InstrumentInfo set Instrument_Collection_ID=" + cmbCollection.SelectedValue + ", Instrument_UserID=" + this.cobInstrument_UserName.SelectedValue + ",Instrument_Name='" + this.txtInstrument_name.Text + "',Instrument_Type='" + cmbType.SelectedIndex + "' where Instrument_ID=" + this.txtInstrument_name.Tag + "";
                    if (LinQBaseDao.ExecuteSql(sql) > 0)
                    {
                        MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string strContent = this.txtInstrument_name.Text;
                        LogInfoDAL.loginfoadd("修改", "修改：" + strContent + "仪表登记的信息", Common.USERNAME);//添加日志
                        BingList();
                        btnUpdate.Enabled = false;
                        btnADD.Enabled = true;
                        btnqk.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("修改失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void cob_ClientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cob_ClientName.SelectedValue != null)
                {
                    //绑定仪表
                    string sql = "select Collection_ID,Collection_Name from dbo.CollectionInfo where Collection_Client_ID='" + cob_ClientName.SelectedValue + "'";
                    DataTable dt = LinQBaseDao.Query(sql).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        cmbCollection.DataSource = dt;
                        cmbCollection.DisplayMember = "Collection_Name";
                        cmbCollection.ValueMember = "Collection_ID";
                        cmbCollection.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

    }
}
