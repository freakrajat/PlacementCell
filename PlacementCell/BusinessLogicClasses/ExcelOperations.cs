using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.Model; // InternalWorkbook
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.SS.Util;
using System.IO;
using PlacementCell.PlacementCellDBModel;
namespace PlacementCell.BusinessLogicClasses
{
    public class ExcelOperations
    {
        PlacementCellDBContext dbObj = new PlacementCellDBContext();

        #region Export Related variables

        public string fileName = "", lastDirectoryPath = "", FileExtension = "", filenameWithoutExt = "", saveExcelFilePath = "";
        public static HSSFWorkbook HSSFWorkBook;                                      //HSSFWorkbook Handle for handling BIFF format (xls files)
        public static XSSFWorkbook XSSFWorkBook;                                      //XSSFWorkbook Handle for handling Open XML format (xlsx files)
        public static HSSFSheet HSSFWorkSheet;
        public static XSSFSheet XSSFWorkSheet;
        public static string userName = "";
        public static string statusCounter = "";
        public static bool exportUC = false;
        public static bool exportUR = false;
        public static bool exportSR = false;
        public static bool exportNF = false;
        public static bool exportAll = false;
        List<EmployeeDBModel> newEmployees = null;

        List<EmployeeDBModel> listOfEmployees = null;

        #endregion

        public void ExportEmployees()
        {
            string exportFileLocation = "C:\\Users\\Rajat\\Documents\\";
            fileName = exportFileLocation + "OmniEmployees.xls";
            listOfEmployees = dbObj.EmployeeTable.ToList();
            FileExtension = Path.GetExtension(fileName);
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            CreateEmployeeExcelFiles();
        }

        public void CreateEmployeeExcelFiles()
        {

            ICellStyle dataRowStyle = null;
            ICellStyle stylehssf = null;
            IFont fonthssf = null;
            switch (FileExtension.ToLower())
            {
                case ".xls":
                    // create sheet
                    HSSFWorkBook = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());


                    fonthssf = HSSFWorkBook.CreateFont();
                    fonthssf.FontHeightInPoints = ((short)10);
                    fonthssf.Boldweight = (int)FontBoldWeight.Bold;

                    //bind font with style 
                    stylehssf = HSSFWorkBook.CreateCellStyle();
                    stylehssf.SetFont(fonthssf);
                    stylehssf.WrapText = true;
                    stylehssf.FillForegroundColor = IndexedColors.Grey25Percent.Index;
                    stylehssf.FillPattern = FillPattern.SolidForeground;





                    dataRowStyle = HSSFWorkBook.CreateCellStyle();
                    dataRowStyle.WrapText = true;
                    dataRowStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.General;
                    dataRowStyle.VerticalAlignment = VerticalAlignment.Top;


                    HSSFWorkSheet = (HSSFSheet)HSSFWorkBook.CreateSheet("Registered Employees");
                    //Create a header row
                    var headerRowUC = HSSFWorkSheet.CreateRow(0);

                    headerRowUC.CreateCell(0).SetCellValue("Employee ID");
                    headerRowUC.CreateCell(1).SetCellValue("Employee Name");
                    headerRowUC.CreateCell(2).SetCellValue("Location");
                    headerRowUC.CreateCell(3).SetCellValue("Date of Birth");
                    headerRowUC.CreateCell(4).SetCellValue("Date of Joining");
                    headerRowUC.CreateCell(5).SetCellValue("Compensation");
                    headerRowUC.CreateCell(6).SetCellValue("Designation");
                    headerRowUC.CreateCell(7).SetCellValue("Email");

                    for (int i = 0; i < 8; i++)
                    {
                        HSSFWorkSheet.GetRow(0).GetCell(i).CellStyle = stylehssf;
                    }

                    HSSFWorkSheet.SetColumnWidth(0, 20 * 250);
                    HSSFWorkSheet.SetColumnWidth(1, 20 * 250);
                    HSSFWorkSheet.SetColumnWidth(2, 20 * 250);
                    HSSFWorkSheet.SetColumnWidth(3, 20 * 250);
                    HSSFWorkSheet.SetColumnWidth(4, 20 * 250);
                    HSSFWorkSheet.SetColumnWidth(6, 20 * 250);
                    HSSFWorkSheet.SetColumnWidth(5, 20 * 250);
                    HSSFWorkSheet.SetColumnWidth(7, 20 * 250);


                    HSSFWorkSheet.CreateFreezePane(0, 1, 0, 1);

                    using (System.IO.FileStream fs = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        HSSFWorkBook.Write(fs);
                    }
                    break;
                case ".xlsx":



                    fonthssf = XSSFWorkBook.CreateFont();
                    fonthssf.FontHeightInPoints = ((short)11);
                    fonthssf.Boldweight = (int)FontBoldWeight.Bold;

                    //bind font with style 
                    stylehssf = XSSFWorkBook.CreateCellStyle();
                    stylehssf.SetFont(fonthssf);
                    stylehssf.WrapText = true;
                    stylehssf.FillForegroundColor = IndexedColors.Grey25Percent.Index;
                    stylehssf.FillPattern = FillPattern.SolidForeground;


                    dataRowStyle = XSSFWorkBook.CreateCellStyle();
                    dataRowStyle.WrapText = true;
                    dataRowStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.General;
                    dataRowStyle.VerticalAlignment = VerticalAlignment.Top;


                    // create sheet
                    XSSFWorkBook = new XSSFWorkbook();

                    XSSFWorkSheet = (XSSFSheet)XSSFWorkBook.CreateSheet("Registered Employees");
                    //Create a header row
                    var headerRowUCX = XSSFWorkSheet.CreateRow(0);
                    headerRowUCX.CreateCell(0).SetCellValue("Employee ID");
                    headerRowUCX.CreateCell(1).SetCellValue("Employee Name");
                    headerRowUCX.CreateCell(2).SetCellValue("Location");
                    headerRowUCX.CreateCell(3).SetCellValue("Date of Birth");
                    headerRowUCX.CreateCell(4).SetCellValue("Date of Joining");
                    headerRowUCX.CreateCell(5).SetCellValue("Compensation");
                    headerRowUCX.CreateCell(6).SetCellValue("Designation");
                    headerRowUCX.CreateCell(7).SetCellValue("Email");
                    for (int i = 0; i < 8; i++)
                    {
                        XSSFWorkSheet.GetRow(0).GetCell(i).CellStyle = stylehssf;
                    }
                    XSSFWorkSheet.SetColumnWidth(0, 20 * 250);
                    XSSFWorkSheet.SetColumnWidth(1, 20 * 250);
                    XSSFWorkSheet.SetColumnWidth(2, 20 * 250);
                    XSSFWorkSheet.SetColumnWidth(3, 20 * 250);
                    XSSFWorkSheet.SetColumnWidth(4, 20 * 250);
                    XSSFWorkSheet.SetColumnWidth(6, 20 * 250);
                    XSSFWorkSheet.SetColumnWidth(5, 20 * 250);
                    XSSFWorkSheet.SetColumnWidth(7, 20 * 250);


                    XSSFWorkSheet.CreateFreezePane(0, 1, 0, 1);

                    using (System.IO.FileStream fs = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write))
                    {
                        XSSFWorkBook.Write(fs);
                    }
                    break;
            }

            WriteEmployeesDataToExcel(dataRowStyle);


        }

        private void WriteEmployeesDataToExcel(ICellStyle dataRowStyle)
        {


            ISheet WorkSheet = null;
            if (FileExtension.ToLower() == ".xls")
                WorkSheet = HSSFWorkBook.GetSheetAt(0);
            else if (FileExtension.ToLower() == ".xlsx")
                WorkSheet = XSSFWorkBook.GetSheetAt(0);
            int UCRow = 1;
            foreach (var emp in listOfEmployees)
            {
                var newRow = WorkSheet.CreateRow(UCRow);
                var createdRow = WorkSheet.GetRow(UCRow);
                for (int i = 0; i < 8; i++)
                {
                    createdRow.CreateCell(i);
                }

                newRow.Cells[0].SetCellValue(emp.EmployeeID);
                newRow.Cells[1].SetCellValue(emp.EmployeeName);
                newRow.Cells[2].SetCellValue(emp.Location);

                DateTime? dob = emp.DOB;
                DateTime? doj = emp.DOJ;

                newRow.Cells[3].SetCellValue(dob.ToString());
                newRow.Cells[4].SetCellValue(doj.ToString());
                newRow.Cells[5].SetCellValue(emp.CTC);
                newRow.Cells[6].SetCellValue(emp.Designation);
                newRow.Cells[7].SetCellValue(emp.Email);


                using (System.IO.FileStream fs = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    HSSFWorkBook.Write(fs);
                }
                for (int i = 0; i < 8; i++)
                {
                    WorkSheet.GetRow(UCRow).GetCell(i).CellStyle = dataRowStyle;
                }
                UCRow++;

            }


        }

        public void ImportEmployees()
        {
            fileName = "C:\\Users\\Rajat\\Documents\\" + "OmniEmployees.xls";
            using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkBook = new HSSFWorkbook(file);
            }
            newEmployees = new List<EmployeeDBModel>();
            //Get worksheet object
            ISheet WorkSheet = null;
            FileExtension = Path.GetExtension(fileName);
            if (FileExtension.ToLower() == ".xls")
                WorkSheet = HSSFWorkBook.GetSheetAt(0);
            else if (FileExtension.ToLower() == ".xlsx")
                WorkSheet = XSSFWorkBook.GetSheetAt(0);

            //Get Header Row from WorkSheet
            IRow HeaderRow = WorkSheet.GetRow(0);

            if (HeaderRow != null)
            {
                System.Collections.IEnumerator Rows = WorkSheet.GetRowEnumerator();

                //Get number of columns in the WorkSheet
                int ColumnCount = HeaderRow.LastCellNum;

                //Get number of rows in the WorkSheet
                int RowCount = WorkSheet.LastRowNum;

                bool skipReadingHeaderRow = Rows.MoveNext();

                //Iterate through Rows in the worksheet and fill up the datatable
                while (Rows.MoveNext())//&& ignoreSheets == false)
                {
                    //int RowFormat = 0;
                    IRow Row = null;
                    if (FileExtension.ToLower() == ".xls")
                        Row = (HSSFRow)Rows.Current;
                    else if (FileExtension.ToLower() == ".xlsx")
                        Row = (XSSFRow)Rows.Current;

                    EmployeeDBModel db = new EmployeeDBModel();
                    db.EmployeeName = Row.Cells[0].StringCellValue;
                    db.Location = Row.Cells[1].StringCellValue;
                    db.DOB = (Row.Cells[2].DateCellValue);
                    db.DOJ = (Row.Cells[3].DateCellValue);
                    db.CTC = Convert.ToInt32(Row.Cells[4].NumericCellValue);
                    db.Designation = Row.Cells[5].StringCellValue;
                    db.Email = Row.Cells[6].StringCellValue;
                    newEmployees.Add(db);

                }
            }

            CreateNewEmployees();

        }

        private void CreateNewEmployees()
        {
            foreach (EmployeeDBModel db in newEmployees)
            {
                dbObj.EmployeeTable.Add(db);
                dbObj.SaveChanges();
            }
        }
    }

}
