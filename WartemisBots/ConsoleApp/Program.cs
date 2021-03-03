using Serilog;
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


            //Printers.PrintNeighboursOfCell(cb.Board.Cells[0]);

            //Printers.PrintPieceEvaluation(cb.Board.Cells[Parsers.TextToIndex("d5")].Piece);

            Printers.PrintBoard(cb.Board, true);
            Printers.PrintBoardEvaluation(cb.Board);



            Move move = cb.Board.GetPotentialMove();
            Log.Information("The move: {move}", move);
            Printers.PrintBoard(move.BoardAfterMove, true);
            Printers.PrintBoardEvaluation(move.BoardAfterMove);
            Printers.PrintPieceEvaluation(move.BoardAfterMove.Cells[Parsers.TextToIndex(move.To.ToString())].Piece);

            //PlanetWarsBot pwb = new PlanetWarsBot("Leviathan");
            //pwb.Start();

            _quitEvent.WaitOne();
            Log.Debug("Shutting down...");
            Console.ReadLine();
        }
    }
}
