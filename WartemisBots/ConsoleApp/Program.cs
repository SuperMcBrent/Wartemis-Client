using Serilog;
using Serilog.Events;
using System;
using System.Threading;
using Chess;
using Chess.Models;
using PlanetWars.Models;
using Chess.Tools;

namespace ConsoleApp {
    class Program {
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            #region Set up logging infrastructure
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Is(LogEventLevel.Information)
                .WriteTo.Console()
                .CreateLogger();
            #endregion

            #region Set up Ctrl + C exiting
            Console.CancelKeyPress += (sender, eArgs) => {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };
            #endregion

            ChessBot cb = new ChessBot("Chesstily");
            cb.Start();

            string message = "{\"state\":{\"fen\": \"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1\"}}";
            cb.Connection.RaiseOnStateReceivedEventManually(message);

            cb.Board.PrintBoard();

            //PlanetWarsBot pwb = new PlanetWarsBot("Leviathan");
            //pwb.Start();

            _quitEvent.WaitOne();
            Console.WriteLine("Shutting down...");
            Console.ReadLine();
        }
    }
}
