using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }
}
