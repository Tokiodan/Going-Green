using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interactables : MonoBehaviour, IPointerEnterHandler
{
    public TextMeshProUGUI description;

    public string text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeDescription(text);
    }
    void ChangeDescription(string text)
    {
        description.text = text;
    }
}
