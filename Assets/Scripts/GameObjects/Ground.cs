using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Maps;

namespace Assets.Scripts.GameObjects
{
   public class Ground : MonoBehaviour, IEnumerable
    {
        private List<GroundBlock> blocksList = new List<GroundBlock>();

        public int count { get { return blocksList.Count; } }

        public void BuildFromMap(GroundMap map)
        {
            foreach (GroundBlockMap blockMap in map)
            {
                GroundBlock newBlock = GameObjectsFactory.getInstance().Create<GroundBlock>();
                newBlock.BuildFromMap(blockMap);
                AddBlock(newBlock);
            }
        }

        public void AddColliders()
        {
            foreach (GroundBlock block in blocksList)
            {
                Transform snowSprite = block.transform.FindChild("Snow");
                BoxCollider2D collider = block.gameObject.AddComponent<BoxCollider2D>();
                collider.size = new Vector2(snowSprite.localScale.x, snowSprite.localScale.y);
                BoxCollider2D trigger = block.gameObject.AddComponent<BoxCollider2D>();
                trigger.isTrigger = true;
                trigger.size = new Vector2(snowSprite.localScale.x, snowSprite.localScale.y);
            }
        }

        public Vector3 GetEnterPoint()
        {
            Vector3 point = new Vector3();
            if (blocksList.Count > 0)
            {
                Transform snowSprite = blocksList[0].transform.FindChild("Snow");
                point = blocksList[0].transform.position + new Vector3(0, snowSprite.localScale.y / 2, 0);
            }
            return point;
        }

        public Vector3 GetExitPoint()
        {
            Vector3 point = new Vector3();
            if (blocksList.Count > 0)
            {
                Transform snowSprite = blocksList[blocksList.Count - 1].transform.FindChild("Snow");
                point = blocksList[blocksList.Count - 1].transform.position + new Vector3(0, -snowSprite.localScale.y / 2, 0);
            }
            return point;
        }

        public List<GameObject> GetGameObjects()
        {
            List<GameObject> list = new List<GameObject>();
            foreach (GroundBlock block in blocksList)
            {
                list.AddRange(block.GetGameObjects());
            }
            return list;
        }

        public void AddBlock(GroundBlock block)
        {
            blocksList.Add(block);
            block.transform.parent = transform;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return blocksList.GetEnumerator();
        }
    }
}
