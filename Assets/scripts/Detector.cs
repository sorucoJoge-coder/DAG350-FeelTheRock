using UnityEngine;

public class Detector : MonoBehaviour
{
    public int laneID;
    private Nota noteInTrigger = null;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Nota"))
        {
            noteInTrigger = other.GetComponent<Nota>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Nota") && other.gameObject == noteInTrigger?.gameObject)
        {
            audioManager.MissNote(laneID);
            audioManager.PlayMissSound();
            noteInTrigger = null;
        }
    }

    public bool TryHitNote()
    {
        if (noteInTrigger != null)
        {
            Nota noteToHit = noteInTrigger; 
            noteInTrigger = null; 
            
            noteToHit.Hit(); 
            
            return true;
        }
        return false;
    }
}
