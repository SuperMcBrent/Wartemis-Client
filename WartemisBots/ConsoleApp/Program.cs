﻿using Serilog;
using Serilog.Events;
using System;
using System.Threading;
using Chess.Models;
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

            ChessBot cb = new ChessBot(name: "Chesstily");
            cb.Start();

            cb.Connection.RaiseOnStateReceivedEventManually(
                fakeReceived: "{\"state\":{\"fen\": \"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1\"}}"
            );

            Printers.PrintBoard(cb.Board);

            Printers.PrintNeighboursOfCell(cb.Board.Cells[0]);
            Printers.PrintNeighboursOfCell(cb.Board.Cells[56]);

            //PlanetWarsBot pwb = new PlanetWarsBot("Leviathan");
            //pwb.Start();

            _quitEvent.WaitOne();
            Log.Debug("Shutting down...");
            Console.ReadLine();
        }
    }
}
