using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthViz : MonoBehaviour
{
    int health;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] Sprite emptyHeart;
    public void SetupHealth(int maxHealth, int minHealth)
    {
        for (int i=minHealth; i <= maxHealth; i++)
        {
            GameObject go = Instantiate(heartPrefab);
            go.transform.SetParent(transform, false);
        }
    }

    public void UpdateHealth()
    {
        foreach (Transform heart in transform) {
            if (heart.GetComponent<Image>().sprite == emptyHeart)
            {
                continue;
            } else 
            {
                heart.GetComponent<Image>().sprite = emptyHeart;
                break;
            }
        }
    }
}
