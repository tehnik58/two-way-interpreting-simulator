using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField]
    public GameObject gm;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        TargetStaticController.Add(this);
    }
    ~LookAt()
    {
        TargetStaticController.Remove(this);
    }

    public void SetObjectTarget(GameObject _gm)
    {
        gm = _gm != gameObject? _gm : gm;
        Vector2 _gmPosition = new Vector2((this.transform.position - gm.transform.position).x, (this.transform.position - gm.transform.position).z).normalized;
        Vector2 _forward = new Vector2(this.transform.right.x, this.transform.right.z);

        float cosA = Vector3.Dot(_gmPosition, _forward)/(Vector3.Magnitude(_gmPosition) * Vector3.Magnitude(-_forward));
        animator.SetFloat("Blend", ((Mathf.Acos(cosA) * Mathf.Rad2Deg - 90.0f) / 45.0f + 1f) / 2f);
    }
}
