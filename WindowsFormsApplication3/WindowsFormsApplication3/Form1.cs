using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;
using System.IO;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        String gyujto(Ember peldany, int szint)
        {
            if (peldany.Bejarva)
                return "";

            peldany.Bejarva = true;

            StringBuilder sb = new StringBuilder();

            if (peldany.parja != null)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(peldany.parja.Nev);
                sb.Append(";");

                sb.Append(gyujto(peldany.parja, szint));
            }

            foreach (Ember e in peldany.gyermekek)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(e.Nev);
                sb.Append("[penwidth =" + szint * 2 + ", color = \"" + ColorTranslator.ToHtml(peldany.Szín) + "\"];");
                sb.Append(gyujto(e, szint + 1));
            }

            return sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ember Apa = new Ember("Apa");
            Ember Anya = new Ember("Anya");

            Ember gy1 = new Ember("Gy1", Color.Red);
            Ember gy2 = new Ember("Gy2");

            Ember u1 = new Ember("U1");

            Ember uu1 = new Ember("UU1");
            Ember uu1parja = new Ember("UU1parja");

            Ember uuu1 = new Ember("UUU1");
            Ember uuu2 = new Ember("UUU2");

            Apa.parja = Anya;
            Anya.parja = Apa;
            uu1.parja = uu1parja;
            uu1parja.parja = uu1;

            Apa.gyermekek.Add(gy1);
            Apa.gyermekek.Add(gy2);
            Anya.gyermekek.Add(gy1);
            Anya.gyermekek.Add(gy2);

            gy1.gyermekek.Add(u1);

            u1.gyermekek.Add(uu1);

            uu1parja.gyermekek.Add(uuu1);
            uu1parja.gyermekek.Add(uuu2);

            String s = gyujto(Apa, 1);

            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            var wrapper = new GraphGeneration(getStartProcessQuery,
                                              getProcessStartInfoQuery,
                                              registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph("digraph{" + s + "}", Enums.GraphReturnType.Png);

            using (MemoryStream ms = new MemoryStream(output))
            {
                Image i = Image.FromStream(ms);
                pictureBox1.Image = i;
            }
        }
    }
}
