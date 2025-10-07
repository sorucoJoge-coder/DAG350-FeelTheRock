using UnityEngine;

public class Nota_Movimiento : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private float fallTime;

    private float timeElapsed = 0f;

    // Esta función es llamada por el Spawner para darle a la nota sus instrucciones.
    public void Initialize(Vector3 start, Vector3 end, float time)
    {
        startPos = start;
        endPos = end;
        fallTime = time;
    }

    void Update()
    {
        // Si fallTime es 0, no hacemos nada para evitar divisiones por cero.
        if (fallTime <= 0) return;

        // Incrementamos el tiempo transcurrido
        timeElapsed += Time.deltaTime;

        // Calculamos el progreso del viaje (un valor de 0 a 1)
        float progress = timeElapsed / fallTime;

        // Usamos Lerp (Linear Interpolation) para mover la nota suavemente.
        // Lerp es perfecto para movimientos de A a B en un tiempo determinado.
        transform.position = Vector3.Lerp(startPos, endPos, progress);

        // Opcional: Si la nota se pasa del final, la destruimos.
        // Esto es una medida de seguridad por si el detector falla.
        if (progress >= 1.1f) 
        {
            Destroy(gameObject);
        }
    }
}