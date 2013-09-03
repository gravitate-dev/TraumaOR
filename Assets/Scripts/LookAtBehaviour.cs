using UnityEngine;
using System.Collections;

//Decompile by Si Borokokok

public class LookAtBehaviour : MonoBehaviour
{
    public Transform Target;


    private void Update()
    {
        if (Target != null)
        {
            transform.LookAt(Target);
        }
    }
}


