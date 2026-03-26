using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVoladorController : MonoBehaviour
{
   public float velocidad = 5f;
   public Transform jugador;
   public int daño = 1;
   public Rigidbody2D rb;
    private Vector2 movimiento;
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador != null)
        {
            // Calcula dirección sin mover verticalmente
            Vector3 direccion = jugador.position - transform.position;
            direccion.Normalize();
            movimiento = new Vector2(direccion.x, direccion.y); 
        }
    }

    void FixedUpdate()
    {
        if (jugador != null)
        {
            // Mueve al enemigo
            rb.velocity = new Vector2(movimiento.x * velocidad, movimiento.y * velocidad);

            // Voltear el sprite según la dirección
            if (movimiento.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            else if (movimiento.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.PerderVida(daño);
                }
            }
    }
}
