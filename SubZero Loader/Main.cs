using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace SubZero_Loader
{
    public partial class Main : Form
    {
        enum ThemeColor
        {
            Primary,
            Secondary,
            Tertiary,
            Text
        }

        private Panel[] panels;

        Dictionary<ThemeColor, Color> Light = new Dictionary<ThemeColor, Color>();
        Dictionary<ThemeColor, Color> Blue = new Dictionary<ThemeColor, Color>();
        Dictionary<ThemeColor, Color> Dark = new Dictionary<ThemeColor, Color>();
        public Main()
        {
            InitializeComponent();
            Username();

            panels = new Panel[] { panel1, panel2, panel3, panel4, panel5 };

            foreach (Panel panel in panels)
            {
                panel.Visible = false;
            }
            panels[0].Visible = true;

            Light = new Dictionary<ThemeColor, Color>() {
                { ThemeColor.Primary, Color.White },
                { ThemeColor.Secondary, Color.Gray },
                { ThemeColor.Tertiary, Color.White },
                { ThemeColor.Text, Color.Black }
            };
            Blue = new Dictionary<ThemeColor, Color>() {
                { ThemeColor.Primary, Color.MidnightBlue },
                { ThemeColor.Secondary, Color.DodgerBlue },
                { ThemeColor.Tertiary, Color.MidnightBlue },
                { ThemeColor.Text, Color.White }
            };
            Dark = new Dictionary<ThemeColor, Color>() {
                { ThemeColor.Primary, Color.Black },
                { ThemeColor.Secondary, Color.FromArgb(25, 25, 25) },
                { ThemeColor.Tertiary, Color.Black },
                { ThemeColor.Text, Color.White }
            };
        }

        private void tab(int index)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].Visible = (i == index);
                panels[i].BringToFront();
            }
        }

        int currentTheme = 1; // 1 = black, 2 = light, 3 = blue
        
        private void Username()
        {
            // Get the current Windows user's identity
            WindowsIdentity currentUser = WindowsIdentity.GetCurrent();

            if (currentUser != null)
            {
                // Get the user's name
                string userName = currentUser.Name;

                // Extract just the username without the domain (if any)
                int idx = userName.IndexOf('\\');
                if (idx != -1)
                {
                    userName = userName.Substring(idx + 1);
                }

                // Display the username on a label
                label10.Text = "Welcome, " + userName + "!";
            }
            else
            {
                label10.Text = "Welcome!";
            }
        }

        private void changeTheme(Color primaryColor, Color secondaryColor, Color tertiaryColor)
        {
            button1.BackColor = primaryColor;
            button2.BackColor = primaryColor;
            button3.BackColor = primaryColor;
            button4.BackColor = primaryColor;
            button5.BackColor = primaryColor;
            button6.BackColor = primaryColor;
            button8.BackColor = Color.Black;
            this.BackColor = tertiaryColor;
            button10.BackColor = secondaryColor;
            newsPanel1.BackColor = secondaryColor;
            newsPanel2.BackColor = secondaryColor;
            newsPanel3.BackColor = secondaryColor;
            spoofLog.BackColor = secondaryColor;
        }

        private void changeText(Color textColor)
        {
            // Change color of text
            label1.ForeColor = textColor;
            label2.ForeColor = textColor;
            button8.ForeColor = Color.White;
            button10.ForeColor = textColor;
            label3.ForeColor = textColor;
            label4.ForeColor = textColor;
            label5.ForeColor = textColor;
            label6.ForeColor = textColor;
            label7.ForeColor = textColor;
            label8.ForeColor = textColor;
            label9.ForeColor = textColor;
            label10.ForeColor = textColor;
            label11.ForeColor = textColor;
        }
        static string GenerateRandomMAC(Random random)
        {
            byte[] macAddress = new byte[6];
            random.NextBytes(macAddress);
            macAddress[0] = (byte)(macAddress[0] & 0xFE); // Ensure the MAC address is not multicast
            macAddress[0] = (byte)(macAddress[0] | 0x02); // Ensure the MAC address is locally administered
            return string.Join(":", macAddress
                .Select(b => b.ToString("X2"))); // Convert bytes to hexadecimal string separated by colons
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/paysonism");
            MessageBox.Show("Made By Payson", "Credits");
        }
        public DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            try
            {
                dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            }
            catch
            {
                dtDateTime = DateTime.MaxValue;
            }
            return dtDateTime;
        }
        private void Main_Load(object sender, EventArgs e)
        {
            // label3.Text = "Expiry: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry));
            // label11.Text = "Expiry: " + UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry));
            timer1.Start();
            string themePath = @"bin\theme";

            try
            {
                string fileContents = File.ReadAllText(themePath);

                if (int.TryParse(fileContents, out int value))
                {
                    currentTheme = value;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {   

            }
            if (currentTheme == 1)
            {
                changeTheme(Dark[ThemeColor.Primary], Dark[ThemeColor.Secondary], Dark[ThemeColor.Tertiary]);
                changeText(Dark[ThemeColor.Text]);
            }

            if (currentTheme == 2)
            {
                changeTheme(Light[ThemeColor.Primary], Light[ThemeColor.Secondary], Light[ThemeColor.Tertiary]);
                changeText(Light[ThemeColor.Text]);
            }
            if (currentTheme == 3)
            {
                changeTheme(Blue[ThemeColor.Primary], Blue[ThemeColor.Secondary], Blue[ThemeColor.Tertiary]);
                changeText(Blue[ThemeColor.Text]);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            changeTheme(Light[ThemeColor.Primary], Light[ThemeColor.Secondary], Light[ThemeColor.Tertiary]);
            changeText(Light[ThemeColor.Text]);
            currentTheme = 2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            changeTheme(Dark[ThemeColor.Primary], Dark[ThemeColor.Secondary], Dark[ThemeColor.Tertiary]);
            changeText(Dark[ThemeColor.Text]);
            currentTheme = 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            changeTheme(Blue[ThemeColor.Primary], Blue[ThemeColor.Secondary], Blue[ThemeColor.Tertiary]);
            changeText(Blue[ThemeColor.Text]);
            currentTheme = 3;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string binFolderPath = @"bin";
            string filePath = Path.Combine(binFolderPath, "theme");

            try
            {
                // Write the currentTheme value to a file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(currentTheme);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving theme.");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Opacity += .2;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/subz");
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/paysonism");
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            //label20.Visible = true;
            await Task.Delay(2500);
            //label21.Visible = true;
            await Task.Delay(10000);
            //label22.Visible = true;
            string url = "https://www.example.com/file-to-download.txt";
            string mapPath = @"bin\mapper.exe";
            string drvPath = @"bin\driver.sys";

            WebClient client = new WebClient();

            try
            {
                client.DownloadFile(url, mapPath);
                client.DownloadFile(url, drvPath);
                Process.Start(mapPath, drvPath);
                File.Delete(mapPath);
                File.Delete(drvPath);
            }
            catch (Exception ex)
            {
                
            }
            await Task.Delay(2500);
            //label23.Visible = true;
            try
            {
                System.Management.ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter");
                ManagementObjectCollection adapters = searcher.Get();

                Random random = new Random();

                foreach (ManagementObject adapter in adapters)
                {
                    if (adapter["MACAddress"] != null)
                    {
                        string newMAC = GenerateRandomMAC(random);
                        adapter.InvokeMethod("Disable", null);
                        adapter["MACAddress"] = newMAC;
                        adapter.Put();
                        adapter.InvokeMethod("Enable", null);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            await Task.Delay(2500);
            //label24.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tab(0);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tab(1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tab(2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tab(3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tab(4);
        }
    }
}
