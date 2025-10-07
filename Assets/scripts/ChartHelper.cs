using UnityEngine;

// Esta es una herramienta para ayudarte a encontrar los 'hitTimes' de una canción.
// No forma parte del juego final, solo se usa en el editor.
public class ChartHelper : MonoBehaviour
{
    [Tooltip("Arrastra aquí el clip de audio que quieres mapear.")]
    public AudioClip songClip;

    private AudioSource audioSource;

    void Start()
        // Creamos un AudioSource temporal para reproducir la música.
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = songClip;
        audioSource.Play();
        Debug.Log("--- INICIANDO CHART HELPER --- \nPresiona la barra espaciadora al ritmo de la música.");
    }

    void Update()
    {
        // Cuando presionas la barra espaciadora...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ...imprimimos el tiempo actual de la canción en la consola.
            Debug.Log($"Hit Time: {audioSource.time}");
        }
    }
}
