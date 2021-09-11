using UnityEngine;

using UnityEngine.EventSystems;

public class TapArea : MonoBehaviour, IPointerDownHandler

{
    public AudioSource gameClip;

    public void OnPointerDown (PointerEventData eventData)

    {
        // Script by Devaldi Akbar Suryadi - 2021
        // https://github.com/devteam21
        GameManager.Instance.CollectByTap (eventData.position, transform);
        gameClip.Play();

    }

}