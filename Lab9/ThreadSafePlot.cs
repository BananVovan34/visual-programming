using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace visualprogramming.Lab9
{
    public class ThreadSafePlot : Control
    {
        private readonly object locker = new();

        // Ключ — имя графика, значение — точки и цвет
        private class Series
        {
            public List<PointF> Points = new();
            public Color Color;
        }

        private readonly Dictionary<string, Series> seriesDict = new();

        public ThreadSafePlot()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        // Создание нового графика
        public void CreateSeries(string name, Color color)
        {
            lock (locker)
            {
                if (!seriesDict.ContainsKey(name))
                    seriesDict[name] = new Series { Color = color };
            }
        }

        // Добавление точки в определённый график
        public void AddPoint(string seriesName, float x, float y)
        {
            lock (locker)
            {
                if (!seriesDict.ContainsKey(seriesName))
                    return;

                seriesDict[seriesName].Points.Add(new PointF(x, y));
            }

            if (!IsDisposed && IsHandleCreated)
                BeginInvoke((Action)(() => Invalidate()));
        }

        public void ClearSeries(string name)
        {
            lock (locker)
            {
                if (seriesDict.ContainsKey(name))
                    seriesDict[name].Points.Clear();
            }
            Invalidate();
        }

        public void ClearAll()
        {
            lock (locker)
                seriesDict.Clear();
            
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DrawGrid(g);
            DrawAxes(g);
            DrawSeries(g);
            DrawLegend(g);
        }

        private void DrawGrid(Graphics g)
        {
            using var pen = new Pen(Color.LightGray, 1);

            for (int x = 0; x < Width; x += 50)
                g.DrawLine(pen, x, 0, x, Height);

            for (int y = 0; y < Height; y += 50)
                g.DrawLine(pen, 0, y, Width, y);
        }

        private void DrawAxes(Graphics g)
        {
            using var pen = new Pen(Color.Black, 2);
            g.DrawLine(pen, 0, Height / 2, Width, Height / 2);
            g.DrawLine(pen, Width / 2, 0, Width / 2, Height);
        }

        private void DrawSeries(Graphics g)
        {
            lock (locker)
            {
                foreach (var ser in seriesDict.Values)
                {
                    if (ser.Points.Count < 2)
                        continue;

                    using var pen = new Pen(ser.Color, 2);

                    for (int i = 1; i < ser.Points.Count; i++)
                    {
                        var p1 = Transform(ser.Points[i - 1]);
                        var p2 = Transform(ser.Points[i]);
                        g.DrawLine(pen, p1, p2);
                    }
                }
            }
        }

        // Легенда справа
        private void DrawLegend(Graphics g)
        {
            lock (locker)
            {
                int x = Width - 150;
                int y = 10;

                foreach (var kv in seriesDict)
                {
                    string name = kv.Key;
                    Color color = kv.Value.Color;

                    using var brush = new SolidBrush(color);
                    using var textBrush = new SolidBrush(Color.Black);

                    g.FillRectangle(brush, x, y, 20, 10);
                    g.DrawString(name, Font, textBrush, x + 30, y - 3);

                    y += 20;
                }
            }
        }

        // Перевод координат
        private PointF Transform(PointF p)
        {
            return new PointF(
                p.X + Width / 2,
                Height / 2 - p.Y
            );
        }
    }
}
