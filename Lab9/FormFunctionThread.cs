using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace visualprogramming.Lab9
{
    public partial class FormFunctionThread : Form
    {
        private readonly ThreadSafePlot plot;

        public FormFunctionThread()
        {
            InitializeComponent();
            Width = 1000;
            Height = 650;

            plot = new ThreadSafePlot
            {
                Left = 10,
                Top = 10,
                Width = 950,
                Height = 550
            };
            Controls.Add(plot);

            Button btnAddFunc = new Button
            {
                Text = "Добавить поток",
                Left = 10,
                Top = 570,
                Width = 150
            };
            btnAddFunc.Click += (s, e) =>
            {
                new FormNewFunction(plot).Show();
            };
            Controls.Add(btnAddFunc);
        }
    }
}