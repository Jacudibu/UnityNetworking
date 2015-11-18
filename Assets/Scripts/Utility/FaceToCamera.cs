using UnityEngine;
using System.Collections;

public class FaceToCamera : MonoBehaviour
{
	void Update ()
    {
        transform.rotation = Camera.main.transform.rotation;
	}
}
