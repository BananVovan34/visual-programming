using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualprogramming.Lab9
{
    public partial class MultiThreadGraphics : Form
    {
        private ThreadSafeChart chart;
        private Button singleThreadButton;
        private Button multiThreadButton;
        private Label modeLabel;

        public MultiThreadGraphics()
        {
            InitializeComponent();

            chart = new ThreadSafeChart
            {
                Dock = DockStyle.Fill
            };

            singleThreadButton = new Button
            {
                Text = "Однопоточный режим",
                Dock = DockStyle.Top,
                Height = 35
            };

            multiThreadButton = new Button
            {
                Text = "Многопоточный режим",
                Dock = DockStyle.Top,
                Height = 35
            };

            modeLabel = new Label
            {
                Text = "Режим: не запущен",
                Dock = DockStyle.Top,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter
            };

            singleThreadButton.Click += SingleThreadButton_Click;
            multiThreadButton.Click += MultiThreadButton_Click;

            Controls.Add(chart);
            Controls.Add(modeLabel);
            Controls.Add(multiThreadButton);
            Controls.Add(singleThreadButton);
        }

        private async void SingleThreadButton_Click(object sender, EventArgs e)
        {
            modeLabel.Text = "Режим: Однопоточный";
            chart.ClearPoints();
            chart.AddSeries("Синус", Color.Red);
            singleThreadButton.Enabled = false;
            multiThreadButton.Enabled = false;

            await Task.Run(() =>
            {
                for (float x = 0; x < 20; x += 0.1f)
                {
                    chart.AddPoint("Синус", (float)Math.Sin(x));
                    Thread.Sleep(50);
                }
            });

            modeLabel.Text = "Однопоточный режим завершён";
            singleThreadButton.Enabled = true;
            multiThreadButton.Enabled = true;
        }

        private void MultiThreadButton_Click(object sender, EventArgs e)
        {
            modeLabel.Text = "Режим: Многопоточный";
            chart.ClearPoints();
            singleThreadButton.Enabled = false;
            multiThreadButton.Enabled = false;

            // создаем серии с разными цветами
            chart.AddSeries("Синус", Color.Red);
            chart.AddSeries("Косинус", Color.Green);
            chart.AddSeries("Шум", Color.Blue);

            // Поток 1 — синус
            Task.Run(() =>
            {
                float x = 0;
                while (x < 20)
                {
                    chart.AddPoint("Синус", (float)Math.Sin(x));
                    x += 0.1f;
                    Thread.Sleep(40);
                }
            });

            // Поток 2 — косинус
            Task.Run(() =>
            {
                float x = 0;
                while (x < 20)
                {
                    chart.AddPoint("Косинус", (float)Math.Cos(x));
                    x += 0.1f;
                    Thread.Sleep(60);
                }
            });

            // Поток 3 — шум
            Task.Run(() =>
            {
                var rnd = new Random();
                for (int i = 0; i < 200; i++)
                {
                    chart.AddPoint("Шум", (float)(rnd.NextDouble() * 2 - 1));
                    Thread.Sleep(70);
                }

                Invoke((Action)(() =>
                {
                    modeLabel.Text = "Многопоточный режим завершён";
                    singleThreadButton.Enabled = true;
                    multiThreadButton.Enabled = true;
                }));
            });
        }
    }
}
