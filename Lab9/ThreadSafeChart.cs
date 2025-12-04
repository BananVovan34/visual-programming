using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace visualprogramming.Lab9
{
    public class ThreadSafeChart : Control
    {
        private readonly object _lock = new();
        private readonly List<Series> _series = new();

        public ThreadSafeChart()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        /// <summary>
        /// Добавление новой серии (графика)
        /// </summary>
        public void AddSeries(string name, Color color)
        {
            lock (_lock)
            {
                if (_series.All(s => s.Name != name))
                    _series.Add(new Series { Name = name, Color = color });
            }
        }

        /// <summary>
        /// Добавление новой точки в конкретную серию
        /// </summary>
        public void AddPoint(string seriesName, float value)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)(() => AddPoint(seriesName, value)));
                return;
            }

            lock (_lock)
            {
                var series = _series.FirstOrDefault(s => s.Name == seriesName);
                if (series == null) return;

                series.Points.Add(value);
                if (series.Points.Count > 1000)
                    series.Points.RemoveAt(0);
            }

            Invalidate();
        }

        public void ClearPoints()
        {
            lock (_lock)
            {
                foreach (var s in _series)
                    s.Points.Clear();
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.Clear(BackColor);

            lock (_lock)
            {
                if (_series.Count == 0) return;

                float width = ClientSize.Width;
                float height = ClientSize.Height;

                // глобальный масштаб по всем сериям
                var allPoints = _series.SelectMany(s => s.Points).ToList();
                if (allPoints.Count < 2) return;

                float globalMax = allPoints.Max();
                float globalMin = allPoints.Min();

                foreach (var series in _series)
                {
                    if (series.Points.Count < 2) continue;

                    using var pen = new Pen(series.Color, 2f);
                    for (int i = 1; i < series.Points.Count; i++)
                    {
                        float x1 = (i - 1) * width / series.Points.Count;
                        float y1 = height - ((series.Points[i - 1] - globalMin) / (globalMax - globalMin + 0.0001f) * height);
                        float x2 = i * width / series.Points.Count;
                        float y2 = height - ((series.Points[i] - globalMin) / (globalMax - globalMin + 0.0001f) * height);
                        g.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

                // Рисуем легенду
                int legendX = 10;
                int legendY = 10;
                int rectSize = 12;
                int spacing = 4;

                foreach (var series in _series)
                {
                    using var brush = new SolidBrush(series.Color);
                    g.FillRectangle(brush, legendX, legendY, rectSize, rectSize);
                    g.DrawRectangle(Pens.Black, legendX, legendY, rectSize, rectSize);
                    g.DrawString(series.Name, Font, Brushes.Black, legendX + rectSize + spacing, legendY - 2);
                    legendY += rectSize + spacing;
                }
            }
        }

        private class Series
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public List<float> Points { get; } = new();
        }
    }
}
