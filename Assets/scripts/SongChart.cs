using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NuevoMapaDeCancion", menuName = "Juego de Ritmo/Nuevo Mapa de Canción")]
public class SongChart : ScriptableObject
{
    [Header("Archivos de Audio")]
    public AudioClip backingTrackClip;
    public List<AudioClip> instrumentTracks;

    // Esta es la "plantilla" de la nota que el NoteSpawner necesita.
    [System.Serializable]
    public class NoteData
    {
        public float spawnTime;
        public int laneID;
        // La duración ya no es necesaria.
    }

    [Header("Mapa de Notas")]
    public List<NoteData> notes;
}
