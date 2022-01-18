using UnityEngine.EventSystems;

public class UIMenuEvents : EventTrigger
{
    public void OnPointerEnter()
    {
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    public void OnPointerClick()
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
