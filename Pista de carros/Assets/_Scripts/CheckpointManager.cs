using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [Header("Configuración de Vueltas")]
    public int maxLaps = 3;         // Vueltas para ganar
    public int currentLap = 0;      // Vuelta actual
    private int nextCheckpointIndex = 0; // El checkpoint que el carro DEBE pisar ahora

    [Header("Checkpoints de la Pista")]
    // Aquí se arrastran todos los cubos invisibles en orden, desde el primero hasta la meta
    public Transform[] checkpoints; 

    private void OnTriggerEnter(Collider other)
    {
        // Validamos si lo que acabamos de atravesar tiene la etiqueta "Checkpoint"
        if (other.CompareTag("Checkpoint"))
        {
            // Buscamos qué número de checkpoint es dentro de nuestra lista
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (other.transform == checkpoints[i])
                {
                    // Si el checkpoint cruzado es exactamente el que tocaba...
                    if (i == nextCheckpointIndex)
                    {
                        Debug.Log("¡Checkpoint " + i + " válido!");
                        nextCheckpointIndex++; // Ahora debe buscar el siguiente
                        FallDetector detector = GetComponent<FallDetector>();
                        if (detector != null)
                        {
                            detector.ultimaPosicionCheckpoint = checkpoints[i].position;
                            detector.ultimaRotacionCheckpoint = checkpoints[i].rotation;
                        }
                        // Si el índice supera la cantidad de checkpoints, ¡completó la vuelta!
                        if (nextCheckpointIndex >= checkpoints.Length)
                        {
                            currentLap++;
                            nextCheckpointIndex = 0; // Reiniciamos la cuenta de checkpoints para la nueva vuelta
                            Debug.Log("¡Vuelta " + currentLap + " completada!");

                            // Verificamos si ya terminó la carrera
                            if (currentLap >= maxLaps)
                            {
                                Debug.Log("¡CARRERA TERMINADA! MI REY, CORONASTE.");
                                // Aquí luego se llama al Canva de UI para mostrar la pantalla de victoria
                            }
                        }
                    }
                    else if (i > nextCheckpointIndex)
                    {
                        Debug.Log("¡Hey! Te saltaste un checkpoint, devuélvete.");
                    }
                    break; // Salimos del ciclo for porque ya encontramos el checkpoint
                }
            }
        }
    }
}
