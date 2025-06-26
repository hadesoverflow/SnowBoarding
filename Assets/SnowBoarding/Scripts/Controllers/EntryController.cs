using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _GAME.Scripts.Controllers
{
    public class EntryController : MonoBehaviour
    {
        [SerializeField] private GameObject manager;
        [SerializeField] private Image Slider;
        [SerializeField] private float loadDuration = 2f;

        private void Awake()
        {
            loadDuration = Random.Range(2, 3);
            DontDestroyOnLoad(manager);
        }

        private void Start()
        {
            Slider.fillAmount = 0f;

            Slider.DOFillAmount(1f, loadDuration)
                .SetEase(Ease.OutCubic)
                .OnComplete(() =>
                {
                    SceneManager.LoadScene(GameConstants.SceneMain);
                });
        }
    }
}