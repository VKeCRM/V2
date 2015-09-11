using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace VKeCRM.Common.Resources
{
    /// <summary>
    /// eg: new SiteMapConverter("E:/workspace/20091207/SiteMapEdited.xls","e:/test.sql",null,-1).CreateSqlFile();
    /// get the data from xml file and create sql script for it
    /// </summary>
    public class SiteMapConverter
    {
        #region propertys
        public string InputFilePath
        {
            get;
            set;
        }

        public string OutputFilePath
        {
            get;
            set;
        }

        private string _sqlFormat = @"insert into VKeCRM.Sitemap(Id,Name,Url,IsActive,SiteCode,CreatedByUserId,CreatedDateTimeUtc,LastModifiedByUserId
            ,LastModifiedDateTimeUtc) values({0},'{1}','{2}',1,{3},1000,GetUtcDate(),1000,GetUtcDate())";
        public string SqlFormat
        {
            get
            {
                return _sqlFormat;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _sqlFormat = value;
                }
            }
        }

        private string _sqlBeforeBody = "delete from zecco.sitemap; set IDENTITY_INSERT VKeCRM.Sitemap on;";
        private string _sqlAfterBody = "set IDENTITY_INSERT VKeCRM.Sitemap off;";

        private int _ignoreRowNum = 1;
        public int IgnoreRowNum
        {
            get
            {
                return _ignoreRowNum;
            }
            set
            {
                if (value >= 0)
                {
                    _ignoreRowNum = value;
                }
            }
        }

        private int _currentId = 1;

        private Dictionary<int, string> _sites;
        private Dictionary<int, string> Sites
        {
            get
            {
                if (_sites == null)
                {
                    _sites = new Dictionary<int, string>();
                    _sites.Add(0, "Portal");
                    _sites.Add(1, "Trading");
                    _sites.Add(2, "Ola");
                    _sites.Add(3, "Wsod");
                    _sites.Add(4, "Nexa");
                }
                return _sites;
            }
        }

        #endregion

        #region public methods
        /// <summary>
        /// Create instance and init params
        /// </summary>
        /// <param name="inputFilePath"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="sqlFormat"></param>
        /// <param name="ignoreRowNum"></param>
        public SiteMapConverter(string inputFilePath, string outputFilePath, string sqlFormat, int ignoreRowNum)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
            SqlFormat = sqlFormat;
            IgnoreRowNum = ignoreRowNum;
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateSqlFile()
        {
            StreamWriter streamWriter = new StreamWriter(new FileStream(OutputFilePath, FileMode.OpenOrCreate));
            ;
            Application application = new Application();
            Workbook workBook = application.Workbooks.Open(InputFilePath, Type.Missing, Type.Missing, Type.Missing,
                                                                 Type.Missing, Type.Missing
                                                                 , Type.Missing, Type.Missing, Type.Missing,
                                                                 Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                                 Type.Missing, Type.Missing);

            streamWriter.WriteLine(_sqlBeforeBody);
            foreach (int s in Sites.Keys)
            {
                string name = string.Empty;
                Sites.TryGetValue(s, out name);

                DelWorkSheet(streamWriter, GetWorkSheetByName(workBook, name), s, SqlFormat);
            }
            streamWriter.WriteLine(_sqlAfterBody);
            streamWriter.Flush();
            streamWriter.Close();
            workBook.Close(XlSaveAction.xlSaveChanges, Type.Missing,Type.Missing);
            application.Quit();
        }

        #endregion

        #region private methods
        /// <summary>
        /// Prevent user call this method directly
        /// </summary>
        private SiteMapConverter()
        {
        }

        /// <summary>
        /// Create sql script by per row and write it to sqlscript file
        /// </summary>
        /// <param name="streamWriter"></param>
        /// <param name="worksheet"></param>
        /// <param name="siteCode"></param>
        /// <param name="sqlFormat"></param>
        private void DelWorkSheet(StreamWriter streamWriter, Worksheet worksheet, int siteCode, string sqlFormat)
        {
            int lineNum = IgnoreRowNum + 1;
            string cellName = ReplaceDirtyChar(((Range)worksheet.Rows.Cells[lineNum, 1]).Text.ToString().Trim());
            string cellUrl = DelUrl(ReplaceDirtyChar(((Range)worksheet.Rows.Cells[lineNum, 2]).Text.ToString().Trim()));
            if (string.IsNullOrEmpty(cellUrl))
            {
                cellUrl = DelUrl(ReplaceDirtyChar(((Range)worksheet.Rows.Cells[lineNum, 3]).Text.ToString().Trim()));
            }

            while (!string.IsNullOrEmpty(cellName))
            {
                streamWriter.WriteLine(string.Format(sqlFormat,_currentId, cellName, cellUrl, siteCode));
                _currentId++;

                lineNum++;
                cellName = ReplaceDirtyChar(((Range)worksheet.Rows.Cells[lineNum, 1]).Text.ToString().Trim());
                cellUrl = DelUrl(ReplaceDirtyChar(((Range)worksheet.Rows.Cells[lineNum, 2]).Text.ToString().Trim()));
                if (string.IsNullOrEmpty(cellUrl))
                {
                    cellUrl = DelUrl(ReplaceDirtyChar(((Range)worksheet.Rows.Cells[lineNum, 3]).Text.ToString().Trim()));
                }
            }
        }

        private string ReplaceDirtyChar(string value)
        {
            return value.Replace("'", "''");
        }

        //

        /// <summary>
        /// If input url is "http://research.qa.zecco.com/research/screener/basic-screen/basic.asp?ddk=eee" 
        /// return "/research/screener/basic-screen/basic.asp"
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string DelUrl(string url)
        {
            if (url.ToLower().IndexOf("http") >= 0)
            {
                if (url.IndexOf("/", 10) >= 0)
                {
                    url = url.Substring(url.IndexOf("/", 10));
                }
            }
            if (url.IndexOf("?") >=0 )
            {
                url = url.Substring(0, url.IndexOf("?"));
            }
            return url;
        }

        /// <summary>
        /// Get worksheet by sheet name
        /// </summary>
        /// <param name="workbook">Workbook entity could not be null</param>
        /// <param name="name">Sheet name</param>
        /// <returns>Sheet entity if not found then return null</returns>
        private Worksheet GetWorkSheetByName(Workbook workbook, string name)
        {
            foreach (Worksheet sheet in workbook.Sheets)
            {
                if (sheet.Name.ToLower().Equals(name.ToLower()))
                {
                    return sheet;
                }
            }
            return null;
        }
        #endregion
    }
}
