using DownloaderAppDifferentUri.Models;
using DownloaderAppDifferentUri.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DownloaderAppDifferentUri;

namespace DownloaderAppDifferentUri.ViewModels
{
   public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region  Variables
        private int i = 0;
        private DownloadItm _model;
        private ObservableCollection<DownloadItm> _models;
        public DownloadItm _selectedDownloadItm { get; set; }

        public System.Windows.Visibility _visibility;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

      


        #region Constructrs
        public MainWindowViewModel()
        {
            _model =new DownloadItm();
           
            _models = new ObservableCollection<DownloadItm>();
            Visibility1 = Visibility.Hidden;
            EddNewDownloadItem = new Relaycommand(DownloadItemEcecute);
            EddDownload = new Relaycommand( AddDownloadItem);

        }
        #endregion




        #region  Propertys

        public ICommand EddNewDownloadItem { get; }
        public ICommand EddDownload { get; }
        public ObservableCollection<DownloadItm> Models
        {
            get { return _models; }
            set
            {
                _models = value;
                RaisePropertyChanged("Models");
            }
        }  

        public DownloadItm SelectedDownloadItm
        {
            get { return _selectedDownloadItm; }
            set
            {
                _selectedDownloadItm = value;
                RaisePropertyChanged("SelectedDownloadItm");
            }
        }


        public Visibility Visibility1
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged("Visibility1");
            }
        }      

        public string FileName
        {
            get { return Model.FileName; }
            set
            {
                Model.FileName = value;
                RaisePropertyChanged("FileName");
            }

        }

        public WebClient Client
        {
            get { return Model.Client; }
            set
            {
                Model.Client = value;
                RaisePropertyChanged("Client");
            }

        }

        public static int e=0;
        public int Progress
        {
            get { return Model.Progress; }
            set
            {
                RaisePropertyChanged("Progress");
                Model.Progress = value;
            }

        }
        public DownloadItm Model
        {
            get { return _model; }
            set
            {
                _model = value;
                RaisePropertyChanged("Model");
            }
        }

        #endregion


        #region Functions
        public  async void AddDownloadItem()
        {
            try
            {
                if (SelectedDownloadItm != null)
                {


                    for (int i = 0; i < Models.Count; i++)
                    {
                        if (Models[i] == SelectedDownloadItm)
                        {
                            //Progress=8976;
                            Uri address = new Uri(Models[i].FileName);

                            string[] ar = address.Segments;

                            string EnterFolderName = $"Folder{i}";
                            string folderPath = System.IO.Path.Combine(
                                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop), EnterFolderName);

                            Directory.CreateDirectory(folderPath);

                            string newFile = folderPath + @"\" + ar[ar.Length - 1];

                            using (Models[i].Client = new WebClient())
                            {

                                Models[i].Client.DownloadProgressChanged += ProgresBarr_ValueChanged;


                                Models[i].Client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                               await Models[i].Client.DownloadFileTaskAsync(address, newFile);
                            }                           

                        }

                    }
                    

                }

                else
                { 
                    MessageBox.Show("Please select DownloadItm ");
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
            i++;
            //MessageBox.Show($"{ConnectionString},{DataToSend},{ResivedData}");
        }


        private void ProgresBarr_ValueChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            Progress = e.ProgressPercentage;//int.Parse(Math.Truncate(percentage).ToString());
            WebClient client = sender as WebClient;
            for (int i = 0; i < Models.Count; i++)
            {
                if (Models[i].Client == client)
                {
                    Models[i].Progress = e.ProgressPercentage;
                }
            }
            //Model.Progress = e.ProgressPercentage;
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Completed");
        }

        public void DownloadItemEcecute()
        {
            try
            {
                var ModelNew = new DownloadItm { FileName = "", Progress =1 };

                i++;
                Models.Add(ModelNew);
                MessageBox.Show(SelectedDownloadItm.ToString());
            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
        }

        public bool CanDownloadItem()
        {
            return true;
        }
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
