using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using password_manager.Model;
using password_manager.Services;

namespace password_manager
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }
        public static void file_create()
        {
            bool control = Directory.Exists(@"C:\cr\pwdmngr");
            if (control)
            {
            }
            else
            {
                Directory.CreateDirectory(@"C:\cr\pwdmngr");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Login";
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AcceptButton = button2;
            this.textBox2.PasswordChar = '*';
            button1.Text = "register";
            button2.Text = "login";
            label1.Text = "username";
            label2.Text = "password";
            file_create();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new_user newUser = new new_user();
            newUser.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("invalid username");
            }
            else
            {
                HashingService hashingService = new HashingService();
                string crypto_user = hashingService.sha256Hash(textBox1.Text);
                string crypto_pass = hashingService.sha256Hash(textBox2.Text);
                user_pass_encry control_user = new user_pass_encry();
                bool result = control_user.user_control(crypto_user, crypto_pass);
                if (result)
                {
                    User.username = textBox1.Text;
                    User.key = hashingService.md5hash(Encoding.ASCII.GetBytes(textBox1.Text + textBox2.Text));
                    User.iv = hashingService.md5hash(Encoding.ASCII.GetBytes(textBox1.Text + textBox2.Text)).Substring(0, 16);
                    textBox1.Clear();
                    textBox2.Clear();
                    control_panel contrlpnl = new control_panel();
                    contrlpnl.Show();
                }
                else
                {
                    MessageBox.Show("username or password is invalid");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
