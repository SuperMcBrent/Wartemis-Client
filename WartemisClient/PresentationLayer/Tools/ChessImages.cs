using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using System.IO;
using System.Drawing.Imaging;
using Brush = System.Drawing.Brush;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Text.RegularExpressions;
using PresentationLayer.Tools;

namespace PresentationLayer.Tools {
    public static class ChessImages {

        public static BitmapImage DefaultBoard => BoardFromFen("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        public static BitmapImage BoardNoPieces => GetEmptyBoard();

        public static BitmapImage BoardFromFen(string fen) {
            string[] cells = null;

            try {
                cells = DeconstructFen(fen);
            } catch (Exception) {
                return BoardNoPieces;
            }

            Bitmap bm = BoardNoPieces.ToBitmap();
            bm.MakeTransparent();

            //Brush fromBrush = new SolidBrush(ColorTranslator.FromHtml("#00a2ff"));
            //Brush toBrush = new SolidBrush(ColorTranslator.FromHtml("#ff9a00"));
            //Brush checkeredBrush = new HatchBrush(HatchStyle.LargeCheckerBoard, Color.White, Color.Black);

            int size = 75;
            int border = 30;

            using (Graphics gr = Graphics.FromImage(bm)) {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.TextRenderingHint = TextRenderingHint.AntiAlias;

                //draw board
                for (int rank = 0; rank < 8; rank++) {
                    for (int file = 0; file < 8; file++) {
                        int index = (rank * 8) + file;
                        string piece = cells[index];
                        Rectangle rect = new Rectangle(border + file * size, border + rank * size, size, size);
                        //if (index == 13) brush = toBrush;
                        //if (index == 28) brush = fromBrush;
                        if (!string.IsNullOrEmpty(piece)) {
                            try {
                                Image pieceImage = GetPieceImage(piece);
                                gr.DrawImage(pieceImage, rect);
                            } catch (Exception) {
                                Debug.WriteLine($"No image for piece: {piece}");
                            }
                        }
                    }
                }

                DrawLabels(gr, size, border);

            }
            return bm.ToBitmapImage();
        }

        private static BitmapImage GetEmptyBoard() {
            Bitmap bm = new Bitmap(660, 660);
            bm.MakeTransparent();

            Brush lightBrush = Brushes.LightGray;
            Brush darkBrush = Brushes.LightSlateGray;

            int size = 75;
            int border = 30;

            using (Graphics gr = Graphics.FromImage(bm)) {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.TextRenderingHint = TextRenderingHint.AntiAlias;

                //draw board
                for (int rank = 0; rank < 8; rank++) {
                    for (int file = 0; file < 8; file++) {
                        Rectangle rect = new Rectangle(border + file * size, border + rank * size, size, size);
                        Brush brush = darkBrush;
                        if (rank % 2 == file % 2) brush = lightBrush;
                        gr.FillRoundedRectangle(brush, rect, 10);
                    }
                }

                DrawLabels(gr, size, border);
            }
            return bm.ToBitmapImage();
        }

        private static void DrawLabels(Graphics gr, int size, int border) {
            //draw text around board
            int fontSize = 16;
            string font = "Tahoma";
            var ranks = "abcdefgh".ToCharArray();
            var files = "87654321".ToCharArray();
            StringFormat sf = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            for (int i = 0; i < 8; i++) {
                Rectangle rectU = new Rectangle(border + i * size, 0, size, border);
                gr.DrawString(ranks[i].ToString(), new Font(font, fontSize), Brushes.Black, rectU, sf);
                Rectangle rectD = new Rectangle(border + i * size, size * 8 + border, size, border);
                gr.DrawString(ranks[i].ToString(), new Font(font, fontSize), Brushes.Black, rectD, sf);
                Rectangle rectL = new Rectangle(0, border + i * size, border, size);
                gr.DrawString(files[i].ToString(), new Font(font, fontSize), Brushes.Black, rectL, sf);
                Rectangle rectR = new Rectangle(size * 8 + border, border + i * size, border, size);
                gr.DrawString(files[i].ToString(), new Font(font, fontSize), Brushes.Black, rectR, sf);
            }
        }

        private static Image GetPieceImage(string piece) {
            string filename = "";
            switch (piece.ToLower()) {
                case "b":
                    filename = "chess_bishop.png";
                    break;
                case "k":
                    filename = "chess_king.png";
                    break;
                case "n":
                    filename = "chess_knight.png";
                    break;
                case "p":
                    filename = "chess_pawn.png";
                    break;
                case "q":
                    filename = "chess_queen.png";
                    break;
                case "r":
                    filename = "chess_rook.png";
                    break;
            }
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Images/chess/{filename}"));

            Image image = Image.FromFile(path);

            if (char.IsLower(piece[0])) {
                Bitmap bmp = (Bitmap)image;
                bmp = ChangeColor(bmp, ColorTranslator.FromHtml("#555555"));
                image = bmp;
            }

            return image;
        }

        private static Bitmap ChangeColor(Bitmap scrBitmap, Color newColor) {
            Color actualColor;
            Bitmap newBitmap = new Bitmap(scrBitmap.Width, scrBitmap.Height);
            for (int i = 0; i < scrBitmap.Width; i++) {
                for (int j = 0; j < scrBitmap.Height; j++) {
                    actualColor = scrBitmap.GetPixel(i, j);
                    int luminance = getLuminance(actualColor.ToArgb());
                    //127 or less is closer to black (0), 128 or above is closer to white (255)
                    if (luminance > 150)
                        newBitmap.SetPixel(i, j, newColor);
                    else
                        newBitmap.SetPixel(i, j, actualColor);
                }
            }
            return newBitmap;
        }

        private static int getLuminance(int argb) {
            int lum = (77 * ((argb >> 16) & 255)
                       + 150 * ((argb >> 8) & 255)
                       + 29 * ((argb) & 255)) >> 8;
            return lum;
        }

        private static DrawingBrush GetCheckeredBrush() {
            DrawingBrush myBrush = new DrawingBrush();

            GeometryDrawing backgroundSquare =
                new GeometryDrawing(
                    System.Windows.Media.Brushes.White,
                    null,
                    new RectangleGeometry(new System.Windows.Rect(0, 0, 100, 100)));

            GeometryGroup aGeometryGroup = new GeometryGroup();
            aGeometryGroup.Children.Add(new RectangleGeometry(new System.Windows.Rect(0, 0, 50, 50)));
            aGeometryGroup.Children.Add(new RectangleGeometry(new System.Windows.Rect(50, 50, 50, 50)));

            System.Windows.Media.LinearGradientBrush checkerBrush = new System.Windows.Media.LinearGradientBrush();
            checkerBrush.GradientStops.Add(new GradientStop(Colors.Black, 0.0));
            checkerBrush.GradientStops.Add(new GradientStop(Colors.Gray, 1.0));

            GeometryDrawing checkers = new GeometryDrawing(checkerBrush, null, aGeometryGroup);

            DrawingGroup checkersDrawingGroup = new DrawingGroup();
            checkersDrawingGroup.Children.Add(backgroundSquare);
            checkersDrawingGroup.Children.Add(checkers);

            myBrush.Drawing = checkersDrawingGroup;
            myBrush.Viewport = new System.Windows.Rect(0, 0, 0.25, 0.25);
            myBrush.TileMode = TileMode.Tile;

            return myBrush;
        }

        private static string[] DeconstructFen(string fen) {

            string[] cells = new string[64];

            Regex r = new Regex(@"\s*([rnbqkpRNBQKP1-8]+\/){7}([rnbqkpRNBQKP1-8]+)\s[bw-]\s(([a-hkqA-HKQ]{1,4})|(-))\s(([a-h][36])|(-))\s\d+\s\d+\s*");
            bool valid = r.IsMatch(fen);
            if (!valid) {
                throw new Exception("Invalid Fen");
            }

            int counter = 0;
            string boardfen = fen.Split(' ')[0];
            string[] ranks = boardfen.Split('/');
            foreach (var rank in ranks) {
                var parts = rank.ToCharArray();
                foreach (var part in parts) {
                    if (char.IsDigit(part)) {
                        counter += int.Parse(part.ToString());
                    } else {
                        cells[counter] = part.ToString();
                        counter++;
                    }
                }
            }

            //string colorPlaying = fen.Split(' ')[1];

            return cells;
        }

    }
}
