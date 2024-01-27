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

namespace client {
    public partial class Form1 : Form {
        bool terminating = false; // Initially it is not terminating
        bool isUsernameValid = true; // Initially assume the username is valid
        bool connected = false; // Initially it is not connected
        Socket clientSocket;
        public Form1() // Start the form
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
                logs_connect.AppendText("Username cannot be empty!\n"); // Let user know
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
                    button_disconnect.Enabled = true;
                    logs_connect.Enabled = true;
                    button_subscribe_if100.Enabled = true;
                    button_subscribe_sps101.Enabled = true;
                    connected = true; // Update connection status
                    Byte[] buffer = Encoding.Default.GetBytes(username);
                    clientSocket.Send(buffer);
                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start(); // Create and start the thread
                    Thread.Sleep(500); // Give some time for the server to respond
                    if (!isUsernameValid)
                    {
                        // If username is not valid, reset the connection
                        clientSocket.Close();
                        connected = false; // Update the connection status
                        isUsernameValid = true; // Reset for next attempt
                        // Edit buttons properities accordingly
                        button_connect.Enabled = true;
                        button_disconnect.Enabled = false;
                        button_subscribe_if100.Enabled = false;
                        button_subscribe_sps101.Enabled = false;
                        return;
                    }
                }
                catch // Wrong IP
                {
                    logs_connect.AppendText("Could not connect to the server!\n");
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

                    if (incomingMessage == "Username exists. Please choose another username.")
                    {
                        logs_connect.AppendText(incomingMessage + "\n");
                        isUsernameValid = false; // Set the flag to false if username is not valid
                        break; // Break out of the loop as the server will close the connection
                    }
                    else if (incomingMessage.StartsWith("Connected"))
                    {
                        logs_connect.AppendText(incomingMessage + "\n");
                        isUsernameValid = true; // Set the flag to true
                        connected = true;
                        button_connect.Enabled = false;
                        button_disconnect.Enabled = true;
                        button_subscribe_if100.Enabled = true;
                        button_subscribe_sps101.Enabled = true;
                    }
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

                }
                catch
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
                }
            }
            if (!isUsernameValid)
            {
                // Reset UI and connection status if username is not valid
                Invoke(new Action(() =>
                {
                    button_connect.Enabled = true;
                    button_disconnect.Enabled = false;
                    button_subscribe_if100.Enabled = false;
                    button_subscribe_sps101.Enabled = false;
                }));
            }


        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) // CLose the form
        {
            connected = false; // Update connection status
            terminating = true; // Termination
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