using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMEWEEntity;
using System.Data;

namespace EMEWEDAL
{
    public class LEDShowDAL
    {
        public static LEDShowEntity GetLEDShow(int iClienID,int iTes_TestItems_ID)
        {
            
            LEDShowEntity lEDShowEntity = null;
            try
            {
                using (DCQUALITYDataContext db = new DCQUALITYDataContext())
                {
                    QCRecord qcrecord = (from c in db.QCRecord 
                                         where c.QCRecord_Client_ID == iClienID && c.QCRecord_ISLEDSHOW == false && c.TestItems.Tes_TestItems_ID == iTes_TestItems_ID && c.Dictionary.Dictionary_Name == "启动" 
                                         select c).First();
                    lEDShowEntity = new LEDShowEntity();
                    lEDShowEntity.CarNum = qcrecord.QCInfo.DRAW_EXAM_INTERFACE.CNTR_NO;//车牌号
                    lEDShowEntity.Grade = qcrecord.QCInfo.DRAW_EXAM_INTERFACE.PROD_ID;//等级货品
                    lEDShowEntity.AvgOne = qcrecord.QCInfo.QCInfo_MOIST_PER_SAMPLE.ToString();//一检平均水分值
                    //qcrecord.QCInfo.QCInfo_UnpackBack_MOIST_PER_SAMPLE.ToString();//拆包后水分平均
                    lEDShowEntity.AvgTwo = qcrecord.QCInfo.QCInfo_UnpackBefore_MOIST_PER_SAMPLE.ToString();//拆包前水分平均
                    lEDShowEntity.Count = qcrecord.QCRecord_QCCOUNT.ToString();//检查序号；
                    lEDShowEntity.Result = qcrecord.QCRecord_RESULT.ToString();//结果
                    //   lEDShowEntity.OneValue = xh + ":" + wate + "%";
                    lEDShowEntity.ID = qcrecord.QCRecord_ID;
                }
            }
            catch (Exception ex){}
            return lEDShowEntity;
        }

        public static bool UpdateQCRecordISLEDSHOW(int iqcrecordId)
        {
            bool rbool = false;
            using (DCQUALITYDataContext db = new DCQUALITYDataContext())
            {
                QCRecord qcrecord = db.QCRecord.First(c => c.QCRecord_ID == iqcrecordId);
                qcrecord.QCRecord_ISLEDSHOW = true;
                db.SubmitChanges();
                rbool = true;
            }
            return rbool;

        }

        //查询视图信息
        public static DataSet LEDShow(string sql)
        {
            return LinQBaseDao.Query(sql);
        }
    }
}
