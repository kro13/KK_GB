using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class Pine:MonoBehaviour, ICollideable
    {
        //unity
        private void OnTriggerEnter2D(Collider2D collider)
        {
            HandlePlayerCollision(collider);
        }
        //unity

        public void HandlePlayerCollision(Collider2D collider)
        {
            if (collider.gameObject.name==Player.PLAYER)
            {
                Player p = collider.gameObject.GetComponent<Player>();
                if (p.GetVelocity().magnitude > 5)
                {
                    gameObject.GetComponent<ParticleSystem>().Emit((int) p.GetVelocity().magnitude);
                }
                p.DecreaseScore();
                Debug.Log("Player hit Pine");
            }
        }
    }
}