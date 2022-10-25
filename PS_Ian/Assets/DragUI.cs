using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragUI : EventTrigger
{
    bool isDragging;
    GameObject current;
    [SerializeField] GameObject buttonPrefab;
    string text;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<Text>().text;
    }

    void Update()
    {
        if (isDragging)
        {
            current.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        current = Instantiate(Resources.Load("DraggableButton") as GameObject);
        current.transform.SetParent(transform.parent.transform.parent, false);
        current.transform.GetChild(0).GetComponent<InputField>().text = text;
        isDragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}
