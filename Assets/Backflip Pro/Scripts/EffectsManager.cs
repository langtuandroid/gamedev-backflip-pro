using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask groundLayer;

    [Header(" Effects ")]
    [SerializeField] private ParticleSystem landingParticles;

    private void Awake()
    {
        PlayerFlipManager.OnPlayerFlipped += PlayLandingParticles;
    }

    private void OnDestroy()
    {
        PlayerFlipManager.OnPlayerFlipped -= PlayLandingParticles;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayLandingParticles()
    {
        RaycastHit hit;
        Physics.Raycast(player.position, Vector3.down, out hit, 2, groundLayer);

        if(hit.collider != null)
        {
            landingParticles.transform.position = hit.point;
            landingParticles.Play();
        }
    }
}
