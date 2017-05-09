using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPosition {

    static public Vector2 WorldToUI(RectTransform r, Vector3 pos) {
        Vector2 screenPos = Camera.main.WorldToViewportPoint(pos); 
        Vector2 viewPos = (screenPos - r.pivot) * 2; 
        float width = r.rect.width / 2; 
        float height = r.rect.height / 2;
        return new Vector2(viewPos.x * width, viewPos.y * height); 
    }

    static public Vector3 UIToWorld(RectTransform r, Vector3 uiPos) {
        float width = r.rect.width / 2; 
        float height = r.rect.height / 2; 
        Vector3 screenPos = new Vector3(((uiPos.x / width) + 1f) / 2, ((uiPos.y / height) + 1f) / 2, uiPos.z); 
        return Camera.main.ViewportToWorldPoint(screenPos);
    }

}
