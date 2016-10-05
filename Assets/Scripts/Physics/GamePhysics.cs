using UnityEngine;

namespace Assets.Scripts.Physics
{
    public class GamePhysics:IPhysics
    {
        private const float baseGroundRes = 0.02f;
        private const float groundResStep = 0.005f;
        private float groundResCoef = 0.01f;

        public void ApplyGroundForcesByAngle(Rigidbody2D body, float angle)
        {
            //get player orientation
            int horizOrientation = 1;
            if (angle < 0 && angle >= -0.5f || angle >= 0.5f && angle < 1)
            {
                horizOrientation = -1;
            }

            //to calculate resistance and horizontal move forces depending on the angle
            float cosCoef = 1 - Mathf.Abs(Mathf.Cos(Mathf.PI * angle));
            float sinCoef = 1 - Mathf.Abs(Mathf.Sin(Mathf.PI * angle));

            float horizMoveCoef = horizOrientation * 0.6f * cosCoef;
            Vector2 horizMoveF = body.mass * horizMoveCoef * new Vector2(Mathf.Pow(body.velocity.y, 2), 0);

            //can be different from horizontal player orientation
            int horizSpeedSign = 1;
            if (body.velocity.x < 0)
            {
                horizSpeedSign = -1;
            }

            Vector2 groundResistanceF = body.mass * groundResCoef *
                                        (new Vector2(-horizSpeedSign * Mathf.Pow(body.velocity.x, 2), 2 * Mathf.Pow(body.velocity.y, 2)));

            float angleResCoefX = -horizSpeedSign * Mathf.Pow(sinCoef, 2);
            float angleResCoefY = Mathf.Pow(cosCoef, 2);
            Vector2 angleResistanceF = body.mass *
                                       new Vector2(angleResCoefX * Mathf.Pow(body.velocity.x, 2), angleResCoefY * Mathf.Pow(body.velocity.y, 2));
            body.AddForce(horizMoveF + groundResistanceF + angleResistanceF);
        }

        public void SetDifficulty(int difficulty)
        {
            groundResCoef = baseGroundRes - (difficulty + 1) * groundResStep;
        }
    }
}