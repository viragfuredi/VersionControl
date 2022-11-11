using irf_gyak8.Entities;
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
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;
        List<PortfolioItem> Portfolio = new List<PortfolioItem>();

        

        public Form1()
        {
            InitializeComponent();
            CreatePortfolio();
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;

           
        }

        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;

        }

        


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'portfolioDataSet.Tick' table. You can move, or remove it, as needed.
            this.tickTableAdapter.Fill(this.portfolioDataSet.Tick);

           

            List<decimal> Nyereségek = new List<decimal>();
            int idointervalum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;
            for (int i = 0; i < z.Days - idointervalum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + idointervalum))
                           - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);
            }

            var nyereségekRendezve = (from x in Nyereségek
                                      orderby x
                                      select x)
                                      .ToList();
            MessageBox.Show(nyereségekRendezve[nyereségekRendezve.Count() / 5].ToString());
        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolio)
            {
                var last = (from x in Ticks
                            where item.Index == x.Index.Trim()
                            && date <= x.TradingDay
                            select x)
                            .First();
                value += (decimal)last.Price * item.Volume;
                
            }
            return value;
        }

       
    }
}
