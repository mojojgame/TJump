using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public static float platformSpeed = 0;
    public static PlatformMovement script;
    Transform me;
    private void Awake()
    {
        script = this;
        me = transform;
    }
    void Update()
    {

        me.Translate(-Vector3.forward * Time.deltaTime * platformSpeed);
        if (me.position.z < 1.5)
            {
                Destroy(this.gameObject);
            }
    }
}
