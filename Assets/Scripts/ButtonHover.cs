using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource audioSource;

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1.1f,1.1f,1.1f), 0.2f).setEaseOutCirc();
        audioSource.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(this.gameObject, new Vector3(1f,1f,1f), 0.2f).setEaseInCirc();
    }
}
