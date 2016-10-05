namespace Assets.Scripts
{
    using UnityEngine;

    public class CameraManager : MonoBehaviour
    {
        public GameObject backgroundPrefab;


        private GameObject background;
        private ParticleSystem snowBox;
        private static GameObject target;
        private Vector3 offset = new Vector3(0, -5, -4);

        //unity
        void Start()
        {
            snowBox = (ParticleSystem)transform.FindChild("SnowBox").GetComponent<ParticleSystem>();
            background = Instantiate(backgroundPrefab);
            background.transform.parent = transform;
            background.transform.localScale = new Vector3(2f, 2f, 1);
            background.transform.localPosition = new Vector3(0, 0, 1);
            if (target)
            {
                transform.position = target.transform.position + offset;
            }
        }

        void Update()
        {
            //TODO: constrain to prevent getting out of the border
            background.transform.localPosition = new Vector3(-transform.localPosition.x * 0.1f, -transform.localPosition.y * 0.01f, 1);
        }

        void LateUpdate()
        {
            if (target)
            {
                transform.position = target.transform.position + offset;
            }
        }
        //unity

        public void SetTarget(GameObject t)
        {
            target = t;
            transform.position = target.transform.position + offset;
        }

        public void PlaySnow(bool play)
        {
            if (play)
            {
                if (!snowBox.isPlaying)
                {
                    snowBox.Play();
                    Debug.Log("Play snow");
                }

            }
            else
            {
                if (snowBox.isPlaying)
                {
                    snowBox.Pause();
                    snowBox.Clear();
                    Debug.Log("Stop snow");
                }
            }
        }
    }
}
