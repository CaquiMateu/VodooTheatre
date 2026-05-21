using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class GameManger : MonoBehaviour
{
   public TiendaManager TiendaManager;
   public PlayerController PlayerController;
   public SpawnEnemigos SpawnEnemigos;
   public CinemachineConfiner2D CinemachineConfiner2D;
   public Foco Foco;
   public GameObject DecoraciónCorazones;
   public Collider2D CámaraDía;
   public Collider2D CámaraNoche;
   public PlayerHealth playerHealth;
    public Light2D GlobalLight;
    private void Start()
    {
        CambioDecámara(CámaraDía);
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (playerHealth.currentHealth > 3)
        {
            DecoraciónCorazones.transform.localScale = new Vector3(4.24f, 2.31f, 0);
        }
        else
        {
            DecoraciónCorazones.transform.localScale = new Vector3(2.31f, 2.31f, 0);

        }
       
    }
    public void CambioDecámara(Collider2D NewCofinder)
    {
        if (CinemachineConfiner2D.m_BoundingShape2D != NewCofinder) 
        {
            CinemachineConfiner2D.m_BoundingShape2D = NewCofinder;
        }
    }
   

    public void CambioDeEscenarioANoche()
    {
        PlayerController.CambioDeZonaANoche(); 
        CambioDecámara(CámaraNoche);
        TiendaManager.ActivarTienda();
       
        
       
    }

    
    public void CambioDeEscenarioADia()
    {
       
        PlayerController.CambioDeZonaADia();
        
        CambioDecámara(CámaraDía);
        TiendaManager.DesactivarTienda();
        SpawnEnemigos.NumSpawnDificultad();
        SpawnEnemigos.HordaAcabada = false;
    }

    public void CambioEscenarioBoss()
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString("#521818", out color);
        GlobalLight.color = color;
    }
}
