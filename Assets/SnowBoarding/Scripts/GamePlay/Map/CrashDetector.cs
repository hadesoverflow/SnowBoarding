using _GAME.Scripts.Controllers;
using UnityEngine;

namespace SnowBoarding.Scripts.GamePlay.Map
{
    public class CrashDetector : MonoBehaviour
    {
        [SerializeField] float loadDelay = 1f;
        [SerializeField] ParticleSystem crashEffect;
        private bool _hasCrashed;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground") && !_hasCrashed)
            {
                _hasCrashed = true;
                GameController.Instance.PlayerHitObstacle();
            }
        }

    }
}
