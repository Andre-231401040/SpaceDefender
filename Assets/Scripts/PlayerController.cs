using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioClip shootSoundClip;

    private AudioSource audioSource;

    Material material;

    float fade = 1f;
    bool isDissolving = false;

    public float speed = 5f; 
    public GameObject PlayerBullet;
    public GameObject bulletPosition;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            material.SetFloat("_Fade", fade);

            if (fade <= 0f)
            {
                fade = 0f;
                isDissolving = false;
                SceneManager.LoadScene("00_Start");
            }

            return;
        }

        // Logika Tembak
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Play sound
            audioSource.clip = shootSoundClip;
            audioSource.Play();

            if (PlayerBullet != null && bulletPosition != null)
            {
                GameObject bullet = Instantiate(PlayerBullet);
                bullet.transform.position = bulletPosition.transform.position;
            }
        }

        // Logika Gerak
        var keyboard = Keyboard.current;
        if (keyboard == null) return; // Mencegah error jika keyboard tidak terdeteksi

        float x = 0f;
        float y = 0f;

        if (keyboard.aKey.isPressed) x = -1f;
        if (keyboard.dKey.isPressed) x = 1f;
        if (keyboard.sKey.isPressed) y = -1f;
        if (keyboard.wKey.isPressed) y = 1f;

        Vector2 direction = new Vector2(x, y).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.36f;
        min.x = min.x + 0.36f;
        max.y = max.y - 0.69f;
        min.y = min.y + 0.69f;

        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    public void Mati()
    {
        Debug.Log("GAME OVER");

        isDissolving = true;
    }
}