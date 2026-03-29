using System;
using System.Linq;

namespace Romert.Helpers;

public class TextHelper {
    public static void Joined(object[] o) => Main.NewText(string.Join(", ", o.AsEnumerable()));
}
