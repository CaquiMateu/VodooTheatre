using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeal : MonoBehaviour
{
    public int VidaEnemigo = 25;
    public bool Golpeado = false;
    public bool boss;

    private PlayerHealth PlayerHealth;
    public ProjectileController ProjectilDaño;
    public PlayerController DañoAtaque;
    public PlayerController DañoAtaqueCargado;
    SpriteRenderer sprite;
    Animator animator;
    public bool IsDead = false;

    void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        DañoAtaque = Player.GetComponent<PlayerController>();
        DañoAtaqueCargado = Player.GetComponent<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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
            StartCoroutine(DamageEffect());


           
        }

        else if (collision.CompareTag("Ataque"))
        {
            if (DañoAtaque == null)
            {
                DañoAtaque = collision.GetComponent<PlayerController>();
            }

            VidaEnemigo -= DañoAtaque.DañoAtaque;
            StartCoroutine(DamageEffect());

           
        }

        else if (collision.CompareTag("Ataque Cargado"))
        {
            if (DañoAtaqueCargado == null)
            {
                DañoAtaqueCargado = collision.GetComponent<PlayerController>();
            }

            VidaEnemigo -= DañoAtaqueCargado.DañoAtaqueCargado;
            StartCoroutine(DamageEffect());

           
        }

       
    }
    IEnumerator DamageEffect()
    {
       
        Time.timeScale = 0.1f;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.01f);
        sprite.color = Color.white;
        Time.timeScale = 1;

        if (boss == false)
        {
            if (VidaEnemigo <= 0)
            {
                Destroy(this.gameObject);
            } 
           
        }

        else 
        {
            if (VidaEnemigo <= 0)
            {
                IsDead = true;
                animator.SetBool("Muerte", true);
                yield return new WaitForSeconds(7);
                Destroy(this.gameObject);

            } 
        }
    }
}
