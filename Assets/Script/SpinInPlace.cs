using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinInPlace : MonoBehaviour
{
    float speed = 45f;

	void Update ()
	{
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
	}
}
