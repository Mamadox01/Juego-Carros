using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlataform : MonoBehaviour
{
    [Header("Ajustes de la Plataforma")]
    public float multiplicadorVelocidad = 2.5f; // Qué tan fuerte es el empujón
    public float duracionEfecto = 2f; // Segundos que dura el impulso

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            CarMovement carro = other.GetComponent<CarMovement>();
            if (carro != null)
            {
                // Llama la corrutina que ya tienes sin destruir el objeto
                carro.StartCoroutine(carro.ActivateBoost(multiplicadorVelocidad, duracionEfecto));
            }
        }
    }
}
