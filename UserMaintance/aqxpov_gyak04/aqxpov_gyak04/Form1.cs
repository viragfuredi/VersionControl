using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace aqxpov_gyak04
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;



        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;


        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }

        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();

                xlWB = xlApp.Workbooks.Add(Missing.Value);

                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
               "Kód",
               "Eladó",
               "Oldal",
               "Kerület",
               "Lift",
               "Szobák száma",
               "Alapterület (m2)",
               "Ár (mFt)",
               "Négyzetméter ár (Ft/m2)"
            };

            for (int i = 0; i < headers.Length; i++)

            {
                xlSheet.Cells[1, 1 + i] = headers[i];
            }

            object[,] values = new object[Flats.Count, headers.Length];


            int counter = 0;
            foreach (Flat x in Flats)
            {
                values[counter, 0] = x.Code;
                values[counter, 1] = x.Vendor;
                values[counter, 2] = x.Side;
                values[counter, 3] = x.District;
                values[counter, 4] = x.Elevator;
                values[counter, 5] = x.NumberOfRooms;
                values[counter, 6] = x.FloorArea;
                values[counter, 7] = x.Price.ToString();
                values[counter, 8] = "";
                counter++;
            }

            

            xlSheet.get_Range(
                GetCell(2, 1),
                GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;

            FormatTable();

        }

        void FormatTable()
        {
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1,9));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);


            Excel.Range tableRange = xlSheet.get_Range(GetCell(2, 9), GetCell(2, 9));
            tableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range thirdRange = xlSheet.get_Range(GetCell(2, 8), GetCell(2, 9));
            thirdRange.Interior.Color = Color.LightGoldenrodYellow;

            Excel.Range fourthRange = xlSheet.get_Range(GetCell(8, 9), GetCell(8, 9));
            fourthRange.Interior.Color = Color.LightSeaGreen;
            



        }

        private string GetCell(int x, int y)
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        
    }
        

   
}


