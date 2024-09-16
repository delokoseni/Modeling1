﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modeling1
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private Graphics g;
        private int[,] task1;
        private int[,] task2;
        public Form1()
        {
            InitializeComponent();
            g = CreateGraphics();
            DisplayPartColors(); // Вызов метода для отображения цветов деталей
            labelI.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            labelA.Visible = false;
            labelA1.Visible = false;
            labelA2.Visible = false;
            labelA3.Visible = false;
            labelA4.Visible = false;
            labelA5.Visible = false;
            labelB.Visible = false;
            labelB1.Visible = false;
            labelB2.Visible = false;
            labelB3.Visible = false;
            labelB4.Visible = false;
            labelB5.Visible = false;
            labelC.Visible = false;
            labelC1.Visible = false;
            labelC2.Visible = false;
            labelC3.Visible = false;
            labelC4.Visible = false;
            labelC5.Visible = false;
            buttonRun1.Visible = false;
            buttonRun12.Visible = false;
            buttonRun21.Visible = false;
            buttonRun22.Visible = false;
            this.BackColor = Color.White;
        }

        private void Form_Load(object sender, EventArgs e) => WindowState = FormWindowState.Maximized;

        #region firts task

        private void DisplayPartColors()
        {
            // Создаем панель для размещения цветных квадратиков
            Panel colorPanel = new Panel();
            colorPanel.Location = new Point(200, 15);
            colorPanel.Size = new Size(200, 150);
            colorPanel.AutoScroll = true;

            // Создаем массив цветов для деталей
            Color[] colors = { Color.Red, Color.Orange, Color.Yellow, Color.LightGreen, Color.Purple };

            // Создаем метки и цветные квадратики
            for (int i = 0; i < 5; i++)
            {
                Label partLabel = new Label();
                partLabel.Text = $"Деталь {i + 1}";
                partLabel.Font = new Font("Arial", 12);
                partLabel.AutoSize = true;
                partLabel.Location = new Point(10, i * 30);

                Panel colorSquare = new Panel();
                colorSquare.Size = new Size(20, 20);
                colorSquare.BackColor = colors[i];
                colorSquare.Location = new Point(partLabel.Width + 20, partLabel.Top);

                colorPanel.Controls.Add(partLabel);
                colorPanel.Controls.Add(colorSquare);
            }

            // Добавляем панель на форму
            this.Controls.Add(colorPanel);
        }

        /**
         * Работа с заданием №1
         */
        private void buttonTask1_Click(object sender, EventArgs e)
        {
            buttonRun21.Visible = false;
            buttonRun22.Visible = false;
            g.Clear(Color.White);

            /**
             * Значения, с которыми работает программа
             */
            task1 = new int[5, 2];
            string filePath = "C:\\Users\\artur\\source\\repos\\Modeling1\\task1.txt"; 
            ArrayLoader.LoadArrayFromFile(filePath, task1);
            LoadDataIntoLabels(task1, true);

            labelDowntime.Location = new Point(13, 142);
            buttonRun1.Visible = true;
            buttonRun12.Visible = true;
            findAmountOfDowntime2xn();
            DrawGanttNx2();
        }

        private void buttonSort1_Click(object sender, EventArgs e)
        {
            task1 = new int[5, 2];
            string filePath = "C:\\Users\\artur\\source\\repos\\Modeling1\\task1.txt";
            ArrayLoader.LoadArrayFromFile(filePath, task1);

            task1 = sort2xn();
            LoadDataIntoLabels(task1, true);
            findAmountOfDowntime2xn();
            g.Clear(Color.White);
            DrawGanttNx2();
        }

        private void buttonSort12_Click(object sender, EventArgs e)
        {
            task1 = new int[5, 2];
            string filePath = "C:\\Users\\artur\\source\\repos\\Modeling1\\task1.txt";
            ArrayLoader.LoadArrayFromFile(filePath, task1);

            task1 = Swaper.GetBestPermutation(task1);
            LoadDataIntoLabels(task1, true);
            g.Clear(Color.White);
            findAmountOfDowntime2xn();
            DrawGanttNx2();
        }

        /**
         * Алгоритм Джонсона для матрицы Nx2
         */
        private int[,] sort2xn()
        {
            int[,] matrix = new int[task1.GetLength(0), task1.GetLength(1)];
            int linesCount = task1.GetLength(0);
            int clolumsCount = task1.GetLength(1);
            int top = 0;
            int button = 4;
            while (linesCount > 0)
            {
                int minElement = task1[0, 0];
                int colum = 0;
                int row = 0;
                //поиск минимального элемента
                for (int i = 0; i < linesCount; i++)
                {
                    for (int j = 0; j < clolumsCount; j++)
                    {
                        if (task1[i, j] < minElement)
                        {
                            minElement = task1[i, j];
                            row = i;
                            colum = j;
                        }
                    }
                }
                //если в первом столбце, то записываю наверх
                if (colum == 0)
                {
                    matrix[top, 0] = task1[row, 0];
                    matrix[top, 1] = task1[row, 1];
                    top++;
                }
                //во втором - записываю вниз
                else
                {
                    matrix[button, 0] = task1[row, 0];
                    matrix[button, 1] = task1[row, 1];
                    button--;
                }
                //удаляю строку из первоначального массива
                for (int i = row; i < linesCount - 1; i++)
                    for (int j = 0; j < clolumsCount; j++)
                        task1[i, j] = task1[i + 1, j];
                linesCount--;
            }
            return matrix;
        }

        private void findAmountOfDowntime2xn()
        {
            int[] x = new int[task1.GetLength(0)];

            // Вычисляем простои для каждого задания
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                int sumTask1 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask1 += task1[n, 0];
                }

                int sumDowntime = 0;
                int sumTask1Duration = 0;
                for (int m = 0; m < i; m++)
                {
                    sumDowntime += x[m];
                    sumTask1Duration += task1[m, 1];
                }

                x[i] = Math.Max(0, sumTask1 - sumDowntime - sumTask1Duration);
            }

            // Вычисляем общее время окончания обработки
            int totalTime = 0;
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                totalTime += x[i];
                totalTime += task1[i, 1];
            }

            labelDowntime.Location = new Point(13, 142);
            labelDowntime.Text = "Время окончания обработки: " + totalTime;
        }


        private void DrawGanttNx2()
        {
            // Определяем кисти для каждого цвета
            SolidBrush sb1 = new SolidBrush(Color.Red);
            SolidBrush sb2 = new SolidBrush(Color.Orange);
            SolidBrush sb3 = new SolidBrush(Color.Yellow);
            SolidBrush sb4 = new SolidBrush(Color.LightGreen);
            SolidBrush sb5 = new SolidBrush(Color.Purple);
            SolidBrush sbDowntime = new SolidBrush(Color.LightGray);

            // Начальная позиция для графиков
            int x = 10; // Измените X для отступа от левого края
            int y = 200;
            int squareSize = 10; // Размер квадратика
            int spacing = 2; // Расстояние между квадратиками

            // Отрисовка графиков Ганта и названий станков
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                int width = task1[i, 0]; // Ширина в единицах
                int numSquares = width; // Количество квадратиков

                // Рисуем квадратики в зависимости от индекса
                for (int j = 0; j < numSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    if (i == 0)
                        g.FillRectangle(sb1, square);
                    else if (i == 1)
                        g.FillRectangle(sb2, square);
                    else if (i == 2)
                        g.FillRectangle(sb3, square);
                    else if (i == 3)
                        g.FillRectangle(sb4, square);
                    else
                        g.FillRectangle(sb5, square);
                }

                // Перемещаем позицию вправо с учетом ширины и расстояния между квадратиками
                x += (numSquares * (squareSize + spacing));
            }

            // Рисуем текст названия станка только для A и B
            g.DrawString("Станок A", this.Font, Brushes.Black, new Point(10, y - 20));
            g.DrawString("Станок B", this.Font, Brushes.Black, new Point(10, y + 30));

            // Отрисовка простоев
            y += 50; // Смещение вниз для следующего ряда
            x = 10; // Сброс X для простоев
            int[] downtime = new int[task1.GetLength(0)];
            // Рисуем простои для первого станка
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                int sumTask1 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask1 += task1[n, 0];
                }

                int sumDowntime = 0;
                int sumTask1Duration = 0;
                for (int m = 0; m < i; m++)
                {
                    sumDowntime += downtime[m];
                    sumTask1Duration += task1[m, 1];
                }

                downtime[i] = Math.Max(0, sumTask1 - sumDowntime - sumTask1Duration);
                int numDowntimeSquares = downtime[i]; // Количество квадратиков для простоев
                for (int j = 0; j < numDowntimeSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    g.FillRectangle(sbDowntime, square);
                }
                x += (numDowntimeSquares * (squareSize + spacing)); // Обновляем x с учетом простоев

                int duration = task1[i, 1];
                int numDurationSquares = duration; // Количество квадратиков для длительности
                for (int j = 0; j < numDurationSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    if (i == 0)
                        g.FillRectangle(sb1, square);
                    else if (i == 1)
                        g.FillRectangle(sb2, square);
                    else if (i == 2)
                        g.FillRectangle(sb3, square);
                    else if (i == 3)
                        g.FillRectangle(sb4, square);
                    else
                        g.FillRectangle(sb5, square);
                }
                x += (numDurationSquares * (squareSize + spacing)); // Обновляем x с учетом длительности
            }
        }

        #endregion

        private void RestoreTask2FromTask1(int[,] task2Copy)
        {
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                task2[i, 0] = task1[i, 0] - task2Copy[i, 1]; // Восстанавливаем значения A
                task2[i, 1] = task2Copy[i, 1]; // Значения B остаются теми же
                task2[i, 2] = task1[i, 1] - task2Copy[i, 1]; // Восстанавливаем значения C
            }
            LoadDataIntoLabels(task2, false);
        }


        /**
         * Работа с заданием №2
         */
        private void buttonTask2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            buttonRun1.Visible = false;
            buttonRun12.Visible = false;

            /**
             * Значения, с которыми работает программа
             */
            task2 = new int[5, 3];
            string filePath = "C:\\Users\\artur\\source\\repos\\Modeling1\\task2.txt";
            ArrayLoader.LoadArrayFromFile(filePath, task2);
            LoadDataIntoLabels(task2, false);

            labelDowntime.Location = new Point(13, 142);
            buttonRun21.Visible = true;
            buttonRun22.Visible = true;
            findAmountOfDowntime3xn();
            DrawGanttNx3();
        }

        //по алгоритму
        private void buttonRun21_Click(object sender, EventArgs e)
        {
            task2 = new int[5, 3];
            string filePath = "C:\\Users\\artur\\source\\repos\\Modeling1\\task2.txt";
            ArrayLoader.LoadArrayFromFile(filePath, task2);
            int[,] task2Copy = (int[,])task2.Clone(); // Создаем копию task2

            if (CheckData())
            {

                // Приводим task2 к формату Nx2 и создаем копию task1
                convertToNx2(task2);
                int[,] task1Copy = (int[,])task1.Clone(); // Создаем копию task1 после преобразования
                task1 = sort2xn(); // Сортируем task1

                // Находим порядок строк и переставляем строки в task2
                int[] newOrder = FindRowOrder(task1, task1Copy);
                RearrangeTask2(task2, newOrder);

                // Обновляем интерфейс
                LoadDataIntoLabels(task2, false);
                g.Clear(Color.White);
                findAmountOfDowntime3xn();
                DrawGanttNx3();

            }
            else
            {
                // Показываем сообщение о том, что условие не выполняется
                MessageBox.Show("Условие не выполняется, результат был найден перебором", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                task2 = Swaper.GetBestPermutationNx3(task2);
                LoadDataIntoLabels(task2, false);
                g.Clear(Color.White);
                findAmountOfDowntime3xn();
                DrawGanttNx3();
            }

        }

        private int[] FindRowOrder(int[,] task1, int[,] task1Copy)
        {
            int rows = task1.GetLength(0);
            int[] order = new int[rows];
            bool[] matched = new bool[rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    // Сравниваем строки
                    if (!matched[j] && task1[i, 0] == task1Copy[j, 0] && task1[i, 1] == task1Copy[j, 1])
                    {
                        order[i] = j; // Запоминаем индекс оригинального массива
                        matched[j] = true; // Помечаем строку как найденную
                        break;
                    }
                }
            }
            return order;
        }

        private void RearrangeTask2(int[,] task2, int[] newOrder)
        {
            int[,] rearrangedTask2 = new int[task2.GetLength(0), task2.GetLength(1)];

            for (int i = 0; i < newOrder.Length; i++)
            {
                rearrangedTask2[i, 0] = task2[newOrder[i], 0];
                rearrangedTask2[i, 1] = task2[newOrder[i], 1];
                rearrangedTask2[i, 2] = task2[newOrder[i], 2];
            }

            // Обновляем task2
            Array.Copy(rearrangedTask2, task2, rearrangedTask2.Length);
        }


        //Перебором
        private void buttonRun22_Click(object sender, EventArgs e)
        {
            task2 = new int[5, 3];
            string filePath = "C:\\Users\\artur\\source\\repos\\Modeling1\\task2.txt";
            ArrayLoader.LoadArrayFromFile(filePath, task2);

            task2 = Swaper.GetBestPermutationNx3(task2);
            LoadDataIntoLabels(task2, false);
            g.Clear(Color.White);
            findAmountOfDowntime3xn();
            DrawGanttNx3();
        }

        /**
         * Метод для проверки условия,
         * возможно ли преобразование матрицы от вида Nx3 к виду Nx2
         */
        private bool CheckData()
        {
            bool flag = false;
            int minA = task2[0, 0];
            int maxB = 0;
            int minC = task2[0, 2];
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                if (minA > task2[i, 0])
                    minA = task2[i, 0];
                if (maxB < task2[i, 1])
                    maxB = task2[i, 1];
                if (minC > task2[i, 2])
                    minC = task2[i, 2];
            }
            if (minA >= maxB || minC >= maxB)
                flag = true;
            return flag;
        }

        /**
         * Метод сводит задачу с тремя станками
         * к задаче с двуммя станками
         */

        private void convertToNx2(int[,] inputTask)
        {
            task1 = new int[inputTask.GetLength(0), 2];
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                task1[i, 0] = inputTask[i, 0] + inputTask[i, 1];
                task1[i, 1] = inputTask[i, 2] + inputTask[i, 1];
            }
        }

        /**
         * Алгоритм Джонсона для матрицы Nx3
         */
        private void sort3xn()
        {
            int[,] matrix = new int[task1.GetLength(0), task1.GetLength(1)];
            int[,] data = new int[task2.GetLength(0), task2.GetLength(1)];
            int linesCount = task1.GetLength(0);
            int top = 0;
            int button = 4;
            while (linesCount > 0)
            {
                int minElement = task1[0, 0];
                int colum = 0;
                int row = 0;
                //поиск минимального элемента
                for (int i = 0; i < linesCount; i++)
                    for (int j = 0; j < task1.GetLength(1); j++)
                        if (task1[i, j] < minElement)
                        {
                            minElement = task1[i, j];
                            colum = j;
                            row = i;
                        }
                //сортировка
                if (colum == 0)
                {
                    matrix[top, 0] = task1[row, 0];
                    matrix[top, 1] = task1[row, 1];
                    data[top, 0] = task2[row, 0];
                    data[top, 1] = task2[row, 1];
                    data[top, 2] = task2[row, 2];
                    top++;
                }
                else
                {
                    matrix[button, 0] = task1[row, 0];
                    matrix[button, 1] = task1[row, 1];
                    data[button, 0] = task2[row, 0];
                    data[button, 1] = task2[row, 1];
                    data[button, 2] = task2[row, 2];
                    button--;
                }
                //удаление строки
                for (int i = row; i < task1.GetLength(0) - 1; i++)
                    for (int j = 0; j < task1.GetLength(1); j++)
                        task1[i, j] = task1[i + 1, j];
                for (int i = row; i < task2.GetLength(0) - 1; i++)
                    for (int j = 0; j < task2.GetLength(1); j++)
                        task2[i, j] = task2[i + 1, j];
                linesCount--;
            }
            task1 = matrix;
            task2 = data;
        }

        /**
         * Метод находит сумму простроев в расписании
         */
        private void findAmountOfDowntime3xn()
        {
            int[] downtime = new int[task2.GetLength(0)];
            int[] downtimeforc = new int[task2.GetLength(0)]; // Массив для простоев

            // Вычисляем простои
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                int sumTask2 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask2 += task2[n, 0]; // Суммируем значения a_i
                }

                int sumDowntime = 0;
                for (int m = 0; m < i; m++)
                {
                    sumDowntime += downtime[m]; // Суммируем предыдущие простои
                }

                int sumTask2Duration = 0;
                for (int m = 0; m < i; m++)
                {
                    sumTask2Duration += task2[m, 1]; // Суммируем длительности b_i
                }

                // Вычисляем downtime для текущего задания
                downtime[i] = Math.Max(0, sumTask2 - sumDowntime - sumTask2Duration);
            }

            // Вычисляем простои для станка C
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                // Суммируем downtime от 0 до i
                int sumDowntimeForc = 0;
                for (int m = 0; m <= i; m++)
                {
                    sumDowntimeForc += downtime[m];
                }

                // Суммируем task2 от 0 до i
                int sumTask1 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask1 += task2[n, 1]; // сумма bi
                }

                // Суммируем downtimeforc от 0 до i-1
                int sumDowntimeForc2 = 0;
                for (int n = 0; n < i; n++)
                {
                    sumDowntimeForc2 += downtimeforc[n];
                }

                // Суммируем task2 от 0 до i-1
                int sumTask2Duration = 0;
                for (int n = 0; n < i; n++)
                {
                    sumTask2Duration += task2[n, 2];
                }

                // Вычисляем downtimeforc[i]
                downtimeforc[i] = Math.Max(0, sumDowntimeForc + sumTask1 - sumDowntimeForc2 - sumTask2Duration);
            }

            int totalTime = 0;
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                totalTime += downtimeforc[i];
                totalTime += task2[i, 2];
            }

            labelDowntime.Text = "Время окончания обработки: " + totalTime;
        }

        private void DrawGanttNx3()
        {
            // Определяем кисти для каждого цвета
            SolidBrush sb1 = new SolidBrush(Color.Red);
            SolidBrush sb2 = new SolidBrush(Color.Orange);
            SolidBrush sb3 = new SolidBrush(Color.Yellow);
            SolidBrush sb4 = new SolidBrush(Color.LightGreen);
            SolidBrush sb5 = new SolidBrush(Color.Purple);
            SolidBrush sbDowntime = new SolidBrush(Color.LightGray);

            // Начальная позиция для графиков
            int x = 10; // Измените X для отступа от левого края
            int y = 200;
            int squareSize = 10; // Размер квадратика
            int spacing = 2; // Расстояние между квадратиками

            // Отрисовка графиков Ганта и названий станков
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                int width = task2[i, 0]; // Ширина в единицах
                int numSquares = width; // Количество квадратиков

                // Рисуем квадратики в зависимости от индекса
                for (int j = 0; j < numSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    if (i == 0)
                        g.FillRectangle(sb1, square);
                    else if (i == 1)
                        g.FillRectangle(sb2, square);
                    else if (i == 2)
                        g.FillRectangle(sb3, square);
                    else if (i == 3)
                        g.FillRectangle(sb4, square);
                    else
                        g.FillRectangle(sb5, square);
                }

                // Перемещаем позицию вправо с учетом ширины и расстояния между квадратиками
                x += (numSquares * (squareSize + spacing));
            }

            g.DrawString("Станок A", this.Font, Brushes.Black, new PointF(10, y - 20));
            g.DrawString("Станок B", this.Font, Brushes.Black, new PointF(10, y + 30));
            g.DrawString("Станок C", this.Font, Brushes.Black, new PointF(10, y + 80));

            // Отрисовка простоев
            y += 50; // Смещение вниз для следующего ряда
            x = 10; // Сброс X для простоев
            int[] downtime = new int[task2.GetLength(0)];
            // Рисуем простои
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                int sumTask1 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask1 += task2[n, 0];
                }

                int sumDowntime = 0;
                int sumTask1Duration = 0;
                for (int m = 0; m < i; m++)
                {
                    sumDowntime += downtime[m];
                    sumTask1Duration += task2[m, 1];
                }

                downtime[i] = Math.Max(0, sumTask1 - sumDowntime - sumTask1Duration);
                int numDowntimeSquares = downtime[i]; // Количество квадратиков для простоев
                for (int j = 0; j < numDowntimeSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    g.FillRectangle(sbDowntime, square);
                }
                x += (numDowntimeSquares * (squareSize + spacing)); // Обновляем x с учетом простоев

                int duration = task2[i, 1];
                int numDurationSquares = duration; // Количество квадратиков для длительности
                for (int j = 0; j < numDurationSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    if (i == 0)
                        g.FillRectangle(sb1, square);
                    else if (i == 1)
                        g.FillRectangle(sb2, square);
                    else if (i == 2)
                        g.FillRectangle(sb3, square);
                    else if (i == 3)
                        g.FillRectangle(sb4, square);
                    else
                        g.FillRectangle(sb5, square);
                }
                x += (numDurationSquares * (squareSize + spacing)); // Обновляем x с учетом длительности
            }

            // Отрисовка простоев для второго ряда
            y += 50; // Смещение вниз для следующего ряда
            x = 10; // Сброс X для простоев

            int[] downtimeforc = new int[task2.GetLength(0)]; // Массив для простоев
                                                              // Рисуем простои
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                // Суммируем downtime от 0 до i
                int sumDowntime = 0;
                for (int m = 0; m <= i; m++)
                {
                    sumDowntime += downtime[m];
                }

                // Суммируем task2 от 0 до i
                int sumTask1 = 0;
                for (int n = 0; n <= i; n++)
                {
                    sumTask1 += task2[n, 1]; // сумма bi
                }

                // Суммируем downtimeforc от 0 до i-1
                int sumDowntimeForc = 0;
                for (int n = 0; n < i; n++)
                {
                    sumDowntimeForc += downtimeforc[n];
                }

                // Суммируем task2 от 0 до i-1
                int sumTask2Duration = 0;
                for (int n = 0; n < i; n++)
                {
                    sumTask2Duration += task2[n, 2];
                }

                // Вычисляем downtimeforc[i]
                downtimeforc[i] = Math.Max(0, sumDowntime + sumTask1 - sumDowntimeForc - sumTask2Duration);
                int numDowntimeSquares = downtimeforc[i]; // Количество квадратиков для простоев
                for (int j = 0; j < numDowntimeSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    g.FillRectangle(sbDowntime, square);
                }
                x += (numDowntimeSquares * (squareSize + spacing)); // Обновляем x с учетом простоев

                int duration = task2[i, 2];
                int numDurationSquares = duration; // Количество квадратиков для длительности
                for (int j = 0; j < numDurationSquares; j++)
                {
                    Rectangle square = new Rectangle(x + j * (squareSize + spacing), y, squareSize, squareSize);
                    if (i == 0)
                        g.FillRectangle(sb1, square);
                    else if (i == 1)
                        g.FillRectangle(sb2, square);
                    else if (i == 2)
                        g.FillRectangle(sb3, square);
                    else if (i == 3)
                        g.FillRectangle(sb4, square);
                    else
                        g.FillRectangle(sb5, square);
                }
                x += (numDurationSquares * (squareSize + spacing)); // Обновляем x с учетом длительности
            }
        }

        /**
         * Метод меняет местами элементы матрицы одной строки
         * с элемантами этой же матрицы другой строки
         */
        static int[,] Swap(int[,] matrix, int row1, int row2)
        {
            int colums = matrix.GetLength(1);
            for (int j = 0; j < colums; j++)
            {
                int temp = matrix[row1, j];
                matrix[row1, j] = matrix[row2, j];
                matrix[row2, j] = temp;
            }
            return matrix;
        }

        static int[,] Sort(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int colums = matrix.GetLength(1);

            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < rows - i - 1; j++)
                {
                    bool swap = false;
                    for (int k = 0; k < colums; k++)
                    {
                        if (matrix[j, k] > matrix[j + 1, k])
                        {
                            swap = true;
                            break;
                        }
                        else if (matrix[j, k] < matrix[j + 1, k])
                        {
                            break;
                        }
                    }
                    if (swap)
                    {
                        matrix = Swap(matrix, j, j + 1);
                    }
                }
            }
            return matrix;
        }
        private void LoadDataIntoLabels(int[,] taskData, bool isTask1)
        {
            ShowILabels();

            // Установим видимость для Label A
            labelA.Visible = true;
            for (int i = 0; i < 5; i++)
            {
                Controls[$"labelA{i + 1}"].Visible = true;
                Controls[$"labelA{i + 1}"].Text = taskData[i, 0].ToString();
            }

            // Установим видимость для Label B
            labelB.Visible = true;
            for (int i = 0; i < 5; i++)
            {
                Controls[$"labelB{i + 1}"].Visible = true;
                Controls[$"labelB{i + 1}"].Text = taskData[i, 1].ToString();
            }

            // Установим видимость для Label C, если это task2
            if (!isTask1)
            {
                labelC.Visible = true;
                for (int i = 0; i < 5; i++)
                {
                    Controls[$"labelC{i + 1}"].Visible = true;
                    Controls[$"labelC{i + 1}"].Text = taskData[i, 2].ToString();
                }
            }
            else
            {
                // Скрыть Label C для task1
                labelC.Visible = false;
                for (int i = 0; i < 5; i++)
                {
                    Controls[$"labelC{i + 1}"].Visible = false;
                }
            }
        }


        private void ShowILabels()
        {
            labelI.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
        }
    }
}
