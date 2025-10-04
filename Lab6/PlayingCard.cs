using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace visualprogramming.Lab6
{
    public partial class PlayingCard : Form
    {
        private readonly List<PlayingCardControl> _cards = new List<PlayingCardControl>();

        public PlayingCard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                return;

            PlayingCardControl.SuitType suitType = comboBox2.SelectedItem.ToString() switch
            {
                "Spades" => PlayingCardControl.SuitType.Spades,
                "Hearts" => PlayingCardControl.SuitType.Hearts,
                "Diamonds" => PlayingCardControl.SuitType.Diamonds,
                "Clubs" => PlayingCardControl.SuitType.Clubs,
                _ => PlayingCardControl.SuitType.Hearts
            };

            string rank = comboBox1.SelectedItem.ToString();

            var card = new PlayingCardControl
            {
                Rank = rank,
                Suit = suitType,
                IsFaceUp = true,
                BackStyle = PlayingCardControl.BackStyleType.Blue,
                Size = new Size(120, 180)
            };

            card.Click += Card_Click;

            Controls.Add(card);
            _cards.Add(card);

            ArrangeCardsLikeHand();
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (sender is PlayingCardControl card)
            {
                _cards.Remove(card);
                Controls.Remove(card);
                card.Dispose();

                ArrangeCardsLikeHand();
            }
        }

        private void ArrangeCardsLikeHand()
        {
            int startX = 20;
            int startY = 200;
            int offset = 30;

            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].Location = new Point(startX + i * offset, startY);
                _cards[i].BringToFront();
            }
        }
    }
}
