using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using password_manager.Services;

namespace password_manager
{
    public partial class new_user : Form
    {
        public new_user()
        {
            InitializeComponent();
        }
        private void new_user_Load(object sender, EventArgs e)
        {
            this.Text = "New User";
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AcceptButton = button1;
            this.textBox2.PasswordChar = '*';
            this.textBox3.PasswordChar = '*';
            label1.Text = "username";
            label2.Text = "password";
            label3.Text = "password";
            label4.Text = "";
            label5.Text = "";
            button1.Text = "create";
        }
        private static bool user_control(string username)
        {
            HashingService hashingService = new CryptoService();
            string crypt_data = hashingService.sha256Hash(username);
            bool controls = false;
            bool control = File.Exists(@"C:\cr\pwdmngr\users.txt");
            if (control)
            {
                var lines = File.ReadLines(@"C:\cr\pwdmngr\users.txt");
                foreach (var line in lines)
                {
                    if (line.Substring(0, 64) == crypt_data)
                    {
                        controls = true;
                    }
                }
            }
            else
            {
                FileStream fs = File.Create(@"C:\cr\pwdmngr\users.txt");
            }
            return controls;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("invalid username");
            }
            else
            {
                bool control = user_control(textBox1.Text);
                if (control)
                {
                    MessageBox.Show("this username is taken, please enter another username");
                }
                else
                {
                    if (textBox2.Text == textBox3.Text)
                    {
                        try
                        {
                            label4.Text = "";
                            label5.Text = "";
                            HashingService hashingService = new HashingService();
                            string crypt_data = hashingService.sha256Hash(textBox1.Text) + ":" + hashingService.sha256Hash(textBox2.Text);
                            StreamWriter sw;
                            sw = File.AppendText(@"D:\cr\pwdmngr\users.txt");
                            sw.WriteLine(crypt_data);
                            sw.Close();
                            if (Directory.Exists(string.Format(@"D:\cr\pwdmngr\{0}", hashingService.sha256Hash(textBox1.Text))))
                            {
                            }
                            else
                            {
                                Directory.CreateDirectory(string.Format(@"D:\cr\pwdmngr\{0}", hashingService.sha256Hash(textBox1.Text)));
                                FileStream fs = File.Create(string.Format(@"D:\cr\pwdmngr\{0}\item.txt", hashingService.sha256Hash(textBox1.Text)));
                                fs.Close();
                            }
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            MessageBox.Show("user created successfully");
                        }
                        catch (FileLoadException ex)
                        {
                            MessageBox.Show("" + ex);
                        }
                    }
                    else
                    {
                        label4.Text = "!";
                        label5.Text = "!";
                    }
                }
            }
        }
    }
}
