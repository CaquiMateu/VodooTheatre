using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Cinemachine;

public class GameManger : MonoBehaviour
{
   public PlayerController PlayerController;
   public SpawnEnemigos SpawnEnemigos;
   public CinemachineConfiner2D CinemachineConfiner2D;
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
        CambioDecámara(CámaraNoche);
    }

    
    public void CambioDeEscenarioADia()
    {
       
        PlayerController.CambioDeZonaADia();
        CambioDecámara(CámaraDía);
        SpawnEnemigos.NumSpawnDificultad();
        SpawnEnemigos.HordaAcabada = false;
    }
}
