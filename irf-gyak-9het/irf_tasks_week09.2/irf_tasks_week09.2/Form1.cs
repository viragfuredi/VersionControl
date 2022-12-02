using irf_tasks_week09._2.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace irf_tasks_week09._2
{
    public partial class Form1 : Form
    {

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
     
        Random rng = new Random(1234);
        List<int> ferfiak = new List<int>();
        List<int> nok = new List<int>();
        new int ZaroEv;
        new string NepPath;
        

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            ZaroEv = int.Parse(numericUpDown1.Value.ToString());
            NepPath = richTextBox1.Text;
        }
     

    public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }
            return population;
        }
        public List<BirthProbability> GetBirthProbabilities(string path)
        {
            List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    BirthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        ChildrenNum = int.Parse(line[1]),
                        Birthchance = double.Parse(line[2].Replace(",", "."))
                    });
                }
            }
            return BirthProbabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string path)
        {
            List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');

                    DeathProbabilities.Add(new DeathProbability()
                    {
                        Age = int.Parse(line[1]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        DeathChance = double.Parse(line[2].Replace(",", "."))

                    });
                }
            }
            return DeathProbabilities;
        }

        private void SimStep(int year, Person person)
        {
            if (!person.IsAlive) return;        
            byte age = (byte)(year - person.BirthYear);
                       
            double pDeath = (from x in DeathProbabilities
                             where x.Gender == person.Gender && x.Age == age
                             select x.DeathChance).FirstOrDefault();
            
            if (rng.NextDouble() <= pDeath)
                person.IsAlive = false;

            if (person.IsAlive && person.Gender == Gender.Female)
            {
                double pBirth = (from x in BirthProbabilities
                where x.Age == age
                select x.Birthchance).FirstOrDefault();
                
                if (rng.NextDouble() <= pBirth)
                {
                    Person újszülött = new Person();
                    újszülött.BirthYear = year;
                    újszülött.NbrOfChildren = 0;
                    újszülött.Gender = (Gender)(rng.Next(1, 3));
                    Population.Add(újszülött);
                }
            }
        }

        public void Szimuláció()
        {
            for (int year = 2005; year <= 2024; year++)
            {

                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            NepPath = richTextBox1.Text;
        }
        public void Simulation()
        {
            for (int year = 2005; year <= ZaroEv; year++)
            {

                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                ferfiak.Add(nbrOfMales);
                nok.Add(nbrOfFemales);
            }
        }
        public void DisplayResults()
        {
            int counter = 0;
            for (int year = 2005; year <= ZaroEv; year++)
            {
                richTextBox1.Text += string.Format("Szimulációs év: {0}\n\tFiúk: {1}\n\tLányok: {2}\n\n", year, ferfiak[counter], nok[counter]);
                counter++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = null;
            ferfiak.Clear();
            nok.Clear();
            Simulation();
            DisplayResults();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK) { NepPath = ofd.FileName; richTextBox1.Text = NepPath; }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ZaroEv = int.Parse(numericUpDown1.Value.ToString());
        }
    }

}
    

