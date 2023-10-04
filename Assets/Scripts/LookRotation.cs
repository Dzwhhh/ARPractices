using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookRotation : MonoBehaviour
{
    [SerializeField]
    private GameObject pivotPlanet = null;

    public float RotateSpeed = 15.0f;
    public float OrbitSpeed = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 自转
        transform.Rotate(Vector3.up, RotateSpeed * Time.deltaTime, Space.Self);

        // 公转
        if (pivotPlanet != null)
        {
            Vector3 orbitAxis;
            if (OrbitSpeed <= 10.0f)
            {
                orbitAxis = new Vector3(0, 1f, 0.5f);
            } else
            {
                orbitAxis = new Vector3(0, 1f, -0.5f);
            }

            transform.RotateAround(pivotPlanet.transform.position, orbitAxis, OrbitSpeed * Time.deltaTime);
        }
    }
}
