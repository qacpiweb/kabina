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
    public partial class Ass_Video_Form : Form
    {
        public Stopwatch stoper= new Stopwatch();
        public long tick=0;
        public long delta_tick=0;
        public long last_frame=0;
        public long stoper_freq=0;
        InputFile plik_wejsciowy;
        
        Int32 szer=520;
        Int32 wys=404;
        
        int current_sample;
        public double audio_length;
        public int audio_stoper_length;
        string plik;
        bool dodaj_date;
        string directory;

        MediaPlayerFactory factory;
        Declarations.Players.IDiskPlayer player;
        Declarations.Media.IMediaFromFile med;
       
    
        //konstruktor
        public Ass_Video_Form()
        {
            InitializeComponent();
            this.Left=Screen.AllScreens[0].WorkingArea.Left;
            this.Top = Screen.AllScreens[0].WorkingArea.Top;
            this.Width = szer;
            this.Height = wys;
            SetupPanel.Visible = true;
            ResponsePanel.Visible = false;
            PresentPanel.Visible = false;
           
            directory = Application.StartupPath;
           
            tBCD.Text = directory;
            stoper_freq = Stopwatch.Frequency;
            delta_tick = stoper_freq / 25;
            Visual_Setup();
            

        }
        //wczytanie pliku wejsciowego
        private void bLoadInputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog oInputFileDlg = new OpenFileDialog();
            oInputFileDlg.Filter = "Text Files (.txt)|*.txt";
            oInputFileDlg.FilterIndex = 0;
            oInputFileDlg.Multiselect = false;
            oInputFileDlg.InitialDirectory = @"\input";

            if (oInputFileDlg.ShowDialog()==DialogResult.OK)
            {
                plik_wejsciowy = new InputFile(oInputFileDlg.FileName);
                tInputFile.Text = oInputFileDlg.FileName;
                numStartSample.Minimum = 0;
                numStartSample.Maximum = plik_wejsciowy.Num_samples - 1;
                numStartSample.Value = 0;
            }
        }
        
        //początek eksperymentu
        private void bExperStart_Click(object sender, EventArgs e)
        {
            plik = VarOutFile.CreateFile(tName.Text, tSurname.Text, tAge.Text,tBCD.Text);
            lInstructions.Text = plik_wejsciowy.Question;
            Visual_Presenting_Sample();
            Dialog dlg = new Dialog(plik_wejsciowy.Instructions);
            dlg.ShowDialog();
            
            current_sample = Convert.ToInt32(numStartSample.Value);
            dodaj_date = true;
            przygotowanie_odtwarzania();
        }


        #region logika eksperymentu
        public void przygotowanie_odtwarzania()
        {
            //czas odtwarzania probki
            audio_stoper_length = plik_wejsciowy.get_sample_length(current_sample);

           
            
                //=================================================================================
                //             Przygotowanie odtwarzacza
                //=================================================================================
               // Player_Setup(Application.StartupPath + "\\" + plik_wejsciowy.get_video_samp(current_sample));
                Player_Setup(plik_wejsciowy.get_video_samp(current_sample));

                //=================================================================================
                //             odtwarzanie
                //=================================================================================
                player.Play();
         
           
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
            
            
            
          
            player.Pause();
            player.Stop();
           
            #region log: poprawne zatrzymanie odtwarzania wideo
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
                    string temp = date_time + " poprawnie zakończono odtwarzanie obrazu";
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
            string b = tAnswer.Text;
        
            string v;
            string a;

            a = "No Audio";
            v = plik_wejsciowy.get_video_samp(current_sample);
            
            VarOutFile.Add_Line(plik, dodaj_date, plik_wejsciowy.file_path, current_sample, v, a, b);
            dodaj_date = false;
            tAnswer.Text="";
            Ustawienie_probek();
        }

     
      #endregion
      private void appTerm_Click(object sender, EventArgs e)
      {
          this.Close();
      }

      private class UISync
      {
          private static ISynchronizeInvoke Sync;

          public static void Init(ISynchronizeInvoke sync)
          {
              Sync = sync;
          }

          public static void Execute(Action action)
          {
              Sync.BeginInvoke(action, null);
          }
      }

      #region events
      void Events_PlayerStopped(object sender, EventArgs e)
      {
          UISync.Execute(( ) => { });
          //UISync.Execute(() => InitControls());
      }

      void Events_MediaEnded(object sender, EventArgs e)
      {
          UISync.Execute(() => { });
          //UISync.Execute(() => InitControls());
          
          //BassAsio.BASS_ASIO_Stop();     //<= potencjalny złoczyńca
          //ResponsePanel.Visible = true;
      }

      void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
      {
          UISync.Execute(() => { });
          //UISync.Execute(() => lblTime.Text = TimeSpan.FromMilliseconds(e.NewTime).ToString().Substring(0, 8));
      }

      void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
      {
          UISync.Execute(() => { });
          //UISync.Execute(() => trackBar1.Value = (int)(e.NewPosition * 100));
      }

      void Events_StateChanged(object sender, MediaStateChange e)
      {
          UISync.Execute(() => { });
          //UISync.Execute(() => label1.Text = e.NewState.ToString());
      }

      void Events_ParsedChanged(object sender, MediaParseChange e)
      {
          // Console.WriteLine(e.Parsed);
      }

      void Events_DurationChanged(object sender, MediaDurationChange e)
      {
          UISync.Execute(() => { });
      }
      #endregion

      


      #region Manipulacja obiektami na formularzu i skróty klawiatury
        
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


      private void Aso_Video_Form_KeyDown(object sender, KeyEventArgs e)
      {
          switch (e.KeyCode)
          {
              case Keys.Enter:
                  button1.PerformClick();
                  this.KeyPreview = false;
                  break;
          }
      }        
      #endregion

      #region Przygotowanie odtwarzaczy
      
      public void Player_Setup(string sciezka)
      {
          factory = new MediaPlayerFactory(true);
          player = factory.CreatePlayer<IDiskPlayer>();
          player.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
          player.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
          player.Events.MediaEnded += new EventHandler(Events_MediaEnded);
          player.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);
          player.WindowHandle = PresentPanel.Handle;
          UISync.Init(this);
          med = factory.CreateMedia<IMediaFromFile>(sciezka);
          med.Events.DurationChanged += new EventHandler<MediaDurationChange>(Events_DurationChanged);
          med.Events.StateChanged += new EventHandler<MediaStateChange>(Events_StateChanged);
          med.Events.ParsedChanged += new EventHandler<MediaParseChange>(Events_ParsedChanged);
          player.Open(med);
          med.Parse(true);
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

      
    }
}
