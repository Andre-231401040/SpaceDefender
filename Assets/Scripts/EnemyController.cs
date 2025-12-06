using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int pointValue;
    public ScoreManager scoreManager;

    [Header("Stats")]
    public float speed = 3f;
    public int hp = 5;
    private float rotationSpeed;

    [Header("Visuals")]
    public Sprite damagedSprite;
    public GameObject explosionEffectPrefab;

    private PlayerController player;
    private SpriteRenderer spriteRenderer;
    private int hitsTaken = 0;

    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Random Size (Ukuran Beragam)
        float randomScale = Random.Range(0.4f, 0.5f);
        transform.localScale = new Vector3(randomScale, randomScale, 1);
        // Random Rotation
        rotationSpeed = Random.Range(-50f, 50f);

        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        // Gerak Turun
        // ambil posisi meteor
        Vector2 pos = transform.position;
        // arah gerak meteor ke bawah
        Vector2 direction = Vector2.down;
        // gerakkan meteor
        pos += direction * speed * Time.deltaTime;
        // tidak pakai clamp, jadi meteor boleh keluar dari layar
        transform.position = pos;

        // Rotasi
        Vector3 rot = transform.eulerAngles;
        rot.z += rotationSpeed * Time.deltaTime;
        transform.eulerAngles = rot;

        // Hapus jika lewat bawah
        Vector2 minScreen = Camera.main.ViewportToWorldPoint(Vector2.zero);
        if (transform.position.y < minScreen.y - 2f)
        {
            // if (player != null) player.KurangiNyawa(1);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            KenaTembak();
        }
        else if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.Mati();
            }
            Destroy(gameObject);
        }
    }

    void KenaTembak()
    {
        hitsTaken++;

        // perkecil ukuran
        Vector3 scale = transform.localScale;

        scale -= new Vector3(0.03f, 0.03f, 0f);

        transform.localScale = scale;

        if (hitsTaken == hp/2 && damagedSprite != null)
        {
            spriteRenderer.sprite = damagedSprite;
        }

        if (hitsTaken >= hp)
        {
            Die();
        }
    }

    void Die()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject ledakan = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(ledakan, 1f); 
        }

        scoreManager.ChangeScore(pointValue);
        Destroy(gameObject);
    }
}