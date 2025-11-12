using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchingpad: MonoBehaviour
{
    public float JF = 20f;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player balsa))
        {
            balsa.rB.AddForce(Vector3.up * JF, ForceMode.Impulse);
        }
    }
}
