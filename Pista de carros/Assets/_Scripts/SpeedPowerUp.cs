using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float multiplicadorVelocidad = 2f; 
    public float duracionEfecto = 3f; 
    public float tiempoReaparicion = 10f; // Segundos que tarda en volver a salir la caja

    // Variables para guardar los componentes de la caja
    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private void Start()
    {
        // Atrapamos los componentes al iniciar el juego
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            CarMovement carro = other.GetComponent<CarMovement>();

            if (carro != null)
            {
                // Le damos el boost de velocidad al carro
                carro.StartCoroutine(carro.ActivateBoost(multiplicadorVelocidad, duracionEfecto));
                
                // Iniciamos el proceso de esconder y reaparecer esta caja
                StartCoroutine(RespawnRoutine());
            }
        }
    }

    private IEnumerator RespawnRoutine()
    {
        // 1. Apagamos la malla (se vuelve invisible) y el collider (se vuelve intocable)
        _meshRenderer.enabled = false;
        _collider.enabled = false;

        // 2. El script espera en silencio el tiempo configurado
        yield return new WaitForSeconds(tiempoReaparicion);

        // 3. Volvemos a prender todo para la siguiente vuelta
        _meshRenderer.enabled = true;
        _collider.enabled = true;
    }
}
