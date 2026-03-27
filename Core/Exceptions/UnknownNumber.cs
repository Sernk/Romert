using System;

namespace Romert.Core.Exceptions;

public class UnknownNumber(int number, int index, string name) : Exception {
    public override string Message => $"An unknown number [c/ff0000:{number}] in index [c/ff0000:{index}] for item [c/ffff00:{name}]";
}