using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameManger GameManger;
    [Header("Zonas")]
    public GameObject[] ZonasDeSpawn;

    [Header("Tipos De Enemigos")]
    public GameObject[] Enemigos;

    [Header("Valores Del Spawn")]
    public float TiempoEntreSpawn;
    public float CantidadDeEnemigosPorHorda; //Enemigos que tienen que aparecer en el nivel
    public int NumeroDeHorda; //El nivel en el que se encuentra el jugador
    public int CantidadDeEnemigosSpawneados; //Recuento de enemigos spwneados en el nivel, en caso de ser igual a CantidadDeEnemigosPorHorda, llama a la función Spawn
    public int EnemigosEnPantalla; //Te dice cuantos ennemigos hay actualmente vivos en pantalla
    public int EnemigosBase = 8; //Número de enemigos base
    public int NumAleatorioEnemigosOleada;
    public int[] NumDeEnemigosGenerados;
    int Tipo;
    Vector3 Zona;
    public bool HordaAcabada = false;
    bool Antibucle = false;

    public float FactorDeCrecimientoLineal = 1.5f;
    public float FactorDeCrecimientoExponencial = 0.01f;
    public float exponente = 0.5f; 
    bool BossFight= false;

    void Start()
    {
        NumeroDeHorda = 1;
        NumSpawnDificultad();
    }
    void Update()
    {
      

        EnemigosEnPantalla = GameObject.FindGameObjectsWithTag("Enemigo").Length;
        if (EnemigosEnPantalla == 0 && HordaAcabada == false)
        {
            DecisiónNumAleatorio();
        }
        if ( HordaAcabada== true && Antibucle == true)
        {
           Antibucle = false;
            NumeroDeHorda++;
            CantidadDeEnemigosSpawneados = 0;
            NumAleatorioEnemigosOleada = 0;
            GameManger.CambioDeEscenarioANoche();

        }
        
            
        
    }

    public void NumSpawnDificultad()
    {
        if (NumeroDeHorda == 2)
        {
            BossFight = true;
        }
        //Dependiendo de el nivel en el que se encuentre el jugador el juego establece un número de monstruos que deben de aparecer en la horda.
        if (BossFight == false) 
        {
            if (NumeroDeHorda <= 10)
            {
                CantidadDeEnemigosPorHorda = EnemigosBase + (FactorDeCrecimientoLineal * NumeroDeHorda);
                CantidadDeEnemigosPorHorda = Mathf.RoundToInt(CantidadDeEnemigosPorHorda);
            }

            else
            {
                FactorDeCrecimientoLineal = 1.5f;
                CantidadDeEnemigosPorHorda = EnemigosBase + (FactorDeCrecimientoLineal * NumeroDeHorda) + FactorDeCrecimientoExponencial * Mathf.Pow(NumeroDeHorda, exponente);
                CantidadDeEnemigosPorHorda = Mathf.RoundToInt(CantidadDeEnemigosPorHorda);
            }
        }
       
    }

    void DecisiónNumAleatorio()
    {
        if (BossFight == true)return;

        if (CantidadDeEnemigosPorHorda - CantidadDeEnemigosSpawneados == 0)
        {
            HordaAcabada = true;
            Antibucle = true;
            return;
        }
        if (CantidadDeEnemigosPorHorda - CantidadDeEnemigosSpawneados <= 8)
        {
            NumAleatorioEnemigosOleada = Mathf.RoundToInt(CantidadDeEnemigosPorHorda) - CantidadDeEnemigosSpawneados;
            CantidadDeEnemigosSpawneados += NumAleatorioEnemigosOleada;
        }

        else
        {
            NumAleatorioEnemigosOleada = UnityEngine.Random.Range(4, 9);
            CantidadDeEnemigosSpawneados += NumAleatorioEnemigosOleada;
        }

        Array.Resize(ref NumDeEnemigosGenerados, NumAleatorioEnemigosOleada);
        for (int i = 0; i < NumDeEnemigosGenerados.Length; i++)
        {

            Tipo = UnityEngine.Random.Range(0, Enemigos.Length);

            NumDeEnemigosGenerados[i]= Tipo;

            ZonaSpawn();
        }
    }

    void ZonaSpawn()
    {
        
        int NumZona = UnityEngine.Random.Range(0,ZonasDeSpawn.Length);
        Zona = ZonasDeSpawn[NumZona].transform.position;
        Spawn();
    }

    void Spawn()
    {
        int Adiciónenx = UnityEngine.Random.Range(-5, 5);
        int Adicióneny = UnityEngine.Random.Range(0, 8);
        Instantiate(Enemigos[Tipo], Zona + new Vector3(Adiciónenx, Adicióneny, 0) , Quaternion.identity);
    }
}
