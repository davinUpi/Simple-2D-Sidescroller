using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{

    [SerializeField] private GameObject collecedFX;
    [SerializeField] private PlayerData playerData;

    [SerializeField] private int score = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(playerData != null)
            playerData.AddScore(score);

        if(collecedFX != null)
            Instantiate(collecedFX, transform.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }


}
