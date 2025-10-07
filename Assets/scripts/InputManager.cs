using UnityEngine;

public class InputManager : MonoBehaviour
{
    public KeyCode[] inputKeys;
    public Detector[] detectors;
    
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        if (audioManager == null) { Debug.LogError("ERROR: No se encontró el AudioManager en la escena."); }
    }

    void Update()
    {
        for (int i = 0; i < inputKeys.Length; i++)
        {
            if (Input.GetKeyDown(inputKeys[i]))
            {
                if (detectors[i] == null)
                {
                    Debug.LogError($"ERROR: El detector para el carril {i} no está asignado en el Inspector.");
                    continue;
                }

                bool success = detectors[i].TryHitNote();

                if (success)
                {
                    audioManager.HitNote(i);
                }
                else
                {
                    audioManager.PlayMissSound();
                    audioManager.MissNote(i);
                }
            }
        }
    }
}