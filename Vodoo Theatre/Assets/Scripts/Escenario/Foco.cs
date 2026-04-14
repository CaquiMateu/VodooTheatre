using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Foco : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    public UnityEvent OnPlayerExit;
    public SpriteRenderer playerRenderer;
    public Color colororiginal;
    public Color coloroscuridad;


    // Start is called before the first frame update
    void Start()
    {
        colororiginal = playerRenderer.color;
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
            EnLuz();
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        
           EnOscuridad();
        
    }
    public void EnOscuridad()
    {
        playerRenderer.color = coloroscuridad;
    }
   public void EnLuz()
    {
        playerRenderer.color = colororiginal;
    }
}
