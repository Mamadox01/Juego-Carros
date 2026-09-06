using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [Header("Ajustes de Moneda")]
    public int valorMoneda = 10;
    public float velocidadGiro = 150f;

    private void Update()
    {
        // Esto hace que la moneda gire sobre sí misma flotando para verse genial
        transform.Rotate(0, velocidadGiro * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo el jugador suma puntos (ignoramos a los NPCs)
        if (other.CompareTag("Player"))
        {
            ScoreManager manager = other.GetComponent<ScoreManager>();
            
            if (manager != null)
            {
                manager.SumarPuntos(valorMoneda);
                Debug.Log ("Puntuacion agregada");
                Destroy(gameObject); // Desaparece la moneda al agarrarla
            }
        }
    }
}
