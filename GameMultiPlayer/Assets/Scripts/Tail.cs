using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public Transform networkedOwner;
    public Transform followTransform;

    [SerializeField] private float delayTime = 0.5f;
    [SerializeField] private float distance = 0.3f;
    [SerializeField] private float moveStep = 10f;

    private Vector3 _targetPosition;
    private void Update()
    {
        if(followTransform.position == _targetPosition) return;

        _targetPosition = followTransform.position - followTransform.forward * distance; //khi đầu di chuyển sẽ tạo ra 1 điểm sau đầu
        //Debug.Log(followTransform.position + " 1 "+ _targetPosition);
        _targetPosition += (transform.position - _targetPosition) * delayTime; // vị trí đuôi hiện tại - vị trí điểm mà đầu tạo ra
        //Debug.Log(followTransform.position + " 2 " + _targetPosition);
        _targetPosition.z = 0f;
        
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * moveStep);

    }

}
