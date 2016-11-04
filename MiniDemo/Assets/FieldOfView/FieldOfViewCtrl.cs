using UnityEngine;
using System.Collections;

public class FieldOfViewCtrl : MonoBehaviour
{

    public float _moveSpeed = 6;

    private Rigidbody rigidbody_ = null;
    private Camera viewCamera_ = null;
    private Vector3 velocity_ = Vector3.zero;

    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
        viewCamera_ = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = viewCamera_.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera_.transform.position.y));
        this.transform.LookAt(mousePos + Vector3.up * transform.position.y);
        velocity_ = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * _moveSpeed;
    }

    void FixedUpdate()
    {
        rigidbody_.MovePosition(rigidbody_.position + velocity_ * Time.fixedDeltaTime);
    }

}
