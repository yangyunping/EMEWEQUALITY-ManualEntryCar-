using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Timers;

namespace EMEWEQUALITY
{
    public class MoistureMeter
    {

        bool State = false;
        SerialPort sp = new SerialPort();
        System.Timers.Timer aTimer = new System.Timers.Timer(100); //实例化Timer类，设置间隔时间为100毫秒；
        /// <summary>
        /// 次数
        /// </summary>
        public int count = -1;
        /// <summary>
        /// 水分值
        /// </summary>
        public string Water = "";

        /// <summary>
        /// 串口练级状态
        /// </summary>
        public bool ISOpen = false;

        int Portname = 0;
        int Baudrate = 0;
        StopBits Stopbits;
        int Databits = 0;
        Parity Parity;
        /// <summary>
        /// 检查串口是否可用
        /// </summary>
        /// <param name="strcom">串口号</param>
        /// <returns></returns>
        public bool ISCheckCom(int port)
        {
            bool istrue = false;
            SerialPort spt = new SerialPort("COM" + port);
            try
            {
                spt.Open();
                istrue = true;
            }
            catch (Exception ex)
            {
                istrue = false;
                string str = ex.Message.ToString();
                if (str.IndexOf("不存在") >= 0)
                {
                    State = false;
                }
            }
            finally
            {
                spt.Close();
            }
            return istrue;
        }

        /// <summary>
        /// 打开串口成功返回True，失败返回False
        /// </summary>
        /// <param name="port">串口号</param>
        /// <param name="baudrate">波特率:300，600，1200,2400,4800,9600,19200</param>
        /// <param name="stopbits">停止位 0： StopBits.None，1： StopBits.One，1.5： StopBits.OnePointFive，2： StopBits.Two</param>
        /// <param name="databits">数据位 5,6,7,8</param>
        /// <param name="parity">奇偶校验 无：Parity.None，奇校验：Parity.Odd，偶校验：Parity.Even </param>
        /// <returns></returns>
        public void OpenCom(int port, int baudrate, StopBits stopbits, int databits, Parity parity)
        {
            Portname = port;
            Baudrate = baudrate;
            Stopbits = stopbits;
            Databits = databits;
            Parity = parity;
            try
            {
                if (OpenComS(port, baudrate, stopbits, databits, parity))
                {
                    ISOpen = true;
                }
                else
                {
                    ISOpen = false;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                times();
            }
        }
        private bool OpenComS(int port, int baudrate, StopBits stopbits, int databits, Parity parity)
        {
            sp.PortName = "COM" + port;
            sp.BaudRate = baudrate;
            sp.StopBits = stopbits;
            sp.DataBits = databits;
            sp.Parity = parity;
            sp.ReadTimeout = -1;
            try
            {
                State = true;
                if (ISCheckCom(port))
                {
                    sp.Open();
                    sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (sp.BytesToRead > 0)
                System.Threading.Thread.Sleep(100);//添加的延时
            serial();
            sp.DiscardInBuffer();
        }

        /// <summary>
        /// 解析水分值
        /// </summary>
        private void serial()
        {
            try
            {
                if (State)
                {
                    string sixvalue = "";
                    string tenvalue = "";
                    string strreceive = sp.ReadExisting();
                    if (!string.IsNullOrEmpty(strreceive))
                    {
                        UTF8Encoding utf8 = new UTF8Encoding();
                        sixvalue = StringToHexString(strreceive, utf8);
                        tenvalue = HexStr2Dec(sixvalue);
                        string wa = "";
                        int counts = 0;
                        string[] st = tenvalue.Split(' ');
                        if (st.Length == 14)
                        {
                            wa = "";//水分值
                            counts = int.Parse(st[1]);//检测次数
                            if (st[8] != "10")
                            {
                                wa += st[8];
                            }
                            if (st[9] != "10")
                            {
                                wa += st[9];
                            }
                            else
                            {
                                wa += "0";
                            }
                            if (st[10] != "10")
                            {
                                wa += "." + st[10];

                            }
                            else
                            {
                                wa += ".0";
                            }
                            count = counts;
                            Water = wa;

                        }
                        else
                        {
                            wa = "";//水分值
                            Water = wa;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 转十六进制
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        private string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += Convert.ToString(b[i], 16) + " ";
            }
            return result;
        }
        /// <summary>   
        /// 十六进制字符串转换为十进制数字  
        /// </summary>     
        /// /// <param name="hexStr"></param>  
        /// /// <returns></returns>      
        private string HexStr2Dec(string hexStr)
        {
            hexStr = hexStr.TrimEnd(' ');
            string[] hexCharList = hexStr.Split(' ');
            string result = "";
            foreach (string hex in hexCharList)
            {
                //int intA1 = Convert.ToInt32(hex);
                int intA2 = Convert.ToInt32(hex, 16);
                result += intA2.ToString() + " ";
            }
            return result;
        }


        /// <summary>
        /// 判断串口是否连接，未连接则尝试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (State)
            {
                try
                {
                    ISOpen = sp.CtsHolding;
                    if (!ISOpen)
                    {
                        sp.Close();
                        if (ISCheckCom(Portname))
                        {
                            OpenComS(Portname, Baudrate, Stopbits, Databits, Parity);
                        }
                    }
                }
                catch (Exception)
                {
                    if (sp.IsOpen)
                    {
                        sp.Close();
                    }
                    if (ISCheckCom(Portname))
                    {
                        OpenComS(Portname, Baudrate, Stopbits, Databits, Parity);
                    }
                }
            }
        }

        private void times()
        {
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent); //到达时间的时候执行事件
            aTimer.AutoReset = true; //设置是执行一次（false）还是一直执行(true)；
            aTimer.Enabled = true; //是否执行System.Timers.Timer.Elapsed事件；
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            sp.Close();
            State = false;
            ISOpen = false;
        }
    }
}
