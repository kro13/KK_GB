using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class GameObjectsFactory:MonoBehaviour
    {
        public Player playerPrefab;
        public Canyon canyonPrefab;
        public FinishMarker finishMarkerPrefab;
        public Ground groundPrefab;
        public GroundBlock groundBlockPrefab;
        public BlockBorder blockBorderPrefab;
        public Pine pinePrefab;
        public Bar barPrefab;
        public Points pointsPrefab;
        public Spot spotPrefab;
        public Background backgroundPrefab;

        private static GameObjectsFactory instance;
        public static GameObjectsFactory getInstance()
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameObjectsFactory>();
                if (instance == null)
                {
                    instance = new GameObject("GameObjectFactory").AddComponent<GameObjectsFactory>();
                }
            }
            return instance;
        }

        public T Create<T>()
        {
            T result;
            switch (typeof(T).Name)
            {
                case "Ground":
                    result = Instantiate(groundPrefab).GetComponent<T>();
                    break;
                case "Bar":
                    result = Instantiate(barPrefab).GetComponent<T>();
                    break;
                case "BlockBorder":
                    result = Instantiate(blockBorderPrefab).GetComponent<T>();
                    break;
                case "Canyon":
                    result = Instantiate(canyonPrefab).GetComponent<T>();
                    break;
                case "FinishMarker":
                    result = Instantiate(finishMarkerPrefab).GetComponent<T>();
                    break;
                case "GroundBlock":
                    result = Instantiate(groundBlockPrefab).GetComponent<T>();
                    break;
                case "Pine":
                    result = Instantiate(pinePrefab).GetComponent<T>();
                    break;
                case "Player":
                    result = Instantiate(playerPrefab).GetComponent<T>();
                    break;
                case "Points":
                    result = Instantiate(pointsPrefab).GetComponent<T>();
                    break;
                case "Spot":
                    result = Instantiate(spotPrefab).GetComponent<T>();
                    break;
                default:
                    throw new Exception("GameObjectsFactory: unknown game object type.");
            }
            return result;
        }
    }
}