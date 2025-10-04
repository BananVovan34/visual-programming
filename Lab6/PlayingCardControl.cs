using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace visualprogramming.Lab6
{
    public class PlayingCardControl : Control
    {
        public enum SuitType { Spades, Hearts, Diamonds, Clubs }
        public enum BackStyleType { Blue, Red, Green }

        private string _rank = "A";
        private SuitType _suit = SuitType.Spades;
        private bool _isFaceUp = true;
        private BackStyleType _backStyle = BackStyleType.Blue;

        private Point _originalLocation;

        [Category("Игральная карта")]
        [Description("Достоинство карты.")]
        [DefaultValue("A")]
        public string Rank
        {
            get => _rank;
            set { _rank = value; Refresh(); }
        }

        [Category("Игральная карта")]
        [Description("Масть карты.")]
        [DefaultValue(SuitType.Spades)]
        public SuitType Suit
        {
            get => _suit;
            set { _suit = value; Refresh(); }
        }

        [Category("Игральная карта")]
        [Description("Положение карты.")]
        [DefaultValue(true)]
        public bool IsFaceUp
        {
            get => _isFaceUp;
            set { _isFaceUp = value; Refresh(); }
        }

        [Category("Игральная карта")]
        [Description("Стиль рубашки карты.")]
        [DefaultValue(BackStyleType.Blue)]
        public BackStyleType BackStyle
        {
            get => _backStyle;
            set { _backStyle = value; Refresh(); }
        }

        public PlayingCardControl()
        {
            DoubleBuffered = true;
            Size = new Size(100, 150);

            MouseEnter += Card_MouseEnter;
            MouseLeave += Card_MouseLeave;
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            _originalLocation = Location;
            Location = new Point(Location.X, Location.Y - 20);
            BringToFront();
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            Location = _originalLocation;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            var rect = ClientRectangle;

            g.FillRectangle(Brushes.White, rect);
            g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);

            if (IsFaceUp)
            {
                Brush brush = (Suit == SuitType.Hearts || Suit == SuitType.Diamonds)
                    ? Brushes.Red : Brushes.Black;

                using var font = new Font("Arial", 14, FontStyle.Bold);
                g.DrawString(Rank, font, brush, 5, 5);

                string suitSymbol = Suit switch
                {
                    SuitType.Spades => "♠",
                    SuitType.Hearts => "♥",
                    SuitType.Diamonds => "♦",
                    SuitType.Clubs => "♣",
                    _ => "?"
                };

                using var bigFont = new Font("Arial", 40, FontStyle.Bold);
                g.DrawString(suitSymbol, bigFont, brush, rect.Width / 2 - 20, rect.Height / 2 - 30);
            }
            else
            {
                Brush backBrush = BackStyle switch
                {
                    BackStyleType.Blue => Brushes.Blue,
                    BackStyleType.Red => Brushes.Red,
                    BackStyleType.Green => Brushes.Green,
                    _ => Brushes.Gray
                };

                g.FillRectangle(backBrush, rect);
                g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
        }
    }
}
