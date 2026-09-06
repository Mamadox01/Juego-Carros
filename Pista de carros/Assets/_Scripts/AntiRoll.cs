using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRoll : MonoBehaviour
{
    [Header("Ruedas del mismo eje")]
    public WheelCollider ruedaIzquierda;
    public WheelCollider ruedaDerecha;

    [Header("Fuerza Estabilizadora")]
    public float fuerzaAntiVuelco = 5000f; // Súbelo si el carro es muy pesado

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Variables para medir cuánto está comprimida la suspensión (1 es totalmente extendida)
        float compresionIzquierda = 1f;
        float compresionDerecha = 1f;

        // Medimos el amortiguador izquierdo
        bool tocandoIzquierda = ruedaIzquierda.GetGroundHit(out WheelHit hitIzq);
        if (tocandoIzquierda)
        {
            compresionIzquierda = (-ruedaIzquierda.transform.InverseTransformPoint(hitIzq.point).y - ruedaIzquierda.radius) / ruedaIzquierda.suspensionDistance;
        }

        // Medimos el amortiguador derecho
        bool tocandoDerecha = ruedaDerecha.GetGroundHit(out WheelHit hitDer);
        if (tocandoDerecha)
        {
            compresionDerecha = (-ruedaDerecha.transform.InverseTransformPoint(hitDer.point).y - ruedaDerecha.radius) / ruedaDerecha.suspensionDistance;
        }

        // Calculamos la diferencia de peso/compresión entre ambas ruedas
        float fuerzaAplicada = (compresionIzquierda - compresionDerecha) * fuerzaAntiVuelco;

        // Empujamos el chasis hacia abajo en el lado que se está levantando
        if (tocandoIzquierda)
        {
            _rb.AddForceAtPosition(ruedaIzquierda.transform.up * -fuerzaAplicada, ruedaIzquierda.transform.position);
        }
        
        if (tocandoDerecha)
        {
            _rb.AddForceAtPosition(ruedaDerecha.transform.up * fuerzaAplicada, ruedaDerecha.transform.position);
        }
    }
}
