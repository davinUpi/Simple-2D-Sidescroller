using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Played Data to store information")]
    [SerializeField] private PlayerData playerData;

    [Header("Variables relate to player's data")]
    [SerializeField] private float IFrameTime = 1f;

    [HideInInspector] public bool IFrameActive {  get; private set; }
    private IEnumerator IFrameTimer()
    {
        IFrameActive = true;
        yield return new WaitForSeconds(IFrameTime);
        IFrameActive = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        if(!IFrameActive && playerData != null)
        {
            StartCoroutine(IFrameTimer());
            playerData.AddHealth(-damage);
        }
    }
}
