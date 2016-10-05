
using Assets.Scripts.Controls;
using Assets.Scripts.GameObjects;
using Assets.Scripts.Physics;
using Assets.Scripts.Score;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static string PLAYER = "Player";

    private Rigidbody2D body;
    private float angle;
    private bool isActive = true;
    private Vector2 storedVelocity;
    private ParticleSystem snowParticles;
    private Vector2 lastPosition;
    private bool isVulnerable = true;
    private IPlayerControl control;
    private IPhysics physics;
    private IScore score;
    private int lastHeight;

    //unity
    private void Start()
    {
        name = PLAYER;
        InvokeRepeating("UpdateScoreBySpeed", 1, 1);
        body = gameObject.GetComponent<Rigidbody2D>();
        snowParticles = GetComponent<ParticleSystem>();
        ShowTrace(isActive);
    }

    private void Update()
    {
        if (isActive)
        {
            ReadAngle();
            snowParticles.maxParticles = (int)body.velocity.magnitude;
        }
        OptimizePixels();
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            UpdateForces();
        }
    }
    //unity

    public void SetControl(IPlayerControl control)
    {
        this.control = control;
    }

    public void SetPhysicsl(IPhysics physics)
    {
        this.physics = physics;
    }

    public void SetPScore(IScore score)
    {
        this.score = score;
    }

    public void Reset()
    {
        if (body)
        {
            body.velocity = new Vector2(0, 0);
            storedVelocity = new Vector2(0, 0);
        }
        Points.DestroyAll();
    }

    public void SetActive(bool active)
    {
        isActive = active;
        if (body == null)
        {
            body = gameObject.GetComponent<Rigidbody2D>();
        }
        if (isActive)
        {
            body.gravityScale = 1;
            body.velocity = storedVelocity;
        }
        else
        {
            body.gravityScale = 0;
            storedVelocity = body.velocity;
            body.velocity = new Vector2(0, 0);
        }
        if (snowParticles != null)
        {
            ShowTrace(isActive);
        }
    }

    public void DecreaseScore()
    {
        if (isVulnerable)
        {
            isVulnerable = false;
            InvokeRepeating("MakeVulnerable", 0.5f, 0);
            EmitScore(score.DecreaseScoreByVelocity(GetVelocity().magnitude));
        }
    }

    public Vector2 GetVelocity()
    {
        Vector2 velocity = new Vector2(0, 0);
        if (body)
        {
            velocity = body.velocity;
        }
        return velocity;
    }

    private void OptimizePixels()
    {
        if(lastHeight != Screen.height)
        {
            lastHeight = Screen.height;
            float screenRatio = lastHeight / 144;
            transform.localScale = Vector3.one * Mathf.Ceil(screenRatio) / screenRatio;
        }
    }

    private void ReadAngle()
    {
        angle = control.GetAngle();
        transform.rotation = Quaternion.Euler(0, 0, 180 * angle);
    }

    private void UpdateForces()
    {
        physics.ApplyGroundForcesByAngle(body, angle);
    }

    private void ShowTrace(bool show)
    {
        if (show)
        {
            if (!snowParticles.isPlaying)
            {
                snowParticles.Play();
            }
        }
        else
        {
            if (snowParticles.isPlaying)
            {
                snowParticles.Stop();
            }
        }
    }

    private void EmitScore(int val)
    {
        if (Mathf.Abs(val) > 0)
        {
            Points points = GameObjectsFactory.getInstance().Create<Points>();
            points.transform.position = transform.position;
            points.SetValue(val);
        }
    }

    //called by timer
    private void UpdateScoreBySpeed()
    {
        if (isActive)
        {
            EmitScore(score.IncreaseScoreByVelocity(GetVelocity().magnitude));
        }
    }

    //called by timer
    private void MakeVulnerable()
    {
        isVulnerable = true;
    }
}
