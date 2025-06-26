using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    [SerializeField] private List<GameObject> bg;
    private GameObject currentBG;

    public void SetBG(int index)
    {
        currentBG = bg[index];
        for (int i = 0; i < bg.Count; i++)
        {
            bg[i].SetActive(i == index);
        }
    }
}