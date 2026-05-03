using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cinemachine;

public class GameManger : MonoBehaviour
{
   public TiendaManager TiendaManager;
   public PlayerController PlayerController;
   public SpawnEnemigos SpawnEnemigos;
   public CinemachineConfiner2D CinemachineConfiner2D;
   public Foco Foco;
   public Collider2D CámaraDía;
   public Collider2D CámaraNoche;
    private void Start()
    {
        CambioDecámara(CámaraDía);
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
        Foco.EnOscuridad();
        CambioDecámara(CámaraNoche);
        TiendaManager.ActivarTienda();
    }

    
    public void CambioDeEscenarioADia()
    {
       
        PlayerController.CambioDeZonaADia();
        Foco.EnLuz();
        CambioDecámara(CámaraDía);
        SpawnEnemigos.NumSpawnDificultad();
        SpawnEnemigos.HordaAcabada = false;
    }
}
