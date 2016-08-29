using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;  // 레지스트리 관련 함수사용
using System.Runtime.InteropServices;   // 시스템 API 호출 시 필요
using System.IO;    // 시스템 입출력


namespace RegTest1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region [함수] INI 파일 읽기 함수(섹션설정)
        public string[] GetIniValue(string Section, string path)
        {
            byte[] ba = new byte[5000];
            uint Flag = GetPrivateProfileSection(Section, ba, 5000, path);
            return Encoding.Default.GetString(ba).Split(new char[1] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region [함수] INI 파일 읽기 함수(섹션,키값설정)
        public string GetIniValue(string Section, string key, string path)
        {
            StringBuilder sb = new StringBuilder(500);
            int Flag = GetPrivateProfileString(Section, key, "", sb, 500, path);
            return sb.ToString();
        }
        #endregion

        #region [함수] INI 파일 쓰기 함수(섹션, 키값설정, 값, 저장 주소)
        public bool SetIniValue(string Section, string Key, string Value, string path)
        {
            return (WritePrivateProfileString(Section, Key, Value, path));
        }
        #endregion

        #region [DLL 함수] INI DLL 로드
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32")]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32")]
        public static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, string lpDefault, string lpFileName);
        [DllImport("kernel32.dll")]
        public static extern uint GetPrivateProfileSection(string lpAppName, byte[] lpPairValues, uint nSize, string lpFileName);
        [DllImport("kernel32.dll")]
        public static extern uint GetPrivateProfileSectionNames(byte[] lpSections, uint nSize, string lpFileName);
        #endregion

        #region 프로그램 날짜 얻어오기
        public void GetSystemDate(out string outTime)
        {
            outTime = string.Format(DateTime.Now.ToString("yyyy.MM.dd"));
        }
        #endregion

        #region 프로그램 날짜 및 시간 얻어오기
        public void GetSystemTime(out string outTime)
        {
            outTime = string.Format("[" + DateTime.Now.ToString("yyyy.MM.dd") + "-" + DateTime.Now.ToString("HH:mm:ss") + "]");
        }
        #endregion

        #region listbox1 에 log 출력 및 log 저장
        #region 크로스스레드 에러를 막기 위해 작업
        delegate void CrossThreadSafetySetLogMessage(string inMessage);

        public void CrossThreadSetLogMessage(string inMessage)
        {
            this.Invoke(new CrossThreadSafetySetLogMessage(SetLogMessage), inMessage);
        }
        #endregion

        public void SetLogMessage(string inMessage)
        {
            string LogMessage = "";
            string strTime = "";
            GetSystemTime(out strTime);

            // 입력받은 문자에 날짜와 시간을 붙여서 출력
            LogMessage = string.Format(strTime.ToString() + inMessage.ToString());
            RegLogViewer.Items.Add(LogMessage);

            // 리스트박스는 맨 마지막줄을 항상 선택
            int index = RegLogViewer.Items.Count;
            RegLogViewer.SelectedIndex = index - 1;

            // 리스트박스 버퍼가 1000줄이 넘으면 가장 오래된 로그를 지운다.
            if (RegLogViewer.Items.Count > 1000)
            {
                RegLogViewer.Items.RemoveAt(0);
            }

            // 검사 결과를 로그에 추가
            SaveLogFile(LogMessage);

        }
        #endregion

        #region LogData 저장
        public void SaveLogFile(string inLogMessage)
        {
            // 로그 데이터 파일에 들어간 날짜 얻어오기
            string strDate;
            GetSystemDate(out strDate);

            // 로그 데이터가 저장될 폴더와 파일명 설정
            string FilePath = string.Format("D:\\Test" + "\\" + strDate + "_Log.xlsx");
            FileInfo fi = new FileInfo(FilePath);

            // 폴더가 존재하는지 확인하고 존재하지 않으면 폴더부터 생성
            DirectoryInfo dir = new DirectoryInfo("D:\\Test");

            if (dir.Exists == false)
            {
                // 새로 생성합니다.
                dir.Create();
            }

            // 기존 로그 데이터가 존재시 이어쓰고 아니면 새로 생성
            try
            {
                if (fi.Exists != true)
                {
                    using (StreamWriter sw = new StreamWriter(FilePath))
                    {
                        sw.WriteLine(inLogMessage);
                        sw.Close();
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(FilePath))
                    {
                        sw.WriteLine(inLogMessage);
                        sw.Close();
                    }
                }
            }
            catch (Exception e)
            {
                CrossThreadSetLogMessage("로그 저장 실패");
            }

        }
        #endregion

        private void ClassID_Click(object sender, EventArgs e)
        {
            RegLogViewer.Items.Clear();
            // registryKey 값에 최하위 키값 상위 경로 지정
            String[] registryKey = 
                {
                    /*0*/ @"SYSTEM\ControlSet001\Control\DeviceClasses\{10497B1B-BA51-44E5-8318-A65C837B6661}",
                    /*1*/ @"SYSTEM\ControlSet001\Control\DeviceClasses\{53F56307-B6BF-11D0-94F2-00A0C91EFB8B}",
                    /*2*/ @"SYSTEM\ControlSet001\Control\DeviceClasses\{53F5630D-B6BF-11D0-94F2-00A0C91EFB8B}",
                    /*3*/ @"SYSTEM\ControlSet001\Control\DeviceClasses\{6AC27878-A6FA-4155-BA85-F98F491D4F33}",
                    /*4*/ @"SYSTEM\ControlSet001\Enum\USBSTOR",
                    /*5*/ @"SOFTWARE\Microsoft\Windows Portable Devices\Devices",
                    // @"SYSTEM\ControlSet001\Enum\WpdBusEnumRoot\UMB",
                    // @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\EMDMgmt",
                };
            // Win32 API를 사용해 RegistryKey 값에 접근 최하위 키값 상위 경로까지
            for (int i = 0; i < registryKey.Length; i++)
            {
                String[] valCount = {
                    /*0*/"DeviceInstance",
                    /*1*/"DeviceInstance",
                    /*2*/"DeviceInstance",
                    /*3*/"DeviceInstance",
                    /*4*/"",
                    /*5*/"FriendlyName"
                };
                using (Microsoft.Win32.RegistryKey Key = Registry.LocalMachine.OpenSubKey(registryKey[i]))
                {
                    // foreach문 최하위 키값의 상위 경로에서 최하위 키값의 이름을 subkeyName에 저장
                    foreach (String subkeyName in Key.GetSubKeyNames())
                    {
                        // 서브키 값 ListBox에 출력
                        RegLogViewer.Items.Add("SubKey : " + subkeyName);
                        if (i == 4) goto EXIT;
                        // 저장된 최하위 키값 경로까지 접근
                        RegistryKey Val = Key.OpenSubKey(subkeyName);

                        // GetValue로 ValueName값 얻어 ListBox에 출력
                        string valName = Val.GetValue(valCount[i]).ToString();

                        RegLogViewer.Items.Add("Value : " + valName);
                        RegLogViewer.Items.Add("\n");

                        EXIT:;
                    }

                }
            }
        }   //완료

        private void InstanceID_Click(object sender, EventArgs e)
        {
            RegLogViewer.Items.Clear();
            // registryKey 값에 최하위 키값 상위 경로 지정
            String[] registryKey = {
               /*0*/ 
               /*1*/ 
               /*2*/ 
               /*3*/ 
               /*4*/ 
               /*5*/ 

               
            };
            // Win32 API를 사용해 RegistryKey 값에 접근 최하위 키값 상위 경로까지
            for (int i = 0; i < registryKey.Length; i++)
            {
                String[] valCount = {
                    /*0*/
                    /*1*/
                    /*2*/
                    /*3*/
                    /*4*/
                    /*5*/


                };
                using (Microsoft.Win32.RegistryKey Key = Registry.LocalMachine.OpenSubKey(registryKey[i]))
                {

                    // foreach문 최하위 키값의 상위 경로에서 최하위 키값의 이름을 subkeyName에 저장
                    foreach (String subkeyName in Key.GetSubKeyNames())
                    {
                        // 서브키 값 ListBox에 출력
                        // 저장된 최하위 키값 경로까지 접근
                        RegistryKey Val = Key.OpenSubKey(subkeyName);

                        // GetValue로 ValueName값 얻어 ListBox에 출력
                        string valName = Val.GetValue(valCount[i]).ToString();

                        RegLogViewer.Items.Add("Value : " + valName);
                        RegLogViewer.Items.Add("\n");

                    EXIT:;

                    }

                }
            }
        }

        private void VIPI_Click(object sender, EventArgs e)
        {
            RegLogViewer.Items.Clear();
            // registryKey 값에 최하위 키값 상위 경로 지정
            String registryKey = @"SYSTEM\ControlSet001\Enum\USB";

            // Win32 API를 사용해 RegistryKey 값에 접근 최하위 키값 상위 경로까지
                using (Microsoft.Win32.RegistryKey Key = Registry.LocalMachine.OpenSubKey(registryKey))
                {
                    // foreach문 최하위 키값의 상위 경로에서 최하위 키값의 이름을 subkeyName에 저장
                    foreach (String subkeyName in Key.GetSubKeyNames())
                    {
                        // 서브키 값 ListBox에 출력
                        RegLogViewer.Items.Add("SubKey : " + subkeyName);

                    }

                }
        }   //완료

        private void USERNAME_Click(object sender, EventArgs e)
        {
            RegLogViewer.Items.Clear();
            // registryKey 값에 최하위 키값 상위 경로 지정
            String registryKey = @"SYSTEM\MountedDevices";
            // Win32 API를 사용해 RegistryKey 값에 접근 최하위 키값 상위 경로까지
            using (Microsoft.Win32.RegistryKey Key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                var valueName = Key.GetValueNames();
                for (int i = 0; i < valueName.Length; i++)
                {
                    var valueKind = Key.GetValueKind(valueName[i]);
                    if (valueKind == RegistryValueKind.Binary)
                    {
                        var value = (byte[])Key.GetValue(valueName[i]);
                        var valueAsString = BitConverter.ToString(value);
                        RegLogViewer.Items.Add("Value :" + valueAsString);
                    }
                }
            }
        }//완료

        private void ConnectT_Click(object sender, EventArgs e)
        {
            RegLogViewer.Items.Clear();
            // registryKey 값에 최하위 키값 상위 경로 지정
            String[] registryKey = 
                {
                    /*1*/ @"SYSTEM\ControlSet001\Control\DeviceClasses\{10497B1B-BA51-44E5-8318-A65C837B6661}",
                    /*2*/ @"SOFTWARE\Microsoft\Windows Portable Devices\Devices"
                    // @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\EMDMgmt",
                };
            // Win32 API를 사용해 RegistryKey 값에 접근 최하위 키값 상위 경로까지
            for (int i = 0; i < registryKey.Length; i++)
            {
                String[] valCount = {
                    /*0*/"DeviceInstance",
                    /*2*/"FriendlyName"
                };
                using (Microsoft.Win32.RegistryKey Key = Registry.LocalMachine.OpenSubKey(registryKey[i]))
                {

                    // foreach문 최하위 키값의 상위 경로에서 최하위 키값의 이름을 subkeyName에 저장
                    foreach (String subkeyName in Key.GetSubKeyNames())
                    {
                        // 서브키 값 ListBox에 출력
                        RegLogViewer.Items.Add("SubKey : " + subkeyName);
                        // 저장된 최하위 키값 경로까지 접근
                        RegistryKey Val = Key.OpenSubKey(subkeyName);

                        // GetValue로 ValueName값 얻어 ListBox에 출력
                        string valName = Val.GetValue(valCount[i]).ToString();

                        RegLogViewer.Items.Add("Value : " + valName);
                        RegLogViewer.Items.Add("\n");
                    }

                }
            }
        }   //완료


    }
}
