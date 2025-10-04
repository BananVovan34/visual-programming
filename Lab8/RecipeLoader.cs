using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace visualprogramming.Lab8
{
    public partial class RecipeLoader : Form
    {
        public RecipeLoader()
        {
            InitializeComponent();
        }

        private void buttonLoadRecipe_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "XML файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";
                ofd.Title = "Выберите XML-файл с рецептом";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XElement recipe = XElement.Load(ofd.FileName);

                        outputText.Clear();

                        outputText.AppendText("=== РЕЦЕПТ ===\r\n");
                        outputText.AppendText(recipe.Element("title")?.Value + "\r\n\r\n");

                        outputText.AppendText("Ингредиенты:\r\n");
                        var ingredients = recipe.Element("ingredients")?.Elements("ingredient");
                        if (ingredients != null)
                        {
                            foreach (var ing in ingredients)
                            {
                                string amount = ing.Attribute("amount")?.Value ?? "";
                                outputText.AppendText($"- {ing.Value} ({amount})\r\n");
                            }
                        }
                        outputText.AppendText("\r\n");

                        outputText.AppendText("Шаги приготовления:\r\n");
                        var steps = recipe.Element("steps")?.Elements("step");
                        if (steps != null)
                        {
                            int i = 1;
                            foreach (var step in steps)
                            {
                                outputText.AppendText($"{i}. {step.Value}\r\n");
                                i++;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки рецепта: " + ex.Message);
                    }
                }
            }
        }
    }
}
