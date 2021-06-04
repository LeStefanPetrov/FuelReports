using FuelReports.Cli;
using Renci.SshNet;
using System;
using System.IO;

namespace FuelReports.SFTP
{
    public class SftpDownloader
    {
        public static void SftpDownload(string path)
        {
            using (var sftp = new SftpClient(AppConfig.GetAppConfigValue(AppConfigKeys.ServerHost), AppConfig.GetAppConfigValue(AppConfigKeys.ServerUsername), AppConfig.GetAppConfigValue(AppConfigKeys.ServerPassword)))
            {
                sftp.Connect();
                var files = sftp.ListDirectory(AppConfig.GetAppConfigValue(AppConfigKeys.XmlServerPath));

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                foreach (var file in files)
                {
                    string remoteFileName = file.Name;

                    if (remoteFileName.StartsWith(AppConfig.GetAppConfigValue(AppConfigKeys.FirstFileCharacter)) && !File.Exists(path + remoteFileName))
                    {
                        using (var stream = new FileStream(path + remoteFileName, FileMode.Create))
                            sftp.DownloadFile(AppConfig.GetAppConfigValue(AppConfigKeys.XmlServerPath) + remoteFileName, stream);
                    }
                }
            }
        }
    }
}
