using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Singleton, permite acceder a este script desde otro.
    public static CameraController instance;

    // Variables necesarias.
    public Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed;
    [SerializeField] private float rotationSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        FollowTarget();
        RotateToTarget();
    }

    public void FollowTarget()
    {
        // Si hay un objetivo, se mueve la posición de la cámara de manera suave al offset dispuesto.
        if (target != null)
        {
            var targetPos = target.TransformPoint(offset);
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }

    public void RotateToTarget()
    {
        if (target != null)
        {
            var direction = target.position - transform.position;
            var rotation = Quaternion.LookRotation(direction);
        
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
