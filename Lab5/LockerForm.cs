using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace visualprogramming.Lab5
{
    public partial class LockerForm : Form
    {
        // поля для перетаскивания
        private Button _draggedButton = null;
        private Point _dragOffset; // смещение курсора внутри кнопки при MouseDown

        // исходные позиции кнопок (чтобы возвращать при таймауте)
        private Point[] _originalPositions;

        // таймер времени (создан в дизайнере как timer1)
        private readonly int _allowedSeconds = 10; // допустимое время на расстановку
        private DateTime _dragStartTime;
        private bool _timerRunning = false;

        // правильный код (слева->справа)
        private readonly int[] _unlockCode = { 2, 3, 1 };

        // событие Unlocked (стандартного вида)
        public event EventHandler Unlocked;

        public LockerForm()
        {
            InitializeComponent();

            _originalPositions = new Point[]
            {
                button1.Location,
                button2.Location,
                button3.Location
            };

            button1.MouseDown += button_MouseDown;
            button2.MouseDown += button_MouseDown;
            button3.MouseDown += button_MouseDown;

            button1.MouseMove += button_MouseMove;
            button2.MouseMove += button_MouseMove;
            button3.MouseMove += button_MouseMove;

            button1.MouseUp += button_MouseUp;
            button2.MouseUp += button_MouseUp;
            button3.MouseUp += button_MouseUp;

            this.MouseMove += Form_MouseMove;
            this.MouseUp += Form_MouseUp;

            timer1.Interval = 1000;
            timer1.Enabled = false;
            timer1.Tick += timer1_Tick;

            this.Unlocked += LockerUnlocked;
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (!(sender is Button btn)) return;
            if (e.Button != MouseButtons.Left) return;

            _draggedButton = btn;
            _dragOffset = e.Location; // относительное смещение внутри кнопки
            _draggedButton.BringToFront();

            if (!_timerRunning)
            {
                _dragStartTime = DateTime.Now;
                timer1.Start();
                _timerRunning = true;
            }
        }

        private void button_MouseMove(object sender, MouseEventArgs e)
        {
            if (_draggedButton == null) return;
            if (e.Button != MouseButtons.Left) return;

            var newLeft = _draggedButton.Left + (e.X - _dragOffset.X);
            var newTop = _draggedButton.Top + (e.Y - _dragOffset.Y);

            newLeft = Math.Max(0, Math.Min(this.ClientSize.Width - _draggedButton.Width, newLeft));
            newTop = Math.Max(0, Math.Min(this.ClientSize.Height - _draggedButton.Height, newTop));

            _draggedButton.Location = new Point(newLeft, newTop);
        }

        private void Form_MouseMove(object sender, MouseEventArgs e)
        {
            if (_draggedButton == null) return;
            if ((Control.MouseButtons & MouseButtons.Left) == 0) return;

            Point cursor = this.PointToClient(Cursor.Position);

            var newLeft = cursor.X - _dragOffset.X;
            var newTop = cursor.Y - _dragOffset.Y;

            newLeft = Math.Max(0, Math.Min(this.ClientSize.Width - _draggedButton.Width, newLeft));
            newTop = Math.Max(0, Math.Min(this.ClientSize.Height - _draggedButton.Height, newTop));

            _draggedButton.Location = new Point(newLeft, newTop);
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            EndDragAndCheck();
        }

        private void Form_MouseUp(object sender, MouseEventArgs e)
        {
            EndDragAndCheck();
        }

        private void EndDragAndCheck()
        {
            if (_draggedButton == null) return;

            _draggedButton = null;

            CheckUnlockCode();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var elapsed = (DateTime.Now - _dragStartTime).TotalSeconds;
            if (elapsed >= _allowedSeconds)
            {
                timer1.Stop();
                _timerRunning = false;
                ResetButtonsToOriginal();
                MessageBox.Show("Время истекло! Кнопки возвращены на исходные позиции.", "Таймер", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CheckUnlockCode()
        {
            var currentOrder = new[] { button1, button2, button3 }
                .OrderBy(b => b.Left)
                .Select(b => int.TryParse(b.Text, out int v) ? v : int.MinValue)
                .ToArray();

            foreach (var num in currentOrder)
            {
                Console.WriteLine(num);
            }

            if (currentOrder.SequenceEqual(_unlockCode))
            {
                timer1.Stop();
                _timerRunning = false;
                Unlocked?.Invoke(this, EventArgs.Empty);
            }
        }
        
        private void LockerUnlocked(object sender, EventArgs e)
        {
            MessageBox.Show("Замок открыт!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ResetButtonsToOriginal()
        {
            button1.Location = _originalPositions[0];
            button2.Location = _originalPositions[1];
            button3.Location = _originalPositions[2];
        }

        private void Locker_Load(object sender, EventArgs e)
        {

        }
    }
}
