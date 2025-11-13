using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float sU = 5f;
    public float sUpTime = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player hsh))
        {
            StartCoroutine(SpeedBoostRoutine(hsh));
        }
    }

    IEnumerator SpeedBoostRoutine(Player hsh)
    {
        float originalSpeed = hsh.mS;     
        hsh.mS += sU;     
        yield return new WaitForSeconds(sUpTime); 
        hsh.mS = originalSpeed;   
    }
}
