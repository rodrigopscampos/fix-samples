using QuickFix;
using QuickFix.Transport;
using System;

namespace SampleFixInitiator
{
    class Program
    {
        static void Main(string[] args)
        {
            SessionSettings settings = new SessionSettings(file: "quickfix.cfg");
            IApplication myApp = new MyQuickFixApp();
            IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
            ILogFactory logFactory = new FileLogFactory(settings);

            var initiator = new SocketInitiator(
                application: myApp,
                storeFactory: storeFactory,
                settings: settings,
                logFactory: logFactory);

            initiator.Start();

            //Console.WriteLine("Pressione Enter para finalizar");
            //Console.ReadLine();
            //initiator.Stop();

            while (true)
            {
                Console.WriteLine("Pressione Enter para enviar uma mensagem FIX");
                Console.ReadLine();
            
                var msgTxt = "8=FIX.4.49=10335=D34=349=BANZAI52=20121105-23:24:4256=EXEC11=135215788257721=138=1000040=154=155=MSFT59=010=062";
                var message = new Message(msgTxt, validate: false);
                
                Session.SendToTarget(
                    message: message,
                    sessionID: new SessionID("FIX.4.4", "I", "A"));
            }
        }
    }

    public class MyQuickFixApp : IApplication
    {
        public void FromAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine($"[FromAdmin] {sessionID}: {message}");
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine($"[FromApp] {sessionID}: {message}");
        }

        public void OnCreate(SessionID sessionID)
        {
            Console.WriteLine($"[OnCreate] {sessionID}");
        }

        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine($"[OnLogon] {sessionID}");
        }

        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine($"[OnLogout] {sessionID}");
        }

        public void ToAdmin(Message message, SessionID sessionID)
        {
            Console.WriteLine($"[ToAdmin] {sessionID}: {message}");
        }

        public void ToApp(Message message, SessionID sessionId)
        {
            Console.WriteLine($"[ToApp] {sessionId}: {message}");
        }
    }
}
