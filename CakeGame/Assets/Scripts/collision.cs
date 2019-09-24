using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bounce " + collision.name);
        this.transform.parent.GetComponentInChildren<Animator>().SetTrigger("bounce");
    }
}
