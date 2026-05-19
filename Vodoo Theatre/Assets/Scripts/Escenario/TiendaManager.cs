using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TiendaManager : MonoBehaviour
{
    public Transform[] Zonas;
    public GameObject[] Objetos;
    public GameObject[] SpawnTienda;
    public bool TiendaActiva = false;
    public GameObject Foco;
    Light2D SpriteFoco;
    [Range (0, 1)] public float t = 1;
    public float currentIntensity;
    public AnimationCurve curve;
    public float intensidad;

    public void Start()
    {
        SpriteFoco = Foco.GetComponent<Light2D>();
        currentIntensity = SpriteFoco.intensity;
    }
    public void ActivarTienda()
    {
        for (int i = 0; i < Zonas.Length; i++)
        {
            int tipodeobjeto = Random.Range(0, Objetos.Length);
            SpawnTienda[i] = Instantiate(Objetos[tipodeobjeto], Zonas[i].position, Quaternion.identity);
        }
        TiendaActiva = true;
    }
    public void DesactivarTienda()
    {
        for (int i = 0; i < SpawnTienda.Length; i++)
        {
            if (SpawnTienda[i] != null)
            {
                Destroy(SpawnTienda[i]);
            }
        }
        TiendaActiva = false;
    }

    private void Update()
    {
        intensidad = SpriteFoco.intensity;
        if (TiendaActiva == true)
        {
           SpriteFoco.intensity= currentIntensity;
            t = 0;


            if (GameObject.FindGameObjectsWithTag("ObjTienda").Length != 3)
            {
                DesactivarTienda();
            }
           
        }

        else
        {
            float ValorMax = currentIntensity;
            float ValorMin = 0;

            if (t < 1)
            {
                t += 1 * Time.deltaTime;
            }
            SpriteFoco.intensity = Mathf.Lerp(ValorMax, ValorMin, curve.Evaluate(t));
        }
       

    }



}
