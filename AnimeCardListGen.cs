using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DevPro_CardManager
{
    public partial class AnimeCardListGen : Form
    {
        
        bool IsWorking = false;
        public AnimeCardListGen()
        {
            InitializeComponent();
            TopLevel = false;
            Dock = DockStyle.Fill;
            Visible = true;

        }

        private void genbtn_Click(object sender, EventArgs e)
        {
            if (!IsWorking)
            {
                Thread gentask = new Thread(GenerateAnimeCardList);
                gentask.IsBackground = true;
                IsWorking = true;
                animelist.Clear();
                gentask.Start();
            }
        }

        private void GenerateAnimeCardList()
        {
            foreach (int id in Program.CardData.Keys)
            {
                if (Program.CardData[id].Ot == 4)
                    AddText(id + " 0");
            }
            IsWorking = false;
        }
        private void AddText(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AddText), text);
            }
            else
            {
                if (animelist.Text == "")
                    animelist.AppendText(text);
                else
                    animelist.AppendText(Environment.NewLine + text);
            }
        }
    }
}
