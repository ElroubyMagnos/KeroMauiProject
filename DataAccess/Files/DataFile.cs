﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeroFruits.Utilities.DataAccess.Files
{
    public class DataFile
    {
        private string _fileName;
        private string _concern;
        public DataFile(string fileName, string concern)
        {
            FileName = fileName;
            Concern = concern;

        }
        /// <summary>
        /// fileName like CsvItems.csv
        /// </summary>
        public string FileName { get => _fileName; set => _fileName = value; }

        /// <summary>
        /// CODE for the subject, informations stored in this file. Ex : "ITEMS"  
        /// </summary>
        public string Concern { get => _concern; set => _concern = value; }

        /// <summary>
        /// full path file Ex : "C:\\COURS\\  ..\\fileName.csv"
        /// </summary>
        public string FullPath => $"{FilesPathDir}{FileName}";

        /// <summary>
        /// directory must be the same for all files => static property
        /// </summary>
        public static string FilesPathDir { get; set; }

    }
}
