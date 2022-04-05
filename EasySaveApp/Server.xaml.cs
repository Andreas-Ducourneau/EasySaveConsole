using Core.Model.Business;
using Core.Model.Service;
using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace EasySaveApp
{
    /// <summary>
    /// Logique d'interaction pour Server.xaml
    /// </summary>
    public partial class Server : Window
    {
        SimpleTcpServer server;
        public Server()
        {
            InitializeComponent();
            this.Show();
            btnSend.IsEnabled = false;
            server = new SimpleTcpServer(txtIP.Text);
            server.Events.ClientConnected += Event_ClientConnected;
            server.Events.ClientDisconnected += Event_ClientDisconnected;
            server.Events.DataReceived += Event_DataReceived;
        }

        private void Event_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                txtInfo.Text += $"{e.IpPort}: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
            }));
            string[] table = Encoding.UTF8.GetString(e.Data).Split(' ');
            string name = table[0];
            string namesave = table[1];
            MainWindow.Play_Socket(name, namesave);
        }

        private void Event_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                txtInfo.Text += $"{e.IpPort} disconnected.{Environment.NewLine}";
                lstClient.Items.Remove(e.IpPort);
            }));

        }

        private void Event_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                txtInfo.Text += $"{e.IpPort} connected.{Environment.NewLine}";
                lstClient.Items.Add(e.IpPort); 
            }));
            ImportSave(e);
        }

        private void btnStart_Click_1(object sender, RoutedEventArgs e)
        {
            server.Start();
            txtInfo.Text += $"Starting...{Environment.NewLine}";
            btnStart.IsEnabled = false;
            btnSend.IsEnabled = true;

        }

        private void txtStatus_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void ImportSave(ClientConnectedEventArgs e)
        {
            if (server.IsListening)
            {
               
                List<Save> saveList = new List<Save>();
                saveList = SaveList.ImportSaveList();
                string msg;
                foreach (var save in saveList)
                {
                    msg = save.name;
                    server.Send(e.IpPort,msg);
                    Thread.Sleep(500);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }        
    }
}
