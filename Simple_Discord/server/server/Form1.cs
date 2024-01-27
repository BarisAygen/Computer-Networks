using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace server
{
    public partial class Form1 : Form
    {

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Create server socket
        List<Socket> clientSockets = new List<Socket>();// Create a list of client sockets
        Dictionary<Socket, List<string>> clientSubscriptions = new Dictionary<Socket, List<string>>(); // Create a dictionary for subscriptions
        Dictionary<Socket, string> clientUsernames = new Dictionary<Socket, string>(); // Create a dictionary for users
        List<string> if100Subscribers = new List<string>(); // List of IF100 subscribers
        List<string> sps101Subscribers = new List<string>(); // List of SPS101 subscribers
        bool terminating = false; // Initially server is no terminating
        bool listening = false; // Initially server is not listening

        public Form1() // Start form
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }
        private void button_listen_Click(object sender, EventArgs e) // Start listening
        {
            int serverPort;

            if(Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(3);
                // Fix buttons properities accordingly
                listening = true;
                button_listen.Enabled = false;
                textBox_message.Enabled = true;
                button_send.Enabled = true;
                Thread acceptThread = new Thread(Accept); // Start thread
                acceptThread.Start();
                logs.AppendText("Started listening on port: " + serverPort + "\n"); // Print listening situation
            }
            else // Wrong port number
            {
                logs.AppendText("Please check port number \n");
            }
        }
        private void Accept()
        {
            while(listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept(); // Create client socket
                    clientSockets.Add(newClient); // Add client in the list

                    Thread receiveThread = new Thread(() => Receive(newClient));
                    receiveThread.Start(); // Create and start the thread
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }
        private void UpdateUserLog()
        {
            logs_users.Clear();
            foreach (var username in clientUsernames.Values)
            {
                logs_users.AppendText(username + "\n"); // Update connection log by printing username
            }
        }

        private void UpdateSubscriptionLog()
        {
            logs_if100_subscriptions.Clear();
            foreach (string username in if100Subscribers)
            {
                logs_if100_subscriptions.AppendText(username + "\n"); // Update IF100 subscribe log by printing username
            }
            logs_sps101_subscriptions.Clear();
            foreach (string username in sps101Subscribers)
            {
                logs_sps101_subscriptions.AppendText(username + "\n"); // Update SPS101 subscribe log by printing username
            }
        }

        private void SubscribeClientToChannel(Socket client, string channel)
        {
            if (!clientSubscriptions.ContainsKey(client)) // If client has not subscribed yet
            {
                clientSubscriptions[client] = new List<string>();
            }
            clientSubscriptions[client].Add(channel); // Add client to channel
        }

        private void BroadcastToChannel(string channel, string message)
        {
            if (channel == "if100")
            {
                Byte[] bufferIf = Encoding.Default.GetBytes("if100:" + message);
                foreach (var client in clientSockets)
                {
                    if (clientUsernames.ContainsKey(client) && if100Subscribers.Contains(clientUsernames[client])) // If client has subscribed to IF100 channel
                    {
                        client.Send(bufferIf); // Send message to the channel
                    }
                }
            }
            else if (channel == "sps101") {
                Byte[] bufferSps = Encoding.Default.GetBytes("sps101:" + message);
                foreach (var client in clientSockets)
                {
                    if (clientUsernames.ContainsKey(client) && sps101Subscribers.Contains(clientUsernames[client])) // If client has subscribed to SPS101 channel
                    {
                        client.Send(bufferSps); // Send message to the channel
                    }
                }
            }
        }

        private void Receive(Socket thisClient)
        {
            bool connected = true; // Connection status is initially true
            bool usernameReceived = false; // Initially no username is recieved
            
            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);
                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (!usernameReceived)
                    {
                        if (clientUsernames.ContainsValue(incomingMessage)) // Check if the received username is unique
                        {
                            string errorMessage = "Username exists. Please choose another username.";
                            Byte[] errorBuffer = Encoding.Default.GetBytes(errorMessage);
                            thisClient.Send(errorBuffer); // Inform the client
                            thisClient.Close(); // Close the client socket
                            connected = false; // Set connected to false to break the loop
                        }
                        else
                        {
                            clientUsernames[thisClient] = incomingMessage; // Assuming the first message from the client is the username
                            usernameReceived = true;
                            logs.AppendText("Client " + incomingMessage + " connected.\n");
                            UpdateUserLog(); // If the username is unique, accept it and proceed
                            string connectMessage = "Connected to the server!\nWelcome " + incomingMessage + ".";
                            Byte[] connectBuffer = Encoding.Default.GetBytes(connectMessage);
                            thisClient.Send(connectBuffer); // Inform the client
                        }
                    }
                    else if (incomingMessage.StartsWith("subscribe:"))
                    {
                        string channel = incomingMessage.Split(':')[1];
                        if (channel == "if100")
                        {
                            string username = clientUsernames[thisClient];
                            if (!if100Subscribers.Contains(username))
                            {
                                SubscribeClientToChannel(thisClient, channel);                        
                                logs.AppendText(username + " subscribed to: " + channel + "\n");
                                if100Subscribers.Add(username);
                                UpdateSubscriptionLog(); // Update the subscriptions log
                            }
                            string confirmationMessage = "subscribed:" + channel;
                            Byte[] confirmationBuffer = Encoding.Default.GetBytes(confirmationMessage);
                            thisClient.Send(confirmationBuffer); // Send confirmation back to client
                        }
                        else if (channel == "sps101")
                        {
                            string username = clientUsernames[thisClient];
                            if (!sps101Subscribers.Contains(username))
                            {
                               SubscribeClientToChannel(thisClient, channel);
                               logs.AppendText(username + " subscribed to: " + channel + "\n");
                               sps101Subscribers.Add(username);
                               UpdateSubscriptionLog(); // Update the subscriptions log
                            }
                            string confirmationMessage = "subscribed:" + channel;
                            Byte[] confirmationBuffer = Encoding.Default.GetBytes(confirmationMessage);
                            thisClient.Send(confirmationBuffer); // Send confirmation back to client
                        }
                    }
                    else if (incomingMessage.StartsWith("unsubscribe:"))
                    {
                        string channel = incomingMessage.Split(':')[1];
                        if (channel == "if100")
                        {
                            string username = clientUsernames[thisClient];
                            if100Subscribers.Remove(username);
                            UpdateSubscriptionLog(); // Update the subscriptions log
                            logs.AppendText(username + " unsubscribed from: " + channel + "\n");
                        }
                        else if (channel == "sps101")
                        {
                            string username = clientUsernames[thisClient];
                            sps101Subscribers.Remove(username);
                            UpdateSubscriptionLog(); // Update the subscriptions log
                            logs.AppendText(username + " unsubscribed from: " + channel + "\n");
                        }
                    }
                    else if (incomingMessage.StartsWith("if100:"))
                    {
                        string message = incomingMessage.Substring(6); // Extract the actual message
                        string username = clientUsernames[thisClient];
                        string formattedMessage = "User: " + username + ": " + message;
                        string formattedMessage2 = username + " sent " + "\"" + message + "\"" + " to if100.\n";
                        logs.AppendText(formattedMessage2);
                        BroadcastToChannel("if100", formattedMessage);
                    }
                    else if (incomingMessage.StartsWith("sps101:"))
                    {
                        string message = incomingMessage.Substring(7); // Extract the actual message
                        string username = clientUsernames[thisClient];
                        string formattedMessage = "User: " + username + ": " + message;
                        string formattedMessage2 = username + " sent " + "\"" + message + "\"" + " to sps101.\n";
                        logs.AppendText(formattedMessage2);
                        BroadcastToChannel("sps101", formattedMessage);
                    }
                }
                catch
                {
                    string username3 = clientUsernames[thisClient];
                    if (!terminating)
                    {
                        
                        logs.AppendText(username3 + " has disconnected\n");
                    }

                    // Also remove from subscriptions
                    if (clientSubscriptions.ContainsKey(thisClient))
                    {
                        clientSubscriptions.Remove(thisClient);
                    }
                    if100Subscribers.Remove(username3);
                    sps101Subscribers.Remove(username3);
                    UpdateSubscriptionLog();
                    clientUsernames.Remove(thisClient);
                    UpdateUserLog();
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e) // Close the form
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
