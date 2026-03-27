using System;

namespace Romert.Core.Exceptions;

public class NoTexture(string name) : Exception {
    public override string Message => $"Element [c/ffff00:{name}],[c/ff0000: no texture]. If this Alchemist reagent: override HasTexture => false";
}

