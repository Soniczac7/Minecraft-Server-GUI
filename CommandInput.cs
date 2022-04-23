namespace Minecraft_Server_GUI
{
    public partial class CommandInput : Form
    {
        public static CommandInput input;

        public CommandInput()
        {
            InitializeComponent();
            input = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Submit
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Cancel
            textBox1.Text = "";
            this.Close();
        }
    }
}
