using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ShopObj : MonoBehaviour
{
   TiendaManager tiendaManager;
    public GameObject tienda;
    public GameObject Textointeractuar;
    public GameObject ObjetoDeLaZona;
    InteractTrigger InteractTrigger;
    Collider2D collider2;
    UseObj useObj;
    public bool EnZona;
    void Start()
    {
       
       tiendaManager = tienda.GetComponent<TiendaManager>();
        collider2 = GetComponent<Collider2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (useObj == null && tiendaManager.TiendaActiva==true) 
        {
            useObj = ObjetoDeLaZona.GetComponent<UseObj>();
        }
       
        if (Input.GetKeyDown(KeyCode.E) && EnZona == true)
        {
            Debug.Log("Recibido");
            useObj.Acción.Invoke();
            Destroy(ObjetoDeLaZona);
            DesactivarCollider();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") == true)
        {
            EnZona = true;
            Textointeractuar.SetActive(true);

            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            Textointeractuar.SetActive(false);
            EnZona = false;
        }
    }

    public void DesactivarCollider()
    {
        collider2.enabled = false;
    }

    public void ActivarCollider()
    {
        collider2.enabled = true;
    }

}
