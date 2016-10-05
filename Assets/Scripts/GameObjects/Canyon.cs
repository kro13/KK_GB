using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Maps;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class Canyon : MonoBehaviour
    {
        private Vector3 enterPoint;
        private Vector3 exitPoint;

        public void Generate(int difficulty)
        {
            int maxGroundPines = difficulty + 1;

            GroundMap groundMap = GroundMap.GenerateRandom(30 + 20 * difficulty);
            groundMap.AddSpots(5);
            groundMap.GrowPines(maxGroundPines);

            GroundMap leftWMap = groundMap.GetLeftMould();
            leftWMap.AddSpots(4);
            leftWMap.GrowPines(4);

            GroundMap rightWMap = groundMap.GetRightMould();
            rightWMap.AddSpots(4);
            rightWMap.GrowPines(4);

            GroundMap leftW1Map = leftWMap.GetLeftMould();
            leftW1Map.AddSpots(10);
            leftW1Map.GrowPines(10);

            GroundMap rightW1Map = rightWMap.GetRightMould();
            rightW1Map.AddSpots(10);
            rightW1Map.GrowPines(10);

            groundMap.SetDepth(2);
            leftWMap.SetDepth(1);
            rightWMap.SetDepth(1);
            leftW1Map.SetDepth(0);
            rightW1Map.SetDepth(0);

            //create nice ending ground
            GroundMap endingMap = GroundMap.BuildEndingMap(leftWMap.GetLastBlock(), rightWMap.GetLastBlock(), groundMap.GetLastBlock());
            endingMap.AddSpots(2);
            endingMap.GrowPines(4);
            endingMap.SetDepth(1);

            GroundMap endingMap1 = GroundMap.BuildEndingMap(leftW1Map.GetLastBlock(), rightW1Map.GetLastBlock(), endingMap.GetLastBlock());
            endingMap1.AddSpots(2);
            endingMap1.GrowPines(4);
            endingMap1.SetDepth(0);

            GroundMap barMap = GroundMap.BuildBarMap(endingMap1);
            barMap.SetDepth(0);
            barMap.AddSpots(20);
            barMap.GrowPines(20);

            //build from maps
            List<GameObject> objects = new List<GameObject>();

            Ground ground = BuildFromMap(groundMap);
            enterPoint = ground.GetEnterPoint();
            exitPoint = ground.GetExitPoint();
            objects.AddRange(ground.GetGameObjects());

            Ground leftW = BuildFromMap(leftWMap);
            leftW.AddColliders();
            objects.AddRange(leftW.GetGameObjects());

            Ground rightW = BuildFromMap(rightWMap);
            rightW.AddColliders();
            objects.AddRange(rightW.GetGameObjects());

            Ground leftW1 = BuildFromMap(leftW1Map);
            leftW1.AddColliders();
            objects.AddRange(leftW1.GetGameObjects());

            Ground rightW1 = BuildFromMap(rightW1Map);
            rightW1.AddColliders();
            objects.AddRange(rightW1.GetGameObjects());

            Ground ending = BuildFromMap(endingMap);
            ending.AddColliders();
            objects.AddRange(ending.GetGameObjects());

            Ground ending1 = BuildFromMap(endingMap1);
            ending1.AddColliders();
            objects.AddRange(ending1.GetGameObjects());

            Ground bar = BuildFromMap(barMap);
            objects.AddRange(bar.GetGameObjects());

            //sort objects
            objects = objects.OrderByDescending(p => p.transform.position.y).ToList();
            int objOrder = 0;
            foreach (GameObject obj in objects)
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = objOrder;
                objOrder++;
            }
        }

        public Vector3 GetEnterPoint()
        {
            return enterPoint;
        }

        public Vector3 GetExitPoint()
        {
            return exitPoint;
        }

        private Ground BuildFromMap(GroundMap groundMap)
        {
            Ground ground = GameObjectsFactory.getInstance().Create<Ground>();
            ground.BuildFromMap(groundMap);
            ground.transform.parent = transform;
            return ground;
        }
    }

}
