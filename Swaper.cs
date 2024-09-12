using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modeling1
{
    internal class Swaper
    {
        public static int[,] GetBestPermutation(int[,] matrix)
        {
            int[] indices = new int[matrix.GetLength(0)];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = i;

            int[] bestPermutation = (int[])indices.Clone();
            int minDowntime = int.MaxValue;

            do
            {
                int currentDowntime = CalculateDowntime(matrix, indices);
                if (currentDowntime < minDowntime)
                {
                    minDowntime = currentDowntime;
                    bestPermutation = (int[])indices.Clone();
                }
            } while (NextPermutation(indices));

            // Создаем новую матрицу для лучшей перестановки
            int[,] bestMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < bestPermutation.Length; i++)
            {
                bestMatrix[i, 0] = matrix[bestPermutation[i], 0];
                bestMatrix[i, 1] = matrix[bestPermutation[i], 1];
            }

            return bestMatrix;
        }

        static int CalculateDowntime(int[,] matrix, int[] indices)
        {
            int downtime = 0;

            for (int i = 0; i < indices.Length; i++)
            {
                if (i == 0)
                    downtime = matrix[indices[i], 0];
                else
                    downtime += Math.Max(matrix[indices[i], 0] - matrix[indices[i - 1], 1], 0);
            }

            for (int i = 0; i < indices.Length; i++)
                downtime += matrix[indices[i], 1];

            return downtime;
        }

        static int CalculateDowntimeNx3(int[,] matrix, int[] indices)
        {
            int xk = 0, xh = 0;

            for (int i = 0; i < indices.Length; i++)
            {
                xk += Math.Max(matrix[indices[i], 0] - matrix[indices[i], 1], 0);
                xh += Math.Max(matrix[indices[i], 1] - matrix[indices[i], 2], 0);
            }

            int x = xk + xh;

            for (int i = 0; i < indices.Length; i++)
            {
                x += matrix[indices[i], 2];
            }

            return x;
        }

        static bool NextPermutation(int[] arr)
        {
            int n = arr.Length;
            int i = n - 2;

            while (i >= 0 && arr[i] >= arr[i + 1])
                i--;

            if (i < 0)
                return false;

            int j = n - 1;
            while (arr[j] <= arr[i])
                j--;

            Swap(arr, i, j);
            Array.Reverse(arr, i + 1, n - i - 1);
            return true;
        }

        static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public static int[,] GetBestPermutationNx3(int[,] matrix)
        {
            int[] indices = new int[matrix.GetLength(0)];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = i;

            int[] bestPermutation = (int[])indices.Clone();
            int minDowntime = int.MaxValue;

            do
            {
                int currentDowntime = CalculateDowntimeNx3(matrix, indices);
                if (currentDowntime < minDowntime)
                {
                    minDowntime = currentDowntime;
                    bestPermutation = (int[])indices.Clone();
                }
            } while (NextPermutation(indices));

            // Создаем новую матрицу для лучшей перестановки
            int[,] bestMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < bestPermutation.Length; i++)
            {
                bestMatrix[i, 0] = matrix[bestPermutation[i], 0];
                bestMatrix[i, 1] = matrix[bestPermutation[i], 1];
                bestMatrix[i, 2] = matrix[bestPermutation[i], 2];
            }

            return bestMatrix;
        }
    }
}


