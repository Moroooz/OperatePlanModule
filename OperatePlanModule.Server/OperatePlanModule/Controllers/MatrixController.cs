using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OperatePlanModule.Services;
using OperatePlanModule.Shared.DTO;
using MatrixLibrary;
using System.Reflection;

namespace OperatePlanModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatrixController : ControllerBase
    {
        [HttpGet("generate/matrix")]
        public Matrix GetNewMatrix(FileService fileService)
        {
            return fileService.GenerateMatrix();
        }
        [HttpPost("generate/file")]
        public IActionResult GenerateExcelFile(FileService fileService)
        {
            var matrix = fileService.GenerateMatrix();
            var filePath = fileService.SaveMatrixToExcel(matrix);
            return Ok(new { FilePath = filePath });
        }
        [HttpPost("upload")]
        public Matrix UploadMatrixFromExcel(FileService fileService, IFormFile file)
        {
            if (file.Length == 0 || !file.FileName.EndsWith(".xlsx"))
                return null;
            Matrix matrix = fileService.GetFromFile(file);
            return matrix;
        }
        [HttpPost("methods")]
        public List<GanttList> MatrixMethods(MethodService methodService, Matrix matrix)
        {
            List<GanttList> ganttList = [];
            ganttList = methodService.MatrixMethods(matrix);
            return ganttList;
        }






            //MatrixOperations matrixOperations = new MatrixOperations();
            //MethodInfo[] methods = typeof(MatrixOperations).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            //.Where(m => m.ReturnType == typeof(ValueTuple<double[][], int[], double, string>)
            //         && m.GetParameters().Length == 1
            //         && m.GetParameters()[0].ParameterType == typeof(double[][]))
            //.ToArray();


            //List<GanttList> ganttList = [];
            //int[] keys0 = new int[matrix.CountDetails];

            //for (int i = 0; i < matrix.CountDetails; i++)
            //{
            //    keys0[i] = i;
            //}
            //List<GanttTask> ganttData0 = fileService.GetGanttData(matrix, keys0);
            //double sumTime0 = MatrixOperations.GetSumTime(matrix.Values);
            //string methodName0 = "Отсутствует";

            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData0,
            //    Keys = fileService.GetStringKeys(keys0),
            //    AllTime = sumTime0,
            //    MethodName = methodName0
            //});

            //foreach (var method in methods)
            //{
            //    var result = method.Invoke(matrixOperations, new object[] { matrix.Values });
            //    var (resultMatrix, keys, sumTime, methodName) = ((double[][], int[], double, string)) result;
            //    Matrix matrixCopy = new Matrix() 
            //    {
            //        CountDetails = matrix.CountDetails,
            //        CountMachines = matrix.CountMachines,
            //        Values = matrix.Values,
            //    };
            //    matrixCopy.Values = resultMatrix;
            //    List<GanttTask> ganttData = fileService.GetGanttData(matrixCopy, keys);

            //    ganttList.Add(new GanttList()
            //    {
            //        GanttTasks = ganttData,
            //        Keys = fileService.GetStringKeys(keys),
            //        AllTime = sumTime,
            //        MethodName = methodName
            //    });

            // Выводим результаты
            //Console.WriteLine($"Результаты метода '{method.Name}':");
            //Console.WriteLine("Массив:");
            //foreach (var arr in modifiedArray)
            //{
            //    Console.WriteLine(string.Join(", ", arr));
            //}
            //Console.WriteLine("Индексы: " + string.Join(", ", indices));
            //Console.WriteLine("Сумма: " + sum);
            //Console.WriteLine();
            //}

            //int[] keys1;
            //int[] keys2;
            //int[] keys3;
            //int[] keys4;
            //int[] keys5;
            //int[] keys6;
            //int[] keys7;
            //int[] keys8;

            //double sumTime1;
            //double sumTime2;
            //double sumTime3;
            //double sumTime4;
            //double sumTime5;
            //double sumTime6;
            //double sumTime7;
            //double sumTime8;

            //var resultValues1 = new double[matrix.CountMachines][];
            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues1[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}

            //var resultValues2 = new double[matrix.CountMachines][];

            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues2[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}
            //var resultValues3 = new double[matrix.CountMachines][];

            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues3[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}
            //var resultValues4 = new double[matrix.CountMachines][];

            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues4[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}

            //var resultValues5 = new double[matrix.CountMachines][];
            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues5[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}

            //var resultValues6 = new double[matrix.CountMachines][];
            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues6[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}

            //var resultValues7 = new double[matrix.CountMachines][];
            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues7[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}
            //var resultValues8 = new double[matrix.CountMachines][];
            //for (int i = 0; i < matrix.CountMachines; i++)
            //{
            //    resultValues8[i] = new double[matrix.CountDetails]; // where 'm' is a variable
            //}

            //(resultValues1, keys1, sumTime1) = MatrixOperations.FirstGeneraliteJhonson(matrix.Values);
            //(resultValues2, keys2, sumTime2) = MatrixOperations.SecondGeneraliteJhonson(matrix.Values);
            //(resultValues3, keys3, sumTime3) = MatrixOperations.ThirdGeneraliteJhonson(matrix.Values);
            //(resultValues4, keys4, sumTime4) = MatrixOperations.FourthGeneraliteJhonson(matrix.Values);
            //(resultValues5, keys5, sumTime5) = MatrixOperations.FifthGeneraliteJhonson(matrix.Values, keys1, keys2, keys3, keys4);
            //(resultValues6, keys6, sumTime6) = MatrixOperations.FirstPetrovSocolitsin(matrix.Values);
            //(resultValues7, keys7, sumTime7) = MatrixOperations.SecondPetrovSocolitsin(matrix.Values);
            //(resultValues8, keys8, sumTime8) = MatrixOperations.ThirdPetrovSocolitsin(matrix.Values);

            //matrix.Values = resultValues1;
            //List<GanttTask> ganttData1 = fileService.GetGanttData(matrix, keys1);
            //matrix.Values = resultValues2;
            //List<GanttTask> ganttData2 = fileService.GetGanttData(matrix, keys2);
            //matrix.Values = resultValues3;
            //List<GanttTask> ganttData3 = fileService.GetGanttData(matrix, keys3);
            //matrix.Values = resultValues4;
            //List<GanttTask> ganttData4 = fileService.GetGanttData(matrix, keys4);
            //matrix.Values = resultValues5;
            //List<GanttTask> ganttData5 = fileService.GetGanttData(matrix, keys5);
            //matrix.Values = resultValues6;
            //List<GanttTask> ganttData6 = fileService.GetGanttData(matrix, keys6);
            //matrix.Values = resultValues7;
            //List<GanttTask> ganttData7 = fileService.GetGanttData(matrix, keys7);
            //matrix.Values = resultValues8;
            //List<GanttTask> ganttData8 = fileService.GetGanttData(matrix, keys8);

            //List<GanttList> ganttList = [];
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData0,
            //    Keys = fileService.GetStringKeys(keys0),
            //    AllTime = sumTime0,
            //    MethodName = methodName0
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData1,
            //    Keys = fileService.GetStringKeys(keys1),
            //    AllTime = sumTime1,
            //    MethodName = "Джонсон 1"
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData2,
            //    Keys = fileService.GetStringKeys(keys2),
            //    AllTime = sumTime2,
            //    MethodName = "Джонсон 2"
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData3,
            //    Keys = fileService.GetStringKeys(keys3),
            //    AllTime = sumTime3,
            //    MethodName = "Джонсон 3"
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData4,
            //    Keys = fileService.GetStringKeys(keys4),
            //    AllTime = sumTime4,
            //    MethodName = "Джонсон 4"
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData5,
            //    Keys = fileService.GetStringKeys(keys5),
            //    AllTime = sumTime5,
            //    MethodName = "Джонсон 5"
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData6,
            //    Keys = fileService.GetStringKeys(keys6),
            //    AllTime = sumTime6,
            //    MethodName = "Петров-Соколицин (сумма 1)"
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData7,
            //    Keys = fileService.GetStringKeys(keys7),
            //    AllTime = sumTime7,
            //    MethodName = "Петров-Соколицин (сумма 2)"
            //});
            //ganttList.Add(new GanttList()
            //{
            //    GanttTasks = ganttData8,
            //    Keys = fileService.GetStringKeys(keys8),
            //    AllTime = sumTime8,
            //    MethodName = "Петров-Соколицин (разность)"
            //});

        //}

    }
}

