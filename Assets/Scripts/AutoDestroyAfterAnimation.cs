using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAfterAnimation : MonoBehaviour
{
    [SerializeField] float _lifeTime = 1;
    [SerializeField] bool _isAnimator;

    void Start()
    {
        if (_isAnimator)
            _lifeTime += GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, _lifeTime);
    }

}
