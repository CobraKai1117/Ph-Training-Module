using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasFollowCamera : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camTransform.rotation;
        transform.position = new Vector3(camTransform.position.x, camTransform.position.y, (camTransform.position.z - .15f));
    }
}
