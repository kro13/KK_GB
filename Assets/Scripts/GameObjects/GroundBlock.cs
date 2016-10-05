using System.Collections.Generic;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Maps;
using UnityEngine;
using Assets.Scripts.Util;

namespace Assets.Scripts.GameObjects
{
    public class GroundBlock : MonoBehaviour, ICollideable
    {

        public static float borderWidth = 0.25f;
        public static float southBorderWidth = 2f;

        private Transform snowSprite;
        private List<GameObject> gameObjects;

        //unity
        private void OnTriggerEnter2D(Collider2D collider)
        {
            HandlePlayerCollision(collider);
        }
        //unity

        public void HandlePlayerCollision(Collider2D collider)
        {
            if (collider.gameObject.name == Player.PLAYER)
            {
                Player p = collider.gameObject.GetComponent<Player>();
                p.DecreaseScore();
                Debug.Log("Player hit GroundBlock");
            }
        }

        public void BuildFromMap(GroundBlockMap map)
        {
            snowSprite = transform.FindChild("Snow");
            snowSprite.localScale = new Vector3(map.size.x, map.size.y, 0);
            transform.localPosition = new Vector3(map.position.x + ((float)map.size.x) / 2, -map.position.y - ((float)map.size.y) / 2, 0);
            //borders
            bool[] borders = map.GetBorders();
            for (int d = 0; d < BorderDirections.count; d++)
            {
                if (borders[d])
                {
                    BlockBorder border = GameObjectsFactory.getInstance().Create<BlockBorder>();
                    border.name = "Border";
                    border.transform.parent = transform;
                    switch ((BorderDirction)d)
                    {
                        case BorderDirction.North:
                            border.transform.localPosition = new Vector3(0, ((float)map.size.y) / 2, 0);
                            border.transform.localScale = new Vector3(map.size.x, borderWidth, 0);
                            break;
                        case BorderDirction.South:
                            border.transform.localPosition = new Vector3(0, -((float)map.size.y) / 2 - southBorderWidth / 2, 0);
                            border.transform.localScale = new Vector3(map.size.x, southBorderWidth, 0);
                            border.GetComponent<SpriteRenderer>().sortingOrder = -1;
                            break;
                        case BorderDirction.East:
                            border.transform.localPosition = new Vector3(-((float)map.size.x) / 2 + borderWidth / 2, 0, 0);
                            border.transform.localScale = new Vector3(borderWidth, map.size.y, 0);
                            border.GetComponent<SpriteRenderer>().sortingOrder = 1;
                            break;
                        case BorderDirction.West:
                            border.transform.localPosition = new Vector3(((float)map.size.x) / 2 - borderWidth / 2, 0, 0);
                            border.transform.localScale = new Vector3(borderWidth, map.size.y, 0);
                            border.GetComponent<SpriteRenderer>().sortingOrder = 1;
                            break;
                    }
                }
            }
            //spots
            System.Collections.Generic.List<IntVector2> spots = map.GetSpots();
            foreach (IntVector2 s in spots)
            {
                Spot spot = GameObjectsFactory.getInstance().Create<Spot>();
                spot.name = "Spot";
                spot.transform.parent = transform;
                spot.transform.localPosition = new Vector3(-((float)map.size.x) / 2 + s.x + 0.5f, ((float)map.size.y) / 2 - s.y - 0.5f, 0);
                spot.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
            //gameObjects
            gameObjects = new System.Collections.Generic.List<GameObject>();
            System.Collections.Generic.List<IntVector2> mapPines = map.GetPines();
            int flip = Random.Range(0, 2);
            foreach (IntVector2 p in mapPines)
            {
                Pine pine = GameObjectsFactory.getInstance().Create<Pine>();
                pine.name = "Pine";
                pine.transform.parent = transform;
                pine.transform.localPosition = new Vector3(-((float)map.size.x) / 2 + p.x + 0.5f, ((float)map.size.y) / 2 - p.y - 0.5f, 0);
                if (flip > 0)
                {
                    pine.GetComponent<SpriteRenderer>().flipX = true;
                }
                gameObjects.Add(pine.gameObject);
            }
            //bar
            IntVector2 b = map.GetBar();
            if (b != null)
            {
                Bar bar = GameObjectsFactory.getInstance().Create<Bar>();
                bar.name = "Bar";
                bar.transform.parent = transform;
                bar.transform.localPosition = new Vector3(-((float)map.size.x) / 2 + b.x + 0.5f, ((float)map.size.y) / 2 - b.y - 0.5f, 0);
                gameObjects.Add(bar.gameObject);
            }
            //depth
            SetDepth(map.GetDepth());
        }

        public List<GameObject> GetGameObjects()
        {
            return gameObjects;
        }

        private void SetDepth(int depth)
        {
            transform.localPosition += new Vector3(0, 0, depth * 0.1f);
            Color snowColor;
            Color spotColor;
            if(depth == 0)
            {
                snowColor = ColorsUtil.white;
                spotColor = ColorsUtil.light;
            }
            else if(depth == 1)
            {
                snowColor = ColorsUtil.light;
                spotColor = ColorsUtil.white;
            }
            else if (depth == 2)
            {
                snowColor = ColorsUtil.dark;
                spotColor = ColorsUtil.light;
            }
            else
            {
                snowColor = ColorsUtil.black;
                spotColor = ColorsUtil.dark;
            }
            foreach (Transform child in transform)
            {
                SpriteRenderer sr = null;
                Color color = new Color(1, 1, 1);
                sr = child.GetComponent<SpriteRenderer>();
                switch (child.name)
                {
                    case "Snow":
                        sr.color = snowColor;
                        break;
                    case "Spot":
                        sr.color = spotColor;
                        break;
                }
            }
        }
    }

}

