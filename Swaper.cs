﻿using System;
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
            int x = 0;

            for (int i = 0; i < indices.Length; i++)
            {
                if (i == 0)
                    x = matrix[indices[i], 0];
                else
                    x += Math.Max(matrix[indices[i], 0] - matrix[indices[i - 1], 1], 0);
            }

            for (int i = 0; i < indices.Length; i++)
                x += matrix[indices[i], 1];

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
    }

}
