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
    public partial class GraphicsForm : Form
    {

        public GraphicsForm()
        {
            Width = 1000;
            Height = 700;
            

            Button btnAdd = new Button
            {
                Text = "Добавить поток",
                Left = 10,
                Top = 620,
                Width = 200
            };
            Controls.Add(btnAdd);
        }
    }
}
