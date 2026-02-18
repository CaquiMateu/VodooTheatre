using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 10f;
    public float ExtraRotation = 0f;
    public float lifeTime = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
       

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DontDestroyBullet") == false)
        {
            Destroy(this.gameObject);
        }
       
    }
    void ProjectileLifeTime()
    {
        Destroy(gameObject);
    }
}
