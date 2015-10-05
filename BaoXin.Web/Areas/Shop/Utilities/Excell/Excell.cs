using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace 飞机订票系统MVC.Areas.Admin.Utilities.Excell
{
    public class Excell
    {
        public static void Create<T>(string savePath, List<T> list, string[] fields)
        {
            Create(savePath, DateTime.Now.ToString("yyyy-MM-dd"), list, fields);
        }

        public static MemoryStream Create<T>(List<T> list, string sheetName, string[] fields)
        {
            var workbook = new HSSFWorkbook();
            AppendSheet(workbook, sheetName, list, fields);
            var stream = new MemoryStream();
            workbook.Write(stream);
            stream.Position = 0;
            return stream;
        }

        public static void Create<T>(string savePath, string sheetName, List<T> list, string[] fields)
        {
            var workbook = new HSSFWorkbook();
            AppendSheet(workbook, sheetName, list, fields);
            SaveExcel(workbook, savePath);
        }

        public static void Create<T>(string savePath, Dictionary<string, List<T>> sheetsAndRecords, string[] fields)
        {
            var workbook = new HSSFWorkbook();
            foreach (var sheetsAndRecord in sheetsAndRecords)
            {
                AppendSheet<T>(workbook, sheetsAndRecord.Key, sheetsAndRecord.Value, fields);
            }
            SaveExcel(workbook, savePath);
        }

        private static void AppendSheet<T>(IWorkbook workbook, string sheetName, IEnumerable<T> records, string[] fields)
        {
            var sheet = workbook.CreateSheet(sheetName);
            //var types = typeof(T);
            //  var fields = types.GetProperties();
            //Create Sheet Header
            //string[] fields = { "外键表","车型ID", "车型名称", "可座人数", "可放行李数", "备注", "图片"};
            var headerRow = sheet.CreateRow(0);
            for (int i = 0; i < fields.Length; i++)
            {
                // headerRow.CreateCell(i).SetCellValue(fields[i].Name);
                headerRow.CreateCell(i).SetCellValue(fields[i]);
            }
            //create rows
            var rowIndex = 1;
            foreach (var item in records)
            {
                var dummyFields = item.GetType().GetProperties();
                var row = sheet.CreateRow(rowIndex);
                for (int i = 0; i < dummyFields.Length; i++)
                {
                    var cell = row.CreateCell(i);
                    cell.SetCellValue(dummyFields[i].GetValue(item, null) == null ? " " : dummyFields[i].GetValue(item, null).ToString());
                }
                rowIndex++;
            }
        }
        private static void SaveExcel(IWorkbook workbook, string savePath)
        {
            if (workbook == null) throw new ArgumentNullException("workbook");
            using (var file = new FileStream(savePath, FileMode.OpenOrCreate))
            {
                workbook.Write(file);
                file.Close();
            }
        }
    }
}
