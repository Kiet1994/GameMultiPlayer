using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class PlayerControler : NetworkBehaviour
{
    [SerializeField] private float speed = 3f   ;
    private Camera _mainCamera;
    private Vector3 _mouseInput = Vector3.zero;

    private void Initialize() // Khởi tạo
    {
        _mainCamera = Camera.main;
    }
    public override void OnNetworkSpawn()// sử dụng giống awake
    {
        base.OnNetworkSpawn();
        Initialize();
    }

    void Update()
    {
        if (!IsOwner || !Application.isFocused) return; // nếu không phải chủ của scprit chứa gameobject và ở ngoài vùng game sẽ không hoạt động
        //movement
        _mouseInput.x = Input.mousePosition.x;
        _mouseInput.y = Input.mousePosition.y;
        _mouseInput.z = _mainCamera.nearClipPlane;
        Vector3 mouseWorldCoordinates = _mainCamera.ScreenToWorldPoint(_mouseInput); //toạ độ thế giới
        transform.position = Vector3.MoveTowards(transform.position, mouseWorldCoordinates, Time.deltaTime * speed);
        //rotate
         if (mouseWorldCoordinates != transform.position)
        {
            Vector3 targetDirection = mouseWorldCoordinates - transform.position;
            targetDirection.z = 0;
            transform.up = targetDirection;
        }
    }
}
