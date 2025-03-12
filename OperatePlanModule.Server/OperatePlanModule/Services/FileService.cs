using MatrixLibrary;
using OfficeOpenXml;
using OperatePlanModule.Shared.DTO;

namespace OperatePlanModule.Services
{
    public class FileService
    {
        public Matrix GenerateMatrix()
        {
            Random rnm = new Random();
            int countDetails = rnm.Next(3, 100);
            int countMachines = rnm.Next(3, 100);
            
            double[][] values = new double[countMachines][];
            for (int i = 0; i < countMachines; i++)
            {
                values[i] = new double[countDetails]; 
            }

            for (int i = 0; i < countMachines; i++)
            {
                for (int j = 0; j < countDetails; j++)
                {
                    values[i][j] = Convert.ToDouble(rnm.Next(3, 10));
                    
                }
            }

            Matrix matrix = new Matrix()
            {
                CountDetails = countDetails,
                CountMachines = countMachines,
                Values = values,
            };

            return matrix;
        }
        public string SaveMatrixToExcel(Matrix matrix)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var filePath = Path.Combine("C:\\учеба\\8 семестр\\MES\\ExcelFiles", $"matrix[{matrix.CountMachines}][{matrix.CountDetails}].xlsx");

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Matrix");

                for (int i = 0; i < matrix.Values.Length; i++)
                {
                    for (int j = 0; j < matrix.Values[i].Length; j++)
                    {
                        worksheet.Cells[i + 1, j + 1].Value = matrix.Values[i][j];
                    }
                }

                package.SaveAs(new FileInfo(filePath));
            }

            return $"/matrix[{matrix.CountMachines}][{matrix.CountDetails}].xlsx"; 
        }

        public Matrix GetFromFile(IFormFile file)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            double[][] values;
            Matrix matrix = new Matrix();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                stream.Position = 0; 

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rows = worksheet.Dimension.Rows;
                    var columns = worksheet.Dimension.Columns;

                    values = new double[rows][];
                    for (int i = 0; i < rows; i++)
                    {
                        values[i] = new double[columns]; 
                    }

                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            values[i][j] = (double)(worksheet.Cells[i + 1, j + 1].Value ?? 0); 
                        }
                    }
                    matrix = new Matrix() { CountMachines = rows, CountDetails = columns, Values = values };
                }
            }
            return matrix;
        }
    }
}
