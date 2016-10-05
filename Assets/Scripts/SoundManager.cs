namespace Assets.Scripts
{
    using UnityEngine;
    using System.Collections;

    public class SoundManager : MonoBehaviour
    {

        private AudioSource audio;
        private bool isPlaying;

        private void Start()
        {
            audio = GetComponent<AudioSource>();
            if (isPlaying)
            {
                ActualPlay();
            }
        }

        public void Play(bool play)
        {
            isPlaying = play;
            ActualPlay();
        }

        private void ActualPlay()
        {
            if (audio)
            {
                if (isPlaying)
                {
                    audio.Play();
                }
                else
                {
                    audio.Stop();
                }
            }
        }

        public void SetTrack(string track)
        {
            switch (track)
            {
                case "Normal":
                    StartCoroutine(UpdatePitch(1));
                    break;
                case "Bar":
                    StartCoroutine(UpdatePitch(0.6f));
                    break;
            }
        }

        private IEnumerator UpdatePitch(float toValue)
        {
            WaitForSeconds delay = new WaitForSeconds(0.1f);
            int sign = 1;
            if (toValue < audio.pitch)
            {
                sign = -1;
            }
            int steps =  Mathf.RoundToInt(Mathf.Abs(audio.pitch - toValue)/0.1f);

            for (int i=0; i<steps; i++)
            {
                audio.pitch += sign*0.1f;
                Debug.Log(audio.pitch);
                yield return audio.pitch;
            }
            yield return audio.pitch;
        }
    }

}
