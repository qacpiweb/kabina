using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Kategoryzacja_Eksper
{
    class InputFile
    {
        public string file_path {get;private set;}
        public UInt16 Audio_channels { get; private set;}
        public Int32 Num_samples { get; private set;}
        public string Instructions { get; private set;}
        public string Question { get; private set; }
        private List<string> video_samples;
        private List<string> audio_samples;
        private List<Int32> sample_length;

        public string get_video_samp(Int32 num_samp)
        {
            if (num_samp < Num_samples)
            { return video_samples[num_samp]; }
            else
            { return "-1"; }
        }
        public string get_audio_samp(Int32 num_samp)
        { 
            if (num_samp<Num_samples)
            { return audio_samples[num_samp];}
            else
            { return "-1"; }   
        }
        public Int32 get_sample_length(Int32 num_samp)
        {
            if (num_samp < Num_samples)
            { return sample_length[num_samp]; }
            else
            { return -1; }
        }
        public InputFile()
        {
            Audio_channels = 0;
            Num_samples = -1;
            video_samples = new List<string>();
            audio_samples = new List<string>();
            sample_length = new List<Int32>();
        }
        public InputFile(string path)
        {
            Audio_channels = 0;
            Num_samples = -1;
            video_samples = new List<string>();
            audio_samples = new List<string>();
            sample_length = new List<Int32>();
            load_from_file(path);
        }
        public void load_from_file(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    file_path=path;                    
                    Audio_channels = Convert.ToUInt16(sr.ReadLine());
                    Num_samples = Convert.ToInt32(sr.ReadLine());
                    Instructions = sr.ReadLine();
                    Question = sr.ReadLine();
       
                    for (Int32 i=0;i<Num_samples;i++)
                    {
                        sample_length.Add(Convert.ToInt32(sr.ReadLine()));
                        video_samples.Add(sr.ReadLine());
                        audio_samples.Add(sr.ReadLine());
                        
                    }
                    
                }
            }
            catch (Exception e)
            {
               // System.Windows.Forms.MessageBox.Show("Błąd odczytu pliku:\n"+e.ToString());
            }
        }
    }

    class OrdInputFile
    {
        public string file_path {get;private set;}
        public UInt16 Audio_channels { get; private set;}
        public string Instructions { get; private set;}
        public string Question { get; private set; }
        public UInt16 Available_samples {get; private set;}
        private List<string> audio_samples;
        private List<string> answer;
        private List<Int32> sample_length;
        public Int32 Num_samples { get; private set;}
        public UInt16 interval_length { get; private set; }
        private List<Int32> order_of_samples;

        public string get_audio_samp(Int32 num_samp)
        { 
            if (num_samp<Num_samples)
            { return audio_samples[order_of_samples[num_samp]];}
            else
            { return "-1"; }   
        }
        public Int32 get_sample_length(Int32 num_samp)
        {
            if (num_samp < Num_samples)
            { return sample_length[order_of_samples[num_samp]]; }
            else
            { return -1; }
        }
        public OrdInputFile()
        {
            Audio_channels = 0;
            Num_samples = -1;
            audio_samples = new List<string>();
            answer = new List<string>();
            sample_length = new List<Int32>();
            order_of_samples = new List<int>();
        }
        public OrdInputFile(string path)
        {
            Audio_channels = 0;
            Num_samples = -1;
            audio_samples = new List<string>();
            answer = new List<string>();
            sample_length = new List<Int32>();
            order_of_samples = new List<int>();
            load_from_file(path);
        }
        public void load_from_file(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    file_path=path;                                 //wczytanie ścieżki pliku    
             
                    string temp;
                    temp=sr.ReadLine();                             //ominięcie linii z ;;    
                    while(temp == ";;") {temp=sr.ReadLine();}

                    Audio_channels=Convert.ToUInt16(temp);          //wczytanie liczby kanałów audio
                    temp=sr.ReadLine();
                    while(temp == ";;") {temp=sr.ReadLine();}
        
                    Instructions=temp;                              //wczytanie instrukcji
                    temp=sr.ReadLine();
                    while(temp == ";;") {temp=sr.ReadLine();}
        
                    Question=temp;                                  //wczytanie polecenia
                    temp=sr.ReadLine();
                    while(temp == ";;") {temp=sr.ReadLine();}

                    Available_samples=Convert.ToUInt16(temp);       //wczytanie liczby dostępnych plików audio
                    

                    for (Int32 i=0;i<Available_samples;i++)         //wczytywanie listy dostępnych plików audio wraz z ich odpowiedziami i długościami plików
                    {
                        temp=sr.ReadLine();
                        while(temp == ";;") {temp=sr.ReadLine();}
                        audio_samples.Add(temp);
                        temp=sr.ReadLine();
                        while(temp == ";;") {temp=sr.ReadLine();}
                        answer.Add(temp);
                        temp=sr.ReadLine();
                        while(temp == ";;") {temp=sr.ReadLine();}
                        sample_length.Add(Convert.ToInt32(temp));
                    }

                    temp=sr.ReadLine();
                    while(temp == ";;") {temp=sr.ReadLine();}
                    Num_samples=Convert.ToInt32(temp);              //wczytywanie liczby prób
                    temp = sr.ReadLine();
                    while (temp == ";;") { temp = sr.ReadLine(); }
                    interval_length = Convert.ToUInt16(temp);       //wczytywanie liczby interwałów w próbie

                    for (Int32 i=0;i<Num_samples*interval_length;i++)   //wczytywanie kolejnych interwałów w kolejnych próbach
                    {
                        temp = sr.ReadLine();
                        while (temp == ";;") { temp = sr.ReadLine(); }
                        order_of_samples.Add(Convert.ToInt32(temp));                        
                    }
                    
                }
            }
            catch (Exception e)
            {
             //   System.Windows.Forms.MessageBox.Show("Błąd odczytu pliku:\n"+e.ToString());
            }
        }

    }
    
    class NewInputFile
    {

    }
    #region Routing kanałów audio
    class AudioRoutingDefinitions
    {
        [XmlAttribute("Dostępne układy")]
        private List<AudioRoutingMatrix> routing_options;
        public AudioRoutingDefinitions()
        {
            routing_options = new List<AudioRoutingMatrix>(0);
        }
        public AudioRoutingDefinitions(string file)
        {
            routing_options = new List<AudioRoutingMatrix>(0);
            LoadFromFile(file);
            
        }

        private int LoadFromFile(string file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<AudioRoutingMatrix>));
            TextReader textReader = new StreamReader(file);
            routing_options = (List<AudioRoutingMatrix>)deserializer.Deserialize(textReader);
            textReader.Close();
            return 0;
        }
        public int SaveToFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<AudioRoutingMatrix>));
            TextWriter textWriter = new StreamWriter(file);
            serializer.Serialize(textWriter, routing_options);
            textWriter.Close();
            return 0;
        }
        public void Add(AudioRoutingMatrix entry)
        {
            routing_options.Add(entry);
        }

        public void Remove(AudioRoutingMatrix entry)
        {
            int i=routing_options.Count - 1;
            while (i>-1)
            {
                if (routing_options[i].Equals(entry))
                {
                    routing_options.RemoveAt(i);
                }
                i--;
            }
        }

        public void Remove(string entry)
        {
            int i = routing_options.Count - 1;
            while (i > -1)
            {
                if (routing_options[i].get_name().Equals(entry))
                {
                    routing_options.RemoveAt(i);
                }
                i--;
            }
        }
    }
    class AudioRoutingMatrix
    {
        [XmlAttribute("Nazwa ustawienia")]
        string setup_name;
        [XmlAttribute("Połączenia")]
        List<short> matrix;
        public string get_name() { return setup_name; }
    }
    #endregion

    #region pliki wejściowe
    
    //dostępne odpowiedzi w szeregowaniu
    class ResponseCollection
    {
        [XmlAttribute("Pula_odpowiedzi")]
        public List<string> responses;

        public int LoadFromFile(string file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<string>));
            TextReader textReader = new StreamReader(file);
            responses = (List<string>)deserializer.Deserialize(textReader);
            textReader.Close();
            return 0;
        }
        public int SaveToFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            TextWriter textWriter = new StreamWriter(file);
            serializer.Serialize(textWriter, responses);
            textWriter.Close();
            return 0;
        }
    }
    
    //lista dostępnych bodźców
    public class SampleCollection
    {
        [XmlAttribute("Lista_próbek")]
        public List<Sample> SampleList;
        public SampleCollection()
        {
            SampleList = new List<Sample>(0);
        }
        public SampleCollection(string file)
        {
            SampleList=new List<Sample>(0);
            LoadFromFile(file);
        }
        public int LoadFromFile(string file)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Sample>));
            StreamReader textReader = new StreamReader(file,Encoding.Unicode);
            SampleList = (List<Sample>)deserializer.Deserialize(textReader);
            textReader.Close();
            return 0;
        }
        public int SaveToFile(string file)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Sample>));
            StreamWriter textWriter = new StreamWriter(file,false,Encoding.Unicode);
            serializer.Serialize(textWriter, SampleList);
            textWriter.Close();
            return 0;
        }
    }
    
    //bodziec
    public class Sample
    {
        [XmlAttribute("Nazwa_próbki")]
        public string sample_name;
        [XmlAttribute("Próbka_audio")]
        public string audio_sample;
        [XmlAttribute("Próbka_wideo")]
        public string video_sample;
        [XmlAttribute("Czas_ms")]
        public int sample_time;
        
        public Sample()
        {
           
        }
        public Sample(string name, string audio, string video, int time)
        {
            sample_name = name;
            audio_sample = audio;
            video_sample = video;
            sample_time = time;
        }
    }
    
    //
    class Experiment
    {
        [XmlAttribute("Instrukcja")]
        public string instruction;
        [XmlAttribute("Pytanie")]
        public string question;
    }

    class ExperimentSample
    {
        [XmlAttribute("Liczba próbek w badaniu")]
        public int interval_count;
        [XmlAttribute("Kolejne próbki")]
        public List<int> Intervals;
    }
    #endregion

    class OutputFile
    {
        public string Name { get; set; }
        public string Surname {get; set;}
        public string Age {get;set;}
        public string input_name;
        public string out_folder;
  
        List<string> answers;

        public OutputFile()
        {
            answers = new List<string>();
        }
        public void Save_Output_File()
        {
            
            if (!File.Exists(out_folder+Surname+"_"+Name+".csv"))
            {
                using (StreamWriter writer = new StreamWriter(@"output\" + Surname + "_" + Name + ".csv", true))
                    {
                        string temp = "\t\t"+Name + "\t" + Surname + "\t"+Age+"\t";
                        writer.WriteLine(temp);
                        temp = "\t\tNr\tVideo Sample\tAudio Sample\tAnswer";
                        writer.WriteLine(temp);
                    }
            }
            try
            {
                using (StreamWriter writer = new StreamWriter(out_folder + Surname + "_" + Name + ".csv", true))
                {
                    string date_time = DateTime.Now.ToString();
                    string temp =input_name + " "+date_time + answers[0];
                    writer.WriteLine(temp);
                    for (Int32 i=1; i<answers.Count;i++)
                    {
                        writer.WriteLine(answers[i]);
                    }
                }
            }
            catch
            { }
            
        }
        public void add_answer(string answer)
        {
            answers.Add(answer);
        }
    }

    class VarOutFile
    {
        public static string CreateFile(string Name, string Surname, string Age, string path)
        {
            if (!File.Exists(Path.Combine(path, Surname + "_" + Name + ".csv")))
            {
                using (StreamWriter writer = new StreamWriter(Path.Combine(path, Surname + "_" + Name + ".csv"), true, Encoding.Default))
                {
                    string temp = "\t\tImię\tNazwisko\tWiek\t";
                    writer.WriteLine(temp);
                    temp = "\t\t" + Name + "\t" + Surname + "\t" + Age + "\t";
                    writer.WriteLine(temp);
                    temp = "\t\tNr\tVideo Sample\tAudio Sample\tAnswer";
                    writer.WriteLine(temp);
                }
            }
            return Path.Combine(path, Surname + "_" + Name + ".csv");
        }

        public static void Add_Line(string file, bool write_input_date, string input, int sample, string vid, string aud, string ans)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(file, true, Encoding.Default))
                {
                    if (write_input_date)
                    {
                        string date_time = DateTime.Now.ToString();
                        string temp = input + "\t" + date_time + "\t" + sample.ToString() + "\t" + vid + "\t" + aud + "\t" + ans;
                        writer.WriteLine(temp);
                    }
                    else
                    {
                        string temp = "\t\t" + sample.ToString() + "\t" + vid + "\t" + aud + "\t" + ans;
                        writer.WriteLine(temp);
                    }
                }
            }
            catch
            { }
        }
    }
}
