using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeConejo : MonoBehaviour
{
   Transform jugador;
    public Transform Puntohuida;
    public int daño;
    public int acciones;
    public float velocidad = 4;
    public float fuerzaSalto;
    public GameObject IconoAdvertenciaSalto;
    public FuegoController Fuego;
    EnemyHeal enemyHeal;
    
    Rigidbody2D rb;
    bool INvunerable = false;
    public bool Accionando = false;
    public bool chasing = false;
    public bool Saltando = false;
    public bool Huyendo = false;
    Vector2 movimiento;
    Vector2 TamañoBase;
    public Vector2 PosiciónCaida;
    float GravedadBase;
    public GameObject ZanahoriaProjectile;
    float CarrotSpeed = 5;
    public Animator animator;
    public Collider2D collider2;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Fuego = GameObject.Find("Fuego").GetComponent<FuegoController>();
        IconoAdvertenciaSalto = GameObject.Find("IconoExclamación");
        GameObject Spawn2 = GameObject.Find("Zona 2");
        Puntohuida = Spawn2.transform;
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        GravedadBase = rb.gravityScale;
        TamañoBase = this.gameObject.transform.localScale;
        enemyHeal = GetComponent<EnemyHeal>();
        collider2 = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Accionando = true;
        StartCoroutine(Cooldown());
    }

   
    void Update()
    {

        if (enemyHeal.IsDead == true) return;

        if (Accionando == false)
        {
            if (enemyHeal.IsDead == true) return;
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

                    StartCoroutine(Salto());
                    break; 
                
                case 3:
                    StartCoroutine(AtaqueZanahorias());
                    break; 
                
                case 4:
                    StartCoroutine(AtaqueFuego());
                    break; 
                
                case 5:
                    StartCoroutine(SaltoDirigido());
                    break;
            }
        }

        if (Huyendo == false)
        {
            if (enemyHeal.IsDead == true) return;

            // Calcula dirección sin mover verticalmente
            Vector3 direccion = jugador.position - transform.position;
            direccion.Normalize();
            movimiento = new Vector2(direccion.x, 0); // Solo eje X para terrestre
        }
        else
        {
            if (enemyHeal.IsDead == true) return;

            Vector3 direccion = Puntohuida.position - transform.position;
            direccion.Normalize();
            movimiento = new Vector2(direccion.x, 0); // Solo eje X para terrestre
        }
       
    }

    void FixedUpdate()
    {
        if (enemyHeal.IsDead == true) return;

        if (jugador != null && chasing == true || Huyendo == true)
        {
            if (enemyHeal.IsDead == true) return;

            // Mueve al enemigo
            rb.velocity = new Vector2(movimiento.x * velocidad, rb.velocity.y);

          
        }
        // Voltear el sprite según la dirección
        if (movimiento.x > 0)
            transform.localScale = new Vector3(-TamañoBase.x, TamañoBase.y, 1);
        else if (movimiento.x < 0)
            transform.localScale = new Vector3(TamañoBase.x, TamañoBase.y, 1);
        if (Saltando == true)
        {
            if (enemyHeal.IsDead == true) return;

            rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
            Saltando = false;
        }
        if (enemyHeal.IsDead == true) return;

    }

    public void EstarQuieto()
    {
        if (enemyHeal.IsDead == true) return;


        StartCoroutine(Cooldown());
    }

    IEnumerator Chase()
    {

        animator.SetBool("CarChase", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("CarChase", false);
        animator.SetBool("Chasing", true);

        chasing = true;
        yield return new WaitForSeconds(1.5f);
        chasing = false;
        animator.SetBool("Chasing", false);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        Debug.Log("Cooldown");
        float tiempo = Random.Range(1.5f,3.1f);
        yield return new WaitForSeconds(tiempo);
        Accionando = false;
    }

    IEnumerator Salto()
    {
        animator.SetBool("CarJump", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("CarJump", false);
        animator.SetBool("Jumping", true);

        Vector2 PosiciónCaida = new Vector2(Random.Range(-16, 22), -0);
        Saltando = true;
        yield return new WaitForSeconds(1f);
        if (gameObject.transform.position.y > 17.33)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(1);
            animator.SetBool("Jumping", false);


        }

        this.gameObject.transform.position = new Vector3(PosiciónCaida.x, 34);
       


        IconoAdvertenciaSalto.SetActive(true);
        IconoAdvertenciaSalto.transform.position = PosiciónCaida;

        yield return new WaitForSeconds(3);
        IconoAdvertenciaSalto.SetActive(false);
        rb.gravityScale = GravedadBase;
        animator.SetBool("Falling",true);
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Falling", false);

        StartCoroutine(Cooldown());

    }

    IEnumerator AtaqueZanahorias()
    {
        Huyendo = true;
        animator.SetBool("Chasing", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Chasing", false);

        int NumTandas = Random.Range(2, 5);
        for (int i = 0; i < NumTandas; i++) 
        { 
          int numDeZanahorias = Random.Range(4, 7);
            for (int j = 0; j < numDeZanahorias; j++)
            {
                if (enemyHeal.IsDead == true) StartCoroutine(Cooldown());

                else
                {
                    float Y = Random.Range(-0.6f, 9.25f);
                    GameObject Projectil = Instantiate(ZanahoriaProjectile, new Vector2(Puntohuida.position.x, Y), Quaternion.Euler(new Vector3(0, 0, -90)));
                    yield return new WaitForSeconds(Random.Range(0.93f, 1.2f));
                }
                    
            }
        }
        Huyendo = false;
        StartCoroutine (Chase());
        
    }

    IEnumerator AtaqueFuego()
    {
        animator.SetBool("Fire", true);
        
        SpriteRenderer FuegoSprite = Fuego.GetComponent<SpriteRenderer>();
        Animator FuegoAnimator = Fuego.GetComponent<Animator>();
        FuegoAnimator.enabled = true;
        FuegoSprite.GetComponent<SpriteRenderer>().enabled = true;
        Fuego.t = 0;
        yield return new WaitUntil(() => Fuego.t >= 1);
        Collider2D FuegoCollider = Fuego.GetComponent<Collider2D>();
        FuegoCollider.enabled = true;
        animator.SetBool("Fire", false);
        yield return new WaitForSeconds (0.5f);
        FuegoAnimator.enabled = false;
        FuegoCollider.enabled= false;
        Fuego.t = 0;
        FuegoSprite.enabled = false;
        
        
        StartCoroutine(Cooldown());
    }

    IEnumerator SaltoDirigido()
    {
        animator.SetBool("CarJump", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("CarJump", false);
        animator.SetBool("Jumping", true);
        Vector2 PosiciónCaida = jugador.position;
        Saltando = true;
        yield return new WaitForSeconds(1f);
        if (gameObject.transform.position.y > 17.33)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(1);
            animator.SetBool("Jumping", false);

        }

        this.gameObject.transform.position = new Vector3(PosiciónCaida.x, 34);


        PosiciónCaida = jugador.position;
       
        rb.gravityScale = GravedadBase;
        animator.SetBool("Falling", true);
        yield return new WaitForSeconds(1.2f);
        animator.SetBool("Falling", false);
        StartCoroutine(Cooldown());

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemyHeal.IsDead==false)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PerderVida(daño * playerHealth.MultiplicadorDeDaño);
            }
        }
    }
}
