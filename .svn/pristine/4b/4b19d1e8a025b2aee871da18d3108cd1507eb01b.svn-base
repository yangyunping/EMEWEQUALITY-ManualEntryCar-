using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;


namespace EMEWEQUALITY.HelpClass
{
    public class WeightSerialPort
    {
        public System.IO.Ports.SerialPort serialPort1;
        public System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();//实例化时间控件
        public string newWeight = "";//重量
        public string oldValue = "";
        public int count = 5;//稳定值标准
        public int nowCount = 0;//当前重复次数
        public bool isNewValue = false;
        public StringBuilder m_currentline = new StringBuilder();
        public string platformScaleName = "";

        /// <summary>
        /// 打开端口
        /// </summary>
        /// <param name="baudRate">波特率</param>
        /// <param name="portName">端口</param>
        /// <param name="dataBits">数据位</param>
        /// <returns></returns>
        public bool Open(int baudRate, string portName, int dataBits)
        {
            try
            {
                serialPort1 = new SerialPort();//实例化对象
                serialPort1.BaudRate = baudRate;
                serialPort1.PortName = portName;
                serialPort1.DataBits = dataBits;
                serialPort1.Open();
                timer1.Tick += new EventHandler(timer1_Tick);
                timer1.Interval = 50;
                timer1.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public void timer1_Tick(object sender, EventArgs e)
        {
            switch (platformScaleName)
            {
                case "江苏理文":
                    JSLWWeight();
                    break;
                case "ind221":
                    ind221();
                    break;
                case "jlerqi":
                    erQi();
                    break;
                case "LP7510":
                    LP7510();
                    break;
                case "D2002E":
                    D2002E();
                    break;
                case "Kejie-ORMT-J2000DA+":
                    Kejie_ORMT_J2000DA_Plus();
                    break;
                case "Kejie-ORMT-J2000B":
                    Kejie_ORMT_J2000B();
                    break;
                case "Kejie-XK3198-B":
                    Kejie_XK3198_B();
                    break;
                default:
                    break;
            }
        }
        #region LP7510
        private void LP7510()
        {
            if (serialPort1.BytesToRead > 12)
            {
                string str = "";
                int length = serialPort1.BytesToRead;
                byte[] buffer = new byte[length];
                serialPort1.Read(buffer, 0, length);
                int index = 0;
                for (int i = index; i < buffer.Length; i++)
                {
                    byte num2 = buffer[i];
                    str = Convert.ToString(num2, 0x10);
                    m_currentline.Append(str);
                }
                string bfStr = m_currentline.ToString().ToUpper().Replace(" ", "");//去掉空格 转大写
                int Last2Aindex = bfStr.LastIndexOf("6B67");//最后一个6B67的位置
                int startIndex = 0;

                int twoCounts = 72;//2条完整数据的长度
                int oneCount = 36;//1条完整数据的长度
                int quCount = 14;//截取长度
                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    startIndex = Last2Aindex - oneCount;
                }
                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    string strBff = bfStr.Substring(startIndex - 12, quCount);//截取最后一个6B67后12位字符
                    if (strBff.Length >= quCount)
                    {
                        string num = convert(strBff);
                        if (oldValue != num)
                        {
                            oldValue = num;
                        }
                        else
                        {
                            nowCount++;
                            if (nowCount == count)
                            {
                                isNewValue = true;
                                nowCount = 0;
                            }
                        }
                    }
                }
                //serialPort1.DiscardInBuffer();
            }

        }
        #endregion
        #region 玖龙二期小磅解析
        private void erQi()
        {
            if (serialPort1.BytesToRead > 12)
            {
                string str = "";
                int length = serialPort1.BytesToRead;
                byte[] buffer = new byte[length];
                serialPort1.Read(buffer, 0, length);
                int index = 0;
                for (int i = index; i < buffer.Length; i++)
                {
                    byte num2 = buffer[i];
                    str = Convert.ToString(num2, 0x10);
                    m_currentline.Append(str);
                }
                string bfStr = m_currentline.ToString().ToUpper().Replace(" ", "");//去掉空格 转大写
                int Last2Aindex = bfStr.LastIndexOf("776E");//最后一个776E的位置
                int startIndex = 0;

                int twoCounts = 48;//2条完整数据的长度
                int oneCount = 24;//1条完整数据的长度
                int quCount = 12;//截取长度
                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    startIndex = Last2Aindex - oneCount;
                }
                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    string strBff = bfStr.Substring(startIndex + 4, quCount);//截取最后一个776E后12位字符
                    if (strBff.Length >= quCount)
                    {
                        string num = convert(strBff);
                        if (oldValue != num)
                        {
                            oldValue = num;
                        }
                        else
                        {
                            nowCount++;
                            if (nowCount == count)
                            {
                                isNewValue = true;
                                nowCount = 0;
                            }
                        }
                    }
                }
                //serialPort1.DiscardInBuffer();
            }

        }
        #endregion

        #region 玖龙托利多ind221小磅解析

        private void ind221()
        {
            if (serialPort1.BytesToRead > 12)
            {
                string str = "";
                int length = serialPort1.BytesToRead;
                byte[] buffer = new byte[length];
                serialPort1.Read(buffer, 0, length);
                int index = 0;
                for (int i = index; i < buffer.Length; i++)
                {
                    byte num2 = buffer[i];
                    str = Convert.ToString(num2, 0x10);
                    m_currentline.Append(str);
                }
                string bfStr = m_currentline.ToString().ToUpper().Replace(" ", "");//去掉空格 转大写

                int Last2Aindex = bfStr.LastIndexOf("2A");//最后一个2A的位置
                int startIndex = 0;

                int twoCounts = 68;//2条完整数据的长度
                int oneCount = 34;//1条完整数据的长度
                int quCount = 12;//截取长度
                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    startIndex = Last2Aindex - 22;
                }

                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    string strBff = bfStr.Substring(startIndex, quCount);//截取最后一个2A后12位字符
                    if (strBff.Length >= quCount)
                    {
                        string num = convert(strBff);
                        if (oldValue != num)
                        {
                            oldValue = num;
                        }
                        else
                        {
                            nowCount++;

                            if (nowCount == count)
                            {
                                isNewValue = true;
                                nowCount = 0;
                            }
                        }
                    }
                }
                //serialPort1.DiscardInBuffer();
            }

        }

        #endregion

        #region 宁波柯力D2002E小磅解析

        private void D2002E()
        {
            if (serialPort1.BytesToRead > 12)
            {
                string str = "";
                int length = serialPort1.BytesToRead;
                byte[] buffer = new byte[length];
                serialPort1.Read(buffer, 0, length);
                int index = 0;
                for (int i = index; i < buffer.Length; i++)
                {
                    byte num2 = buffer[i];
                    str = Convert.ToString(num2, 0x10);
                    m_currentline.Append(str);
                }
                string bfStr = m_currentline.ToString().ToUpper().Replace(" ", "");//去掉空格 转大写

                int Last2Aindex = bfStr.LastIndexOf("2E");//最后一个2A的位置
                int startIndex = 0;

                int twoCounts = 68;//2条完整数据的长度
                int oneCount = 34;//1条完整数据的长度
                int quCount = 12;//截取长度
                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    startIndex = Last2Aindex - 14;
                }

                if (bfStr.Length >= twoCounts && Last2Aindex >= oneCount)
                {
                    string strBff = bfStr.Substring(startIndex-2, quCount);//截取最后一个2A后12位字符
                    if (strBff.Length >= quCount)
                    {
                        string num = convertTwo(strBff);
                        if (oldValue != num)
                        {
                            oldValue = num;
                        }
                        else
                        {
                            nowCount++;

                            if (nowCount == count)
                            {
                                isNewValue = true;
                                nowCount = 0;
                            }
                        }
                    }
                }
            }

        }

        #endregion

        /// <summary>
        /// 转换10进制
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string convert(string str)
        {
            char ch2;
            string strTemp = "";
            try
            {
                for (int j = 0; j < str.Length; j += 2)
                {
                    string temp = str.Substring(j, 2);
                    if (temp.Length == 2)
                    {
                        ch2 = (char)Convert.ToInt32(temp, 0x10);
                        strTemp += ch2;
                    }
                }
                return Convert.ToDouble(strTemp).ToString();
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// 转换十进制（江苏理文小磅要排倒序）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string convertTwo(string str)
        {
            char ch2;
            string strTemp = "";
            try
            {
                for (int j = 0; j < str.Length; j += 2)
                {
                    string temp = str.Substring(j, 2);
                    if (temp.Length == 2)
                    {
                        ch2 = (char)Convert.ToInt32(temp, 0x10);
                        strTemp += ch2;
                    }
                }
                //排倒序
                string value = "";
                for (int i = strTemp.Length - 1; i >= 0; i--)
                {
                    value += strTemp[i];
                }
                return Convert.ToDouble(value).ToString();
            }
            catch
            {
                return "";

            }
        }

        #region 江苏理文小磅解析

        private void JSLWWeight()
        {
            if (serialPort1.BytesToRead > 8)
            {

                string str = "";


                int length = serialPort1.BytesToRead;
                byte[] buffer = new byte[length];

                serialPort1.Read(buffer, 0, length);


                int index = 0;

                for (int i = index; i < buffer.Length; i++)
                {
                    byte num2 = buffer[i];
                    str = Convert.ToString(num2, 0x10);
                    m_currentline.Append(str);
                }

                string bfStr = m_currentline.ToString().ToUpper().Replace(" ", "");//去掉空格 转大写
                int Last3Dindex = bfStr.LastIndexOf("3D");//最后一个3D的位置
                int startIndex = 0;
                if (bfStr.Length > 34 && Last3Dindex >= 0)
                {
                    startIndex = Last3Dindex - 16;
                }
                if (bfStr.Length > 34 && Last3Dindex > 16)
                {
                    string strBff = bfStr.Substring(startIndex, 16);//截取最后一个3D前16位字符
                    if (strBff.Length >= 16)
                    {
                        string num = convertTwo(strBff);
                        if (oldValue != num)
                        {
                            oldValue = num;
                        }
                        else
                        {
                            nowCount++;

                            if (nowCount == count)
                            {
                                isNewValue = true;
                                nowCount = 0;
                            }
                        }
                    }
                }
            }
        }

        #endregion


        //江西理文磅解析 2016.4.10添加
        public void Kejie_ORMT_J2000B()
        {
            Kejie_ORMT_J2000DA_Plus();
        }

        public void Kejie_ORMT_J2000DA_Plus()
        {
            string startFlag = "02";
            string endFlag = "03";
            int packBytes = 12;
            int packLength = 36;
            if (serialPort1.BytesToRead >= packBytes)
            {
                int length = serialPort1.BytesToRead;
                byte[] buffer = new byte[length];
                serialPort1.Read(buffer, 0, length);
                for (int i = 0; i < buffer.Length; i++)
                {
                    try
                    {
                        string sValue = Convert.ToString(buffer[i], 0x10);
                        if (sValue.Length == 1)
                            sValue = "0" + sValue;
                        sValue += " ";
                        _comData += sValue;
                    }
                    catch
                    {
                    }
                }
            }
            List<string> validPackageList = new List<string>();
            string remainedInput = "";
            GetValidPackage(_comData, startFlag, endFlag, packLength, ref validPackageList, ref remainedInput);
            _comData = remainedInput;
            Console.WriteLine("_comData.Length:" + _comData.Length.ToString());
            if (validPackageList.Count > 0)
            {
                string sPackage = validPackageList.Last();
                if (GetORMT_J2000DA_Plus_Data(sPackage) != null)
                {
                    oldValue = GetORMT_J2000DA_Plus_Data(sPackage);
                    isNewValue = true;
                }
            }
        }

        public string GetORMT_J2000DA_Plus_Data(string sPackage)
        {
            int dataStartPos = 5;
            int dataLength = 18;
            string sData = sPackage.Substring(dataStartPos, dataLength);
            sData = sData.Replace(" 3", "");
            try
            {
                int data = int.Parse(sData);
                return data.ToString();
            }
            catch
            {
                return null;
            }
        }

        //江西理文磅解析 
        private string _comData = "";
        public void Kejie_XK3198_B()
        {
            string flag = "3d";
            int packLength = 27;
            if (serialPort1.BytesToRead > 9)
            {
                int length = serialPort1.BytesToRead;
                byte[] buffer = new byte[length];
                serialPort1.Read(buffer, 0, length);
                for (int i = 0; i < buffer.Length; i++)
                {
                    try
                    {
                        string sValue = Convert.ToString(buffer[i], 0x10);
                        sValue += " ";
                        _comData += sValue;
                    }
                    catch
                    {
                    }
                }
            }
            List<string> validPackageList = new List<string>();
            string remainedInput = "";
            GetValidPackage(_comData, flag, packLength, ref validPackageList, ref remainedInput);
            _comData = remainedInput;
            //Console.WriteLine("_comData.Length:" + _comData.Length.ToString());
            if (validPackageList.Count > 0)
            {
                string sPackage = validPackageList.Last();
                oldValue = GetKejie_XK3198_BData(sPackage, flag);
                isNewValue = true;
            }
        }

        public string GetKejie_XK3198_BData(string package, string startFlag)
        {
            string sData = package.Substring(startFlag.Length);
            sData = sData.Substring(0, sData.Length - 3); //去除校验位
            sData = ReverseString(sData);       
            sData = sData.Replace("3 ", "");           
            int data = int.Parse(sData);
            return data.ToString();
        }

        private string ReverseString(string original)
        {
            char[] arr = original.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public void GetValidPackage(
            string input,
            string flag,
            int packLength,
            ref List<string> validPackageList,
            ref string remainedInput)
        {
            string fullFlag = flag + " ";
            int startFlagPos = input.IndexOf(fullFlag);
            if (startFlagPos < 0)
            {
                if (input.Length >= packLength)
                    input = "";
                remainedInput = input;
            }
            else
            {
                int packEndPos = startFlagPos + packLength;
                if (input.Length < packEndPos + fullFlag.Length)
                    remainedInput = input;
                else
                {
                    string sGuessEndFlag = input.Substring(packEndPos, fullFlag.Length);
                    if (sGuessEndFlag == fullFlag)
                    {
                        string validPack = input.Substring(startFlagPos, packLength);
                        validPackageList.Add(validPack);
                        input = input.Substring(packEndPos);
                    }
                    else
                    {
                        int newStartPos = input.IndexOf(fullFlag, startFlagPos + fullFlag.Length);
                        input = newStartPos > 0 ? input.Substring(newStartPos) : "";
                        if (input.Length > packLength * 100)
                            input = "";
                    }
                    GetValidPackage(input, flag, packLength, ref validPackageList, ref remainedInput);
                }
            }
        }


        public void GetValidPackage(
            string input,
            string startFlag,
            string endFlag,
            int packLength,
            ref List<string> validPackageList,
            ref string remainedInput)
        {
            string fullStartFlag = startFlag + " ";
            string fullEndFlag = endFlag + " ";
            int startFlagPos = input.IndexOf(fullStartFlag);
            if (startFlagPos < 0)
            {
                if (input.Length >= packLength)
                    input = "";
                remainedInput = input;
            }
            else
            {
                int endFlagPos = startFlagPos + packLength - fullEndFlag.Length;
                if (input.Length < endFlagPos + fullEndFlag.Length)
                    remainedInput = input;
                else
                {
                    string sGuessEndFlag = input.Substring(endFlagPos, fullEndFlag.Length);
                    if (sGuessEndFlag == fullEndFlag)
                    {
                        string validPack = input.Substring(startFlagPos, packLength);
                        validPackageList.Add(validPack);
                        input = input.Substring(endFlagPos + fullEndFlag.Length);
                    }
                    else
                    {
                        int newStartPos = input.IndexOf(fullStartFlag, startFlagPos + fullStartFlag.Length);
                        input = newStartPos > 0 ? input.Substring(newStartPos) : "";
                        if (input.Length > packLength * 100)
                            input = "";
                    }
                    GetValidPackage(input, startFlag, endFlag, packLength, ref validPackageList, ref remainedInput);
                }
            }
        }
        //-----
    }
}
