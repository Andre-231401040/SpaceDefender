using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        // ambil posisi peluru
        Vector2 position = transform.position;

        // hitung posisi baru peluru
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        // update posisi peluru
        transform.position = position;

        // ambil bagian kanan atas dari screen (max)
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        // hancurkan peluru jika keluar dari screen
        if(transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }
}
