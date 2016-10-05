
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public class GroundBlockMap
    {
        private static IntVector2 minBlockSize = new IntVector2(3, 3);
        private static IntVector2 maxBlockSize = new IntVector2(20, 20);

        private IntVector2 _size;
        private IntVector2 _position;
        private bool[] borders = new bool[BorderDirections.count];
        private List<IntVector2> pines = new List<IntVector2>();
        private List<IntVector2> spots = new List<IntVector2>();
        private IntVector2 bar;
        private int depth;

        public IntVector2 size
        {
            get { return _size; }
            set
            {
                _size = value;
            }
        }

        public IntVector2 position
        {
            get { return _position; }
            set
            {
                _position = value;
            }
        }

        public bool[] GetBorders()
        {
            return borders;
        }

        public IntVector2 GetBar()
        {
            return bar;
        }

        public List<IntVector2> GetPines()
        {
            return pines;
        }

        public List<IntVector2> GetSpots()
        {
            return spots;
        }

        public void AddBorder(BorderDirction dir)
        {
            borders[(int)dir] = true;
        }


        public void BuildBar()
        {
            //assume that the bar is large enough
            bar = new IntVector2(Random.Range(size.x / 2 - 5, size.x / 2 + 5), Random.Range(1, 5));
        }

        public void GrowPines(int max = 3)
        {
            //TODO: constrain pines growth in ground bottlenecks
            int pinesCount = Random.Range(0, Mathf.Min(max, size.x));
            if (size.x > 1 && size.y > 1)
            {
                for (int i = 0; i < pinesCount; i++)
                {
                    IntVector2 pine = new IntVector2(Random.Range(1, size.x - 1), Random.Range(1, size.y - 1));
                    pines.Add(pine);
                }
            }
            pines = pines.OrderBy(t => t.y).ToList();
        }

        public void AddSpots(int max = 3)
        {
            int spotsCount = Random.Range(0, max);
            if (size.x > 1 && size.y > 1)
            {
                for (int i = 0; i < spotsCount; i++)
                {
                    IntVector2 spot = new IntVector2(Random.Range(1, size.x - 1), Random.Range(1, size.y - 1));
                    spots.Add(spot);
                }
            }

        }

        public static GroundBlockMap GenerateRandom()
        {
            GroundBlockMap newBlock = new GroundBlockMap();
            IntVector2 randomSize = new IntVector2(Random.Range(minBlockSize.x, maxBlockSize.x), Random.Range(minBlockSize.y, maxBlockSize.y));
            newBlock.size = randomSize;
            newBlock.position = new IntVector2(0, 0);
            return newBlock;
        }

        public void SetDepth(int d)
        {
            depth = d;
            position += new IntVector2(0, depth);
        }

        public int GetDepth()
        {
            return depth;
        }
    }
}
