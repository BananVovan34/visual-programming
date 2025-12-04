using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualprogramming.Lab9
{
    public partial class FormNewFunction : Form
    {
        private Thread thread;
        private bool running;
        private readonly ThreadSafePlot plot;

        TextBox txtName, txtFunction;
        Button btnStart, btnStop;
        Color seriesColor;

        public FormNewFunction(ThreadSafePlot plotRef)
        {
            plot = plotRef;

            Width = 400;
            Height = 230;

            Label ln = new Label { Text = "Имя функции:", Left = 10, Top = 10 };
            Controls.Add(ln);

            txtName = new TextBox { Left = 120, Top = 10, Width = 250, Text = "sin(x)" };
            Controls.Add(txtName);

            Label lf = new Label { Text = "y = f(x):", Left = 10, Top = 40 };
            Controls.Add(lf);

            txtFunction = new TextBox { Left = 120, Top = 40, Width = 250, Text = "Math.Sin(x/20)*100" };
            Controls.Add(txtFunction);

            Button btnColor = new Button { Text = "Цвет", Left = 10, Top = 80, Width = 100 };
            btnColor.Click += (s, e) =>
            {
                ColorDialog dlg = new();
                if (dlg.ShowDialog() == DialogResult.OK) seriesColor = dlg.Color;
            };
            Controls.Add(btnColor);

            btnStart = new Button { Text = "Старт", Left = 10, Top = 130, Width = 120 };
            btnStop = new Button { Text = "Стоп", Left = 150, Top = 130, Width = 120 };

            btnStart.Click += (s, e) => StartThread();
            btnStop.Click += (s, e) => StopThread();

            Controls.Add(btnStart);
            Controls.Add(btnStop);

            seriesColor = Color.Blue;
        }

        private void StartThread()
        {
            if (running) return;
            running = true;

            string name = txtName.Text;
            string formula = txtFunction.Text;

            plot.CreateSeries(name, seriesColor);

            thread = new Thread(() =>
            {
                for (float x = -400; x <= 400 && running; x++)
                {
                    float y = Eval(formula, x);
                    plot.AddPoint(name, x, y);
                    Thread.Sleep(5);
                }

                running = false;
            });

            thread.IsBackground = true;
            thread.Start();
        }

        private float Eval(string expr, float x)
        {
            try
            {
                var dt = new DataTable();
                string code = expr.Replace("x", x.ToString(System.Globalization.CultureInfo.InvariantCulture));
                return Convert.ToSingle(dt.Compute(code, ""));
            }
            catch
            {
                return 0;
            }
        }

        private void StopThread()
        {
            running = false;
        }
    }
}
