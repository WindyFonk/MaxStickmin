using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LaunchObject : MonoBehaviour
{

    private void OnMouseOver()
    {
        UnityEngine.Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.6f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    private void OnMouseExit()
    {
        UnityEngine.Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }


}
