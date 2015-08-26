using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testerd
{
    public partial class Dotan : Form
    {
        public Dotan()
        {
            InitializeComponent();
        }
        public void Set(string i_Str)
        {
            richTextBox1.Text = i_Str;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
