using System;

namespace Romert.Core.Exceptions;

public class UnknownNumber(int number, int index) : Exception {
    public override string Message => $"An unknown number {number} in index {index}";
}