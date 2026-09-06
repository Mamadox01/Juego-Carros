using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Puntaje del Jugador")]
    public int puntajeTotal = 0;
    public TextMeshProUGUI textoPuntaje;

    public void SumarPuntos(int cantidad)
    {
        puntajeTotal += cantidad;
        
        // Actualiza el texto en la pantalla al instante
        if (textoPuntaje != null)
        {
            textoPuntaje.text = "Puntos: " + puntajeTotal;
            Debug.Log (puntajeTotal);
        }
    }
}
