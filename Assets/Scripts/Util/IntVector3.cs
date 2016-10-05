public class IntVector3
{
    public int x { get { return _x; } }
    public int y { get { return _y; } }
    public int z { get { return _z; } }

    private int _x;
    private int _y;
    private int _z;

    public IntVector3(int x, int y, int z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    public static IntVector3 operator +(IntVector3 a, IntVector3 b)
    {
        return new IntVector3(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static IntVector3 operator -(IntVector3 a, IntVector3 b)
    {
        return new IntVector3(a.x - b.x, a.y - b.y, a.z - b.z);
    }
}
