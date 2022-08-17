using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Planet : MonoBehaviour
{
    public int id = -1;
    public float Mass {
        get { return mass; }
        set {
            rigidbody.mass = value;
            mass = value;
        }
    }

    private float mass;
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass *= Random.Range(1f,2f);
        mass = rigidbody.mass;
        rigidbody.velocity = Random.insideUnitSphere.normalized * Random.Range(0,3);
        transform.localScale *= mass;
    }

    public void AddForceTo(float thrust, Vector3 direction)
    {
        Debug.Log($"{id} planet apply force to {direction} with thrust {thrust}");
        Debug.DrawLine(transform.position, direction,Color.red,0.2f);
        rigidbody.AddForce(direction * thrust);
    }


}
