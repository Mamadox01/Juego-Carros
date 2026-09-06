using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Ruta del NPC")]
    public Transform rutaPadre; // Aquí arrastraremos el objeto principal
    private Transform[] waypoints; // Ahora es privado, el código lo llena solo
    private int waypointActual = 0;

    private CarMovement _carMovement;

    void Start()
    {
        _carMovement = GetComponent<CarMovement>();

        // Preparamos el arreglo con la cantidad exacta de hijos que tenga la ruta
        waypoints = new Transform[rutaPadre.childCount];
        
        // Recorremos cada hijo y lo guardamos en la lista en orden
        for (int i = 0; i < rutaPadre.childCount; i++)
        {
            waypoints[i] = rutaPadre.GetChild(i);
        }
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Buscar hacia dónde está el siguiente punto
        Transform objetivo = waypoints[waypointActual];
        Vector3 direccionAlObjetivo = objetivo.position - transform.position;

        // Calcular el ángulo para girar el volante
        float anguloAlObjetivo = Vector3.SignedAngle(transform.forward, direccionAlObjetivo, Vector3.up);
        
        // Convertimos ese ángulo a un valor entre -1 (Izquierda) y 1 (Derecha)
        _carMovement.inputGiro = Mathf.Clamp(anguloAlObjetivo / 45f, -1f, 1f);

        // El NPC siempre tiene el acelerador a fondo (1)
        _carMovement.inputAceleracion = 1f;

        // Si estamos a menos de 10 metros del waypoint, pasar al siguiente
        if (Vector3.Distance(transform.position, objetivo.position) < 10f)
        {
            waypointActual++;
            // Si llega al final de los puntos, vuelve a empezar (circuito)
            if (waypointActual >= waypoints.Length)
            {
                waypointActual = 0;
            }
        }
    }
}
