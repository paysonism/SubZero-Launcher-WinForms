using KeyAuth;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.IO.Compression;

namespace SubZero_Loader
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private const string updateServerURL = "https://raw.githubusercontent.com/paysonism/SubZero-Launcher-WinForms/main/version";

        public void CheckForUpdates()
        {
            try
            {
                WebClient webClient = new WebClient();
                string serverVersion = webClient.DownloadString(updateServerURL);

                Version latestVersion = new Version(serverVersion);
                Version currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                if (latestVersion > currentVersion)
                {
                    MessageBox.Show("Outdated Version. Now downloading the latest release.", "Outdated");
                    DownloadUpdate();
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private void ReplaceFiles(string extractPath)
        {
            string[] updateFiles = Directory.GetFiles(extractPath);

            foreach (string file in updateFiles)
            {
                string fileName = Path.GetFileName(file);
                string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                File.Copy(file, destinationPath, true);
            }
        }

        private void DownloadUpdate()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadFile("https://github.com/paysonism/SubZero-Launcher-WinForms/blob/main/Debug.rar", "update.zip");

                string extractPath = "update";
                ZipFile.ExtractToDirectory("update.zip", extractPath);

                // Replace files in the application directory
                ReplaceFiles(extractPath);

                // Clean up temporary files
                Directory.Delete(extractPath, true);

                Process.Start("Launcher.exe");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error downloading update: " + ex.Message);
            }
        }

        public static api KeyAuthApp = new api(
            name: "SubZeroFn",
            ownerid: "RRMW917rnj",
            secret: "265317db5944f51d569353d9b0af023db788d2d57ee7052ab475b38cc938b4c4",
            version: "1.0"
        );

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            this.Opacity = .9;
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            loginPanel.Hide();
            registerPanel.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            registerPanel.Hide();
            loginPanel.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            timer1.Start();
            // KeyAuthApp.init();
            registerPanel.Hide();
            loginPanel.Show();
            CheckForUpdates();
        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            /*
            KeyAuthApp.login(username.Text, password.Text);
            if (KeyAuthApp.response.valid)
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password, please try again.", "Incorrect User/Pass", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
            if (username.Text == "test" && password.Text == "test")
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
        }

        private void regBtn_Click(object sender, EventArgs e)
        {
            /*
            KeyAuthApp.register(regUser.Text, regPass.Text, key.Text);
            if (KeyAuthApp.response.valid)
            {
                Main main = new Main();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Key. Please ensure that your key was typed correctly.", "Invalid");
            }
            */
            MessageBox.Show("enable keyauth in the app to use registration.", "");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/paysonism");
            MessageBox.Show("Made By Payson", "Credits");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Opacity += .2;
        }
    }
}
