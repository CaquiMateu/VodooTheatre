using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeTítere : MonoBehaviour
{
    Transform jugador;
    public Transform PuntoMedio;
    public int daño;
    public int acciones;
    public float velocidad = 4;
    EnemyHeal enemyHeal;

    Rigidbody2D rb;
    bool INvunerable = false;
    public bool Accionando = false;
    public bool chasing = false;
    Vector2 movimiento;
    Vector3 TamañoBase;
    public Vector3 posiciónInicio;
    public Vector3 posicion;
    public Animator animator;
    public bool alejado = true;
    public bool Pequeño = false;
    Collider2D col;
    [Range(0, 1)] public float t = 1;
    public AnimationCurve curve;
    Vector3 direccion;
    public Transform Objetivo;
    public GameObject EnemigoAraña;
    public GameObject EnemigoBat;
    public GameObject EnemigoSombra;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        TamañoBase = this.gameObject.transform.localScale;
        enemyHeal = GetComponent<EnemyHeal>();
        col = GetComponent<Collider2D>();
        posiciónInicio = transform.position;
        PuntoMedio = GameObject.FindGameObjectWithTag("Punto").transform;
    }
    void Start()
    {
        Accionando = true;
        StartCoroutine(Cooldown());
       
    }


    void Update()
    {
        posicion = this.transform.position;

        if (enemyHeal.IsDead == true) return;

        if (Accionando == false)
        {
            if (enemyHeal.IsDead == true) return;
            Accionando = true;

            acciones = Random.Range(0, 6);

            switch (acciones)
            {
                case 0: //Se queda quieto
                    EstarQuieto();
                    break;

                case 1: 
                    
                    StartCoroutine(Acercarse());

                       
                    
                    break;

                case 2:

                    StartCoroutine(AparicionEnemigos());
                    break;

                case 3:
                    StartCoroutine(BossSombra());
                    break;

                case 4:
                    StartCoroutine(Acercarse());
                    break;

                case 5:
                    StartCoroutine(AparicionEnemigos());
                    break;
            }
        }
       
        if (alejado == true)
        {
            gameObject.layer = LayerMask.NameToLayer("Invunerable");
            col.enabled = false;

            if (transform.localScale != new Vector3 (3.840215f, 3.840215f, 3.840215f))
            {
                Pequeño = true;
              

                if (t < 1)
                {
                    t += 1f * Time.deltaTime;
                }

                transform.localScale = Vector3.Lerp(TamañoBase, new Vector3(3.840215f, 3.840215f, 3.840215f), curve.Evaluate(t));

                
            }
        }
       else
        {
            gameObject.layer = LayerMask.NameToLayer("Enemigo");
            

            if (transform.localScale != TamañoBase)
            {
                Pequeño = false;


                if (t > 0)
                {
                    t -= 1f * Time.deltaTime;
                }
               
                transform.localScale = Vector3.Lerp( TamañoBase, new Vector3(3.840215f, 3.840215f, 3.840215f), curve.Evaluate(t));
                
            }
            col.enabled = true;
        }

        
        if (Pequeño == true)
        {
            chasing = true;
            if (jugador != null && chasing == true)
            {
                Objetivo = jugador;
                // Calcula dirección sin mover verticalmente
                direccion = Objetivo.position - transform.position;
                direccion.Normalize();
                movimiento = new Vector2(direccion.x, 0);


            }
        }

       
        else
        {
            Objetivo = PuntoMedio;
            direccion = Objetivo.position - transform.position;
            direccion.Normalize();
            movimiento = new Vector2(direccion.x, 0);
        }


    }

    void FixedUpdate()
    {
        if (enemyHeal.IsDead == true) return;
        if(Vector3.Distance(transform.position, Objetivo.position) > 0.5f)
        {
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
        else
        {
            rb.velocity = Vector3.zero;
        }
        


    }

    public void EstarQuieto()
    {
        if (enemyHeal.IsDead == true) return;


        StartCoroutine(Cooldown());
    }

    

    IEnumerator Cooldown()
    {
        Debug.Log("Cooldown");
        float tiempo = Random.Range(1.5f, 3.1f);
        yield return new WaitForSeconds(tiempo);
        Accionando = false;
    }

    IEnumerator Acercarse()
    {

        alejado = false;
        yield return new WaitForSeconds(5.5f);
        alejado = true;
        StartCoroutine (Cooldown());
    }

    IEnumerator AparicionEnemigos()
    {
        yield return null;
        for (int i = 0; i < Random.Range(1,5); i++)
        {
            int Tipo = Random.Range(1, 3);
            int lugar = Random.Range(1, 3);
            Vector3 Spawn;
            if (lugar == 1)
            {
                Spawn = new Vector3(Random.Range(-35, -21), Random.Range(0, 10), 0);
            }
            else
            {
                Spawn = new Vector3(Random.Range(29, 33), Random.Range(0, 10), 0);
            }

            if (Tipo == 1)
            {

                Instantiate(EnemigoAraña, Spawn, Quaternion.identity);
            }
            else if (Tipo == 2)
            {
                Instantiate(EnemigoBat, Spawn, Quaternion.identity);
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(3);
        StartCoroutine (Cooldown());

       
        
    }

    IEnumerator BossSombra()
    {
        yield return null;
        Instantiate(EnemigoSombra, new Vector3(6.22f, 10.45f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);
        StartCoroutine(Cooldown());
    }
 

    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemyHeal.IsDead == false)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PerderVida(daño * playerHealth.MultiplicadorDeDaño);
            }
        }
    }
}
