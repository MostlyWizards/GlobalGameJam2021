using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    public InputActionAsset actions;
    public float speed;
    public Vector3 boundsMax;
    public Vector3 boundsMin;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        var map = actions.FindActionMap("Ship");
        map.Enable();
        map["MoveX"].performed += OnMoveX;
        map["MoveZ"].performed += OnMoveZ;
    }

    void MinVec3(ref Vector3 v1, Vector3 v2)
    {
        v1.x = Mathf.Min(v1.x, v2.x);
        //v1.y = Mathf.Min(v1.y, v2.y);
        v1.z = Mathf.Min(v1.z, v2.z);
    }
    void MaxVec3(ref Vector3 v1, Vector3 v2)
    {
        v1.x = Mathf.Max(v1.x, v2.x);
        //v1.y = Mathf.Max(v1.y, v2.y);
        v1.z = Mathf.Max(v1.z, v2.z);
    }

    // Update is called once per frame
    void Update()
    {
        var realSpeed = direction.x != 0 && direction.z !=0 ? speed * 0.7f : speed;
        var nextPosition = transform.position + (direction * realSpeed * Time.deltaTime);
        MinVec3(ref nextPosition, boundsMax);
        MaxVec3(ref nextPosition, boundsMin);
        transform.position = nextPosition;
    }

    public void OnMoveX(InputAction.CallbackContext context)
    {
        direction.x = context.ReadValue<float>();
    }

    public void OnMoveZ(InputAction.CallbackContext context)
    {
        direction.z = context.ReadValue<float>();
    }
}
