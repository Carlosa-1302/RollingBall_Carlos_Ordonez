using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CofreIsometrica : MonoBehaviour
{
    private Outline outline;
    [SerializeField] private Texture2D iconoInteraccion;
    [SerializeField] private Texture2D iconoPorDefecto;
    // Start is called before the first frame update
    void Start()
    {
       outline = GetComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(iconoInteraccion, Vector2.zero, CursorMode.Auto);
        outline.enabled = true;
        
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(iconoPorDefecto, Vector2.zero, CursorMode.Auto);
        outline.enabled = false;
    }
}
