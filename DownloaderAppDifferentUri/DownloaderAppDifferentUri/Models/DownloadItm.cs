using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DownloaderAppDifferentUri.Models
{
    public class DownloadItm
    {
        private static  int _id=0;
        public bool SelectedDownloadItm { get;set; }
        public int ID { get; }
        public string FileName { get; set; }       
        public int Progress { get; set; }
        public WebClient Client { get; set; }      
    
        public CancellationTokenSource Cts = new CancellationTokenSource();      
        public DownloadItm(string fileName)
        {
            _id += 1;
             FileName = fileName;
            
          
            Cts.Token.Register(() =>
            {
                Client.CancelAsync();
            });
        }
        public DownloadItm()
        {
        }
        public override string ToString()
        {
            return $"{FileName}" ;
        }
    }
}
