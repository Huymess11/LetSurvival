using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    public Texture2D cursor;

    private void Start()
    {
        Vector2 cursorPoint = new Vector2(cursor.width/2, cursor.height/2);
        Cursor.SetCursor(cursor,cursorPoint,CursorMode.Auto);
    }
}
