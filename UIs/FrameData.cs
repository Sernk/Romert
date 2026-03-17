namespace Romert.UIs;

public struct FrameData(byte horizontal = 1, byte vertical = 1, byte x = 0, byte y = 0, byte sizeOffsetX = 0, byte sizeOffsetY = 0) {
    public byte Horizontal = horizontal;
    public byte Vertical = vertical;
    public byte X = x;
    public byte Y = y;
    public byte SizeOffsetX = sizeOffsetX;
    public byte SizeOffsetY = sizeOffsetY;
}