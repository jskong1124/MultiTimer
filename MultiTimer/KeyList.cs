using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiTimer
{
    public static class KeyList
    {
        public static IEnumerable<KeyValuePair<Key, string>> Get104Keys()
        {
            // 알파벳 A-Z
            var alphabetKeys = Enumerable.Range((int)Key.A, 26)
                                          .Select(k => new KeyValuePair<Key, string>((Key)k, ((char)('A' + (k - (int)Key.A))).ToString()));

            // 숫자 0-9
            var numberKeys = Enumerable.Range((int)Key.D0, 10)
                                       .Select(k => new KeyValuePair<Key, string>((Key)k, ((char)('0' + (k - (int)Key.D0))).ToString()));

            // 기능 키 F1-F12
            var functionKeys = Enumerable.Range((int)Key.F1, 12)
                                         .Select(k => new KeyValuePair<Key, string>((Key)k, $"F{k - (int)Key.F1 + 1}"));

            // 특수 키
            var specialKeys = new List<KeyValuePair<Key, string>>
        {
            new KeyValuePair<Key, string>(Key.Enter, "Enter"),
            new KeyValuePair<Key, string>(Key.Escape, "Escape"),
            new KeyValuePair<Key, string>(Key.Tab, "Tab"),
            new KeyValuePair<Key, string>(Key.Back, "Backspace"),
            new KeyValuePair<Key, string>(Key.Space, "Space"),
            new KeyValuePair<Key, string>(Key.Insert, "Insert"),
            new KeyValuePair<Key, string>(Key.Delete, "Delete"),
            new KeyValuePair<Key, string>(Key.Home, "Home"),
            new KeyValuePair<Key, string>(Key.End, "End"),
            new KeyValuePair<Key, string>(Key.PageUp, "Page Up"),
            new KeyValuePair<Key, string>(Key.PageDown, "Page Down"),
            new KeyValuePair<Key, string>(Key.Left, "Left Arrow"),
            new KeyValuePair<Key, string>(Key.Right, "Right Arrow"),
            new KeyValuePair<Key, string>(Key.Up, "Up Arrow"),
            new KeyValuePair<Key, string>(Key.Down, "Down Arrow")
        };

            // 특수문자 키
            var symbolKeys = new List<KeyValuePair<Key, string>>
        {
            new KeyValuePair<Key, string>(Key.OemComma, ","),
            new KeyValuePair<Key, string>(Key.OemPeriod, "."),
            new KeyValuePair<Key, string>(Key.OemQuestion, "?"),
            new KeyValuePair<Key, string>(Key.OemSemicolon, ";"),
            new KeyValuePair<Key, string>(Key.OemQuotes, "'"),
            new KeyValuePair<Key, string>(Key.OemOpenBrackets, "["),
            new KeyValuePair<Key, string>(Key.OemCloseBrackets, "]"),
            new KeyValuePair<Key, string>(Key.OemPipe, "|"),
            new KeyValuePair<Key, string>(Key.OemMinus, "-"),
            new KeyValuePair<Key, string>(Key.OemPlus, "+"),
            new KeyValuePair<Key, string>(Key.OemTilde, "~")
        };

            // 키패드 키
            var numpadKeys = Enumerable.Range((int)Key.NumPad0, 10)
                                       .Select(k => new KeyValuePair<Key, string>((Key)k, ((char)('0' + (k - (int)Key.NumPad0))).ToString()))
                                       .Concat(new[]
                                       {
                                       new KeyValuePair<Key, string>(Key.Add, "Add"),
                                       new KeyValuePair<Key, string>(Key.Subtract, "Subtract"),
                                       new KeyValuePair<Key, string>(Key.Multiply, "Multiply"),
                                       new KeyValuePair<Key, string>(Key.Divide, "Divide"),
                                       new KeyValuePair<Key, string>(Key.Decimal, "Decimal")
                                       });

            // 전체 키 합치기
            return alphabetKeys
                .Concat(numberKeys)
                .Concat(functionKeys)
                .Concat(specialKeys)
                .Concat(symbolKeys)
                .Concat(numpadKeys);
        }
    }
}
