using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ProjReadExcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Data
            DataTable dt = new DataTable();

            var xls = new XLWorkbook(@"D:\Input\Teste.xlsx");
            var planilha = xls.Worksheets.First(w => w.Name == "Planilha1");
            var totalLinhas = planilha.Rows().Count();

            //Get Columns
            List<String> columns = new List<string>();
            var qtdColumns = planilha.Columns().Count();
            for (int i = 1; i < qtdColumns; i++)
            {
                var col = planilha.Column(i).FirstCell().Value.ToString();
                dt.Columns.Add(col);
                columns.Add(col);
            }
            //Get Values
            var d = new string[columns.Count()];
            for (int l = 2; l <= totalLinhas; l++)
            {
                int index = 0;
                for (int i = 1; i <= columns.Count(); i++)
                {
                    d[index] = planilha.Column(i).Cell(l).Value.ToString();
                    index++;
                }
                dt.Rows.Add(d);
            }

            //Example Data Order by
            dt.DefaultView.Sort = "cep ASC";
            dt = dt.DefaultView.ToTable();

            //Print Data
            StringBuilder sbArquivo = new StringBuilder();

            for (int i = 1; i < dt.Rows.Count -1; i++)
            {
                int j = 0;
                foreach (var item in columns)
                {
                    sbArquivo.Append(item + ": " + dt.Rows[i][j] + Environment.NewLine);
                    j++;
                }
            }
            Console.WriteLine(sbArquivo.ToString());    
        }
    }
}
