using System;
using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class FinishMarker:MonoBehaviour, ICollideable
    {
        public EventHandler onHitFinish;
        
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
                EventArgs eArgs = EventArgs.Empty;
                if (onHitFinish != null)
                {
                    onHitFinish(this, eArgs);
                }
                
                Debug.Log("Player hit Finish");
            }
        }
    }
}