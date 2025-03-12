using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    public class MatrixOperations
    {
        public static double[][] CopyMatrix(double[][] matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            double[][] copy = new double[matrix.Length][];
            for (int i = 0; i < matrix.Length; i++)
            {
                copy[i] = new double[matrix[i].Length];
                Array.Copy(matrix[i], copy[i], matrix[i].Length);
            }
            return copy;
        }
        public static double GetSumTime(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] resultMatrix = new double[rows][];

            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }
            
            for (int i = 0; i < rows; i++)
            {
                if (i == 0)
                {
                    resultMatrix[i][0] = matrix[i][0];
                }
                else
                {
                    resultMatrix[i][0] = matrix[i][0] + resultMatrix[i - 1][0];
                }
            }
            for (int i = 0; i < cols; i++)
            {
                if (i == 0)
                {
                    resultMatrix[0][i] = matrix[0][i];
                }
                else
                {
                    resultMatrix[0][i] += matrix[0][i] + resultMatrix[0][i - 1];
                }
            }
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    if (resultMatrix[i][j - 1] > resultMatrix[i - 1][j])
                    {
                        resultMatrix[i][j] = matrix[i][j] + resultMatrix[i][j - 1];
                    }
                    else
                    {
                        resultMatrix[i][j] = matrix[i][j] + resultMatrix[i - 1][j];
                    }
                }
            }
            return resultMatrix[rows - 1][cols - 1];
        }
        public static (double[][], int[], double, string) FirstGeneraliteJhonson(double[][] matrix)
        {
            int rows = matrix.Length; 
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }
            
            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++) {
                keys[key] = key;
            }

            Array.Sort(keys, (a, b) => matrixCopy[0][a].CompareTo(matrixCopy[0][b]));

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }
            return (resultMatrix, keys, GetSumTime(resultMatrix), "Джонсон 1");
        }
        public static (double[][], int[], double, string) SecondGeneraliteJhonson(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }

            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++)
            {
                keys[key] = key;
            }

            Array.Sort(keys, (a, b) => matrixCopy[rows - 1][b].CompareTo(matrixCopy[rows - 1][a]));


            // Заполняем новую матрицу отсортированными значениями
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }
            return (resultMatrix, keys, GetSumTime(resultMatrix), "Джонсон 2");
        }
        public static (double[][], int[], double, string) ThirdGeneraliteJhonson(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }

            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++)
            {
                keys[key] = key;
            }

            double[] maxDetailTime = new double[cols];
            (int, int)[] maxDetailIndexList = new (int, int)[cols];
            for (int i = 0; i < cols; i++)
            {
                maxDetailTime[i] = 0;
            }
            for (int i = cols - 1; i >= 0; i--)
            {
                for (int j = rows - 1; j >= 0; j--)
                {
                    if (matrixCopy[j][i] > maxDetailTime[i] )
                    {
                        maxDetailTime[i] = matrixCopy[j][i];
                        maxDetailIndexList[i] = (j, i);
                    }
                }
            }

            Array.Sort(keys, (a, b) => maxDetailIndexList[b].CompareTo(maxDetailIndexList[a]));
            Array.Sort(keys, (a, b) => maxDetailTime[b].CompareTo(maxDetailTime[a]));

            // Заполняем новую матрицу отсортированными значениями
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }
            return (resultMatrix, keys, GetSumTime(resultMatrix), "Джонсон 3");
        }
        public static (double[][], int[], double, string) FourthGeneraliteJhonson(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }

            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++)
            {
                keys[key] = key;
            }

            double[] sumDetailsTimeList = new double[cols];

            // Заполняем новую матрицу отсортированными значениями
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sumDetailsTimeList[j] += matrixCopy[i][j];
                }
            }

            Array.Sort(keys, (a, b) => sumDetailsTimeList[b].CompareTo(sumDetailsTimeList[a]));

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }

            return (resultMatrix, keys, GetSumTime(resultMatrix), "Джонсон 4");
        }
        public static (double[][], int[], double, string) FifthGeneraliteJhonson(double[][] matrix)
        {
            double[][] matrix1;
            int[] firstKeys;
            double sumTime1;
            int[] secondKeys;
            int[] thirdKeys;
            int[] fourthKeys;
            string name;

            (matrix1, firstKeys, sumTime1, name) = FirstGeneraliteJhonson(matrix);
            (matrix1, secondKeys, sumTime1, name) = SecondGeneraliteJhonson(matrix);
            (matrix1, thirdKeys, sumTime1, name) = ThirdGeneraliteJhonson(matrix);
            (matrix1, fourthKeys, sumTime1, name) = FourthGeneraliteJhonson(matrix);

            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }

            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++)
            {
                keys[key] = key;
            }
            int[] sumKeys = new int[cols];

            for (int i = 0; i < cols; i++)
            {
                sumKeys[i] = 0;
            }

            for (int i = 0; i < cols; i++)
            {
                if (firstKeys.FirstOrDefault(f => f == i) != null)
                {
                    sumKeys[i] += Array.IndexOf(firstKeys, firstKeys.FirstOrDefault(f => f == i)) + 1;
                }
                if (secondKeys.FirstOrDefault(f => f == i) != null)
                {
                    sumKeys[i] += Array.IndexOf(secondKeys, secondKeys.FirstOrDefault(f => f == i)) + 1;
                }
                if (thirdKeys.FirstOrDefault(f => f == i) != null)
                {
                    sumKeys[i] += Array.IndexOf(thirdKeys, thirdKeys.FirstOrDefault(f => f == i)) + 1;
                }
                if (fourthKeys.FirstOrDefault(f => f == i) != null)
                {
                    sumKeys[i] += Array.IndexOf(fourthKeys, fourthKeys.FirstOrDefault(f => f == i)) + 1;
                }
            }

            Array.Sort(keys, (a, b) => sumKeys[a].CompareTo(sumKeys[b]));

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }

            return (resultMatrix, keys, GetSumTime(resultMatrix), "Джонсон 5");
        }
        public static (double[][], int[], double, string) FirstPetrovSocolitsin(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }

            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++)
            {
                keys[key] = key;
            }

            double[] sumDetailsTimeList1 = new double[cols];
            double[] sumDetailsTimeList2 = new double[cols];
            double[] diffDetailsTimeList = new double[cols];
            double sum = 0;

            // Заполняем новую матрицу отсортированными значениями
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows - 1; j++)
                {
                    sum += matrixCopy[j][i];
                }
                sumDetailsTimeList1[i] = sum;
                sum = 0;
            }

            Array.Sort(keys, (a, b) => sumDetailsTimeList1[a].CompareTo(sumDetailsTimeList1[b]));

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }

            return (resultMatrix, keys, GetSumTime(resultMatrix), "Петров-Соколицин (сумма 1)");
        }
        public static (double[][], int[], double, string) SecondPetrovSocolitsin(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }

            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++)
            {
                keys[key] = key;
            }

            double[] sumDetailsTimeList2 = new double[cols];
            double sum = 0;

            for (int i = 0; i < cols; i++)
            {
                for (int j = 1; j < rows; j++)
                {
                    sum += matrixCopy[j][i];
                }
                sumDetailsTimeList2[i] = sum;
                sum = 0;
            }

            Array.Sort(keys, (a, b) => sumDetailsTimeList2[b].CompareTo(sumDetailsTimeList2[a]));

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }

            return (resultMatrix, keys, GetSumTime(resultMatrix), "Петров-Соколицин (сумма 2)");
        }
        public static (double[][], int[], double, string) ThirdPetrovSocolitsin(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            double[][] matrixCopy = CopyMatrix(matrix);

            double[][] resultMatrix = new double[rows][];
            for (int m = 0; m < rows; m++)
            {
                resultMatrix[m] = new double[cols];
            }

            int[] keys = new int[cols];
            for (int key = 0; key < cols; key++)
            {
                keys[key] = key;
            }

            double[] sumDetailsTimeList1 = new double[cols];
            double[] sumDetailsTimeList2 = new double[cols];
            double[] diffDetailsTimeList = new double[cols];
            double sum = 0;

            // Заполняем новую матрицу отсортированными значениями
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows - 1; j++)
                {
                    sum += matrixCopy[j][i];
                }
                sumDetailsTimeList1[i] = sum;
                sum = 0;
            }

            for (int i = 0; i < cols; i++)
            {
                for (int j = 1; j < rows; j++)
                {
                    sum += matrixCopy[j][i];
                }
                sumDetailsTimeList2[i] = sum;
                sum = 0;
            }

            for (int i = 0; i < cols; i++)
            {
                diffDetailsTimeList[i] = sumDetailsTimeList2[i] - sumDetailsTimeList1[i];
            }

            Array.Sort(keys, (a, b) => diffDetailsTimeList[b].CompareTo(diffDetailsTimeList[a]));

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    resultMatrix[j][i] = matrixCopy[j][keys[i]];
                }
            }

            return (resultMatrix, keys, GetSumTime(resultMatrix), "Петров-Соколицин (разность)");
        }
    }
}
