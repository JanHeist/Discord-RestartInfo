using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;

namespace RestartWebhook
{
    public class dWebHook : IDisposable
    {
        private readonly WebClient dWebClient;
        private static NameValueCollection discordValues = new NameValueCollection();
        public dWebHook()
        {
            dWebClient = new WebClient();
        }
        public void SendMessage(string msgSend)
        {
            discordValues = new NameValueCollection();
            discordValues.Add("username", "RestartInfo");
            discordValues.Add("avatar_url", "https://i.ibb.co/8K7HYD1/server.png");
            discordValues.Add("content", msgSend);
            dWebClient.UploadValues("WEBHOOK-URL-HERE", discordValues); // @TODO: Edit Webhook-URL
        }
        public void Dispose()
        {
            dWebClient.Dispose();
        }
    }

    class Program
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        static void Main(string[] args)
        {
            using (dWebHook dcWeb = new dWebHook())
            {
                dcWeb.SendMessage("```" + Environment.MachineName + " has just been booted.\nIPv4: " + GetLocalIPAddress() + "```");
            }
        }
    }
}
