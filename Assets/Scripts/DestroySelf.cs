using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float delay = 0.65f;

    void Start()
    {
        // Hancurkan game object ini setelah menunggu selama 'delay' detik
        Destroy(gameObject, delay);
    }
}