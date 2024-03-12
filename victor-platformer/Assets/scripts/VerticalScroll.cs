using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    // Start is called before the first frame update
    [Header ("Game Settings")]

    [Tooltip("Game units per second")]
    [SerializeField] private float scrollSpeed = 0.2f;
    void Update()
    {
        transform.Translate(new Vector2(0.0f, scrollSpeed * Time.deltaTime));
    }
}
