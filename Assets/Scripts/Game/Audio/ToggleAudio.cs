using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attached to a button, will allow us to toggle music/sfx
public class ToggleAudio : MonoBehaviour
{
    [SerializeField] private bool toggleMusic, toggleEffects;

    public void Toggle()
    {
        if (toggleMusic)
        {
            AudioManager.Instance.ToggleMusic();
        }
        if (toggleEffects)
        {
            AudioManager.Instance.ToggleEffects();
        }
    }
}
