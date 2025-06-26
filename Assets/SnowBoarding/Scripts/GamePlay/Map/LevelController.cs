using System.Collections.Generic;
using UnityEngine;

namespace SnowBoarding.Scripts.GamePlay.Map
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] public List<SurfaceEffector2D> surfaceEffector2D;
        [SerializeField] public Transform startPoint;
        [SerializeField] public int bgType;

    }
}