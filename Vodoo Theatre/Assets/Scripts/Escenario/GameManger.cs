using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManger : MonoBehaviour
{
   public PlayerController PlayerController;
   public SpawnEnemigos SpawnEnemigos;

   

    public void CambioDeEscenarioANoche()
    {
        PlayerController.CambioDeZonaANoche();

    }
    public void CambioDeEscenarioADia()
    {
        PlayerController.CambioDeZonaADia();
        SpawnEnemigos.NumSpawnDificultad();
        SpawnEnemigos.HordaAcabada = false;
    }
}
