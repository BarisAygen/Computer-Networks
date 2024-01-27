using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {
        bool terminating = false; // Initially it is not terminating
        bool connected = false; // Initially it is not connected
        Socket clientSocket;

        public Form1() // Start form
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void button_connect_Click(object sender, EventArgs e) // When client clicks to connect
        {
            string username = textBox_username.Text;
            if (string.IsNullOrWhiteSpace(username)) // Check if username is empty or consists only of white-space
            {
                logs_connect.AppendText("Username cannot be empty!\n");
                return; // Prevent further execution if username is empty
            }

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            int portNum;
            if (Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);

                    // Edit buttons properties accordingly
                    button_connect.Enabled = false;
                    logs_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_subscribe_if100.Enabled = false;
                    button_subscribe_sps101.Enabled = false;
                    connected = true;
                    // Send the username to the server for validation
                    Byte[] buffer = Encoding.Default.GetBytes(username);
                    clientSocket.Send(buffer);

                    // Start the thread to receive messages from the server
                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();

                }
                catch // Wrong IP
                {
                    logs_connect.AppendText("Could not connect to the server!\n");

                    // Re-enable the connect button in case of connection failure
                    button_connect.Enabled = true;
                }
            }
            else // Wrong port number
            {
                logs_connect.AppendText("Check the port\n");
            }
        }


        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    this.Invoke((MethodInvoker)delegate
                        {
                            if (incomingMessage == "Username exists. Please choose another username.")
                            {
                                logs_connect.AppendText(incomingMessage + "\n");
                                button_connect.Enabled = true;
                                // Disable other buttons that require a connection
                                button_disconnect.Enabled = false;
                                button_subscribe_if100.Enabled = false;
                                button_subscribe_sps101.Enabled = false;
                                connected = false;

                            }
                            else if (incomingMessage == "Username valid")
                            {
                                // Username is valid
                                logs_connect.AppendText("Connected to the server!\n");
                                logs_connect.AppendText("Welcome " + textBox_username.Text + "\n");

                                // Enable relevant UI elements
                                button_disconnect.Enabled = true;
                                button_subscribe_if100.Enabled = true;
                                button_subscribe_sps101.Enabled = true;

                            }

                            else
                            {
                                if (incomingMessage.StartsWith("subscribed:"))
                                {
                                    string channel = incomingMessage.Split(':')[1];
                                    if (channel == "if100")
                                    {
                                        // Edit form properities accordingly
                                        logs_if100.Enabled = true;
                                        textBox_message_if100.Enabled = true;
                                        button_send_if100.Enabled = true;
                                        button_subscribe_if100.Enabled = false;
                                        button_unsubscribe_if100.Enabled = true;
                                        logs_connect.AppendText("Subscribed to the IF100 channel.\n");
                                    }
                                    else if (channel == "sps101")
                                    {
                                        // Edit form properities accordingly
                                        logs_sps101.Enabled = true;
                                        textBox_message_sps101.Enabled = true;
                                        button_send_sps101.Enabled = true;
                                        button_subscribe_sps101.Enabled = false;
                                        button_unsubscribe_sps101.Enabled = true;
                                        logs_connect.AppendText("Subscribed to the SPS101 channel.\n");
                                    }
                                }
                                else if (incomingMessage.StartsWith("if100:"))
                                {
                                    string channelMessage = incomingMessage.Substring(11); // Remove the "if100:" prefix
                                    logs_if100.AppendText(channelMessage + "\n");
                                }
                                else if (incomingMessage.StartsWith("sps101:"))
                                {
                                    string channelMessage = incomingMessage.Substring(12); // Remove the "sps101:" prefix
                                    logs_sps101.AppendText(channelMessage + "\n");
                                }
                                else
                                {
                                    logs_connect.AppendText("Server: " + incomingMessage + "\n");
                                }
                            }
                    });


                }
                catch
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (!terminating)
                        {
                            logs_connect.AppendText("The server has disconnected\n");
                            button_connect.Enabled = true;
                            textBox_message_if100.Enabled = false;
                            button_send_if100.Enabled = false;
                            textBox_message_sps101.Enabled = false;
                            button_send_sps101.Enabled = false;
                        }

                        clientSocket.Close();
                        connected = false;
                    });
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) // CLose the form
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_if100_Click(object sender, EventArgs e)
        {
            string message = textBox_message_if100.Text;

            if (message != "" && message.Length <= 64)
            {
                string formattedMessage = "if100:" + message; // Format the message to indicate it's for the if100 channel
                Byte[] buffer = Encoding.Default.GetBytes(formattedMessage);
                clientSocket.Send(buffer);
            }
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                string disconnectMessage = "disconnect"; // Send a message to the server indicating that the client is disconnecting
                Byte[] buffer = Encoding.Default.GetBytes(disconnectMessage);
                clientSocket.Send(buffer);

                clientSocket.Close(); // Close the socket and update the connection status
                connected = false;
                terminating = true;

                // Edit form properities
                button_connect.Enabled = true;
                button_disconnect.Enabled = false;
                button_subscribe_if100.Enabled = false;
                button_subscribe_sps101.Enabled = false;
                button_unsubscribe_if100.Enabled = false;
                button_unsubscribe_sps101.Enabled = false;
                textBox_message_if100.Enabled = false;
                textBox_message_sps101.Enabled = false;
                button_send_if100.Enabled = false;
                button_send_sps101.Enabled = false;
                logs_if100.Enabled = false;
                logs_sps101.Enabled = false;
                logs_if100.Clear();
                logs_sps101.Clear();
                logs_connect.AppendText("Disconnected from the server.\n");
            }
            else
            {
                logs_connect.AppendText("You are not connected to a server.\n");
            }
        }

        private void button_subscribe_if100_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                string subscribeMessage = "subscribe:if100"; // This is the message you will send to the server
                Byte[] buffer = Encoding.Default.GetBytes(subscribeMessage);
                clientSocket.Send(buffer); // Send message to the server
                logs_connect.AppendText("Attempting to subscribe to if100...\n");
            }
            else
            {
                logs_connect.AppendText("You are not connected to a server.\n");
            }
        }

        private void button_unsubscribe_if100_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                string unsubscribeMessage = "unsubscribe:if100"; // Unsubscribe request format
                Byte[] buffer = Encoding.Default.GetBytes(unsubscribeMessage);
                clientSocket.Send(buffer);

                // Update properities accordingly
                button_unsubscribe_if100.Enabled = false;
                textBox_message_if100.Enabled = false;
                button_send_if100.Enabled = false;
                button_subscribe_if100.Enabled = true;
                logs_if100.Clear();
                logs_if100.Enabled = false;
                logs_connect.AppendText("Unsubscribed from if100 channel.\n");
            }
            else
            {
                logs_connect.AppendText("You are not connected to a server.\n");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button_subscribe_sps101_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                string subscribeMessage = "subscribe:sps101"; // This is the message you will send to the server
                Byte[] buffer = Encoding.Default.GetBytes(subscribeMessage);
                clientSocket.Send(buffer); // Send message to the server
                logs_connect.AppendText("Attempting to subscribe to sps101...\n");
            }
            else
            {
                logs_connect.AppendText("You are not connected to a server.\n");
            }
        }

        private void button_unsubscribe_sps101_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                string unsubscribeMessage = "unsubscribe:sps101"; // Unsubscribe request format
                Byte[] buffer = Encoding.Default.GetBytes(unsubscribeMessage);
                clientSocket.Send(buffer);

                // Update properities accordingly
                button_unsubscribe_sps101.Enabled = false;
                textBox_message_sps101.Enabled = false;
                button_send_sps101.Enabled = false;
                button_subscribe_sps101.Enabled = true;
                logs_sps101.Clear();
                logs_sps101.Enabled = false;
                logs_connect.AppendText("Unsubscribed from sps101 channel.\n");
            }
            else
            {
                logs_connect.AppendText("You are not connected to a server.\n");
            }
        }

        private void button_send_sps101_Click(object sender, EventArgs e)
        {
            string message = textBox_message_sps101.Text;

            if (message != "" && message.Length <= 64)
            {
                string formattedMessage = "sps101:" + message; // Format the message to indicate it's for the sps101 channel
                Byte[] buffer = Encoding.Default.GetBytes(formattedMessage);
                clientSocket.Send(buffer);
            }
        }
    }
}