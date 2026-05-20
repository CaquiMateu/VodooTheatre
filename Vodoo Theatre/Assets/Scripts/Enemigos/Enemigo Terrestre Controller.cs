using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoTerrestreController : MonoBehaviour
{
   public float velocidad = 5f;
   public Transform jugador;
   public int daño = 1;
   public Rigidbody2D rb;
    private Vector2 movimiento;
    EnemyHeal enemyHeal;
    public PlayerHealth playerHealth;
    public Vector3 direccion;
    public bool Choque = false;


    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.position= new Vector3(transform.position.x, -1, 0);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        enemyHeal = GetComponent<EnemyHeal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHeal.IsDead==true) return;
        if (jugador != null)
        {
            if (playerHealth ==null || Choque == false)
            {
                // Calcula dirección sin mover verticalmente
                direccion = jugador.position - transform.position;
                direccion.Normalize();
                movimiento = new Vector2(direccion.x, 0); // Solo eje X para terrestre
            }

            else if (playerHealth.IsDead == false)
            {
                // Calcula dirección sin mover verticalmente
                direccion = jugador.position - transform.position;
                direccion.Normalize();
                movimiento = new Vector2(direccion.x, 0); // Solo eje X para terrestre
            }
            else if(playerHealth.IsDead == true) 
            {
                int Multiplicador = Random.Range(-1, 2);
                if (Multiplicador <= 0)
                {
                    Multiplicador = -1;
                }
                else
                {
                    Multiplicador = 1;
                }
                direccion = new Vector3 (Multiplicador * Random.Range(10,20), Random.Range(10,20));
            }
           
        }
    }

    void FixedUpdate()
    {
        if (enemyHeal.IsDead == true) return;
        if (jugador != null)
        {
            // Mueve al enemigo
            rb.velocity = new Vector2(movimiento.x * velocidad, rb.velocity.y);

            // Voltear el sprite según la dirección
            if (movimiento.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (movimiento.x < 0)
                transform.localScale = new Vector3(1, 1, 1);
        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (enemyHeal.IsDead == true) return;
           
            if (collision.gameObject.CompareTag("Player"))
            Choque = true;
        {
                playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.PerderVida(daño* playerHealth.MultiplicadorDeDaño);
                }
            }
    }
}

