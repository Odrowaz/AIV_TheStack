using System;
using UnityEngine;

public class DragWithMouse : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 _nextPosition;
    private Rigidbody _rb;
    private float _scrollWheelValue;
    
    [SerializeField]
    private float _scrollWheelSpeed = 50f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        _scrollWheelValue = 0;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + _scrollWheelValue);
        offset = transform.position - Camera.main.ScreenToWorldPoint(position);
        _rb.isKinematic = true;
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + _scrollWheelValue);
        _nextPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(_nextPosition, Vector3.zero) > 0.1f)
        {
            _rb.MovePosition(_nextPosition);
        }
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _scrollWheelValue -= _scrollWheelSpeed * Time.deltaTime;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _scrollWheelValue += _scrollWheelSpeed * Time.deltaTime;
        }
    }

    private void OnMouseUp()
    {
        _rb.isKinematic = false;
        _nextPosition = Vector3.zero;
    }
}
