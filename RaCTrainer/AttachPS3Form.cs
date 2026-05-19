using sackMAN.LBP1;
using sackMAN.LBP2;
using sackMAN.Memory;
using sackMAN.offsets.LBP1;
using sackMAN.offsets.LBP2;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace sackMAN
{
    public partial class AttachPS3Form : Form
    {
        bool useOldAPI = false;

        public static SackmanConsole console;

        public static SackmanScripting scripting;

        static ModLoaderForm modLoaderForm;
        static MemoryForm memoryForm;
        public static bool notSupported = false;

        public AttachPS3Form()
        {
            InitializeComponent();

            SackmanConsole.RedirectOutput();

            console = new SackmanConsole();
            scripting = new SackmanScripting();

            currentVerLabel.Text = "v" + Assembly.GetEntryAssembly().GetName().Version.ToString(3);

            if (File.Exists(Environment.CurrentDirectory + @"\config.txt"))
            {
                ip = func.GetConfigData("config.txt", "ip");
            }
            else
            {
                var config = File.Create("config.txt");
                config.Close();
            }
            IPTextBox.Text = ip;

            // Make a confirm alert dialog to make sure the user confirms to the terms of service
            // If they don't, close the program
            var tos = func.GetConfigData("config.txt", "tos");

            if (tos == "")
            {
                var dialogResult = MessageBox.Show("By using this program, you agree that trans rights are human rights?", "Terms of Service", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    // Show a dialog that explains why it's important to agree to the terms of service
                    MessageBox.Show("Get fucked.");
                    Environment.Exit(0);
                }
                else
                {
                    func.ChangeFileLines("config.txt", "yes", "tos");
                }
            }

            ConfigureCombos.GetCombos();
        }

        public static string ip;
        public static int pid;
        public static string game;
        public static string gameName;

        private int pleaseStartTheGame = 1;

        private string[] startGameText = {
                "You need to start the game first." ,
                "Bro, you need to start the game first.",
                "You're not in a game. You need to be in a game to attach sackMAN.",
                "Are you even reading the error messages? Please start the game.",
                "What the fuck? Can you please start the game before hitting \"Attach\"?",
                "???",
                "Fr, start the game on your PS3.",
                "Why? What's your problem?",
                "Fuck you",
                "This is getting ridiculous.",
                "I'm begging you, start the game.",
        };


        private void AttachPS3Form_Load(object sender, EventArgs e)
        {

        }

        private void attachButton_Click(object sender, EventArgs e)
        {
            ip = IPTextBox.Text;
            func.ChangeFileLines("config.txt", Convert.ToString(ip), "ip");

            func.api = this.useOldAPI ? (IPS3API)new WebMAN(ip) : (IPS3API)new Ratchetron(ip);

            if (!this.useOldAPI)
            {
                if (!func.PrepareRatchetron(ip))
                {
                    return;
                }
            }

            Attach(func.api);
        }

        private void Attach(IPS3API api)
        {
            if (!api.Connect())
            {
                MessageBox.Show("Couldn't connect to the game.");
                return;
            }

            try
            {
                game = func.current_game(ip);
                pid = func.current_pid(ip);
            }
            catch
            {
                MessageBox.Show("invalid ip/web exception.");
            }

            if (pid == 0)
            {
                MessageBox.Show(startGameText[pleaseStartTheGame - 1], "Game is not running");

                if (pleaseStartTheGame < startGameText.Length)
                {
                    pleaseStartTheGame += 1;
                }

                return;
            }

            if (game == "BCUS98245" || game == "BCES00850" || game == "BCES01086" || game == "BCJS30058" || game == "BCAS20113" || game == "BCKS10150")
            {
                gameName = "LBP2";
                func.api.Notify("sackMAN connected!");
                LBP2VersionForm formVersion = new LBP2VersionForm();
                formVersion.ShowDialog();
                Hide();
                switch (LBP2VersionForm.LBP2VersionType)
                {

                    case "v1.33/Latest":
                        {
                            LBP2Form lbp2 = new LBP2Form(new lbp2(func.api));
                            lbp2.ShowDialog();
                            break;
                        }
                    case "v1.00/Unpatched":
                        {
                            LBP2Form lbp2 = new LBP2Form(new lbp2(func.api));
                            lbp2.ShowDialog();
                            break;
                        }
                }
            }
            else if (game == "NPUA80662" || game == "NPEA00324" || game == "BCUS98372" || game == "BCES01693" || game == "BCES01694" || game == "NPEA00437")
            {
                gameName = "LBP2";
                Hide();
                func.api.Notify("sackMAN connected!");
                MessageBox.Show("You're on a release of the game that does not support v1.00. This will default to thinking the game is on latest patch. Be sure that you are on latest!");
                LBP2Form lbp2 = new LBP2Form(new lbp2(func.api));
                lbp2.ShowDialog();
            }
            else if (game == "BCES00141" || game == "BCUS98148" || game == "BCAS20058" || game == "BCJS30018" || game == "BCUS98208" || game == "BCAS20078")
            {
                gameName = "LBP1";
                func.api.Notify("sackMAN connected!");
                LBP1VersionForm formVersion = new LBP1VersionForm();
                formVersion.ShowDialog();
                Hide();
                switch (LBP1VersionForm.LBP1VersionType)
                {

                    case "v1.30/Latest":
                        {
                            LBP1Form lbp1 = new LBP1Form(new lbp1(func.api));
                            lbp1.ShowDialog();
                            break;
                        }
                    case "v1.21":
                        {
                            LBP1Form lbp1 = new LBP1Form(new lbp1(func.api));
                            lbp1.ShowDialog();
                            break;
                        }
                }
            }
            else if (game == "NPEA00241" || game == "NPUA80472" || game == "NPJA00052" || game == "NPHA80092" || game == "BCES00611")
            {
                Hide();
                func.api.Notify("sackMAN connected!");
                MessageBox.Show("You're on a release of the game that does not support v1.21. This will default to thinking the game is on latest patch. Be sure that you are on latest!");
                LBP1Form lbp1 = new LBP1Form(new lbp1(func.api));
                gameName = "LBP1";
                lbp1.ShowDialog();
            }
            else
            {
                if (game.Length > 0)
                {
                    MessageBox.Show($"{game} isn't supported yet. You can still apply mods if you have any.");

                    if ((Application.OpenForms["ModLoaderForm"] as ModLoaderForm) != null)
                    {
                        modLoaderForm.Activate();
                    }
                    else
                    {
                        // memory viewer does not do anything if you dont initialize one of the forms... for whatever reason
                        // horrible hack i am so fucking lazy to figure out this shit
                        // fuck this codebase
                        new LBP1Form(new lbp1(func.api));

                        modLoaderForm = new ModLoaderForm();
                        modLoaderForm.Show();

                        memoryForm = new MemoryForm();
                        memoryForm.Show();
                        notSupported = true;
                    }
                }
                else
                {
                    MessageBox.Show("Game isn't running or isn't supported yet.");
                }
            }
        }

        private void currentVerLabel_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.useOldAPI = ((CheckBox)sender).Checked;
        }

        private void IPTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                attachButton_Click(IPTextBox, e);
            }
        }
        public static bool isEmulator;
        private void AttachRPCS3Button_Click(object sender, EventArgs e)
        {
            func.api = new RPCS3("FUCK");
            isEmulator = true;

            Attach(func.api);
        }
    }
}
