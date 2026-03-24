using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour
{
    public int VidaEnemigo = 25;
    public bool Golpeado = false;

    private PlayerHealth PlayerHealth;
    public ProjectileController ProjectilDaño;
    public PlayerController DañoAtaque;
    public PlayerController DañoAtaqueCargado;

    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        DañoAtaque = Player.GetComponent<PlayerController>();
        DañoAtaqueCargado = Player.GetComponent<PlayerController>();
        
      
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proyectil"))
        {
            Golpeado = true;
            if (ProjectilDaño == null)
            {
                ProjectilDaño = collision.GetComponent<ProjectileController>();
            }
             
            VidaEnemigo -= ProjectilDaño.DañoBala;
            Destroy(collision.gameObject);

            if (VidaEnemigo <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        else if (collision.CompareTag("Ataque"))
        {
            if (DañoAtaque == null)
            {
                DañoAtaque = collision.GetComponent<PlayerController>();
            }

            VidaEnemigo -= DañoAtaque.DañoAtaque;

            if (VidaEnemigo <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        else if (collision.CompareTag("Ataque Cargado"))
        {
            if (DañoAtaqueCargado == null)
            {
                DañoAtaqueCargado = collision.GetComponent<PlayerController>();
            }

            VidaEnemigo -= DañoAtaqueCargado.DañoAtaqueCargado;

            if (VidaEnemigo <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
