using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedFX : MonoBehaviour
{
    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
