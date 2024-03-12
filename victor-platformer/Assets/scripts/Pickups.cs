using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickUpSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coinPickUpSFX != null)
        {
            // Use collision.gameObject.transform.position instead of Camera.main.transform.position
            AudioSource.PlayClipAtPoint(coinPickUpSFX, collision.gameObject.transform.position);
        }
        else
        {
            Debug.LogError("Coin pick up sound effect is not assigned to the Pickups script.");
        }

        Destroy(gameObject);
    }
}