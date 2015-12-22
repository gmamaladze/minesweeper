namespace try2
{
    internal class DrawParams
    {
        public DrawParams(Size size, Point offset, Scale scale)
        {
            Size = size;
            Offset = offset;
            Scale = scale;
        }

        public Size Size { get; }

        public Point Offset { get; }

        public Scale Scale { get; }
    }
}