using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CNNBasedPHM.Extension;

namespace CNNBasedPHM
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page_Monitor : Window
    {
        public static Action staticMoveLeft;
        //SerialHelper arduino;
        public Page_Monitor()
        {
            InitializeComponent();
            /*
            arduino = new SerialHelper(9);
            arduino.HeartBeatDelayTime = 1000;
            arduino.HeartBeatDeadTime = 10000;
            arduino.Start();
            */

            //무조건 반복 UI
            Task.Run(()=> 
            {
                while(true)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process0_1.IsEnabled = true;
                        xml_process0_2.IsEnabled = false;
                        xml_process0_3.IsEnabled = false;
                        xml_process0_4.IsEnabled = false;
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process0_1.IsEnabled = false;
                        xml_process0_2.IsEnabled = true;
                        xml_process0_3.IsEnabled = false;
                        xml_process0_4.IsEnabled = false;
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process0_1.IsEnabled = false;
                        xml_process0_2.IsEnabled = false;
                        xml_process0_3.IsEnabled = true;
                        xml_process0_4.IsEnabled = false;
                    });
                    Thread.Sleep(500);
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process0_1.IsEnabled = false;
                        xml_process0_2.IsEnabled = false;
                        xml_process0_3.IsEnabled = false;
                        xml_process0_4.IsEnabled = true;
                    });
                    Thread.Sleep(500);
                }
            });


            Task.Run(() =>
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        StartRecording(12);

                    });
                    Thread.Sleep(12500);


                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process1_1.IsEnabled = true;
                        xml_process1_2.IsEnabled = true;
                        xml_process1_3.IsEnabled = true;

                    });
                   

                    //CHROMA CQT
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 시작 \n";
                        xml_log.Text += "   추출함수 : CHROMA CQT \n";
                        xml_log.ScrollToEnd();
                        xml_process2_1.IsEnabled = true;
                    });

                    PythonPlugin featureExtractor1 = new PythonPlugin(@"featureExtractor.py --wavePath ./currentWave.wav --function chroma_cqt");

                    if (featureExtractor1.ReadLine() != "complete")
                        continue;


                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process2_1.ElementImage = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "\\currentWave_chroma_cqt.jpg");
                    });
                    

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 종료 \n";
                        xml_log.ScrollToEnd();
                    });


                    //CHROMA STFT
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 시작 \n";
                        xml_log.Text += "   추출함수 : CHROMA STFT \n";
                        xml_log.ScrollToEnd();
                        xml_process2_2.IsEnabled = true;
                    });
                    PythonPlugin featureExtractor2 = new PythonPlugin(@"featureExtractor.py --wavePath ./currentWave.wav --function chroma_stft");

                    if (featureExtractor2.ReadLine() != "complete")
                        continue;

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process2_2.ElementImage = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "\\currentWave_chroma_stft.jpg");
                    });

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 종료 \n";
                        xml_log.ScrollToEnd();
                    });

                    //MFCC
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 시작 \n";
                        xml_log.Text += "   추출함수 : mfcc \n";
                        xml_log.ScrollToEnd();
                        xml_process2_3.IsEnabled = true;
                    });
                    PythonPlugin featureExtractor3 = new PythonPlugin(@"featureExtractor.py --wavePath ./currentWave.wav --function mfcc");

                    if (featureExtractor3.ReadLine() != "complete")
                        continue;

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process2_3.ElementImage = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "\\currentWave_mfcc.jpg");
                    });

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 종료 \n";
                        xml_log.ScrollToEnd();
                    });

                    //Melspectogram
                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 시작 \n";
                        xml_log.Text += "   추출함수 : Melspectrogram \n";
                        xml_log.ScrollToEnd();
                        xml_process2_4.IsEnabled = true;
                    });
                    PythonPlugin featureExtractor4 = new PythonPlugin(@"featureExtractor.py --wavePath ./currentWave.wav --function melspectrogram");

                    if (featureExtractor4.ReadLine() != "complete")
                        continue;

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process2_4.ElementImage = LoadImage(AppDomain.CurrentDomain.BaseDirectory + "\\currentWave_melspectrogram.jpg");
                    });

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   피쳐추출 종료 \n";
                        xml_log.ScrollToEnd();
                    });


                    this.Dispatcher.Invoke(() =>
                    {
                        xml_process1_1.IsEnabled = false;
                        xml_process1_2.IsEnabled = false;
                        xml_process1_3.IsEnabled = false;


                        xml_process3_1.IsEnabled = true;
                        xml_process3_2.IsEnabled = true;

                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   Classifier 초기화 시작 \n";
                        xml_log.ScrollToEnd();
                    });

                    //Classifying
                    PythonPlugin p = new PythonPlugin(@"inceptionv3_classifier.py --imagePath ./currentWave_chroma_cqt.jpg");
                    String result = p.ReadLine();

                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   Classifier 초기화 완료 \n";
                        xml_log.ScrollToEnd();
                    });

                    float foreignmatterValue = float.Parse(result.Parse("foreignmatter:", ","));
                    float normalValue = float.Parse(result.Parse("normal:", ","));
                    float powererrorValue = float.Parse(result.Parse("powererror:", ","));
                    float stoppedValue = float.Parse(result.Parse("stopped:", ","));


                    this.Dispatcher.Invoke(() =>
                    {
                        xml_log.Text += "< "+System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                        xml_log.Text += "   Classification result 반환됨. \n";
                        xml_log.Text += "   정상 작동 Class 확률 : " + normalValue * 100 + "%\n";
                        xml_log.Text += "   이물질 접촉 Class 확률 : " + foreignmatterValue * 100 + "%\n";
                        xml_log.Text += "   전원 이상 Class 확률 : " + powererrorValue * 100 + "%\n";
                        xml_log.Text += "   동작 중지 Class 확률 : " + stoppedValue * 100 + "%\n";
                        xml_log.ScrollToEnd();

                        /*
                        Task.Run(() =>
                        {
                            if (normalValue > foreignmatterValue && normalValue > powererrorValue && normalValue > stoppedValue)
                                arduino.SendData("1");
                            else
                                arduino.SendData("2");
                        });*/


                            xml_foreignmatter.Probability = foreignmatterValue * 100;
                        xml_normal.Probability = normalValue * 100;
                        xml_powererror.Probability = powererrorValue * 100;
                        xml_stopped.Probability = stoppedValue * 100;

                        xml_process2_1.IsEnabled = false;
                        xml_process2_2.IsEnabled = false;
                        xml_process2_3.IsEnabled = false;
                        xml_process2_4.IsEnabled = false;

                        xml_process3_1.IsEnabled = false;
                        xml_process3_2.IsEnabled = false;
                    });
                }
            });



            //PythonPlugin p = new PythonPlugin(@"inceptionv3_retrain.py --image_dir ..\..\..\MachineSounds");
            staticMoveLeft = MoveLeft;
        }

        private BitmapImage LoadImage(string myImageFile)
        {
            BitmapImage myRetVal = null;
            if (myImageFile != null)
            {
                BitmapImage image = new BitmapImage();
                using (FileStream stream = File.OpenRead(myImageFile))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                myRetVal = image;
            }
            return myRetVal;
        }

        void StartRecording(int time)
        {
            //Wave 그래프
            WaveIn wi;
            WaveFileWriter wfw = null;
            Polyline pl = null;
            double canH = 0;
            double canW = 0;
            double plH = 0;
            double plW = 0;
            double seconds = 0;
            List<byte> totalbytes = null;
            Queue<Point> displaypts = null;
            Queue<Int32> displaysht = null;
            long count = 0;
            int numtodisplay = 2205;

            Point Normalize(Int32 x, Int32 y)
            {
                Point p = new Point();


                p.X = 1.0 * x / numtodisplay * plW;
                //p.Y = plH/2.0 - y / (short.MaxValue*1.0) * (plH/2.0);
                p.Y = plH / 2.0 - y / (Int32.MaxValue * 1.0) * (plH / 2.0);
                return p;
            }


            this.Dispatcher.Invoke(() =>
            {
                xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                xml_log.Text += "   오디오 레코더 초기화 \n";
                xml_log.Text += "   비트레이트 : 44100 \n";
                xml_log.Text += "   채널 : Mono \n";
                xml_log.Text += "   파일명 : currentWave.wav \n";
                xml_log.Text += "   길이 : 1200ms \n";
                xml_log.ScrollToEnd();
            });

            wi = new WaveIn();
            wi.DataAvailable += new EventHandler<WaveInEventArgs>((dynamic_sender, dynamic_e) => 
            {
                seconds += (double)(1.0 * dynamic_e.BytesRecorded / wi.WaveFormat.AverageBytesPerSecond * 1.0);
                if (seconds > time)
                {
                    wi.StopRecording();
                }


                wfw.Write(dynamic_e.Buffer, 0, dynamic_e.BytesRecorded);
                totalbytes.AddRange(dynamic_e.Buffer);


                //byte[] shts = new byte[2];
                byte[] shts = new byte[4];


                for (int i = 0; i < dynamic_e.BytesRecorded - 1; i += 500)
                {
                    shts[0] = dynamic_e.Buffer[i];
                    shts[1] = dynamic_e.Buffer[i + 1];
                    shts[2] = dynamic_e.Buffer[i + 2];
                    shts[3] = dynamic_e.Buffer[i + 3];
                    if (count < numtodisplay)
                    {
                        displaysht.Enqueue(BitConverter.ToInt32(shts, 0));
                        ++count;
                    }
                    else
                    {
                        displaysht.Dequeue();
                        displaysht.Enqueue(BitConverter.ToInt32(shts, 0));
                    }
                }
                this.waveCanvas.Children.Clear();
                pl.Points.Clear();
                //short[] shts2 = displaysht.ToArray();
                Int32[] shts2 = displaysht.ToArray();
                for (Int32 x = 0; x < shts2.Length; ++x)
                {
                    pl.Points.Add(Normalize(x, shts2[x]));
                }
                this.waveCanvas.Children.Add(pl);
            });
            wi.RecordingStopped += new EventHandler<StoppedEventArgs>((dynamic_sender,dynamic_e)=> 
            {
                wi.Dispose();
                wi = null;
                wfw.Close();
                wfw.Dispose();
                wfw = null;

                this.Dispatcher.Invoke(() =>
                {
                    xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                    xml_log.Text += "   오디오 레코딩 종료 \n";
                    xml_log.Text += "   currentWave.wav 파일저장 완료 \n";
                    xml_log.ScrollToEnd();
                });
            });
            wi.WaveFormat = new WaveFormat(44100, 1);


            wfw = new WaveFileWriter(@"currentWave.wav", wi.WaveFormat);


            canH = waveCanvas.Height;
            canW = waveCanvas.Width;


            pl = new Polyline();
            pl.Stroke = Brushes.Blue;
            pl.Name = "waveform";
            pl.StrokeThickness = 1;
            pl.MaxHeight = canH - 4;
            pl.MaxWidth = canW - 4;


            plH = pl.MaxHeight;
            plW = pl.MaxWidth;



            displaypts = new Queue<Point>();
            totalbytes = new List<byte>();
            //displaysht = new Queue<short>();
            displaysht = new Queue<Int32>();

            wi.StartRecording();

            this.Dispatcher.Invoke(() =>
            {
                xml_log.Text += "< " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " >\n";
                xml_log.Text += "   오디오 레코딩 시작 \n";
                xml_log.ScrollToEnd();
            });
        }

        //우측이동 애니메이션
        void MoveRight()
        {

            DoubleAnimation MyDouble = new DoubleAnimation();

            MyDouble.To = xml_Scroll.HorizontalOffset + SystemParameters.PrimaryScreenWidth;
            MyDouble.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
            MyDouble.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            Mediator.BeginAnimation(ScrollViewerOffsetMediator.ScrollableWidthMultiplierProperty, MyDouble);
        }
        //좌측이동 애니메이션
        void MoveLeft()
        {
            DoubleAnimation MyDouble = new DoubleAnimation();

            MyDouble.To = xml_Scroll.HorizontalOffset - SystemParameters.PrimaryScreenWidth;
            MyDouble.Duration = new Duration(TimeSpan.FromMilliseconds(1500));
            MyDouble.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
            Mediator.BeginAnimation(ScrollViewerOffsetMediator.ScrollableWidthMultiplierProperty, MyDouble);

        }

        private void xml_toHardware_MouseUp(object sender, MouseButtonEventArgs e)
        {
            xml_hardware.Visibility = Visibility.Visible;
            MoveRight();
        }

        private void xml_toSoftware_MouseUp(object sender, MouseButtonEventArgs e)
        {
            xml_software.Visibility = Visibility.Visible;
            MoveRight();
        }
    }
}
