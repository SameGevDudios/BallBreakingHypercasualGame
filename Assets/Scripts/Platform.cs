using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Animator _platformAnimator, _flyAwayAnimator;
    public Animator GetPlatformAnimator() => _platformAnimator;
    public Animator GetFlyAwayAnimator() => _flyAwayAnimator;
}
