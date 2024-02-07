using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDeplacementCameraTemp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * 50 * Time.deltaTime, 0f));

        Vector3 input = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")), 1f);
    }
}
