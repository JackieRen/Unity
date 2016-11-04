using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropList : MonoBehaviour, IPointerEnterHandler {

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(eventData.pointerDrag != null){
			DragAndDropItem item = eventData.pointerDrag.GetComponent<DragAndDropItem>();
			if(item != null){
				item._parent = this.transform;
				item._substituteParent = this.transform;
			}
		}
	}

}
