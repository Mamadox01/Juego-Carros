using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapItem : MonoBehaviour
{
    [Header("Ajustes de la Trampa")]
    public float multiplicadorLentitud = 0.3f; // 0.3 significa que andará al 30% de su velocidad original
    public float duracionCastigo = 4f; // Cuánto tiempo estará lento y con el otro modelo
    public float tiempoReaparicion = 10f; // Tiempo para que la trampa vuelva a aparecer

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private void Start()
    {
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
                // Activamos la trampa mandando la lentitud y el tiempo
                carro.StartCoroutine(carro.ActivateTrap(multiplicadorLentitud, duracionCastigo));
                
                // Escondemos el objeto para que reaparezca después
                StartCoroutine(RespawnRoutine());
            }
        }
    }

    private IEnumerator RespawnRoutine()
    {
        _meshRenderer.enabled = false;
        _collider.enabled = false;

        yield return new WaitForSeconds(tiempoReaparicion);

        _meshRenderer.enabled = true;
        _collider.enabled = true;
    }
}
