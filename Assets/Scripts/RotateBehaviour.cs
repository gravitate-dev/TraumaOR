using UnityEngine;
using System.Collections;

//Decompile by Si Borokokok

public class RotateBehaviour : MonoBehaviour
{
    public Vector3 RotationAmount;


    private void Update()
    {
        transform.Rotate((Vector3) (RotationAmount * Time.deltaTime));
    }
}


