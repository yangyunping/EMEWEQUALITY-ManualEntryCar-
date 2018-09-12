using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq.Expressions;
using EMEWEEntity;
using EMEWEDAL;
using EMEWEQUALITY.HelpClass;
using System.Data.Linq.SqlClient;

namespace EMEWEQUALITY.NewAdd
{
    public partial class CollectionForm : Form
    {
        public CollectionForm()
        {
            InitializeComponent();
        }

        private void CollectionForm_Load(object sender, EventArgs e)
        {
            BindState();
            LoadData("");
        }
        public MainFrom mf;//主窗体
        Expression<Func<View_Collection, bool>> expr = null;
        /// <summary>
        /// 分页控件
        /// </summary>
        PageControl page = new PageControl();

        int cid = 0;
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData(string strName)
        {
            try
            {
                expr = PredicateExtensionses.True<View_Collection>();
                if (txtSCollectionName.Text.Trim() != "")
                {
                    expr = expr.And(n => SqlMethods.Like(n.Collection_Name, "%" + txtSCollectionName.Text.Trim() + "%"));
                }
                if (cmbSClient.Text != "全部" && cmbSClient.Text != "")
                {
                    int did = Convert.ToInt32(cmbSClient.SelectedValue.ToString());
                    expr = expr.And(n => (n.Client_ID == did));
                }
                this.dgvClient.DataSource = null;
                page.PageMaxCount = tscbxPageSize2.SelectedItem.ToString();
                this.dgvClient.AutoGenerateColumns = false;
                dgvClient.DataSource = page.BindBoundControl<View_Collection>(strName, txtCurrentPage2, lblPageCount2, expr);
            }
            catch (Exception ex)
            {
                Common.WriteTextLog("采集端管理 LoadData()" + ex.Message.ToString());
            }
        }


        private void BindState()
        {
            Expression<Func<ClientInfo, bool>> expr = n => n.Client_Dictionary_ID == 2;
            cmbClient.DataSource = ClientDAL.Query(expr);
            this.cmbClient.DisplayMember = "Client_NAME";
            cmbClient.ValueMember = "Client_ID";
            if (cmbClient.DataSource != null)
            {
                cmbClient.SelectedIndex = 0;
            }

            DCQUALITYDataContext dc = new DCQUALITYDataContext();
            List<ClientInfo> client = new List<ClientInfo>();
            client = (from c in dc.ClientInfo where c.Client_Dictionary_ID == 2 select c).ToList();
            //Expression<Func<ClientInfo, bool>> exp = n => n.Client_Dictionary_ID == 2;
            //var client = ClientDAL.Query(exp);
            ClientInfo dic = new ClientInfo();
            dic.Client_ID = 0;
            dic.Client_NAME = "全部";
            client.Insert(0, dic);
            cmbSClient.DataSource = client;
            this.cmbSClient.DisplayMember = "Client_NAME";
            cmbSClient.ValueMember = "Client_ID";
            if (cmbSClient.DataSource != null)
            {
                cmbSClient.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtCollectionName.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(this, "采集端名称不能为空");
                return;
            }
            if (cmbClient.SelectedIndex < 0)
            {
                MessageBox.Show(this, "请选择客户端");
                return;
            }
            if (btnSave.Text == "添加")
            {
                if (ClientDAL.ISClientInfoName(name))
                {
                    MessageBox.Show(this, "采集端名称已存在");
                    return;
                }
                int id = 0;
                CollectionInfo client = new CollectionInfo();
                client.Collection_Name = name;
                client.Collection_Client_ID = Convert.ToInt32(cmbClient.SelectedValue);
                client.Collection_Remark = txtRemark.Text.Trim();

                if (CollectionDAL.InsertOneQCRecord(client))
                {
                    MessageBox.Show("添加成功！");
                    Empty();
                }
                else
                {
                    MessageBox.Show("添加失败！");
                }
            }
            else
            {
                if (cid == 0)
                {
                    return;
                }
                object obj = LinQBaseDao.GetSingle("select count(0) from CollectionInfo where Collection_ID !=" + cid + " and Collection_Name='" + name + "'");
                if (Convert.ToInt32(obj.ToString()) > 0)
                {
                    MessageBox.Show(this, "采集端名称已存在");
                    return;
                }
                Expression<Func<CollectionInfo, bool>> exp = n => n.Collection_ID == cid;
                Action<CollectionInfo> ap = s =>
                {
                    s.Collection_Name = name;
                    s.Collection_Client_ID = Convert.ToInt32(cmbClient.SelectedValue);
                    s.Collection_Remark = txtRemark.Text.Trim();
                };
                CollectionDAL.Update(exp, ap);
                Empty();
            }
            LoadData("");
        }

        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCler_Click(object sender, EventArgs e)
        {
            Empty();
        }
        /// <summary>
        /// 清空
        /// </summary>
        private void Empty()
        {
            txtCollectionName.Text = "";
            txtRemark.Text = "";
            txtSCollectionName.Text = "";
            btnSave.Text = "添加";
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            LoadData("");
        }


        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslSelectAll_Click()
        {
            for (int i = 0; i < dgvClient.Rows.Count; i++)
            {
                dgvClient.Rows[i].Selected = true;
            }
        }
        /// <summary>
        /// 取消全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tslNotSelect_Click()
        {
            for (int i = 0; i < this.dgvClient.Rows.Count; i++)
            {
                dgvClient.Rows[i].Selected = false;
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
            LoadData("");
        }
        //分页菜单响应事件
        private void bdnInfo_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "tslSelectAll") // 全选
            {
                tslSelectAll_Click();
                return;
            }
            if (e.ClickedItem.Name == "tslNotSelect") // 取消全选
            {
                tslNotSelect_Click();
                return;
            }
            // LoadData(e.ClickedItem.Name);
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvClient.SelectedRows.Count > 0)
                {
                    int ID = int.Parse(this.dgvClient.SelectedRows[0].Cells["Collection_ID"].Value.ToString());
                    cid = ID;
                    Expression<Func<View_Collection, bool>> funviewinto = n => n.Collection_ID == ID;
                    foreach (var n in CollectionDAL.Query(funviewinto))
                    {
                        if (n.Client_NAME != null)
                        {
                            this.txtCollectionName.Text = n.Collection_Name;
                        }
                        if (n.Client_Dictionary_ID > 0)
                        {
                            this.cmbClient.SelectedValue = n.Client_ID;
                        }
                        if (n.Client_REMARK != null)
                        {
                            this.txtRemark.Text = n.Collection_Remark;
                        }
                        break;
                    }
                    btnSave.Text = "修改";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
