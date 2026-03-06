using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour
{
    public int VidaEnemigo = 25;

    private PlayerHealth PlayerHealth;
    public ProjectileController ProjectilDaño;
    public PlayerController DañoAtaque;
    public PlayerController DañoAtaqueCargado;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Proyectil"))
        {
            if (ProjectilDaño == null)
            {
                ProjectilDaño = collision.GetComponent<ProjectileController>();
            }

            VidaEnemigo -= ProjectilDaño.DañoBala;

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
