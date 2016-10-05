using UnityEngine;

namespace Assets.Scripts.Physics
{
    public interface IPhysics
    {
        void ApplyGroundForcesByAngle(Rigidbody2D body, float angle);
        void SetDifficulty(int difficulty);
    }
}