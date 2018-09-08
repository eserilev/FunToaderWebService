using FuntoaderWebService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace FuntoaderWebService.Services
{
    public class CommandSender
    {
        enum State
        {
            Live,
            Offline,
            MidiSetup
        };

        public enum CommandMethod
        {
            WIFI,
            BOTH,
            CELL
        };

        private string IPADDR = "224.0.0.251";
        private Int32 PORT = 5353;
        private Socket mSender = null;
        private State mState;
        private Stopwatch mStopWatch;
        private CommandMethod mCommandMethod;
        private CommandModel command;
        private decimal mCommandRetries;
        

        public CommandSender()
        {
            mCommandRetries = 5;
            mState = State.Live;

            mStopWatch = new Stopwatch();
            mStopWatch.Reset();
            mStopWatch.Start();
            
            IPAddress IPaddr = IPAddress.Parse(IPADDR);
            if (mSender != null)
            {
                mSender.Dispose();
            }

            mSender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(IPADDR), int.Parse(PORT.ToString()));

            // For WinXP, we need to bind (to any address) before we can call SetSocketOption(...AddMembership).
            IPEndPoint bindAddr = new IPEndPoint(IPAddress.Any, 1234);
            mSender.Bind(bindAddr);

            mSender.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPaddr));
            mSender.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, int.Parse("1"));

            Console.WriteLine("Connecting...");

            mSender.Connect(ipep);
        }



        private void Delay(int millisec)
        {
            System.Threading.Thread.Sleep(millisec);
        }

        public MessageModel SendMessage(MessageModel message)
        {
            int i;
            int sentBytes = 0;
            long timeMsec = 0;
            message.time = DateTime.Now.ToUniversalTime();
      
            switch (mState)
            {
                case State.Offline:
                    break;

                case State.MidiSetup:
                    break;

                case State.Live:
                default:
                    try
                    {
                        for (i = 0; i < mCommandRetries; i++)
                        {
                            switch (mCommandMethod)
                            {
                                case (CommandMethod.WIFI):
                                    timeMsec = mStopWatch.ElapsedMilliseconds;
                                    sentBytes = mSender.Send(message.messageBuffer, message.messageContent.Length, SocketFlags.None);
                                    Console.WriteLine("Sending msg = " + message.messageContent + ", bytes sent = " + sentBytes + ", time = " + timeMsec);
                                    Delay(5);
                                    break;
                                case (CommandMethod.BOTH):
                                    timeMsec = mStopWatch.ElapsedMilliseconds;
                                    sentBytes = mSender.Send(message.messageBuffer, message.messageContent.Length, SocketFlags.None);
                                    Console.WriteLine("Sending msg = " + message.messageContent + ", bytes sent = " + sentBytes + ", time = " + timeMsec);
                                    Delay(5);
                                   // mPushNotificationController.SendPushNotification(cmdMsg, PushNotificationController.mPushType.command);
                                    break;
                                case (CommandMethod.CELL):
                                   // mPushNotificationController.SendPushNotification(cmdMsg, PushNotificationController.mPushType.command);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                       
                    }
                    break;
            }
            return message;
        }

    }
}