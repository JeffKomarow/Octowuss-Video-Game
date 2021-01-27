using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAccountCreatedScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(RemoveAfterSeconds(1, gameObject));
    }
    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }

}
