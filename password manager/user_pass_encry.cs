using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using password_manager.Services;

namespace password_manager
{
    class user_pass_encry
    {
        public bool control;
        public bool user_control(string username, string password)
        {
            bool control = false;
            var lines = File.ReadLines(@"D:\cr\pwdmngr\users.txt");
            foreach (var line in lines)
            {
                if (line.Substring(0, 64) == username && line.Substring(65, 64) == password)
                {
                    control = true;
                }
            }
            return control;
        }
    }
}
