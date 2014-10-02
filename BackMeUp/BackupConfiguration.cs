using System;
using System.Collections.Generic;

namespace BackMeUp
{
    [Serializable]
    public class BackupConfiguration
    {
        public string Destination { get; set; }

        public List<string> SourceDirectories { get; set; }

        //public Dictionary<string, string> FileHashes { get; set; }

        public BackupConfiguration()
        {
        }

        public BackupConfiguration(string destination, List<string> sourceDirectories/*, Dictionary<string, string> fileHashes*/)
        {
            Destination = destination;
            SourceDirectories = sourceDirectories;
            //FileHashes = fileHashes;
        }

    }
}
