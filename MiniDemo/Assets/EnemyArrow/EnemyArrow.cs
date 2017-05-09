using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {
    public RectTransform arrowCanvas;
    public UnityEngine.UI.Image arrow;

    public Renderer enemy;
    
    void Update() {
        UpdateArrow(arrowCanvas, enemy.transform.position, arrow);
        arrow.enabled = !enemy.isVisible;
    }
    
    private void UpdateArrow(RectTransform r, Vector3 enemyWorldPos, UnityEngine.UI.Image arrow) {
        Vector2 enemyUIPos = UIPosition.WorldToUI(r, enemyWorldPos); 

        float width = r.rect.width / 2;
        float height = r.rect.height / 2;
        Vector2 uiPos = new Vector2(Mathf.Clamp(enemyUIPos.x, -width, width), Mathf.Clamp(enemyUIPos.y, -height, height));

        arrow.rectTransform.localPosition = uiPos; 
        float angle = Mathf.Atan2(uiPos.y, uiPos.x) * Mathf.Rad2Deg;
        arrow.rectTransform.localEulerAngles = new Vector3(0, 0, angle - 90); 
    }

}
