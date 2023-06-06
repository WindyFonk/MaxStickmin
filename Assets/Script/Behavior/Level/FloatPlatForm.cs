using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPlatForm : MonoBehaviour
{
    [SerializeField] Lockbox lockbox;
    public float top, bottom;
    private bool up;
    void Update()
    {
        if (transform.position.y <= bottom)
        {
            up = true;
        }

        if (transform.position.y >= top)
        {
            up = false;
        }

        if (lockbox.unlocked)
        {
            if (up)
            {
                transform.Translate(new Vector3(0, Time.deltaTime * 1 * 15, 0));
            }
            else
            {
                transform.Translate(new Vector3(0, Time.deltaTime * -1 * 15, 0));
            }
        }
    }

}
