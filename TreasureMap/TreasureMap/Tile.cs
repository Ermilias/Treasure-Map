namespace TreasureMap
{
    public class Tile
    {
        private bool isTraversable;
        private int treasure;
        private string draw;

        public bool IsTraversable
        {
            get { return isTraversable; }
            set { isTraversable = value; }
        }
        public int Treasure
        {
            get { return treasure; }
            set { treasure = value; }
        }
        public string Draw
        {
            get { return draw; }
            set { draw = value; }
        }

        public Tile()
        {
            draw = ".";
            isTraversable = true;
            treasure = 0;
        }
    }
}