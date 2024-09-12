using System;
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
        private bool check;
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
            buttonCheck.Visible = false;
            labelDowntime.Visible = false;
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
            labelA.Text = "Ai";
            labelB.Text = "Bi";
            buttonRun21.Visible = false;
            buttonRun22.Visible = false;
            g.Clear(Color.White);
            labelDowntime.Visible = true;
            buttonCheck.Visible = false;
            labelI.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            labelA.Visible = true;
            labelA1.Visible = true;
            /**
            * Значения, с которыми работает программа
            */
            labelA1.Text = "12";
            labelA2.Visible = true;
            labelA2.Text = "7";
            labelA3.Visible = true;
            labelA3.Text = "4";
            labelA4.Visible = true;
            labelA4.Text = "2";
            labelA5.Visible = true;
            labelA5.Text = "1";
            labelB.Visible = true;
            labelB1.Visible = true;
            labelB1.Text = "2";
            labelB2.Visible = true;
            labelB2.Text = "4";
            labelB3.Visible = true;
            labelB3.Text = "2";
            labelB4.Visible = true;
            labelB4.Text = "8";
            labelB5.Visible = true;
            labelB5.Text = "7";

            /**
             * Значения, с которыми работает программа
             */
            task1 = new int[,]
            {
                { 12, 2 },
                { 7, 4 },
                { 4, 2 },
                { 2, 8 },
                { 1, 7 }
            };
            buttonRun1.Visible = true;
            buttonRun12.Visible = true;
        }

        private void buttonSort1_Click(object sender, EventArgs e)
        {
            task1 = sort2xn();
            labelA1.Text = Convert.ToString(task1[0, 0]);
            labelA2.Text = Convert.ToString(task1[1, 0]);
            labelA3.Text = Convert.ToString(task1[2, 0]);
            labelA4.Text = Convert.ToString(task1[3, 0]);
            labelA5.Text = Convert.ToString(task1[4, 0]);
            labelB1.Text = Convert.ToString(task1[0, 1]);
            labelB2.Text = Convert.ToString(task1[1, 1]);
            labelB3.Text = Convert.ToString(task1[2, 1]);
            labelB4.Text = Convert.ToString(task1[3, 1]);
            labelB5.Text = Convert.ToString(task1[4, 1]);
            findAmountOfDowntime2xn();
            DrawGant2xn();
        }

        private void buttonSort12_Click(object sender, EventArgs e)
        {
            task1 = Swaper.GetBestPermutation(task1);
            labelA1.Text = Convert.ToString(task1[0, 0]);
            labelA2.Text = Convert.ToString(task1[1, 0]);
            labelA3.Text = Convert.ToString(task1[2, 0]);
            labelA4.Text = Convert.ToString(task1[3, 0]);
            labelA5.Text = Convert.ToString(task1[4, 0]);
            labelB1.Text = Convert.ToString(task1[0, 1]);
            labelB2.Text = Convert.ToString(task1[1, 1]);
            labelB3.Text = Convert.ToString(task1[2, 1]);
            labelB4.Text = Convert.ToString(task1[3, 1]);
            labelB5.Text = Convert.ToString(task1[4, 1]);
            findAmountOfDowntime2xn();
            DrawGant2xn();
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
            int x = 0;
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                if (i == 0)
                    x = task1[0, 0];
                else
                    x += Math.Max(task1[i, 0] - task1[i - 1, 1], 0);
            }
            for (int i = 0; i < task1.GetLength(0); i++)
                x += task1[i, 1];
            labelDowntime.Visible = true;
            labelDowntime.Location = new Point(13, 142);
            labelDowntime.Text = "Сумма простоев: ";
            labelDowntime.Text += Convert.ToString(x);
        }

        private void DrawGant2xn()
        {
            // Определяем кисти для каждого цвета
            SolidBrush sb1 = new SolidBrush(Color.Red);
            SolidBrush sb2 = new SolidBrush(Color.Orange);
            SolidBrush sb3 = new SolidBrush(Color.Yellow);
            SolidBrush sb4 = new SolidBrush(Color.LightGreen);
            SolidBrush sb5 = new SolidBrush(Color.Purple);
            SolidBrush sbDowntime = new SolidBrush(Color.LightGray);

            // Начальная позиция для графиков
            Rectangle rec = new Rectangle(100, 200, 1, 20); // Измените X для отступа от левого края
            int width;

            // Отрисовка графиков Ганта и названий станков
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                // Устанавливаем ширину прямоугольника
                width = task1[i, 0] * 10;
                rec.Width = width;

                // Рисуем прямоугольник в зависимости от индекса
                if (i == 0)
                    g.FillRectangle(sb1, rec);
                else if (i == 1)
                    g.FillRectangle(sb2, rec);
                else if (i == 2)
                    g.FillRectangle(sb3, rec);
                else if (i == 3)
                    g.FillRectangle(sb4, rec);
                else
                    g.FillRectangle(sb5, rec);

                // Рисуем текст названия станка только для A и B
                if (i == 0)
                {
                    g.DrawString("Станок A", this.Font, Brushes.Black, new Point(10, rec.Y + i * 50));
                }
                else if (i == 1)
                {
                    g.DrawString("Станок B", this.Font, Brushes.Black, new PointF(10, rec.Y + i * 50));
                }

                // Перемещаем прямоугольник вправо
                rec.X += width;
            }

            // Отрисовка простоев
            rec.Y += 50; // Смещение вниз для следующего ряда
            rec.X = 100; // Сброс X для простоев

            width = task1[0, 0] * 10;
            rec.Width = width;
            g.FillRectangle(sbDowntime, rec);
            rec.X += width;

            width = task1[0, 1] * 10;
            rec.Width = width;
            g.FillRectangle(sb1, rec);
            rec.X += width;

            for (int i = 1; i < task1.GetLength(0); i++)
            {
                int downtime = Math.Max(task1[i, 0] - task1[i - 1, 1], 0);
                if (downtime > 0)
                {
                    width = downtime * 10;
                    rec.Width = width;
                    g.FillRectangle(sbDowntime, rec);
                    rec.X += width;
                }
                width = task1[i, 1] * 10;
                rec.Width = width;
                if (i == 0)
                    g.FillRectangle(sb1, rec);
                else if (i == 1)
                    g.FillRectangle(sb2, rec);
                else if (i == 2)
                    g.FillRectangle(sb3, rec);
                else if (i == 3)
                    g.FillRectangle(sb4, rec);
                else
                    g.FillRectangle(sb5, rec);
                rec.X += width;
            }
        }



        #endregion

        /**
         * Работа с заданием №2
         */
        private void buttonTask2_Click(object sender, EventArgs e)
        {
            labelA.Text = "Ai";
            labelB.Text = "Bi";
            g.Clear(Color.White);
            labelDowntime.Visible = false;
            buttonRun1.Visible = false;
            buttonRun12.Visible = false;
            labelI.Visible = true;
            labelI.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            labelA.Visible = true;
            labelA1.Visible = true;
            /**
             * Числа из таблицы идут по порядку, первая строка первого столбца,
             * вторая строка первого столбца и т.д.
             */
            labelA1.Text = "12";
            labelA2.Visible = true;
            labelA2.Text = "7";
            labelA3.Visible = true;
            labelA3.Text = "4";
            labelA4.Visible = true;
            labelA4.Text = "6";
            labelA5.Visible = true;
            labelA5.Text = "5";
            labelB.Visible = true;
            labelB1.Visible = true;
            labelB1.Text = "2";
            labelB2.Visible = true;
            labelB2.Text = "4";
            labelB3.Visible = true;
            labelB3.Text = "2"; 
            labelB4.Visible = true;
            labelB4.Text = "3";
            labelB5.Visible = true;
            labelB5.Text = "2";
            labelC.Visible = true;
            labelC1.Visible = true;
            labelC1.Text = "2";
            labelC2.Visible = true;
            labelC2.Text = "6";
            labelC3.Visible = true;
            labelC3.Text = "7";
            labelC4.Visible = true;
            labelC4.Text = "4";
            labelC5.Visible = true;
            labelC5.Text = "8";

            /**
             * Значения, с которыми работает программа
             */
            task2 = new int[,]
            {
                { 12, 2, 2 },
                { 7, 4, 6 },
                { 4, 2, 7 },
                { 6, 3, 4 },
                { 5, 2, 8 }
            };
            buttonCheck.Visible = true;
            buttonRun21.Visible = true;
            buttonRun22.Visible = true;
        }

        private void buttonCheck_Click(object sender, EventArgs e)
        {
            check = CheckData();
            if (check == true)
            {
                labelDowntime.Visible = true;
                labelDowntime.Location = new Point(13, 142);
                labelDowntime.Text = "Условие сходимости выполняется";
                convertToNx2();
            }
            else
            {
                labelDowntime.Visible = true;
                labelDowntime.Location = new Point(13, 142);
                labelDowntime.Text = "Условие сходимости не выполняется";
                task2 = Sort(task2); //непонятно
            }
        }

        //Предположительно по алгоритму
        private void buttonRun21_Click(object sender, EventArgs e)
        {
            if (check == true)
            {
                sort3xn();
                labelA1.Text = Convert.ToString(task1[0, 0]);
                labelA2.Text = Convert.ToString(task1[1, 0]);
                labelA3.Text = Convert.ToString(task1[2, 0]);
                labelA4.Text = Convert.ToString(task1[3, 0]);
                labelA5.Text = Convert.ToString(task1[4, 0]);
                labelB1.Text = Convert.ToString(task1[0, 1]);
                labelB2.Text = Convert.ToString(task1[1, 1]);
                labelB3.Text = Convert.ToString(task1[2, 1]);
                labelB4.Text = Convert.ToString(task1[3, 1]);
                labelB5.Text = Convert.ToString(task1[4, 1]);
            }
            else
            {
                labelDowntime.Text = "Время обработки: ";
                int t = 0;
                for (int i = 0; i < task2.GetLength(0); i++)
                {
                    t += Math.Max(task2[i, 0] - task2[i, 1], 0);
                    t += Math.Max(task2[i, 1] - task2[i, 2], 0);
                }
                for (int i = 0; i < task2.GetLength(0); i++)
                {
                    t += task2[i, 2];
                }
                labelDowntime.Text += Convert.ToString(t);
                labelA1.Text = Convert.ToString(task2[0, 0]);
                labelA1.Text = Convert.ToString(task2[0, 0]);
                labelA2.Text = Convert.ToString(task2[1, 0]);
                labelA3.Text = Convert.ToString(task2[2, 0]);
                labelA4.Text = Convert.ToString(task2[3, 0]);
                labelA5.Text = Convert.ToString(task2[4, 0]);
                labelB1.Text = Convert.ToString(task2[0, 1]);
                labelB2.Text = Convert.ToString(task2[1, 1]);
                labelB3.Text = Convert.ToString(task2[2, 1]);
                labelB4.Text = Convert.ToString(task2[3, 1]);
                labelB5.Text = Convert.ToString(task2[4, 1]);
                labelC1.Text = Convert.ToString(task2[0, 2]);
                labelC2.Text = Convert.ToString(task2[1, 2]);
                labelC3.Text = Convert.ToString(task2[2, 2]);
                labelC4.Text = Convert.ToString(task2[3, 2]);
                labelC5.Text = Convert.ToString(task2[4, 2]);
            }
        }

        //Предположительно перебором
        private void buttonRun22_Click(object sender, EventArgs e)
        {
            if (check == true)
            {
                labelA.Text = "Ai";
                labelA1.Text = Convert.ToString(task2[0, 0]);
                labelA1.Text = Convert.ToString(task2[0, 0]);
                labelA2.Text = Convert.ToString(task2[1, 0]);
                labelA3.Text = Convert.ToString(task2[2, 0]);
                labelA4.Text = Convert.ToString(task2[3, 0]);
                labelA5.Text = Convert.ToString(task2[4, 0]);
                labelB.Text = "Bi";
                labelB1.Text = Convert.ToString(task2[0, 1]);
                labelB2.Text = Convert.ToString(task2[1, 1]);
                labelB3.Text = Convert.ToString(task2[2, 1]);
                labelB4.Text = Convert.ToString(task2[3, 1]);
                labelB5.Text = Convert.ToString(task2[4, 1]);
                labelC.Visible = true;
                labelC1.Visible = true;
                labelC1.Text = Convert.ToString(task2[0, 2]);
                labelC2.Visible = true;
                labelC2.Text = Convert.ToString(task2[1, 2]);
                labelC3.Visible = true;
                labelC3.Text = Convert.ToString(task2[2, 2]);
                labelC4.Visible = true;
                labelC4.Text = Convert.ToString(task2[3, 2]);
                labelC5.Visible = true;
                labelC5.Text = Convert.ToString(task2[4, 2]);
                findAmountOfDowntime3xn();
                draw3xn();
            }
            else
            {
                draw3xn();
            }

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
        private void convertToNx2()
        {
            task1 = new int[5, 2];
            for (int i = 0; i < task1.GetLength(0); i++)
            {
                task1[i, 0] = task2[i, 0] + task2[i, 1];
                task1[i, 1] = task2[i, 2] + task2[i, 1];
            }
            labelA.Text = "Di";
            labelA1.Text = Convert.ToString(task1[0, 0]);
            labelA1.Text = Convert.ToString(task1[0, 0]);
            labelA2.Text = Convert.ToString(task1[1, 0]);
            labelA3.Text = Convert.ToString(task1[2, 0]);
            labelA4.Text = Convert.ToString(task1[3, 0]);
            labelA5.Text = Convert.ToString(task1[4, 0]);
            labelB.Text = "Ei";
            labelB1.Text = Convert.ToString(task1[0, 1]);
            labelB2.Text = Convert.ToString(task1[1, 1]);
            labelB3.Text = Convert.ToString(task1[2, 1]);
            labelB4.Text = Convert.ToString(task1[3, 1]);
            labelB5.Text = Convert.ToString(task1[4, 1]);
            labelC.Visible = false;
            labelC1.Visible = false;
            labelC2.Visible = false;
            labelC3.Visible = false;
            labelC4.Visible = false;
            labelC5.Visible = false;
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
            int xk = 0, xh = 0;
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                xk += Math.Max(task2[i, 0] - task2[i, 1], 0);
                xh += Math.Max(task2[i, 1] - task2[i, 2], 0);
            }
            int x = xk + xh;
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                x += task2[i, 2];
            }
            labelDowntime.Text = "Сумма простоев: ";
            labelDowntime.Text += Convert.ToString(x);
        }

        private void draw3xn()
        {
            SolidBrush sb1 = new SolidBrush(Color.Red);
            SolidBrush sb2 = new SolidBrush(Color.Orange);
            SolidBrush sb3 = new SolidBrush(Color.Yellow);
            SolidBrush sb4 = new SolidBrush(Color.LightGreen);
            SolidBrush sb5 = new SolidBrush(Color.Purple);
            SolidBrush sbDowntime = new SolidBrush(Color.LightGray);
            Rectangle rec = new Rectangle(100, 200, 1, 20);
            int width;

            // Отрисовка графиков Ганта и названий станков
            for (int i = 0; i < task2.GetLength(0); i++)
            {
                width = task2[i, 0] * 5;
                rec.Width = width;

                // Рисуем прямоугольник в зависимости от индекса
                if (i == 0)
                    g.FillRectangle(sb1, rec);
                else if (i == 1)
                    g.FillRectangle(sb2, rec);
                else if (i == 2)
                    g.FillRectangle(sb3, rec);
                else if (i == 3)
                    g.FillRectangle(sb4, rec);
                else
                    g.FillRectangle(sb5, rec);

                // Рисуем текст названия станка только для A и B
                if (i == 0)
                {
                    g.DrawString("Станок A", this.Font, Brushes.Black, new PointF(10, rec.Y + i * 50));
                }
                else if (i == 1)
                {
                    g.DrawString("Станок B", this.Font, Brushes.Black, new PointF(10, rec.Y + i * 50));
                }
                else if (i == 2)
                {
                    g.DrawString("Станок C", this.Font, Brushes.Black, new PointF(10, rec.Y + i * 50));
                }

                // Перемещаем прямоугольник вправо
                rec.X += width;
            }

            // Отрисовка простоев
            rec.Y += 50; // Смещение вниз для следующего ряда
            rec.X = 100; // Сброс X для простоев

            width = task2[0, 0] * 5;
            rec.Width = width;
            g.FillRectangle(sbDowntime, rec);
            rec.X += width;

            width = task2[0, 1] * 5;
            rec.Width = width;
            g.FillRectangle(sb1, rec);
            rec.X += width;

            for (int i = 1; i < task2.GetLength(0); i++)
            {
                int downtime = Math.Max(task2[i, 0] - task2[i - 1, 1], 0);
                if (downtime > 0)
                {
                    width = downtime * 5;
                    rec.Width = width;
                    g.FillRectangle(sbDowntime, rec);
                    rec.X += width;
                }
                width = task2[i, 1] * 5;
                rec.Width = width;
                if (i == 0)
                    g.FillRectangle(sb1, rec);
                else if (i == 1)
                    g.FillRectangle(sb2, rec);
                else if (i == 2)
                    g.FillRectangle(sb3, rec);
                else if (i == 3)
                    g.FillRectangle(sb4, rec);
                else
                    g.FillRectangle(sb5, rec);
                rec.X += width;
            }

            // Отрисовка простоев
            rec.Y += 50; // Смещение вниз для следующего ряда
            rec.X = 100; // Сброс X для простоев

            width = task2[0, 0] * 5;
            rec.Width = width;
            g.FillRectangle(sbDowntime, rec);
            rec.X += width;

            width = task2[0, 1] * 5;
            rec.Width = width;
            g.FillRectangle(sbDowntime, rec);
            rec.X += width;

            width = task2[0, 2] * 5;
            rec.Width = width;
            g.FillRectangle(sb1, rec);
            rec.X += width;

            for (int i = 1; i < task2.GetLength(0); i++)
            {
                int downtime = Math.Max(task2[i, 0] + task2[i - 1, 1] - task2[i - 1, 2], 0);
                if (downtime > 0)
                {
                    width = downtime * 5;
                    rec.Width = width;
                    g.FillRectangle(sbDowntime, rec);
                    rec.X += width;
                }
                width = task2[i, 2] * 5;
                rec.Width = width;
                if (i == 0)
                    g.FillRectangle(sb1, rec);
                else if (i == 1)
                    g.FillRectangle(sb2, rec);
                else if (i == 2)
                    g.FillRectangle(sb3, rec);
                else if (i == 3)
                    g.FillRectangle(sb4, rec);
                else
                    g.FillRectangle(sb5, rec);
                rec.X += width;
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
    }
}
