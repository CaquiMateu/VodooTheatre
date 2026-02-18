using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

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

    
    //Dash
    public float FuerzaDash = 10f;
    bool Dashing;
    public float TiempoDash = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 5. Rotar el brazo
        arm.transform.rotation = Quaternion.Euler(0, 0, angle);


        if (Input.GetMouseButtonDown(1) && moving== false && Dashing == false && Atacking == false)
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
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }


                //Mirar a la izquierda
                else if (input < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
           

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
            Atacking = true;
            CargeTime -= 1 * Time.deltaTime;
            if (CargeTime <= 0)
            {
                rb.transform.localScale = new Vector3(1.5F, 1.5F, 1.5F);
            }


        }
        else if (Input.GetMouseButtonUp(0) && CargeTime > 0 && !Input.GetKey(KeyCode.S) && Aiming == false && Dashing == false && GameObject.FindGameObjectWithTag("Ataque") == false)
        {
            CargeTime = 1.5f;
            Atacking = true;
            MeleeAttackRange.gameObject.SetActive(true);
            Invoke("cesaAtaque", 0.2f);

        }

        //Ataque Cargado
        else if (Input.GetMouseButtonUp(0) && CargeTime <= 0 && Aiming == false && !Input.GetKey(KeyCode.S) && Dashing == false && GameObject.FindGameObjectWithTag("Ataque") == false)
        {
            CargeTime = 1.5f;
            Atacking = true;
            MeleeAttackRangeCharged.gameObject.SetActive(true);
            Invoke("cesaAtaque", 0.5f);
        }



        //Ataque Hacia Abajo
        if (Input.GetKey(KeyCode.S) && Aiming == false && GameObject.FindGameObjectWithTag("Ataque") == false && Dashing == false && IsGrounded == false)
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && Dashing == false)
        {
            Dashing = true;

            rb.AddForce(transform.right * FuerzaDash, ForceMode2D.Impulse);

            StartCoroutine(FinDashCrt());

        }
        #endregion
    }
    
    
    IEnumerator FinDashCrt()
    {
        yield return new WaitForSeconds(TiempoDash);
        Dashing = false;
    }

    
    
    private void FixedUpdate()
    {
        if (Aiming == false && Dashing == false)
        {
            
            //modificador de velocidad en el eje X y dejarla igual en el eje Y
            rb.velocity = new Vector2(input * Movespeed, rb.velocity.y);
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
        Atacking = false;
        MeleeAttackRange.gameObject.SetActive(false);
        MeleeAttackRangeDown.gameObject.SetActive(false);
        MeleeAttackRangeCharged.gameObject.SetActive(false);
        rb.transform.localScale = new Vector3(1, 1, 1);
    }

    //Hace visible el área de comprobación del suelo en la escena para facilitar su ajuste
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + GroundCheckPosition, GroundCheckSize);
    }
}
