using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.BassAsio;
using Declarations.Events;
using Declarations.Media;
using Declarations.Players;
using Implementation;
using System.Runtime.InteropServices;


namespace Kategoryzacja_Eksper
{
    public partial class Ord_Audio_Form : Form
    {
        public Stopwatch stoper= new Stopwatch();
        public long tick=0;
        public long delta_tick=0;
        public long last_frame=0;
        public long stoper_freq=0;
        OrdInputFile plik_wejsciowy;
        
        Int32 szer=520;
        Int32 wys=404;
        int urzadzenie = -1;
        int current_sample;
        public double audio_length;
        public List<int> audio_stoper_lengths;
        string plik;
        bool dodaj_date;
        string directory;
        bool binaural = false;
        Int16 first_ch;
        List<string> odpowiedzi;
        Int32 ile_w_wierszu;

        //niezniszczalny uchwyt i bufor ;)
        private List<GCHandle> _hGCFiles;
        List<byte[]> audio_buffers;
        List<long> lengths;

        public Un4seen.BassAsio.ASIOPROC _myAsioProc = new Un4seen.BassAsio.ASIOPROC(AsioCallback);
        
        private static int AsioCallback(bool input, int channel, IntPtr buffer, int length, IntPtr user)
        {
            // Note: 'user' contains the underlying stream channel (see above) 
            // We can simply use the bass method to get some data from a decoding channel  
            // and store it to the asio buffer in the same moment... 
            // :(
            
            return Bass.BASS_ChannelGetData(user.ToInt32(), buffer, length);
        }


        //konstruktor
        public Ord_Audio_Form()
        {
            InitializeComponent();
            this.Left=Screen.AllScreens[0].WorkingArea.Left;
            this.Top = Screen.AllScreens[0].WorkingArea.Top;
            this.Width = szer;
            this.Height = wys;
            SetupPanel.Visible = true;
            ResponsePanel.Visible = false;
          
           
            BassNet.Registration("p.goralski@10g.pl", "2X253829171365");
            int liczba = Un4seen.BassAsio.BassAsio.BASS_ASIO_GetDeviceCount();
            for (int i=0; i<liczba;i++)
                {
                    listDevices.Items.Add(Un4seen.BassAsio.BassAsio.BASS_ASIO_GetDeviceInfo(i).name); 
                }
            try
            {
                listDevices.SelectedIndex = 0;
            }
            catch { }
            directory = Application.StartupPath;
           
            tBCD.Text = directory;
            stoper_freq = Stopwatch.Frequency;
            delta_tick = stoper_freq / 25;
            Visual_Setup();
            

        }
        #region wczytanie pliku wejsciowego i pliku z dostępnymi odpowiedziami
        private void bLoadInputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog oInputFileDlg = new OpenFileDialog();
            oInputFileDlg.Filter = "Text Files (.txt)|*.txt";
            oInputFileDlg.FilterIndex = 0;
            oInputFileDlg.Multiselect = false;
            oInputFileDlg.InitialDirectory = @"\input";

            if (oInputFileDlg.ShowDialog()==DialogResult.OK)
            {
                plik_wejsciowy = new OrdInputFile(oInputFileDlg.FileName);
                tInputFile.Text = oInputFileDlg.FileName;
                numStartSample.Minimum = 0;
                numStartSample.Maximum = plik_wejsciowy.Num_samples - 1;
                numStartSample.Value = 0;
            }
        }

        private void bLoadAnsFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog oInputFileDlg = new OpenFileDialog();
            oInputFileDlg.Filter = "Text Files (.txt)|*.txt";
            oInputFileDlg.FilterIndex = 0;
            oInputFileDlg.Multiselect = false;
            oInputFileDlg.InitialDirectory = @"\input";
            if (oInputFileDlg.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(oInputFileDlg.FileName))
                {
                    int max = Convert.ToInt32(sr.ReadLine());
                    ile_w_wierszu = Convert.ToInt32(sr.ReadLine());
                    for (int i = 0; i < max; i++)
                    {
                        odpowiedzi.Add(sr.ReadLine());
                    }
                    TAnsFile.Text = oInputFileDlg.FileName;
                }
            }
        }
        #endregion

        //początek eksperymentu
        private void bExperStart_Click(object sender, EventArgs e)
        {
            plik = VarOutFile.CreateFile(tName.Text, tSurname.Text, tAge.Text,tBCD.Text);
            lInstructions.Text = plik_wejsciowy.Question;
            Visual_Presenting_Sample();
            Dialog dlg = new Dialog(plik_wejsciowy.Instructions);
            dlg.ShowDialog();
            //wybor urzadzenia ASIO
            int liczba = Un4seen.BassAsio.BassAsio.BASS_ASIO_GetDeviceCount();
            urzadzenie = -1;
            for (int i = 0; i < liczba; i++)
            {
                if (listDevices.Items[listDevices.SelectedIndex].ToString() == Un4seen.BassAsio.BassAsio.BASS_ASIO_GetDeviceInfo(i).name) { urzadzenie = i; }
            }
            
            current_sample = Convert.ToInt32(numStartSample.Value);
            dodaj_date = true;
            przygotowanie_odtwarzania();
        }


        #region logika eksperymentu
        public void przygotowanie_odtwarzania()
        {
            //czas odtwarzania probki
            
            audio_buffers = new List<byte[]>();
            audio_stoper_lengths = new List<int>();
            lengths=new List<long>();
            _hGCFiles=new List<GCHandle>();

            #region jesli ma byc odtwarzany dzwiek
            try
            {
                BassAsio.BASS_ASIO_Stop();
                BassAsio.BASS_ASIO_Free();
            }
            catch
            { }
                //=================================================================================
                //              Kopiowanie wszystkich strumieni audio do pamięci
                //
                //otwarcie pliku audio, pobranie jego wielkosci
                //wczytanie pliku do buforu
                //zamkniecie pliku
                //utworzenie uchwytu, ktory zapobiegnie usunieciu pliku
                //ustawienie czasu odtwarzania
                //=================================================================================
            for (int j=0;j<plik_wejsciowy.interval_length;j++)
            {
                audio_stoper_lengths.Add(plik_wejsciowy.get_sample_length(current_sample*plik_wejsciowy.interval_length+j));
                FileStream fs = File.OpenRead(plik_wejsciowy.get_audio_samp(current_sample * plik_wejsciowy.interval_length+j));
                byte[] temp = new byte[44];
                fs.Read(temp, 0, 44);

                long lf = 44 + BitConverter.ToUInt32(temp, 40);
                lengths.Add(lf + BitConverter.ToUInt32(temp, 28));

                audio_buffers.Add(new byte[lengths[j]]);
                for (long i = 0; i < lengths[j]; i++)
                {
                    audio_buffers[j][i] = 0;
                }
                fs.Position = 0;
                fs.Read(audio_buffers[j], 0, (int)lf);
                fs.Close();
                _hGCFiles.Add(GCHandle.Alloc(audio_buffers[j],GCHandleType.Pinned));
            }
            
                //=================================================================================
                //              Przygotowanie całego BASS_Asio
                //=================================================================================
                Asio_Setup(urzadzenie);
            
            #endregion
            
                if (!BassAsio.BASS_ASIO_Start(0))
                {
                    //    BASSError blad = BassAsio.BASS_ASIO_ErrorGetCode();
                    //    MessageBox.Show("błąd odtwarzania: " + blad.ToString());
                }
            
            #region log: poprawne rozpoczecie odtwarzania sampli
            if (!File.Exists(@"log.txt"))
            {
                using (StreamWriter writer = new StreamWriter(@"log.txt", true))
                {
                    string tmp = "log file for experiment";
                    writer.WriteLine(tmp);
                }
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(@"log.txt", true))
                {
                    string date_time = DateTime.Now.ToString();
                    string tmp = date_time + " poprawnie rozpoczecie odtarzania sampli: \n\t" + plik_wejsciowy.get_video_samp(current_sample) + "\n\t"+ plik_wejsciowy.get_audio_samp(current_sample);
                    writer.WriteLine(tmp);
                }
            }
            catch
            { }
            #endregion
            timer1.Interval = audio_stoper_length;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //  PresentPanel.Visible = false;
            #region log: poprawne przejscie do timera
            if (!File.Exists(@"log.txt"))
            {
                using (StreamWriter writer = new StreamWriter(@"log.txt", true))
                {
                    string temp = "log file for experiment";
                    writer.WriteLine(temp);
                }
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(@"log.txt", true))
                {
                    string date_time = DateTime.Now.ToString();
                    string temp = date_time + " poprawnie wejscie do timera";
                    writer.WriteLine(temp);
                }
            }
            catch
            { }
            #endregion
            
            
                BassAsio.BASS_ASIO_Stop();
                _hGCFile.Free();
                GC.Collect();
            
            #region log: poprawne zatrzymanie odtwarzania dźwięku
            if (!File.Exists(@"log.txt"))
            {
                using (StreamWriter writer = new StreamWriter(@"log.txt", true))
                {
                    string temp = "log file for experiment";
                    writer.WriteLine(temp);
                }
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(@"log.txt", true))
                {
                    string date_time = DateTime.Now.ToString();
                    string temp = date_time + " poprawnie zakończono odtwarzanie dźwieku";
                    writer.WriteLine(temp);
                }
            }
            catch
            { }
            #endregion
            
            
                
            Visual_Taking_Response();
            System.Threading.Thread.Sleep(200);
            GC.Collect();
            timer1.Enabled = false;
        }


        public void Ustawienie_probek()
        {
            current_sample++;
            if (current_sample==plik_wejsciowy.Num_samples)
            {
                //powrot do normy
                Visual_Setup();
            }
            else
            {
                Visual_Presenting_Sample();
                
                przygotowanie_odtwarzania();
            }
        }
        
        public void zapisz_odpowiedz(object sender, EventArgs e)
        {
            var b = (Button)sender;
         
            string v;
            string a;

            
                a = plik_wejsciowy.get_audio_samp(current_sample);
            
                v = plik_wejsciowy.get_video_samp(current_sample);
            
            VarOutFile.Add_Line(plik, dodaj_date, plik_wejsciowy.file_path, current_sample, v, a, b.Text);
            dodaj_date = false;
            Ustawienie_probek();
        }

     
      #endregion
      private void appTerm_Click(object sender, EventArgs e)
      {
          this.Close();
      }

      


      #region Manipulacja obiektami na formularzu
        
      private void Visual_Setup()
      {
          if (cbScreenOrder.Checked)
          {
              this.Left = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Left;
              this.Top = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Top;
              
          }
          else
          {
              this.Left = Screen.AllScreens[0].WorkingArea.Left;
              this.Top = Screen.AllScreens[0].WorkingArea.Top;
          }
          this.Width = 520;
          this.Height = 404;
          SetupPanel.Left = 1;
          SetupPanel.Top = 1;
          PresentPanel.Left = 0;
          PresentPanel.Top = 0;
          PresentPanel.Width = this.Width;
          PresentPanel.Height = this.Height;
          ResponsePanel.Left = (this.Width - ResponsePanel.Width) / 2;
          ResponsePanel.Top = this.Height * 3 / 4;
          
          SetupPanel.Visible = true;
          ResponsePanel.Visible = false;
          PresentPanel.Visible = false;

      }
      private void Visual_Presenting_Sample()
      {
          if (cbScreenOrder.Checked)
          {
              this.Left = Screen.AllScreens[0].WorkingArea.Left;
              this.Top = Screen.AllScreens[0].WorkingArea.Top;
              this.Width = Screen.AllScreens[0].WorkingArea.Width;
              this.Height = Screen.AllScreens[0].WorkingArea.Height;
          }
          else
          {
              this.Left = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Left;
              this.Top = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Top;
              this.Width = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Width;
              this.Height = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Height;
          }
          SetupPanel.Left = 1;
          SetupPanel.Top = 1;
          PresentPanel.Left = 0;
          PresentPanel.Top = 0;
          PresentPanel.Width = this.Width;
          PresentPanel.Height = this.Height;
          ResponsePanel.Left = (this.Width - ResponsePanel.Width) / 2;
          ResponsePanel.Top = this.Height * 3 / 4;

          SetupPanel.Visible = false;
          ResponsePanel.Visible = false;
          PresentPanel.Visible = true;
      }
      private void Visual_Taking_Response()
      {
          if (cbScreenOrder.Checked)
          {
              this.Left = Screen.AllScreens[0].WorkingArea.Left;
              this.Top = Screen.AllScreens[0].WorkingArea.Top;
              this.Width = Screen.AllScreens[0].WorkingArea.Width;
              this.Height = Screen.AllScreens[0].WorkingArea.Height;
          }
          else
          {
              this.Left = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Left;
              this.Top = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Top;
              this.Width = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Width;
              this.Height = Screen.AllScreens[Screen.AllScreens.Length - 1].WorkingArea.Height;
          }
          SetupPanel.Left = 1;
          SetupPanel.Top = 1;
          PresentPanel.Left = 0;
          PresentPanel.Top = 0;
          PresentPanel.Width = this.Width;
          PresentPanel.Height = this.Height;
          ResponsePanel.Left = (this.Width - ResponsePanel.Width) / 2;
          ResponsePanel.Top = this.Height * 3 / 4;

          SetupPanel.Visible = false;
          ResponsePanel.Visible = true;
          PresentPanel.Visible = false;
          this.KeyPreview = true;
      }
      #endregion

      #region Przygotowanie odtwarzaczy
      public void Asio_Setup(int dev)
      {
          if (!(dev == -1))
          {

              BassAsio.BASS_ASIO_Free();                               //przygotowanie BASS_Asio - zwolnienie istniejącego i
              Bass.BASS_Free();
              BassAsio.BASS_ASIO_SetDevice(dev);                       //wybór urządzenia

              if (!BassAsio.BASS_ASIO_Init(dev, 0))                    //inicjacja urządzenia + przechwycenie wyjątku
              { BASSError blad = BassAsio.BASS_ASIO_ErrorGetCode(); MessageBox.Show("błąd inicjalizacji: " + blad.ToString()); }
              else
              {

                  Bass.BASS_Init(-1, 48000, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

                  int strumien = Bass.BASS_StreamCreateFile(_hGCFile.AddrOfPinnedObject(), 0L, length, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);
                  _myAsioProc = new Un4seen.BassAsio.ASIOPROC(AsioCallback);
                  BassAsio.BASS_ASIO_ChannelEnable(false, 0, _myAsioProc, new IntPtr(strumien));
                  int num_ch = 0;


                  if (binaural)
                  {
                      first_ch = Convert.ToInt16(textBox1.Text);
                      num_ch = 2;
                  }
                  else
                  {
                      num_ch = BitConverter.ToUInt16(audio_buffer, 22);
                      first_ch = 0;
                  }

                  for (int i = first_ch; i < num_ch + first_ch; i++)
                  {
                      BassAsio.BASS_ASIO_ChannelSetFormat(false, i, BASSASIOFormat.BASS_ASIO_FORMAT_FLOAT);
                  }
                  for (int i = first_ch + 1; i < num_ch + first_ch; i++)
                  {
                      BassAsio.BASS_ASIO_ChannelJoin(false, i, 0);
                  }
              }
          }
      }
      
      #endregion

      private void Form1_Load(object sender, EventArgs e)
      {
          Visual_Setup();
      }

      private void tBCD_TextChanged(object sender, EventArgs e)
      {
          
      }

      private void bFolderAdd_Click(object sender, EventArgs e)
      {
          FolderBrowserDialog dir = new FolderBrowserDialog();
          dir.ShowNewFolderButton=true;
          if (dir.ShowDialog()==DialogResult.OK)
          {
              tBCD.Text = dir.SelectedPath;

          }
      }

      private void Cat_Both_Form_KeyDown(object sender, KeyEventArgs e)
      {
          switch (e.KeyCode)
          {
              case Keys.Oemtilde:
                  button1.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D1:
                  button2.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D2:
                  button3.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D3:
                  button4.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D4:
                  button5.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D5:
                  button6.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D6:
                  button7.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D7:
                  button8.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D8:
                  button9.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D9:
                  button10.PerformClick();
                  this.KeyPreview = false;
                  break;
              case Keys.D0:
                  button11.PerformClick();
                  this.KeyPreview = false;
                  break;
          }
      }

      private void bAmBinSelect_Click(object sender, EventArgs e)
      {
          if (binaural)
          {
              bAmBinSelect.Text = "Odsłuch ambisoniczny";
              binaural = false;
          }
          else
          {
              bAmBinSelect.Text = "Odsłuch binauralny";
              binaural = true;
          }
      }

    
    }
}
