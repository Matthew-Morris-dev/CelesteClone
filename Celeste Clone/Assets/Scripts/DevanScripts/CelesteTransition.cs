using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelesteTransition : MonoBehaviour
{
    public Vector3 target;
    public float speed;

    private Vector3 _originalPos;
    private bool _transition;

    private void Start()
    {
        _originalPos = this.transform.position;
        _transition = false;
    }

    void Update()
    {
        if (_transition)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (this.transform.position.Equals(target))
            {
                this.transform.position = _originalPos;
                _transition = false;
            }
        }
    }

    public void Transition()
    {
        _transition = true;
    }

}
