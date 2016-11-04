using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class FixedJoystickHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler {

	[Serializable]
    public class VirtualJoystickEvent: UnityEvent<Vector3>{}
    
    public VirtualJoystickEvent _controlling;
    public UnityEvent _beginContol;
    public UnityEvent _endControl;
    public Transform _content;
    
    public void OnBeginDrag(PointerEventData eventData){
        _beginContol.Invoke();
    }
    
    public void OnDrag(PointerEventData eventData){
        _controlling.Invoke(_content.localPosition.normalized);
    }
    
    public void OnEndDrag(PointerEventData eventData){
        _endControl.Invoke();
    }
    
}
