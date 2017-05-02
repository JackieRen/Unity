using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTextOnEditor : MonoBehaviour {

    private void OnDrawGizmos() {
        DrawText(transform.position, transform.name);
    }

    public void DrawText(Vector3 pos, string text) {
        Vector3 scrn_pos = Camera.current.WorldToScreenPoint(pos);
        if (scrn_pos.z < 0f)
            return;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = (System.Array.IndexOf(UnityEditor.Selection.objects, this.gameObject) != -1) ? 16 : 8;
        scrn_pos += new Vector3(2, -2, 2);
        UnityEditor.Handles.Label(Camera.current.ScreenToWorldPoint(scrn_pos), text, style);
        style.normal.textColor = Color.white;
        UnityEditor.Handles.Label(pos, text, style);
    }

}
