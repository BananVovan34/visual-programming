using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace visualprogramming.AiMethodsLab4
{
    public partial class Recognizer : Form
    {
        private const int ImageSize = 256;
        private Bitmap canvasBitmap;
        private bool isDrawing = false;

        private TabControl tabControl;
        private PictureBox pictureBox;
        private Button btnAddImage, btnAddClass, btnDeleteImage, btnDeleteClass, btnRecognize, btnClear;
        private Button btnSaveClass, btnLoadClass;

        private Dictionary<string, List<Bitmap>> dataset = new();

        public Recognizer()
        {
            InitializeComponent();
            Text = "Система распознавания образов";
            Size = new Size(1250, 850);
            DoubleBuffered = true;

            canvasBitmap = new Bitmap(ImageSize, ImageSize);

            pictureBox = new PictureBox
            {
                Left = 20,
                Top = 50,
                Width = 512,
                Height = 512,
                BorderStyle = BorderStyle.FixedSingle,
                Image = canvasBitmap,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            pictureBox.MouseDown += StartDraw;
            pictureBox.MouseMove += ContinueDraw;
            pictureBox.MouseUp += StopDraw;
            Controls.Add(pictureBox);

            tabControl = new TabControl
            {
                Left = 600,
                Top = 50,
                Width = 512,
                Height = 512
            };
            Controls.Add(tabControl);

            btnAddClass = new Button { Text = "Добавить образ", Left = 600, Top = 10, Width = 150 };
            btnDeleteClass = new Button { Text = "Удалить образ", Left = 760, Top = 10, Width = 150 };
            btnAddImage = new Button { Text = "Добавить пример", Left = 600, Top = 570, Width = 150 };
            btnDeleteImage = new Button { Text = "Удалить пример", Left = 780, Top = 570, Width = 150 };
            btnRecognize = new Button { Text = "Распознать", Left = 600, Top = 600, Width = 150 };
            btnClear = new Button { Text = "Очистить", Left = 780, Top = 600, Width = 150 };
            btnSaveClass = new Button { Text = "Сохранить образ", Left = 600, Top = 630, Width = 150 };
            btnLoadClass = new Button { Text = "Загрузить образ", Left = 780, Top = 630, Width = 150 };

            Controls.AddRange(new[] { btnAddClass, btnDeleteClass, btnAddImage, btnDeleteImage, btnRecognize, btnClear, btnSaveClass, btnLoadClass });

            btnAddClass.Click += (s, e) => AddNewClass();
            btnDeleteClass.Click += (s, e) => DeleteClass();
            btnAddImage.Click += (s, e) => AddTrainingExample();
            btnDeleteImage.Click += (s, e) => DeleteSelectedExample();
            btnRecognize.Click += (s, e) => Recognize();
            btnClear.Click += (s, e) => ClearCanvas();

            btnSaveClass.Click += (s, e) => SaveCurrentClass();
            btnLoadClass.Click += (s, e) => LoadClass();
        }

        private void StartDraw(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            DrawPoint(e);
        }

        private void ContinueDraw(object sender, MouseEventArgs e)
        {
            if (isDrawing)
                DrawPoint(e);
        }

        private void StopDraw(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void DrawPoint(MouseEventArgs e)
        {
            using (Graphics g = Graphics.FromImage(canvasBitmap))
            {
                int scaleX = (int)(e.X * (ImageSize / (float)pictureBox.Width));
                int scaleY = (int)(e.Y * (ImageSize / (float)pictureBox.Height));
                if (e.Button == MouseButtons.Left)
                    g.FillEllipse(Brushes.Black, scaleX, scaleY, 6, 6);
                else if (e.Button == MouseButtons.Right)
                    g.FillEllipse(Brushes.White, scaleX, scaleY, 8, 8);
            }
            pictureBox.Invalidate();
        }

        private void ClearCanvas()
        {
            if (canvasBitmap != null)
                canvasBitmap.Dispose();

            canvasBitmap = new Bitmap(ImageSize, ImageSize);
            using (Graphics g = Graphics.FromImage(canvasBitmap))
                g.Clear(Color.White);

            pictureBox.Image = canvasBitmap;
            pictureBox.Invalidate();
        }


        private void AddNewClass()
        {
            string name = Microsoft.VisualBasic.Interaction.InputBox("Введите название нового образа:", "Новый образ", "НовыйОбраз");
            if (string.IsNullOrWhiteSpace(name)) return;
            if (dataset.ContainsKey(name))
            {
                MessageBox.Show("Такой образ уже существует!");
                return;
            }

            dataset[name] = new List<Bitmap>();

            var listBox = new ListBox { Dock = DockStyle.Fill };
            listBox.SelectedIndexChanged += (s, e) => ShowExamplePreview(name, listBox.SelectedIndex);

            var page = new TabPage(name) { Tag = name };
            page.Controls.Add(listBox);
            tabControl.TabPages.Add(page);
            tabControl.SelectedTab = page;
        }

        private void DeleteClass()
        {
            if (tabControl.SelectedTab == null) return;
            string name = tabControl.SelectedTab.Tag.ToString();
            if (MessageBox.Show($"Удалить образ '{name}' и все его примеры?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataset.Remove(name);
                tabControl.TabPages.Remove(tabControl.SelectedTab);
            }
        }

        private void AddTrainingExample()
        {
            if (tabControl.SelectedTab == null)
            {
                MessageBox.Show("Сначала выберите или создайте образ!");
                return;
            }

            string name = tabControl.SelectedTab.Tag.ToString();
            dataset[name].Add((Bitmap)canvasBitmap.Clone());
            UpdateExampleList(name);
            MessageBox.Show($"Пример добавлен в образ '{name}'");
        }

        private void DeleteSelectedExample()
        {
            if (tabControl.SelectedTab == null) return;
            var listBox = tabControl.SelectedTab.Controls[0] as ListBox;
            int index = listBox.SelectedIndex;
            if (index >= 0)
            {
                string name = tabControl.SelectedTab.Tag.ToString();
                dataset[name].RemoveAt(index);
                UpdateExampleList(name);
                ClearCanvas();
            }
        }

        private void UpdateExampleList(string name)
        {
            var tab = tabControl.TabPages.Cast<TabPage>().FirstOrDefault(p => p.Tag.ToString() == name);
            if (tab == null) return;
            var listBox = tab.Controls[0] as ListBox;
            listBox.Items.Clear();
            for (int i = 0; i < dataset[name].Count; i++)
                listBox.Items.Add($"Пример {i + 1}");
        }

        private void ShowExamplePreview(string className, int index)
        {
            if (index < 0) return;

            var example = dataset[className][index];

            if (canvasBitmap != null)
                canvasBitmap.Dispose();

            canvasBitmap = (Bitmap)example.Clone();
            pictureBox.Image = canvasBitmap;
            pictureBox.Invalidate();
        }


        private void SaveCurrentClass()
        {
            if (tabControl.SelectedTab == null) return;
            string name = tabControl.SelectedTab.Tag.ToString();

            if (!dataset.ContainsKey(name) || dataset[name].Count == 0)
            {
                MessageBox.Show("Нет данных для сохранения!");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json";
                sfd.FileName = name + ".json";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var json = new List<string>();
                    foreach (var bmp in dataset[name])
                    {
                        using var ms = new MemoryStream();
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        json.Add(Convert.ToBase64String(ms.ToArray()));
                    }
                    File.WriteAllText(sfd.FileName, JsonSerializer.Serialize(json));
                    MessageBox.Show("Образ успешно сохранён!");
                }
            }
        }

        private void LoadClass()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "JSON files (*.json)|*.json";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string jsonText = File.ReadAllText(ofd.FileName);
                    var data = JsonSerializer.Deserialize<List<string>>(jsonText);

                    string name = Path.GetFileNameWithoutExtension(ofd.FileName);
                    if (!dataset.ContainsKey(name))
                        AddNewClassByName(name);

                    dataset[name].Clear();
                    foreach (var base64 in data)
                    {
                        byte[] bytes = Convert.FromBase64String(base64);
                        using var ms = new MemoryStream(bytes);
                        dataset[name].Add(new Bitmap(ms));
                    }
                    UpdateExampleList(name);
                    MessageBox.Show($"Образ '{name}' загружен ({data.Count} примеров)");
                }
            }
        }

        private void AddNewClassByName(string name)
        {
            dataset[name] = new List<Bitmap>();
            var listBox = new ListBox { Dock = DockStyle.Fill };
            listBox.SelectedIndexChanged += (s, e) => ShowExamplePreview(name, listBox.SelectedIndex);

            var page = new TabPage(name) { Tag = name };
            page.Controls.Add(listBox);
            tabControl.TabPages.Add(page);
        }

        private void Recognize()
        {
            if (dataset.Count == 0)
            {
                MessageBox.Show("Нет обучающих данных!");
                return;
            }

            var potentials = new Dictionary<string, double>();

            foreach (var entry in dataset)
            {
                double sumPotential = 0;
                foreach (var sample in entry.Value)
                {
                    int R = HammingDistance(canvasBitmap, sample);
                    double phi = 1_000_000.0 / (1 + R * R);
                    sumPotential += phi;
                }
                potentials[entry.Key] = sumPotential;
            }

            var best = potentials.OrderByDescending(p => p.Value).First();
            string result = string.Join("\n", potentials.Select(p => $"{p.Key}: {p.Value:F2}"));
            MessageBox.Show($"Потенциалы:\n{result}\n\nРаспознан как: {best.Key}", "Результат");
        }

        private int HammingDistance(Bitmap a, Bitmap b)
        {
            int size = 32;
            Bitmap aSmall = new Bitmap(a, size, size);
            Bitmap bSmall = new Bitmap(b, size, size);
            Bitmap bMirror = new Bitmap(bSmall);
            bMirror.RotateFlip(RotateFlipType.RotateNoneFlipX);

            int normalDist = 0;
            int mirrorDist = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    bool aBlack = aSmall.GetPixel(x, y).GetBrightness() < 0.6f;
                    bool bBlack = bSmall.GetPixel(x, y).GetBrightness() < 0.6f;
                    bool bBlackMirror = bMirror.GetPixel(x, y).GetBrightness() < 0.6f;

                    if (aBlack != bBlack)
                        normalDist++;

                    if (aBlack != bBlackMirror)
                        mirrorDist++;
                }
            }

            return Math.Min(normalDist, mirrorDist);
        }


    }
}
