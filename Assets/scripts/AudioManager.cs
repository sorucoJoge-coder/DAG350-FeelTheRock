using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [Tooltip("Arrastra aquí los AudioSource para cada instrumento. El orden debe coincidir con las pistas en el SongChart.")]
    public List<AudioSource> instrumentSources;
    [Tooltip("El AudioSource para la música de fondo no jugable.")]
    public AudioSource backingTrackSource;
    [Tooltip("El AudioSource dedicado a los efectos de sonido cortos.")]
    public AudioSource sfxSource; // Nuevo

    [Header("Audio Clips")]
    [Tooltip("El sonido que se reproduce al fallar una nota.")]
    public AudioClip missSoundClip; // Nuevo

    [Header("Configuración de Volumen")]
    private float[] targetInstrumentVolumes;
    public float volumeFadeSpeed = 10f;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        targetInstrumentVolumes = new float[instrumentSources.Count];
    }

    private void Update()
    {
        // Suaviza el cambio de volumen de los instrumentos
        for (int i = 0; i < instrumentSources.Count; i++)
        {
            instrumentSources[i].volume = Mathf.Lerp(instrumentSources[i].volume, targetInstrumentVolumes[i], Time.deltaTime * volumeFadeSpeed);
        }
    }

    public void SetupSong(SongChart song)
    {
        backingTrackSource.clip = song.backingTrackClip;
        for (int i = 0; i < instrumentSources.Count; i++)
        {
            if (i < song.instrumentTracks.Count)
            {
                instrumentSources[i].clip = song.instrumentTracks[i];
                instrumentSources[i].Play();
                targetInstrumentVolumes[i] = 0f; // Empezamos con todos los instrumentos silenciados
            }
        }
        backingTrackSource.Play();
    }

    public void HitNote(int laneID)
    {
        if (laneID < targetInstrumentVolumes.Length)
        {
            targetInstrumentVolumes[laneID] = 1f;
        }
    }

    public void MissNote(int laneID)
    {
        if (laneID < targetInstrumentVolumes.Length)
        {
            targetInstrumentVolumes[laneID] = 0f;
        }
    }

    // --- NUEVA FUNCIÓN PARA EL SONIDO DE FALLO ---
    public void PlayMissSound()
    {
        if (sfxSource != null && missSoundClip != null)
        {
            sfxSource.PlayOneShot(missSoundClip);
        }
    }
}