using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //Identification by TAG
        if (collision.gameObject.CompareTag("Recolor"))
        {
            collision.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Random.ColorHSV();
        }

        //Identification by LAYER
        if (collision.gameObject.layer == LayerMask.NameToLayer("ennemi"))
        {
            Destroy(collision.gameObject);
        }

        //Identification by NAME
        if (collision.gameObject.name.Contains("Cube"))
        {
            Rigidbody rb = collision.rigidbody;
            if (rb) rb.AddForce(Vector3.up * 20, ForceMode.Impulse);

        }

        //Identification by COMPONENT
        if (collision.gameObject.GetComponent<Ennemy>())
        {
            Destroy(collision.gameObject);
        }

        //Identification by INTERFACE
        Ipropel propel = collision.gameObject.GetComponent<Ipropel>();
        if (propel != null)
        {
            propel.Propel();
        }
    }
}
