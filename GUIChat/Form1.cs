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
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace GUIChat
{
    public partial class Form1 : Form
    {

        private static int mAuthPort = 3461;
        private static int mWritePort = 3462;
        private static int mReadPort = 3463;
        private static String authKey = "";
        private static System.Collections.ArrayList mMessages = new System.Collections.ArrayList();
        public static string inputAddress = "";
        public static string inputName = "";
        public static string inputPass = "";


        public Form1()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void HelloButton_Click(object sender, EventArgs e)

        {
            inputAddress = AddressText.Text;
            inputName = NameText.Text;
            inputPass = PasswordText.Text;

            //create a tcp client connection to the address on the specified port
            TcpClient client = new TcpClient(inputAddress, mAuthPort);
            //Create a buffer stream from the client connection
            BufferedStream stream = new BufferedStream(client.GetStream());
            //reader for the stream
            BinaryReader reader = new BinaryReader(stream);
            //writer of the stream
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(IPAddress.HostToNetworkOrder(inputName.Length));
            writer.Write(Encoding.UTF8.GetBytes(inputName));
            writer.Write(IPAddress.HostToNetworkOrder(inputPass.Length));
            writer.Write(Encoding.UTF8.GetBytes(inputPass));
            //get authenication string
            int auth = IPAddress.NetworkToHostOrder(reader.ReadInt32());
            byte[] authBytes = reader.ReadBytes(auth);
            authKey = Encoding.UTF8.GetString(authBytes);
            reader.Close();
            writer.Close();
            client.Close();

            this.Hide(); //Close Form1,the current open form.

            Form2 frm2 = new Form2();

            frm2.Show(); // Launch Form2,the new form.
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
