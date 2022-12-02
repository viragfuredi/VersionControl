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

        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            Random rng = new Random(1234);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
