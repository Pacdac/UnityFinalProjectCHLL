using UnityEngine.EventSystems;

public class UIMenuEvents : EventTrigger
{
    public void OnPointerEnter()
    {
        FindObjectOfType<AudioManager>().Play("ButtonOver");
    }

    public void OnPointerClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
