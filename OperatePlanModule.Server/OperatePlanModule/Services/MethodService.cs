using MatrixLibrary;
using OperatePlanModule.Shared.DTO;
using System.Reflection;

namespace OperatePlanModule.Services
{
    public class MethodService
    {
        public string GetStringKeys(int[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] += 1;
            }
            return string.Join("-", keys);
        }
        public string GetStringDate(DateTime date)
        {
            string month = date.Month > 9 ? date.Month.ToString() : "0" + date.Month.ToString();
            string day = date.Day > 9 ? date.Day.ToString() : "0" + date.Day.ToString();
            string hour = date.Hour > 9 ? date.Hour.ToString() + ":" + "00" : "0" + date.Hour.ToString() + ":" + "00";
            return date.Year.ToString() + "-" + month + "-" + day + " " + hour;
        }
        public List<GanttTask> GetGanttData(Matrix matrix, int[] keys)
        {
            var ganttData = new List<GanttTask>();
            TimeSpan time = new TimeSpan(8, 00, 00); // hours, minutes, seconds
            DateTime machineStartTime = DateTime.Now.Date + time;

            for (int i = 0; i < matrix.Values.Length; i++)
            {
                for (int j = 0; j < matrix.Values[i].Length; j++)
                {
                    DateTime endTime;
                    if (i == 0)
                    {
                        double taskDuration = matrix.Values[i][j];
                        endTime = machineStartTime.AddHours(taskDuration);
                    }
                    else
                    {
                        double taskDuration = matrix.Values[i][j];
                        if (j != 0 && ganttData.FirstOrDefault(gd => gd.IdI == i - 1 && gd.IdJ == j).EndDate < ganttData.FirstOrDefault(gd => gd.IdI == i && gd.IdJ == j - 1).EndDate)
                        {
                            machineStartTime = ganttData.FirstOrDefault(gd => gd.IdI == i && gd.IdJ == j - 1).EndDate;
                        }
                        else
                        {
                            machineStartTime = ganttData.FirstOrDefault(gd => gd.IdI == i - 1 && gd.IdJ == j).EndDate;
                        }


                        endTime = machineStartTime.AddHours(taskDuration);
                    }

                    ganttData.Add(new GanttTask
                    {
                        IdI = i,
                        IdJ = j,
                        Machine = $"Станок {i + 1}",
                        Part = $"Деталь {keys[j] + 1}",
                        Start = GetStringDate(machineStartTime),
                        End = GetStringDate(endTime),
                        StartDate = machineStartTime,
                        EndDate = endTime,
                    });

                    machineStartTime = endTime;
                }
            }
            return ganttData;
        }
        public List<GanttList> MatrixMethods(Matrix matrix)
        {
            MatrixOperations matrixOperations = new MatrixOperations();
            MethodInfo[] methods = typeof(MatrixOperations).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
            .Where(m => m.ReturnType == typeof(ValueTuple<double[][], int[], double, string>)
                     && m.GetParameters().Length == 1
                     && m.GetParameters()[0].ParameterType == typeof(double[][]))
            .ToArray();

            List<GanttList> ganttList = [];
            int[] keys0 = new int[matrix.CountDetails];

            for (int i = 0; i < matrix.CountDetails; i++)
            {
                keys0[i] = i;
            }

            List<GanttTask> ganttData0 = GetGanttData(matrix, keys0);
            double sumTime0 = MatrixOperations.GetSumTime(matrix.Values);
            string methodName0 = "Отсутствует";

            ganttList.Add(new GanttList()
            {
                GanttTasks = ganttData0,
                Keys = GetStringKeys(keys0),
                AllTime = sumTime0,
                MethodName = methodName0
            });

            foreach (var method in methods)
            {
                var result = method.Invoke(matrixOperations, new object[] { matrix.Values });
                var (resultMatrix, keys, sumTime, methodName) = ((double[][], int[], double, string))result;
                Matrix matrixCopy = new Matrix()
                {
                    CountDetails = matrix.CountDetails,
                    CountMachines = matrix.CountMachines,
                    Values = matrix.Values,
                };
                matrixCopy.Values = resultMatrix;
                List<GanttTask> ganttData = GetGanttData(matrixCopy, keys);

                ganttList.Add(new GanttList()
                {
                    GanttTasks = ganttData,
                    Keys = GetStringKeys(keys),
                    AllTime = sumTime,
                    MethodName = methodName
                });
            }

            return ganttList;
        }
    }
}
