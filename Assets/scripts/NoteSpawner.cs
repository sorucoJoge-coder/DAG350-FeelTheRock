using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("Configuración del Chart")]
    public SongChart currentSong;
    [Tooltip("El tiempo en segundos que tarda una nota en ir desde el spawn hasta el detector.")]
    public float fallTime = 2f; // Puedes ajustar esto para cambiar la velocidad de las notas

    [Header("Referencias de Carriles")]
    public GameObject[] notePrefabs; // Un prefab por carril
    public Transform[] laneStartPositions;
    public Transform[] laneEndPositions;

    private List<SongChart.NoteData> notes;
    private int nextNoteIndex = 0;
    private float startTime = 0;

    void Start()
    {
        InitializeSong();
    }

    void InitializeSong()
    {
        if (currentSong == null)
        {
            Debug.LogError("ERROR: No hay un SongChart asignado en el NoteSpawner.");
            return;
        }

        notes = new List<SongChart.NoteData>(currentSong.notes);
        // Opcional: Ordenar las notas por tiempo por si no lo están en el editor.
        notes.Sort((a, b) => a.spawnTime.CompareTo(b.spawnTime));

        startTime = (float)AudioSettings.dspTime;
    }

    void Update()
    {
        if (nextNoteIndex >= notes.Count)
        {
            // No hay más notas que spawnear.
            return;
        }

        float songTime = (float)AudioSettings.dspTime - startTime;

        // Si el tiempo de la canción ha alcanzado el tiempo de spawn de la siguiente nota...
        if (songTime >= notes[nextNoteIndex].spawnTime)
        {
            SpawnNote(notes[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    void SpawnNote(SongChart.NoteData noteData)
    {
        int laneId = noteData.laneID;

        // Validaciones para evitar errores
        if (laneId < 0 || laneId >= notePrefabs.Length)
        {
            Debug.LogError($"ERROR: Lane ID ({laneId}) fuera de rango.");
            return;
        }

        GameObject notePrefab = notePrefabs[laneId];
        Transform startPos = laneStartPositions[laneId];
        Transform endPos = laneEndPositions[laneId];

        // Instanciamos la nota
        GameObject noteObject = Instantiate(notePrefab, startPos.position, Quaternion.identity);
        
        // Le pasamos la información de movimiento
        Nota_Movimiento noteMovement = noteObject.GetComponent<Nota_Movimiento>();
        if (noteMovement != null)
        {
            noteMovement.Initialize(startPos.position, endPos.position, fallTime);
        }
        else
        {
            Debug.LogError($"ERROR: El prefab de nota para el carril {laneId} no tiene el script 'Nota_Movimiento'.");
        }
    }
}
