using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DownloaderAppDifferentUri.ViewModels
{
   public static class HelpperFunctions
    {
        public static string FolderCreater()
        {
            string newFolder = "DownLoader";

            string path = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop), newFolder
            );

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("General Error: " + e.Message);
                    throw;
                }

            }
            return path;

        }


        public static void FileCreater(string path)
        {

            if (!File.Exists(path))
            {
                try
                {
                    File.Create(path);
                }
                catch (IOException ie)
                {
                    Console.WriteLine("IO Error: " + ie.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("General Error: " + e.Message);
                    throw;
                }

            }

        }


        public static async Task Downloanding(Uri url, string filename)
        {
            using (WebClient client = new WebClient())
            {

                try
                {

                    client.DownloadFileAsync(url, filename);
                   
                }
                catch (UriFormatException ex)
                {

                   
                }
                catch (WebException ex)
                {
                    

                }
                catch (InvalidOperationException ex)
                {
                   

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                }

            }


        }


        private static int client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            return int.Parse(Math.Truncate(percentage).ToString());
        }

        private static void buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
