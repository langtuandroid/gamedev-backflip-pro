using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{

    [Header(" Sounds ")]
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource wooshSound;
    [SerializeField] private AudioSource flipSound;

    private void Awake()
    {
        PlayerController.OnPlayerJumped += PlayJumpSound;

        PlayerFlipManager.OnPlayerFlipped += PlayFlipSound;
        PlayerFlipManager.OnPlayerHalfFlipped += PlayWooshSound;
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerJumped -= PlayJumpSound;

        PlayerFlipManager.OnPlayerFlipped -= PlayFlipSound;
        PlayerFlipManager.OnPlayerHalfFlipped -= PlayWooshSound;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayJumpSound()
    {
        jumpSound.pitch = Random.Range(.9f, 1.1f);
        jumpSound.Play();
    }

    public void PlayWooshSound()
    {
        wooshSound.pitch = Random.Range(.9f, 1.1f);
        wooshSound.Play();
    }

    private void PlayFlipSound()
    {
        flipSound.Play();
    }
}
