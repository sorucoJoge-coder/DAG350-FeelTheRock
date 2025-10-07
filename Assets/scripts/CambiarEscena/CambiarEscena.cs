using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SpriteClickCambiaEscena : MonoBehaviour, IPointerClickHandler
{
    public string nombreEscena;

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
