namespace try2
{
    internal class Cell
    {
        public Cell(Icon icon, Point position)
        {
            _icon = icon;
            _position = position;
        }

        private readonly Icon _icon;
        private readonly Point _position;

        public void Draw(DrawParams drawParams)
        {
            _icon.Draw(drawParams, _position);            
        }
    }
}