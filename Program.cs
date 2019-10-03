using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScannerDisplay
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Login login = new Login();
            GetUserType(login);

            Application.Run(login);

            if (Loginuser != null)
            {
                Application.Run(new Mainframe((User)Loginuser));
            }
        }

        private static User? Loginuser
        { get; set; }

        private static void GetUserType(Login login)
        {
            login.OnSuccess += new Login.LoginSuccess(StartMainframe);
        }

        private static void StartMainframe(object sender, User user)
        {
            Loginuser = user;
        }
    }
}
