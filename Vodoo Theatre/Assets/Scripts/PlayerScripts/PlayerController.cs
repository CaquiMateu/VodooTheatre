using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float Movespeed = 4;
    public float jumpforce = 4000;
    public bool IsGrounded = true;
    public Vector3 GroundCheckPosition;
    public Vector2 GroundCheckSize;
    public LayerMask GroundLayer;
    float input;
    public bool moving = false;
    public float angle;
    public AudioSource Salto;
    Vector3 TamañoBase;
    public int direccion;
    public float Aceleración = 10;

    //Ataque a distancia
    public GameObject bullet;
    public GameObject firePoint;
    public GameObject arm;
    public bool Aiming = false;

    //Ataque cuerpo a cuerpo
    public bool Atacking = false;
    public GameObject MeleeAttackRangeDown;
    public GameObject MeleeAttackRange;
    public GameObject MeleeAttackRangeCharged;
    public float CargeTime = 1.5f;
    public int DañoAtaque = 20;
    public int DañoAtaqueCargado = 35;
    bool Cargando = false; 


    //Dash
    public float FuerzaDash = 10f;
    bool Dashing;
    public float TiempoDash = 1.5f;
    float CooldownDash = 0.3f; 
    bool DashOnCooldown = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TamañoBase = transform.localScale;
        direccion = 1;
    }


    void Update()
    {
        GroundChecker();
        MoveChecker();
        #region Rotación Brazo


        // 1. Obtener la posición del ratón en la pantalla
        Vector3 mousePos = Input.mousePosition;


        // 2. Convertir la posición de la pantalla a coordenadas del mundo
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // 3. Calcular la dirección
        Vector2 direction = new Vector2
        (
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
        );
        // 4. Calcular el ángulo en grados
        if (direccion == -1)
        {
            direction.x = -direction.x;
            direction.y = -direction.y;
        }
        else
        {
            direction.x = direction.x;
            direction.y = direction.y;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 5. Rotar el brazo
        arm.transform.rotation = Quaternion.Euler(0, 0, angle);


        if (Input.GetMouseButtonDown(1) && moving== false && Dashing == false && Atacking == false && Cargando== false)
        {
            Aiming = true;
            arm.gameObject.SetActive(true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Aiming = false;
            arm.gameObject.SetActive(false);
           
        }


        #endregion
        #region movimiento
       
        if (Aiming == false)
        {
            input = Input.GetAxisRaw("Horizontal");
           
            if (Atacking == false)
            {
                //Mirar a la Derecha
                if (input > 0)
                {
                    direccion = 1;
                }


                //Mirar a la izquierda
                if (input < 0)
                {
                   direccion= -1;
                }
            }

            float scalex = Mathf.Abs(transform.localScale.x) * direccion;
          transform.localScale = new Vector3 (scalex, transform.localScale.y, transform.localScale.z );
           

            //saltar
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)
            {
                AudioSource.PlayClipAtPoint(Salto.clip, transform.position);
                rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            }

            //Comprobar si estamos en el suelo
              
        } 
        else
        {
            moving = false;
        }
        #endregion;
        #region Disparo
        if (Input.GetMouseButtonDown(0) && Aiming == true && GameObject.FindGameObjectWithTag("Proyectil") == false && Atacking==false)
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        }
        #endregion
        #region Ataque cuerpo a cuerpo

        //Ataque Lateral
        if (Input.GetMouseButton(0) && Aiming == false && GameObject.FindGameObjectWithTag("Ataque")==false && Dashing== false && !Input.GetKey(KeyCode.S))
        {
            Cargando = true;
            CargeTime -= 1 * Time.deltaTime;
            if (CargeTime <= 0)
            {
                if (direccion == -1)
                {
                    rb.transform.localScale = new Vector3(TamañoBase.x * direccion, TamañoBase.y , 0) - new Vector3(0.05f , -0.05f , 0);
                }
                else
                {
                    rb.transform.localScale = TamañoBase + new Vector3(0.05f * direccion, 0.05f * direccion, 0);
                }
                    
                
            }


        }
        else if (Input.GetMouseButtonUp(0) && CargeTime > 0 && !Input.GetKey(KeyCode.S) && Aiming == false && Dashing == false && GameObject.FindGameObjectWithTag("Ataque") == false && Atacking == false)
        {
            CargeTime = 1.5f;
            Atacking = true;
            MeleeAttackRange.gameObject.SetActive(true);
            Invoke("cesaAtaque", 0.1f);

        }

        //Ataque Cargado
        else if (Input.GetMouseButtonUp(0) && CargeTime <= 0 && Aiming == false && !Input.GetKey(KeyCode.S) && Dashing == false && GameObject.FindGameObjectWithTag("Ataque") == false && Atacking== false)
        {
            CargeTime = 1.5f;
            Atacking = true;
            MeleeAttackRangeCharged.gameObject.SetActive(true);
            Invoke("cesaAtaque", 0.2f);
        }



        //Ataque Hacia Abajo
        if (Input.GetKey(KeyCode.S) && Aiming == false && GameObject.FindGameObjectWithTag("Ataque") == false && Dashing == false && IsGrounded == false && Atacking == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Atacking = true;
                MeleeAttackRangeDown.gameObject.SetActive(true);
                Invoke("cesaAtaque", 0.2f);

               
            }
            

        }

        #endregion
        #region Dash

        if (Input.GetKeyDown(KeyCode.LeftShift) && Dashing == false && Aiming== false && Atacking == false && DashOnCooldown==false)
        {
            Dashing = true;

            rb.AddForce(new Vector2(direccion, 0) * FuerzaDash, ForceMode2D.Impulse);

            StartCoroutine(FinDashCrt());

        }
        #endregion
     
    }


    IEnumerator FinDashCrt()
    {
        yield return new WaitForSeconds(TiempoDash);
        
        DashOnCooldown = true;
        Dashing = false;
        StartCoroutine(DashCooldownCrt());
    }

    IEnumerator DashCooldownCrt()
    {
        yield return new WaitForSeconds(CooldownDash);
        DashOnCooldown = false;
       
    }
    
    private void FixedUpdate()
    {
        if (Aiming == false && Dashing == false)
        {

            float MaxSpeed = input * Movespeed;
            float SpeedToApply = MaxSpeed - rb.velocity.x;
            rb.AddForce(new Vector2(SpeedToApply * Aceleración, 0));
        }
        else if (Dashing == false)
        {
             moving = false;
             rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }

    void GroundChecker()
    {
        Collider2D ground = Physics2D.OverlapBox(transform.position + GroundCheckPosition, GroundCheckSize, 0, GroundLayer);
        if (ground != null)

        {
            IsGrounded = true;
            
        }

        else
        {
            IsGrounded = false;
        }

    }
    void MoveChecker()
    {
        if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.LeftArrow))
        {
            moving = true;
        }
        else
        {
            moving = false;
        }
    }

    void cesaAtaque()
    {
        if (direccion < 0)
        {
            rb.AddForce(new Vector2 (20, 0), ForceMode2D.Impulse);
        }
        else 
        {
            rb.AddForce(new Vector2(-20, 0), ForceMode2D.Impulse);

        }

        

        StartCoroutine(Cooldown());

    }

    IEnumerator Cooldown()
    {
        MeleeAttackRange.gameObject.SetActive(false);
        MeleeAttackRangeDown.gameObject.SetActive(false);
        MeleeAttackRangeCharged.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.13f);
        Atacking = false;
        Cargando = false;

        rb.transform.localScale = new Vector2(direccion * TamañoBase.x, TamañoBase.y);

    }

    //Hace visible el área de comprobación del suelo en la escena para facilitar su ajuste
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + GroundCheckPosition, GroundCheckSize);
    }

   
}
