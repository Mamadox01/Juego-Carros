using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlataform : MonoBehaviour
{
    [Header("Ajustes del Charco/Barro")]
    public float multiplicadorLentitud = 0.4f; // 0.4 significa que andará al 40% de su velocidad
    public float duracionEfecto = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            CarMovement carro = other.GetComponent<CarMovement>();
            if (carro != null)
            {
                // Al mandarle 0.4, la función de boost actúa al revés como un freno temporal
                carro.StartCoroutine(carro.ActivateBoost(multiplicadorLentitud, duracionEfecto));
                Debug.Log ("realentizado");
            }
        }
    }
}
