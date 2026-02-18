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

    //Ataque a distancia
    public GameObject bullet;
    public GameObject firePoint;
    public GameObject arm;
    public bool Aiming = false;

    //Ataque cuerpo a cuerpo
    public bool Atacking = false;
    
    public GameObject MeleeAttackRange;


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


        if (Input.GetMouseButtonDown(1) && moving== false)
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
        if (Input.GetMouseButtonDown(0) && Aiming == true && GameObject.FindGameObjectWithTag("Proyectil") == false)
        {
            Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        }
        #endregion
        #region Ataque cuerpo a cuerpo

        //Ataque Lateral
        if (Input.GetMouseButtonDown(0) && Aiming == false && GameObject.FindGameObjectWithTag("Ataque")==false)
        {
            Atacking = true;
            MeleeAttackRange.gameObject.SetActive(true);
            Invoke("cesaAtaque", 0.2f);
        }

        //Ataque Hacia Abajo

        #endregion
    }

    private void FixedUpdate()
    {
        if (Aiming == false)
        {
            
            //modificador de velocidad en el eje X y dejarla igual en el eje Y
            rb.velocity = new Vector2(input * Movespeed, rb.velocity.y);
        }
        else
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
    }

    //Hace visible el área de comprobación del suelo en la escena para facilitar su ajuste
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position + GroundCheckPosition, GroundCheckSize);
    }
}
