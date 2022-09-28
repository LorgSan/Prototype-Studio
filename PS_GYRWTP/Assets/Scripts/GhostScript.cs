using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    [SerializeField] Color endColor;
    SpriteRenderer sr;
    Color startColor;
    float step;
    [HideInInspector] public bool doneFading = false;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        step += Time.deltaTime;
        Color newAlpha = Color.Lerp(startColor, endColor, step);
        sr.color = newAlpha;
        if (sr.color == endColor)
        {
            doneFading = true;
            Destroy(gameObject);
            //Debug.Log("Done changing");
        }
    }
}
