using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using password_manager.Services;
using password_manager.Model;
using System.Windows.Forms;

namespace password_manager
{
    public partial class control_panel : Form
    {
        public control_panel()
        {
            InitializeComponent();
        }
        private void control_panel_Load(object sender, EventArgs e)
        {
            label1.Text = "name";
            label2.Text = "username";
            label3.Text = "password";
            button1.Text = "save";
            this.AcceptButton = button1;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            show_item();
        }
        private void show_item()
        {
            listBox1.Items.Clear();
            CryptoService cryptoService = new CryptoService();
            var lines = File.ReadLines(string.Format(@"D:\cr\pwdmngr\{0}\item.txt", cryptoService.sha256Hash(User.username)));
            foreach (var line in lines)
            {
                listBox1.Items.Add(cryptoService.textDecrytion(line, User.key, User.iv));
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""& textBox2.Text == "" & textBox3.Text == "")
            {
                MessageBox.Show("invalid registration information");
            }
            else
            {
                CryptoService cryptoService = new CryptoService();
                string wrting = cryptoService.textEncrytion(textBox1.Text + ":" + textBox2.Text + ":" + textBox3.Text, User.key, User.iv);
                StreamWriter sw;
                sw = File.AppendText(string.Format(@"D:\cr\pwdmngr\{0}\item.txt", cryptoService.sha256Hash(User.username)));
                sw.WriteLine(wrting);
                sw.Close();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                show_item();
            }  
        }
    }
}
