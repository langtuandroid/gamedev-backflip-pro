using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private ParticleSystem confetti;
    [SerializeField] private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerCallback()
    {
        confetti.Play();
        collider.enabled = false;
    }
}
