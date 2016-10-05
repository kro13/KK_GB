

    public class IntVector2
    {
        public int x { get { return _x; } }
        public int y { get { return _y; } }

        private int _x;
        private int _y;

        public IntVector2 (int x, int y)
        {
            _x = x;
            _y = y;
        }

        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.x + b.x, a.y + b.y);
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            return new IntVector2(a.x - b.x, a.y - b.y);
        }
    }

