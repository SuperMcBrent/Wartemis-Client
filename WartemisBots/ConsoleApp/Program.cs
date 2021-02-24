using Serilog;
using Serilog.Events;
using System;
using System.Threading;
using Chess.Models;

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

            cb.Board.PrintBoard();

            Cell cell = cb.Board.Cells[8];
            Log.Information("Neighbours of {cell}, {piece}", cell, cell.Piece);
            Log.Information("North: {cell}, {piece}", cell.CellToNorth(), cell.CellToNorth()?.Piece);
            Log.Information("East: {cell}, {piece}", cell.CellToEast(), cell.CellToEast()?.Piece);
            Log.Information("South: {cell}, {piece}", cell.CellToSouth(), cell.CellToSouth()?.Piece);
            Log.Information("West: {cell}, {piece}", cell.CellToWest(), cell.CellToWest()?.Piece);

            Console.WriteLine();
            cell = cb.Board.Cells[4];
            Log.Information("Neighbours of {cell}, {piece}", cell, cell.Piece);
            Log.Information("North: {cell}, {piece}", cell.CellToNorth(), cell.CellToNorth()?.Piece);
            Log.Information("East: {cell}, {piece}", cell.CellToEast(), cell.CellToEast()?.Piece);
            Log.Information("South: {cell}, {piece}", cell.CellToSouth(), cell.CellToSouth()?.Piece);
            Log.Information("West: {cell}, {piece}", cell.CellToWest(), cell.CellToWest()?.Piece);

            //PlanetWarsBot pwb = new PlanetWarsBot("Leviathan");
            //pwb.Start();

            _quitEvent.WaitOne();
            Log.Debug("Shutting down...");
            Console.ReadLine();
        }
    }
}
