using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class InteractTrigger : MonoBehaviour
{
    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerExit;
    public UnityEvent Charla;
    public TextMeshProUGUI EscritorDeLaCharla;
    public TextMeshProUGUI InteractableText;
    public UnityEvent PararDeHablar;
    public string[] TiposCharla;
    public bool EnZona = false;
    public bool hablando = false;

    private void Start()
    {
        PararDeHablar.Invoke();
        EnZona = false;
    }

    private void Update()
    {
        if (EnZona == true && Input.GetKeyDown(KeyCode.E) && hablando == false)
        {
            Debug.Log("Hablando");
            Charla.Invoke();
            Charlar();
            hablando = true;
        }

        else if (hablando==true && Input.GetKeyDown(KeyCode.E))
        {
            PararDeHablar.Invoke();
            hablando = false;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTriggerEnter.Invoke();
            EnZona = true;
        }
       
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit.Invoke();
        EnZona = false;
    }

    public void Charlar()
    {
        int Índice = Random.Range(0, TiposCharla.Length);
        EscritorDeLaCharla.text = TiposCharla[Índice];
    }

   
}
