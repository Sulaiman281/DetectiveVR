using UnityEngine;

public class ObjectFacingCheck : MonoBehaviour
{
    public Transform object1;
    public Transform object2;

    void LateUpdate()
    {
        var dot = Vector3.Dot(object1.forward, object2.forward);
        if(dot < -0.9)
            Debug.Log("Objects are facing towards each other");
        Debug.Log("Object 1 Facing angle: " + dot + " degrees");
    }
}