using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public interface ICollideable
    {
        void HandlePlayerCollision(Collider2D collider);
    }
}