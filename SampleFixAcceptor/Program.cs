using QuickFix;
using System;
using System.Threading;

namespace SampleFixAcceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            SessionSettings settings = new SessionSettings(file: "quickfix.cfg");
            IApplication myApp = new MyQuickFixApp();
            IMessageStoreFactory storeFactory = new FileStoreFactory(settings);
            ILogFactory logFactory = new FileLogFactory(settings);

            var acceptor = new ThreadedSocketAcceptor(
                application: myApp,
                storeFactory: storeFactory,
                settings: settings,
                logFactory: logFactory);

            acceptor.Start();

            Console.WriteLine("Pressione Enter para finalizar");
            Console.ReadLine();

            acceptor.Stop();
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
            //Thread.Sleep(1000);
            
            //Session.SendToTarget(message, sessionID);
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
