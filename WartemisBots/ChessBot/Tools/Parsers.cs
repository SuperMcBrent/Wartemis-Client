using System;
using System.Collections.Generic;
using System.Text;
using Wartemis.Tools;

namespace Chess.Tools {
    public static class Parsers {

        public static char[] Files = "abcdefgh".ToCharArray();
        public static char[] Ranks = "87654321".ToCharArray();

        public static string IndexToText(int index) {
            int col = index % 8;
            int row = (index - col) / 8;
            return $"{Files[col]}{Ranks[row]}";
        }

        public static int TextToIndex(string text) {
            if (!char.IsLetter(text.ToCharArray()[0])) {
                text = text.Reverse();
            }
            int col = Array.IndexOf(Files, text.ToLower().ToCharArray()[0]);
            int row = Array.IndexOf(Ranks, text.ToCharArray()[1]);
            return (row * 8) + col;
        }

    }
}
