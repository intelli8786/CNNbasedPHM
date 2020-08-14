using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CNNBasedPHM
{
    class SerialHelper
    {
        private SerialPort m_spDevice;
        private Stopwatch m_swHeartBeat;
        private List<CallBackContainer> m_CBCList;
        private int m_HeartBeatDelayTime;
        private int m_HeartBeatDeadTime;
        private int m_ReadTimeout;
        private bool m_bIsRunning;


        public int HeartBeatDelayTime
        {
            get
            {
                return m_HeartBeatDelayTime;
            }
            set
            {
                m_HeartBeatDelayTime = value;
            }
        }

        public int HeartBeatDeadTime
        {
            get
            {
                return m_HeartBeatDeadTime;
            }
            set
            {
                m_HeartBeatDeadTime = value;
            }
        }
        public int ReadTimeout
        {
            get
            {
                return m_ReadTimeout;
            }
            set
            {
                m_ReadTimeout = value;
            }
        }

        public SerialHelper(int m_intPort)
        {
            //변수 초기화
            m_HeartBeatDelayTime = 2000;
            m_HeartBeatDeadTime = 60000;
            m_ReadTimeout = 1000;

            //콜백리스트 초기화
            m_CBCList = new List<CallBackContainer>();

            new Thread(new ThreadStart(() =>
            {
                String m_strPort = "COM" + m_intPort;
                if (System.IO.Ports.SerialPort.GetPortNames().Contains(m_strPort))
                {
                    //장치정보 초기화
                    m_spDevice = new SerialPort();
                    m_spDevice.PortName = m_strPort;
                    m_spDevice.BaudRate = (int)9600;
                    m_spDevice.ReadTimeout = m_ReadTimeout;

                    //최소연결시간제한 초기화
                    m_swHeartBeat = new Stopwatch();
                }
                else
                {
                    MessageBox.Show(m_strPort + "장치가 존재하지 않습니다.");
                }
            })).Start();
        }

        public void Start()
        {
            m_bIsRunning = true;

            //장치 연결
            Connect();

            //최소연결시간 체크 시작
            m_swHeartBeat.Start();

            //하트비트 전송
            new Thread(new ThreadStart(() =>
            {
                while (m_bIsRunning)
                {
                    Thread.Sleep(m_HeartBeatDelayTime);
                    SendData("H");
                }
            })).Start();

            //메시지 수신
            new Thread(new ThreadStart(() =>
            {
                while (m_bIsRunning)
                {
                    Thread.Sleep(100);
                    String Packet = ReceiveData();
                    if (Packet != null) //정상 리시브
                    {
                        m_swHeartBeat.Restart();

                        if (Packet.Split(':').Length < 2)
                            MessageBox.Show("시리얼 패킷 에러");

                        String m_strType = Packet.Split(':')[0];
                        String m_strValue = Packet.Split(':')[1];

                        foreach (CallBackContainer m_CBCTemp in m_CBCList)
                        {
                            if (m_CBCTemp.type.Equals(m_strType))
                                m_CBCTemp.action(m_strValue);
                        }
                    }

                    if (m_swHeartBeat.ElapsedMilliseconds > m_HeartBeatDeadTime) //연결 끊김
                    {
                        this.Connect();
                        m_swHeartBeat.Restart();
                    }
                }
            })).Start();
        }

        public void AddCommand(String type, Action<String> m_fAction)
        {
            //람다식 콜백 등록
            m_CBCList.Add(new CallBackContainer(type, m_fAction));
        }

        public void Stop()
        {
            m_bIsRunning = false;
            DisConnect();
        }


        private String ReceiveData()
        {
            if (m_spDevice.IsOpen)
            {
                try
                {
                    return m_spDevice.ReadLine();
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void SendData(String m_strMessage)
        {
            if (m_spDevice.IsOpen)
            {
                try
                {
                    m_spDevice.Write(m_strMessage);
                }
                catch
                {
                    return;
                }
            }
        }


        public void Connect()
        {
            try
            {
                if (m_spDevice.IsOpen)
                    m_spDevice.Close();

                m_spDevice.Open();
            }
            catch
            {
            }
        }

        public void DisConnect()
        {
            if (m_spDevice.IsOpen)
                m_spDevice.Close();
        }
    }

    class CallBackContainer
    {
        public String type;
        public Action<String> action;

        public CallBackContainer(String type, Action<String> action)
        {
            this.type = type;
            this.action = action;
        }
    }
}
