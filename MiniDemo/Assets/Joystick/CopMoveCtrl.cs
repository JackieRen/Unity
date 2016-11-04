using UnityEngine;
using System.Collections;
using System;

public class CopMoveCtrl : MonoBehaviour {
    
    [Serializable]
    public class AnimatorParameters{
        public string _moving;
        public string _horizotal;
        public string _vertical;
    }
    
    public Animator _target;
    public float _speed = 1f;
    public AnimatorParameters _parameters;
    
    private Vector3 _direction;
    private Coroutine _cououtine;
    
    //Joysitck _beginContol invoke
    public void BeginMove(){
        _target.SetBool(_parameters._moving, true);
        _cououtine = StartCoroutine(Move());
    }
    //Joysitck _endControl invoke
    public void EndMove(){
        _target.SetBool(_parameters._moving, false);
        StopCoroutine(_cououtine);
    }
    //Joysitck _controlling invoke
    public void UpdateDirection(Vector3 direction){
        _direction = direction;
    }
    
    private IEnumerator Move(){
        while(true){
            _target.transform.position += _direction * Time.deltaTime * _speed;
            _target.SetFloat(_parameters._horizotal, _direction.x);
            _target.SetFloat(_parameters._vertical, _direction.y);
            yield return null;
        }
    }
	
}
