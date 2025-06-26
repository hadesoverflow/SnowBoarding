using UnityEngine;

namespace SnowBoarding.Scripts.GamePlay.Map
{
    public class DustTrail : MonoBehaviour
    {
        [SerializeField] ParticleSystem dustParticles;

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                dustParticles.Play();
            }
        }
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                dustParticles.Stop();
            }
        }
    }
}