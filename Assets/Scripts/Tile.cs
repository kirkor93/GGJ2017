using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Tile : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D other)
    {
        Bat bat = other.gameObject.GetComponent<Bat>();
        if (bat == null)
        {
            Debug.Log(string.Format("Tile collided with {0}. Nothing to do here.", other.gameObject.name));
            return;
        }

        ScreenShaker.Instance.Shake();
    }
}
