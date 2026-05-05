using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeConejo : MonoBehaviour
{
   Transform jugador;
    public int daño;
    public int acciones;
    public float velocidad = 4;
    Rigidbody2D rb;
    bool INvunerable = false;
    public bool Accionando = false;
    public bool chasing = false;
    Vector2 movimiento;
    Vector2 TamañoBase;
    
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        TamañoBase = this.gameObject.transform.localScale;
    }

   
    void Update()
    {
        if (Accionando == false)
        {
            Accionando = true;

            acciones = Random.Range(0, 6);

            switch(acciones)
            {
              case 0: //Se queda quieto
                    EstarQuieto();
                    break;

              case 1: //Persigue al Jugador Durante un segundo y medio
                    if (jugador != null)
                    {
                        
                        StartCoroutine(Chase());
                    }
                    break;

                case 2:
                    StartCoroutine(Cooldown());
                    break; 
                
                case 3:
                    StartCoroutine(Cooldown());
                    break; 
                
                case 4:
                    StartCoroutine(Cooldown());
                    break; 
                
                case 5:
                    StartCoroutine(Cooldown());
                    break;
            }
        }
        // Calcula dirección sin mover verticalmente
        Vector3 direccion = jugador.position - transform.position;
        direccion.Normalize();
        movimiento = new Vector2(direccion.x, 0); // Solo eje X para terrestre
    }

    void FixedUpdate()
    {
        if (jugador != null && chasing == true)
        {
            // Mueve al enemigo
            rb.velocity = new Vector2(movimiento.x * velocidad, rb.velocity.y);

          
        }
        // Voltear el sprite según la dirección
        if (movimiento.x > 0)
            transform.localScale = new Vector3(-TamañoBase.x, TamañoBase.y, 1);
        else if (movimiento.x < 0)
            transform.localScale = new Vector3(TamañoBase.x, TamañoBase.y, 1);
    }

    public void EstarQuieto()
    {
        StartCoroutine(Cooldown());
    }

    IEnumerator Chase()
    {
        chasing = true;
        yield return new WaitForSeconds(1.5f);
        chasing = false;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        Debug.Log("Cooldown");
        int tiempo = Random.Range(5,10);
        yield return new WaitForSeconds(tiempo);
        Accionando = false;
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
