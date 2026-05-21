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
    public JefeConejo jefeConejo;
    public JefeTítere jefeTítere;
    public bool IsDead = false;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    public bool Esconejo = false;
    public bool EsSombra = false;

    void Start()
    {
       spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GameObject Player = GameObject.FindWithTag("Player");
        DañoAtaque = Player.GetComponent<PlayerController>();
        DañoAtaqueCargado = Player.GetComponent<PlayerController>();
        sprite = GetComponent<SpriteRenderer>();
        if (Esconejo== true)
        {
            jefeConejo = GetComponent<JefeConejo>();
            Esconejo = true;
        }
        else if (GameObject.FindGameObjectWithTag("Títere"))
        {
           jefeTítere = GetComponent<JefeTítere>();
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (EsSombra == true)
        {
            jefeConejo.spriteRenderer.color = Color.black;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsDead == true) return;
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
                IsDead = true;
                rb.gravityScale = 2.23f;
                gameObject.layer = LayerMask.NameToLayer("Invunerable");
                animator.SetBool("IsDead", true);
                yield return new WaitForSeconds(0.1f);
                animator.SetBool("IsDead", false);
                yield return new WaitForSeconds(0.65f);
                Color color = Color.white;
                float alpha = 1;
                while(alpha > 0)
                {
                    alpha -= Time.deltaTime;
                    color.a = alpha;
                    spriteRenderer.color = color;
                    yield return null;
                }

                Destroy(this.gameObject);
            } 
           
        }

        else 
        {
            if (VidaEnemigo <= 0)
            {
               if (Esconejo == true)
               {
                    
                    IsDead = true;
                    jefeConejo.animator.SetBool("Muerte", true);
                    yield return new WaitForSeconds(0.1f);
                    jefeConejo.animator.SetBool("Muerte", false);
                    Time.timeScale = 0.3f;
                    yield return new WaitForSeconds(0.5f);
                    jefeConejo.gameObject.layer = LayerMask.NameToLayer("Invunerable");
                    Time.timeScale = 1;
               }
               else
               {
                    IsDead = true;
                    jefeTítere.animator.SetBool("Muerte", true);
                    yield return new WaitForSeconds(0.1f);
                    jefeTítere.animator.SetBool("Muerte", false);
                    Time.timeScale = 0.3f;
                    yield return new WaitForSeconds(0.5f);
                    Time.timeScale = 1;
               }
               
                
                yield return new WaitForSeconds(7);
                Destroy(this.gameObject);

            } 
        }
    }
}
