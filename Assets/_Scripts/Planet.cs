using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Planet : MonoBehaviour
{
    public int id = -1;
    public string name;
    public float Mass {
        get { return mass; }
        set {
            rigidbody.mass = value;
            mass = value;
        }
    }

    private float mass;
    private Rigidbody rigidbody;

    public void Init(float radius)
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass *= radius*2;
        rigidbody.useGravity = false;
        mass = rigidbody.mass;

        GetComponent<SphereCollider>().radius = radius;

        rigidbody.velocity = Random.insideUnitSphere.normalized * Random.Range(0,3);
        name = GenerateName(Random.Range(3,10));
    }

    public void AddForceTo(float thrust, Vector3 direction)
    {
        if (!rigidbody) return;
        //Debug.Log($"{id} planet apply force to {direction} with thrust {thrust}");
        Debug.DrawLine(transform.position, direction*5+transform.position,Color.red,0.2f);
        rigidbody.AddForce(direction * thrust);
    }

    //cite: https://stackoverflow.com/questions/14687658/random-name-generator-in-c-sharp
    private string GenerateName(int len)
    {
        System.Random r = new System.Random();
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        string Name = "";
        Name += consonants[r.Next(consonants.Length)].ToUpper();
        Name += vowels[r.Next(vowels.Length)];
        int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
        while (b < len)
        {
            Name += consonants[r.Next(consonants.Length)];
            b++;
            Name += vowels[r.Next(vowels.Length)];
            b++;
        }

        return Name;


    }


}
