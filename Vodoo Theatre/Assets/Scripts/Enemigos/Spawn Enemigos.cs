using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    [Header("Zonas")]
    public GameObject ZonaDeSpawn1;
    public GameObject ZonaDeSpawn2;

    [Header("Tipos De Enemigos")]
    public GameObject[] Enemigos;

    [Header("Valores Del Spawn")]
    public float TiempoEntreSpawn;
    public float CantidadDeEnemigosPorHorda;
    public int NumeroDeHorda; 
    public int CantidadDeEnemigosSpawneados;
    public int EnemigosEnPantalla;
    public int EnemigosBase = 8;

    public float FactorDeCrecimientoLineal = 1.5f;
    public float FactorDeCrecimientoExponencial = 0.01f;
    public float exponente = 0.5f; 

    void Start()
    {
        NumeroDeHorda = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Spawn();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            NumeroDeHorda++;
        }
    }

    void Spawn()
    {
        if (NumeroDeHorda <= 10)
        {
            CantidadDeEnemigosPorHorda = EnemigosBase + (FactorDeCrecimientoLineal * NumeroDeHorda);
            CantidadDeEnemigosPorHorda = Mathf.RoundToInt(CantidadDeEnemigosPorHorda);
        }

        else
        {
            CantidadDeEnemigosPorHorda = EnemigosBase + (FactorDeCrecimientoLineal * NumeroDeHorda) + FactorDeCrecimientoExponencial * Mathf.Pow(NumeroDeHorda, exponente);
            CantidadDeEnemigosPorHorda = Mathf.RoundToInt(CantidadDeEnemigosPorHorda);
        }
       
    }
}
