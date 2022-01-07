using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundFx : ScriptableObject
{
    [SerializeField]
    private List<AudioClip> _audioClips;

    public List<AudioClip> AudioClips { get; set; }
}