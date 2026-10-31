using System;
using System.Windows.Forms;
using sackMAN.offsets.LBP2;

namespace sackMAN.LBP2
{
    public partial class LBP2Form : Form
    {
        static ModLoaderForm modLoaderForm;
        public Form InputDisplay;
        public lbp2 game;
        private AutosplitterHelper autosplitterHelper;
        public LBP2Form(lbp2 game)
        {
            this.game = game;
            
            InitializeComponent();

            if (this.game.HasInputDisplay)
            {
                this.game.SetupInputDisplayMemorySubs();
            }
        }

        private void LBP2Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.api.Disconnect();
            func.api.Disconnect();
            Application.Exit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                // Disable autosplitter.
                autosplitterHelper.Stop();
                autosplitterHelper = null;
            }
            else
            {
                // Enable autosplitter
                Console.WriteLine("Autosplitter starting!");
                autosplitterHelper = new AutosplitterHelper();
                autosplitterHelper.StartAutosplitterForGame(this.game);
            }
        }


        private void switchGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClosed -= LBP2Form_FormClosed;
            Program.AttachPS3Form.Show();
            Close();
        }

        private void memoryViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemoryForm memoryForm = new MemoryForm();
            memoryForm.Show();
        }

        private void patchLoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["ModLoaderForm"] as ModLoaderForm) != null)
            {
                modLoaderForm.Activate();
            }
            else
            {
                modLoaderForm = new ModLoaderForm();
                modLoaderForm.Show();
            }
        }

        private void inputDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InputDisplay == null)
            {
                InputDisplay = new InputDisplay();
                InputDisplay.FormClosed += InputDisplay_FormClosed;
                InputDisplay.Show();
            }
        }

        private void InputDisplay_FormClosed(object sender, FormClosedEventArgs e)
        {
            InputDisplay = null;
        }
    }

    
}
