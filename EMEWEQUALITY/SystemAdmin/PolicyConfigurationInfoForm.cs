using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;

namespace EMEWEQUALITY.SystemAdmin
{
    public partial class PolicyConfigurationInfoForm : Form
    {
        public PolicyConfigurationInfoForm()
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
        private void LingQuBiaoForm_Load(object sender, EventArgs e)
        {
            txtCurrentPage2.Text = "1";
            Bindcob();
            InstrumentList();
        }
        /// <summary>
        /// 绑定下拉列表框
        /// </summary>
        public void Bindcob()
        {
            try
            {
                //绑定客户端
                string sql = " select Client_ID,Client_NAME from dbo.ClientInfo";
                DataTable dt = LinQBaseDao.Query(sql).Tables[0];
                cob_ClientName.DataSource = dt;
                cob_ClientName.DisplayMember = "Client_NAME";
                cob_ClientName.ValueMember = "Client_ID";
                //绑定采集端
                sql = "select Collection_ID,Collection_Name from dbo.CollectionInfo";
                dt = LinQBaseDao.Query(sql).Tables[0];
                cob_Collection_Name.DataSource = dt;
                cob_Collection_Name.DisplayMember = "Collection_Name";
                cob_Collection_Name.ValueMember = "Collection_ID";

            }
            catch (Exception)
            {

            }


        }
        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            string Instrument_ID = Getclbinstrument();
            try
            {
                if (this.cob_ClientName.Text != "" && this.clbInstrument_Name.Text != "" && this.cob_Collection_Name.Text != "" && Instrument_ID != "")
                {
                    string sql = null;
                    //调用判断当前采集端是否被客户端占用的方法
                    if (!GetCollection_id(false))
                    {
                        MessageBox.Show("修改失败,该采集端已被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //判断当前采集端是否被客户端占用或者仪表被占用
                    if (GetInstrument_ID(false))
                    {
                        MessageBox.Show("修改失败,该仪表已被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    sql = "insert into dbo.PolicyConfigurationInfo values('" + this.cob_ClientName.SelectedValue + "','" + this.cob_Collection_Name.SelectedValue + "','" + Instrument_ID + "')";

                    int cou = LinQBaseDao.ExecuteSql(sql);
                    if (cou > 0)
                    {
                        MessageBox.Show("登记成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string strContent = "客户端为：" + this.cob_ClientName.Text;
                        LogInfoDAL.loginfoadd("登记", "登记" + strContent + "的策略配置的信息", Common.USERNAME);//添加日志
                        InstrumentList();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("登记失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("客户端，采集端端，仪表不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            catch (Exception)
            {
            }
            InstrumentList();
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
        public void InstrumentList()
        {
            try
            {
                this.btnUpdate.Visible = false;
                this.btnqk.Visible = false;
                this.btnAdd.Enabled = true;
                int pageNum = Convert.ToInt32(this.txtCurrentPage2.Text);
                int pagesize = Convert.ToInt32(tscbxPageSize2.SelectedItem.ToString());
                string sql = "select top (" + pagesize + ")  PolicyConfiguration_Id,Client_NAME,Collection_Name,Instrument_Name from dbo.PolicyConfigurationInfo join dbo.ClientInfo on PolicyConfiguration_Client_ID=Client_ID join dbo.CollectionInfo on PolicyConfiguration_Collection_ID=Collection_ID join InstrumentInfo on PolicyConfiguration_Instrument_ID=Instrument_ID where PolicyConfiguration_Id not in (select top (" + (pageNum - 1) * pagesize + ") PolicyConfiguration_Id from dbo.PolicyConfigurationInfo join dbo.ClientInfo on PolicyConfiguration_Client_ID=Client_ID join dbo.CollectionInfo on PolicyConfiguration_Collection_ID=Collection_ID join InstrumentInfo on PolicyConfiguration_Instrument_ID=Instrument_ID)";
                dgvPolicyConfigurationInfo.Columns.Clear();
                DataGridViewColumnADD("编号", "PolicyConfiguration_Id", dgvPolicyConfigurationInfo);
                DataGridViewColumnADD("客户端PC", "Client_NAME", dgvPolicyConfigurationInfo);
                DataGridViewColumnADD("采集端", "Collection_Name", dgvPolicyConfigurationInfo);
                DataGridViewColumnADD("仪表", "Instrument_Name", dgvPolicyConfigurationInfo);
                dgvPolicyConfigurationInfo.DataSource = LinQBaseDao.Query(sql).Tables[0];
                dgvPolicyConfigurationInfo.AllowUserToAddRows = false;//隐藏最后空白行
                sql = "select count(*) from dbo.PolicyConfigurationInfo join dbo.ClientInfo on PolicyConfiguration_Client_ID=Client_ID join dbo.CollectionInfo on PolicyConfiguration_Collection_ID=Collection_ID join InstrumentInfo on PolicyConfiguration_Instrument_ID=Instrument_ID";
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
            page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
            InstrumentList();
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Name == "tslHomPage1")//首页
            {
                txtCurrentPage2.Text = "1";
                InstrumentList();
                return;
            }
            if (e.ClickedItem.Name == "tslPreviousPage1")//上一页
            {
                int yeshu = int.Parse(txtCurrentPage2.Text);
                if (yeshu > 1)
                {
                    txtCurrentPage2.Text = (yeshu - 1).ToString();
                }
                else
                {
                    MessageBox.Show("这已经第一页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                InstrumentList();
                return;
            }
            if (e.ClickedItem.Name == "tslNextPage1")//下一页
            {
                int yeshu = int.Parse(txtCurrentPage2.Text);
                if (yeshu < zongyeshu)
                {
                    txtCurrentPage2.Text = (yeshu + 1).ToString();
                }
                else
                {
                    MessageBox.Show("这已经最后页！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                InstrumentList();
                return;
            }
            if (e.ClickedItem.Name == "tslLastPage1")//尾页
            {
                txtCurrentPage2.Text = zongyeshu.ToString();
                InstrumentList();
                return;
            }
            if (e.ClickedItem.Name == "tsbtnUpdate")//修改
            {
                InstrumentUpdate();
                return;
            }
            if (e.ClickedItem.Name == "tsbtnDel")//删除
            {
                DeleteInstrumentdataYanzheng();
                InstrumentList();
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect")//取消全选
            {
                for (int i = 0; i < dgvPolicyConfigurationInfo.Rows.Count; i++)
                {

                    dgvPolicyConfigurationInfo.Rows[i].Selected = false;
                }
                return;
            }
            if (e.ClickedItem.Name == "tslSelectAll")//全选
            {
                for (int i = 0; i < dgvPolicyConfigurationInfo.Rows.Count; i++)
                {

                    dgvPolicyConfigurationInfo.Rows[i].Selected = true;
                }
                return;
            }
        }
        /// <summary>
        /// 清空
        /// </summary>
        public void empty()
        {
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            btnqk.Enabled = false;
            this.cob_ClientName.Tag = "";
            this.cob_ClientName.Text = "";
            this.cob_Collection_Name.Text = "";
            for (int i = 0; i < clbInstrument_Name.Items.Count; i++)
            {
                clbInstrument_Name.SetItemChecked(i, false);
            }
        }
        /// <summary>
        /// 查询客户端是否已配置
        /// </summary>
        /// <returns></returns>
        public bool selectPC()
        {
            int pID = 0;
            if (this.cob_ClientName.Tag != "")
            {
                pID = Convert.ToInt32(this.cob_ClientName.Tag);
            }
            string sql = "select * from dbo.PolicyConfigurationInfo where PolicyConfiguration_Client_ID=" + this.cob_ClientName.SelectedValue + " and PolicyConfiguration_Id!=" + pID;
            DataTable dt = LinQBaseDao.Query(sql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据仪表名称查询编号
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetInstrument_ID(string name)
        {
            string sql = "select Instrument_ID from dbo.InstrumentInfo where Instrument_Name='" + name + "'";
            int id = Convert.ToInt32(LinQBaseDao.Query(sql).Tables[0].Rows[0]["Instrument_ID"].ToString());
            return id;
        }
        /// <summary>
        /// 根据采集端查询策略配置表
        /// </summary>
        /// <returns></returns>
        public bool GetCollection_id(bool isadd)
        {
            try
            {
                string sql = "";
                if (isadd)
                {
                    sql = "select * from dbo.PolicyConfigurationInfo where PolicyConfiguration_Collection_ID=" + this.cob_Collection_Name.SelectedValue;
                }
                else
                {
                    sql = "select * from dbo.PolicyConfigurationInfo where PolicyConfiguration_Collection_ID=" + this.cob_Collection_Name.SelectedValue + " and PolicyConfiguration_Id!=" + this.cob_ClientName.Tag;
                }

                DataTable dt = LinQBaseDao.Query(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        /// <summary>
        /// 取仪表选中项的值
        /// </summary>
        /// <returns></returns>
        public string Getclbinstrument()
        {
            string Instrument_ID = "";
            string a = null;
            for (int i = 0; i < clbInstrument_Name.CheckedItems.Count; i++)
            {
                DataRowView dv = ((DataRowView)clbInstrument_Name.CheckedItems[i]);
                //Instrument_ID += a + GetInstrument_ID(dv["Instrument_ID"].ToString());
                Instrument_ID += a + dv["Instrument_ID"].ToString();
                a = ",";
                break;
            }
            return Instrument_ID;
        }
        /// <summary>
        /// 判断仪表是否正在使用
        /// </summary>
        /// <returns></returns>
        public bool GetInstrument_ID(bool isadd)
        {
            string Instrument_Name = Getclbinstrument();
            string sql = "";
            if (isadd)
            {
                sql = "select * from dbo.PolicyConfigurationInfo";
            }
            else
            {
                sql = "select * from dbo.PolicyConfigurationInfo where PolicyConfiguration_Id!=" + this.cob_ClientName.Tag;
            }
            DataTable dt = LinQBaseDao.Query(sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string piid = dt.Rows[i]["PolicyConfiguration_Instrument_ID"].ToString();
                foreach (string item in Instrument_Name.Split(','))
                {
                    foreach (string iid in piid.Split(','))
                    {
                        if (item == iid)
                        {
                            //被占用
                            return true;
                        }
                    }
                }
            }
            //没有被占用
            return false;
        }
        /// <summary>
        /// 修改,赋值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InstrumentUpdate()
        {
            try
            {
                empty();
                this.btnUpdate.Visible = true;
                btnUpdate.Enabled = true;
                this.btnqk.Visible = true;
                btnqk.Enabled = true;
                this.btnAdd.Enabled = false;
                if (this.dgvPolicyConfigurationInfo.SelectedRows.Count > 0)//选中删除
                {
                    this.cob_ClientName.Tag = dgvPolicyConfigurationInfo.SelectedRows[0].Cells["PolicyConfiguration_Id"].Value.ToString();
                    this.cob_ClientName.Text = dgvPolicyConfigurationInfo.SelectedRows[0].Cells["Client_NAME"].Value.ToString();
                    this.cob_Collection_Name.Text = dgvPolicyConfigurationInfo.SelectedRows[0].Cells["Collection_Name"].Value.ToString();
                    string Instrument = dgvPolicyConfigurationInfo.SelectedRows[0].Cells["Instrument_Name"].Value.ToString();
                    string piid = GetInstrument_ID(Instrument).ToString();
                    foreach (string item in piid.Split(','))
                    {
                        for (int i = 0; i < clbInstrument_Name.Items.Count; i++)
                        {
                            DataRowView dv = ((DataRowView)clbInstrument_Name.Items[i]);
                            string id = dv["Instrument_ID"].ToString();
                            if (id == item)
                            {
                                clbInstrument_Name.SetItemChecked(i, true);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }



        }
        /// <summary>
        /// 删除
        /// </summary>
        public void DeleteInstrumentdataYanzheng()
        {
            string sql = null;
            try
            {
                if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.dgvPolicyConfigurationInfo.SelectedRows.Count > 0)//选中删除
                    {
                        bool r = false;
                        //选中数量
                        int count = dgvPolicyConfigurationInfo.SelectedRows.Count;
                        int cou = 0;
                        for (int i = 0; i < count; i++)
                        {
                            int id = Convert.ToInt32(dgvPolicyConfigurationInfo.SelectedRows[i].Cells["PolicyConfiguration_Id"].Value.ToString());
                            sql = "delete from dbo.PolicyConfigurationInfo where PolicyConfiguration_Id= " + id;
                            //受影响的行数
                            if (LinQBaseDao.ExecuteSql(sql) > 0)
                            {
                                string strContent = "：" + dgvPolicyConfigurationInfo.SelectedRows[i].Cells["PolicyConfiguration_Id"].Value.ToString();
                                LogInfoDAL.loginfoadd("删除", "删除" + strContent + "策略配置登记的信息", Common.USERNAME);//添加日志
                                cou++;
                            }
                        }
                        if (count > 0)
                        {
                            MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

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
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //得到要修改的仪表
            string Instrument_Name = Getclbinstrument();
            try
            {
                if (this.cob_ClientName.Text != "" && this.clbInstrument_Name.Text != "" && this.cob_Collection_Name.Text != "")
                {
                    string sql = null;
                    //调用判断当前采集端是否被客户端占用的方法
                    if (!GetCollection_id(false))
                    {
                        MessageBox.Show("修改失败,该采集端已被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //判断当前采集端是否被客户端占用或者仪表被占用
                    if (GetInstrument_ID(false))
                    {
                        MessageBox.Show("修改失败,该仪表已被占用", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //修改客户端，采集表
                    int id = int.Parse(cob_ClientName.Tag.ToString());
                    sql = "Update dbo.PolicyConfigurationInfo set PolicyConfiguration_Client_ID=" + this.cob_ClientName.SelectedValue + ",PolicyConfiguration_Collection_ID=" + this.cob_Collection_Name.SelectedValue + ", PolicyConfiguration_Instrument_ID='" + Instrument_Name + "' where PolicyConfiguration_Id=" + id;
                    if (LinQBaseDao.ExecuteSql(sql) > 0)
                    {
                        MessageBox.Show("修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        string strContent = "编号为：" + this.cob_ClientName.Tag;
                        LogInfoDAL.loginfoadd("修改", "修改" + strContent + "的策略配置的信息", Common.USERNAME);//添加日志
                        InstrumentList();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("修改失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnqk_Click(object sender, EventArgs e)
        {
            empty();
        }

        private void cob_Collection_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cob_Collection_Name.SelectedValue != null)
                {
                    //绑定仪表
                    string sql = "select Instrument_ID,Instrument_Name from dbo.InstrumentInfo where Instrument_Collection_ID='" + cob_Collection_Name.SelectedValue + "'";
                    DataTable dt = LinQBaseDao.Query(sql).Tables[0];
                    clbInstrument_Name.DataSource = dt;
                    clbInstrument_Name.DisplayMember = "Instrument_Name";
                    clbInstrument_Name.ValueMember = "Instrument_ID";
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
