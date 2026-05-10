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


    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        GravedadBase = rb.gravityScale;
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

                    StartCoroutine(Salto());
                    break; 
                
                case 3:
                    StartCoroutine(AtaqueZanahorias());
                    break; 
                
                case 4:
                    StartCoroutine(Cooldown());
                    break; 
                
                case 5:
                    StartCoroutine(Cooldown());
                    break;
            }
        }

        if (Huyendo == false)
        {
            // Calcula dirección sin mover verticalmente
            Vector3 direccion = jugador.position - transform.position;
            direccion.Normalize();
            movimiento = new Vector2(direccion.x, 0); // Solo eje X para terrestre
        }
        else
        {
            Vector3 direccion = Puntohuida.position - transform.position;
            direccion.Normalize();
            movimiento = new Vector2(direccion.x, 0); // Solo eje X para terrestre
        }
       
    }

    void FixedUpdate()
    {
        if (jugador != null && chasing == true || Huyendo == true)
        {
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
            rb.AddForce(new Vector2(0, fuerzaSalto), ForceMode2D.Impulse);
            Saltando = false;
        }

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

    IEnumerator Salto()
    {
        Vector2 PosiciónCaida = new Vector2(Random.Range(-16, 22), -0);
        Saltando = true;
        yield return new WaitForSeconds(1f);
        if (gameObject.transform.position.y > 17.33)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector3.zero;
            yield return new WaitForSeconds(1);
           
        }

        this.gameObject.transform.position = new Vector3(PosiciónCaida.x, 34);
       


        IconoAdvertenciaSalto.SetActive(true);
        IconoAdvertenciaSalto.transform.position = PosiciónCaida;

        yield return new WaitForSeconds(3);
        IconoAdvertenciaSalto.SetActive(false);
        rb.gravityScale = GravedadBase;
        yield return new WaitForSeconds(1);
        StartCoroutine (Cooldown());

    }

    IEnumerator AtaqueZanahorias()
    {
        Huyendo = true;
        yield return new WaitForSeconds(1.5f);
        int NumTandas = Random.Range(2, 5);
        for (int i = 0; i < NumTandas; i++) 
        { 
          int numDeZanahorias = Random.Range(4, 7);
            for (int j = 0; j < numDeZanahorias; j++)
            {
                float Y = Random.Range(-0.6f, 9.25f);
                GameObject Projectil =Instantiate(ZanahoriaProjectile, new Vector2(Puntohuida.position.x, Y), Quaternion.identity);
                yield return new WaitForSeconds(Random.Range(0.93f, 1.2f));
            }
        }
        Huyendo = false;
        StartCoroutine (Chase());
        
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
