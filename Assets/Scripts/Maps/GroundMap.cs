
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    public class GroundMap : IEnumerable
    {
        private List<GroundBlockMap> blocksList = new List<GroundBlockMap>();

        public int count { get { return blocksList.Count; } }

        public GroundMap GetLeftMould()
        {
            GroundMap newGround = new GroundMap();

            for (int i = 0; i < count; i++)
            {
                GroundBlockMap block = blocksList[i];
                int minSizeX = 1;
                GroundBlockMap prevBlock = GetPrevBlock(block);
                GroundBlockMap nextBlock = GetNextBlock(block);
                if (prevBlock != null && nextBlock != null)
                {
                    int prevX = Mathf.Abs(prevBlock.position.x - block.position.x);
                    int nextX = Mathf.Abs(nextBlock.position.x - block.position.x);
                    if (block.position.x <= prevBlock.position.x && block.position.x <= nextBlock.position.x)
                    {
                        minSizeX = 1;
                    }
                    else
                    {
                        if (prevBlock.position.x < nextBlock.position.x)
                        {
                            minSizeX = prevX;
                        }
                        else
                        {
                            minSizeX = nextX;
                        }
                    }
                }
                else if (prevBlock != null)
                {
                    if (block.position.x <= prevBlock.position.x)
                    {
                        minSizeX = 1;
                    }
                    else
                    {
                        minSizeX = Mathf.Abs(prevBlock.position.x - block.position.x);
                    }
                }
                else if (nextBlock != null)
                {
                    if (block.position.x <= nextBlock.position.x)
                    {
                        minSizeX = 1;
                    }
                    else
                    {
                        minSizeX = Mathf.Abs(nextBlock.position.x - block.position.x);
                    }
                }

                GroundBlockMap newBlock = new GroundBlockMap();
                int sizeX = Random.Range(minSizeX, minSizeX + 5);
                newBlock.size = new IntVector2(sizeX, block.size.y);
                newBlock.position = new IntVector2(block.position.x - newBlock.size.x, block.position.y);

                newBlock.AddBorder(BorderDirction.West);
                newBlock.AddBorder(BorderDirction.South);
                newGround.AddBlock(newBlock);
            }
            return newGround;
        }

        public GroundMap GetRightMould()
        {
            GroundMap newGround = new GroundMap();

            foreach (GroundBlockMap block in blocksList)
            {
                int minSizeX = 1;
                GroundBlockMap prevBlock = GetPrevBlock(block);
                GroundBlockMap nextBlock = GetNextBlock(block);

                if (prevBlock != null && nextBlock != null)
                {
                    int prevX = Mathf.Abs(prevBlock.position.x + prevBlock.size.x - block.position.x - block.size.x);
                    int nextX = Mathf.Abs(nextBlock.position.x + nextBlock.size.x - block.position.x - block.size.x);
                    if (block.position.x + block.size.x >= prevBlock.position.x + prevBlock.size.x &&
                            block.position.x + block.size.x >= nextBlock.position.x + nextBlock.size.x)
                    {
                        minSizeX = 1;
                    }
                    else
                    {
                        if (prevBlock.position.x + prevBlock.size.x > nextBlock.position.x + nextBlock.size.x)
                        {
                            minSizeX = prevX;
                        }
                        else
                        {
                            minSizeX = nextX;
                        }
                    }
                }
                else if (prevBlock != null)
                {
                    if (block.position.x + block.size.x >= prevBlock.position.x + prevBlock.size.x)
                    {
                        minSizeX = 1;
                    }
                    else
                    {
                        minSizeX = Mathf.Abs(prevBlock.position.x + prevBlock.size.x - block.position.x - block.size.x);
                    }
                }
                else if (nextBlock != null)
                {
                    if (block.position.x + block.size.x >= nextBlock.position.x + nextBlock.size.x)
                    {
                        minSizeX = 1;
                    }
                    else
                    {
                        minSizeX = Mathf.Abs(nextBlock.position.x + nextBlock.size.x - block.position.x - block.size.x);
                    }
                }

                GroundBlockMap newBlock = new GroundBlockMap();
                int sizeX = Random.Range(minSizeX, minSizeX + 5);
                newBlock.size = new IntVector2(sizeX, block.size.y);
                newBlock.position = new IntVector2(block.position.x + block.size.x, block.position.y);

                newBlock.AddBorder(BorderDirction.East);
                newBlock.AddBorder(BorderDirction.South);
                newGround.AddBlock(newBlock);
            }
            return newGround;
        }

        public static GroundMap GenerateRandom(int blocksCount = 40)
        {
            GroundMap newGround = new GroundMap();
            while (newGround.count < blocksCount)
            {
                GroundBlockMap newBlock = GroundBlockMap.GenerateRandom();
                GroundBlockMap lastBlock = newGround.GetLastBlock();
                if (lastBlock != null)
                {
                    int xR = Random.Range(-newBlock.size.x + 3, lastBlock.size.x - 3);
                    newBlock.position = lastBlock.position + new IntVector2(xR, lastBlock.size.y);
                }
                newBlock.AddBorder(BorderDirction.South);
                newGround.AddBlock(newBlock);
            }
            return newGround;
        }

        public void AddBlock(GroundBlockMap block)
        {
            blocksList.Add(block);
        }

        public GroundBlockMap GetLastBlock()
        {
            GroundBlockMap lastBlock = null;
            if (blocksList.Count > 0)
            {
                lastBlock = blocksList[blocksList.Count - 1];
            }
            return lastBlock;
        }

        public GroundBlockMap GetFirstBlock()
        {
            GroundBlockMap firstBlock = null;
            if (blocksList.Count > 0)
            {
                firstBlock = blocksList[0];
            }
            return firstBlock;
        }

        public GroundBlockMap GetPrevBlock(GroundBlockMap block)
        {
            GroundBlockMap prevBlock = null;
            int i = blocksList.IndexOf(block) - 1;
            if (i >= 0)
            {
                prevBlock = blocksList[i];
            }
            return prevBlock;
        }

        public GroundBlockMap GetNextBlock(GroundBlockMap block)
        {
            GroundBlockMap nextBlock = null;
            int i = blocksList.IndexOf(block) + 1;
            if (i < blocksList.Count)
            {
                nextBlock = blocksList[i];
            }
            return nextBlock;
        }

        public void GrowPines(int max = 3)
        {
            foreach (GroundBlockMap block in blocksList)
            {
                block.GrowPines(max);
            }
        }

        public void AddSpots(int max = 3)
        {
            foreach (GroundBlockMap block in blocksList)
            {
                block.AddSpots(max);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return blocksList.GetEnumerator();
        }

        public static GroundMap BuildEndingMap(GroundBlockMap leftBlock, GroundBlockMap rightBlock, GroundBlockMap middleBlock)
        {
            int depthsDiff = -leftBlock.GetDepth();

            GroundMap endingMap = new GroundMap();

            GroundBlockMap endingBlockLeft = new GroundBlockMap();
            endingBlockLeft.position = leftBlock.position + new IntVector2(0, leftBlock.size.y + depthsDiff);
            endingBlockLeft.size = new IntVector2(leftBlock.size.x, middleBlock.position.y + middleBlock.size.y - leftBlock.position.y - leftBlock.size.y);
            endingBlockLeft.AddBorder(BorderDirction.West);
            endingMap.AddBlock(endingBlockLeft);

            GroundBlockMap endingBlockRight = new GroundBlockMap();
            endingBlockRight.position = rightBlock.position + new IntVector2(0, rightBlock.size.y + depthsDiff);
            endingBlockRight.size = new IntVector2(rightBlock.size.x, middleBlock.position.y + middleBlock.size.y - rightBlock.position.y - rightBlock.size.y);
            endingBlockRight.AddBorder(BorderDirction.East);
            endingMap.AddBlock(endingBlockRight);

            GroundBlockMap endingBlock = new GroundBlockMap();
            endingBlock.position = endingBlockLeft.position + new IntVector2(0, endingBlockLeft.size.y);
            endingBlock.size = new IntVector2(endingBlockLeft.size.x + middleBlock.size.x + endingBlockRight.size.x, 1);
            endingMap.AddBlock(endingBlock);
            return endingMap;
        }

        public static GroundMap BuildBarMap(GroundMap endingMap)
        {
            GroundMap barMap = new GroundMap();
            GroundBlockMap barBlock1 = new GroundBlockMap();
            barBlock1.size = endingMap.GetLastBlock().size + new IntVector2(Random.Range(10, 20), Random.Range(5, 10));
            barBlock1.position = endingMap.GetLastBlock().position + new IntVector2(-Random.Range(5, 10), endingMap.GetLastBlock().size.y);
            barBlock1.BuildBar();
            barBlock1.GrowPines(10);
            barBlock1.AddSpots(6);
            barMap.AddBlock(barBlock1);
            GroundBlockMap barBlock2 = new GroundBlockMap();
            barBlock2.size = barBlock1.size + new IntVector2(Random.Range(10, 20), 20);
            barBlock2.position = barBlock1.position + new IntVector2(-Random.Range(5, 10), barBlock1.size.y);
            barBlock2.GrowPines(15);
            barBlock2.AddSpots(10);
            barMap.AddBlock(barBlock2);

            return barMap;
        }

        public void SetDepth(int d)
        {
            foreach (GroundBlockMap block in blocksList)
            {
                block.SetDepth(d);
            }
        }

    }
}
