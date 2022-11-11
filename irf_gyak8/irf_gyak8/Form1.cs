using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace irf_gyak8
{
    public partial class Form1 : Form
    {
        PortfolioDataSet context = new PortfolioDataSet();
        List<Tick> Ticks;
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'portfolioDataSet.Tick' table. You can move, or remove it, as needed.
            this.tickTableAdapter.Fill(this.portfolioDataSet.Tick);

        }
    }
}
