using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public PlayerController playerController;
    Rigidbody2D rb;
    public float speed = 10f;
    public float ExtraRotation = 0f;
    public float lifeTime = 5f;
    public AudioSource SpawnSound;
    public int DañoBala = 25;
    public float Cooldown;
    public SpriteRenderer SpriteRenderer;
    public Collider2D Collider;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        SpawnSound.Play();
        Vector3 mousePos = Input.mousePosition;


        // 2. Convertir la posición de la pantalla a coordenadas del mundo
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // 3. Calcular la dirección
        Vector2 direction = new Vector2
        (
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );
        // 4. Calcular el ángulo en grados
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 5. Rotar el brazo
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle + ExtraRotation); 

        rb.velocity = direction.normalized * speed;

        Invoke("ProjectileLifeTime", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null) 
        {
            Debug.Log("CodigoRecibido");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DontDestroyBullet") == false)
        {

            Debug.Log("Hostión");
            StartCoroutine(CooldownBala());
            SpriteRenderer.enabled = false;
            Collider.enabled = false;


        }
    }
    void ProjectileLifeTime()
    {
        Destroy(gameObject);
    }

    IEnumerator CooldownBala()
    {
        yield return new WaitForSeconds(Cooldown);
        playerController.Disparando = false;
        Destroy(gameObject);
    }
}
