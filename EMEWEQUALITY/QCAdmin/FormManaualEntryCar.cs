using EMEWEDAL;
using EMEWEEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace EMEWEQUALITY.QCAdmin
{
    public partial class FormManaualEntryCar : Form
    {
        public FormManaualEntryCar()
        {
            InitializeComponent();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtBuMen_Code.Text = "";
            txtchkNum.Text = "";
            txtDuiWei_code.Text = "";
            txtsendNum.Text = "";
            txt_CNTR_NO.Text = "";
            txt_PO_NO.Text = "";
            txt_REF_NO.Text = "";
            txt_SHIPMENT_NO.Text = "";
            txt_WEIGHT_TICKET_NO.Text = "";
            cmb_PROD_ID.Text = "";
        }

        private void btn_CarAdd_Click(object sender, EventArgs e)
        {
            try
            {
                DRAW_EXAM_INTERFACE d = new DRAW_EXAM_INTERFACE();

                QCInfo c = new QCInfo();

                RegisterLoosePaperDistribution r = new RegisterLoosePaperDistribution();

                Packets  p= new Packets();

                if (txt_SHIPMENT_NO.Text != "")
                {
                    d.SHIPMENT_NO = txt_SHIPMENT_NO.Text.Trim();
                }
                else
                {
                    MessageBox.Show("送货单号不能为空");
                    return;
                }
                if (txt_PO_NO.Text != "")
                {
                    d.PO_NO = txt_PO_NO.Text.Trim();
                }
                else
                {
                    MessageBox.Show("采购单号不能为空");
                    return;
                }
                if (cmb_PROD_ID.Text != "")
                {
                    d.PROD_ID = cmb_PROD_ID.Text.Trim();
                }
                else
                {
                    MessageBox.Show("货品不能为空");
                    return;
                }
                if (txt_CNTR_NO.Text != "")
                {
                    d.CNTR_NO = txt_CNTR_NO.Text.Trim();
                }
                else
                {
                    MessageBox.Show("车牌号不能为空");
                    return;
                }

                DataTable dt = LinQBaseDao.Query("select * from Unusualstandard where Unusualstandard_PROD ='"+ cmb_PROD_ID.Text.Trim()+ "' ").Tables[0];
                if (dt != null  && dt.Rows.Count > 0)
                {
                    
                    d.DEGRADE_MATERIAL_PERCT =Convert.ToDecimal( dt.Rows[0]["Unusualstandard_DEGRADE_MATERIAL_PERCT"]);
                    d.DEGRADE_MOISTURE_PERCT = Convert.ToDecimal(dt.Rows[0]["Unusualstandard_DEGRADE_MOISTURE_PERCT"]);
                    d.DEGRADE_OUTTHROWS_PERCT = Convert.ToDecimal(dt.Rows[0]["DEGRADE_OUTTHROWS_PERCT"]);
                }

                //过数表填写数据
                
                d.COMPANY_ID = "9";
                d.CREATE_DTTM = d.WEIGHT_DATE = DateTime.Now;
                d.WEIGHT_TICKET_NO = txt_WEIGHT_TICKET_NO.Text.Trim();
                d.REF_NO = txt_REF_NO.Text.Trim();
                d.NO_OF_BALES =Convert.ToInt32( txtsendNum.Text.Trim());
                d.DRAW_ONE = 0;
                d.DRAW_TWO = 0;
                d.DRAW_THREE = 0;
                d.DRAW_FOUR = 0;
                d.DRAW_FIVE = 0;
                d.DRAW_SIX = 0;
                d.DRAW_7 = 0;
                d.DRAW_8 = 0;
                d.DRAW_9 = 0;
                d.DRAW_10 = 0;
                d.DRAW_11 =0;
                d.DRAW_12 = 0;
                d.DRAW_13 = 0;
                d.DRAW_14 = 0;
                d.IS_FINISHED = "Y";
                d.CREATE_BY = Common.USERNAME; //记录人：当前登录人
                d.IS_DengJi = "Y";
                d.FINISHED_BY = "";
                d.FINISHED_DTTM =Convert.ToDateTime("1900 - 01 - 01 00:00:00");
                d.RECV_RMA_METHOD = "";
                d.TRANS_TO_WPIS_FLAG = "Y";
                d.TRANS_TO_WPIS_DTTM = DateTime.Now;
                d.TRANS_TO_DTS_FLAG = "";
                d.TRANS_TO_DTS_DTTM = Convert.ToDateTime("1900 - 01 - 01 00:00:00");
                //d.IsSource = "手动";

                string[] dtsList = txtchkNum.Text.Split(',');

                for (int i= 0;i<dtsList.Length;i++)
                {
                    switch (i)
                    {
                        case 0:
                            d.DRAW_ONE =Convert.ToInt32( dtsList[i]);
                            break;
                        case 1:
                            d.DRAW_TWO = Convert.ToInt32(dtsList[i]);
                            break;
                        case 2:
                            d.DRAW_THREE = Convert.ToInt32(dtsList[i]);
                            break;
                        case 3:
                            d.DRAW_FOUR = Convert.ToInt32(dtsList[i]);
                            break;
                        case 4:
                            d.DRAW_FIVE = Convert.ToInt32(dtsList[i]);
                            break;
                        case 5:
                            d.DRAW_SIX = Convert.ToInt32(dtsList[i]);
                            break;
                        case 6:
                            d.DRAW_7 = Convert.ToInt32(dtsList[i]);
                            break;
                        case 7:
                            d.DRAW_8 = Convert.ToInt32(dtsList[i]);
                            break;
                        case 8:
                            d.DRAW_9 = Convert.ToInt32(dtsList[i]);
                            break;
                        case 9:
                            d.DRAW_10 = Convert.ToInt32(dtsList[i]);
                            break;
                        case 10:
                            d.DRAW_11 = Convert.ToInt32(dtsList[i]);
                            break;
                        case 11:
                            d.DRAW_12 = Convert.ToInt32(dtsList[i]);
                            break;
                        case 12:
                            d.DRAW_13 = Convert.ToInt32(dtsList[i]);
                            break;
                        case 13:
                            d.DRAW_14 = Convert.ToInt32(dtsList[i]);
                            break;
                    }
                }

                int result = 0;
                DRAW_EXAM_INTERFACEDAL.InsertOneCarInfo(d, out result);
                if (result > 0)
                {
                    //MessageBox.Show("添加成功！");
                    //this.Close();
                    //  LoadData();

                    //查询新增过数ID

                    //质检登记成功填写数据
                    int result2 = 0;
                    r.OrganizationID = "ChongQingPaper";
                    r.R_DRAW_EXAM_INTERFACE_ID = result;
                    r.DepartmentCode = txtBuMen_Code.Text.Trim();
                    r.ExtensionField2 = txtDuiWei_code.Text.Trim();
                    r.issend = false;
                    RegisterLoosePaperDistributionDAL.InsertOneCarInfo(r, out result2);

                    //质检表填写数据
                    int result3 = 0;
                    c.QCInfo_Dictionary_ID = 8;
                    c.QCInfo_STATE = 2;
                    c.QCInfo_DEGREE = 1;
                    c.QCInfo_UnpackBefore_MOIST_PER_COUNT = 0;
                    c.QCInfo_UnpackBack_MOIST_COUNT = 0;
                    c.QCInfo_TIME = DateTime.Now;//日期时间
                    c.QCInfo_Client_ID = Common.CLIENTID;//客户端配置编号
                    c.QCInfo_UserId = EMEWE.Common.Converter.ToInt(Common.USERID); //记录人：当前登录人
                    c.QCInfo_DRAW_EXAM_INTERFACE_ID = result;
                    c.QCInfo_PumpingPackets = dtsList.Length;
                    c.QCInfo_DRAW = txtchkNum.Text.Trim();

                    if (dtsList.Length <= 4)
                    {
                        c.QCInfo_MOIST_Count = dtsList.Length * 8;
                    }
                    else
                    {
                        c.QCInfo_MOIST_Count = 32;
                    }

                    QCInfoDAL.InsertOneCarInfo(c, out result3);

                    if (result > 0)
                    {
                        MessageBox.Show("添加成功！");

                        int result4 = 0;
                        p.Packets_DTS = txtchkNum.Text.Trim();
                        p.Packets_this = "1";
                        p.Packets_QCInfo_DRAW_EXAM_INTERFACE_ID = result;
                        p.Packets_Time = DateTime.Now;
                        PacketsDAL.InsertOneCarInfo(p,out  result4);

                        this.Close();
                        //  LoadData();
                    }


                }

            }
            catch (Exception err)
            {
            }


        }

        private void BindCBX()
        {
            DataTable dt = LinQBaseDao.Query("select Unusualstandard_PROD from Unusualstandard").Tables[0];
            DataRow dr = dt.NewRow();
            dr["Unusualstandard_PROD"] = "";
            dt.Rows.InsertAt(dr, 0);
            cmb_PROD_ID.DataSource = dt;
            cmb_PROD_ID.DisplayMember = "Unusualstandard_PROD";
            cmb_PROD_ID.ValueMember = "Unusualstandard_PROD";
          

        }

        private void FormManaualEntryCar_Load(object sender, EventArgs e)
        {
            BindCBX();
        }
    }
}
