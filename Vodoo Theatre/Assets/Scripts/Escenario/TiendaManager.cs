using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiendaManager : MonoBehaviour
{
    public Transform[] Zonas;
    public GameObject[] Objetos;

   
   
    public void ActivarTienda()
    {
        for (int i = 0; i < Zonas.Length; i++)
        {
            int tipodeobjeto = Random.Range(0, Objetos.Length);
            Instantiate(Objetos[tipodeobjeto], Zonas[i].position, Quaternion.identity);
        }
    }
   
}
