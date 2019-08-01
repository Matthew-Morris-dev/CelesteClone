using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelesteTransition : MonoBehaviour
{
    public float speed;
    public float transitionGap;
    public float gapBeforeCamera;

    private Vector3 _startingPos;
    private Vector3 _endingPos;
    private bool _transition;

    private void Start()
    {
        _transition = false;
    }

    void Update()
    {

        if (_transition)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _endingPos, step);

            if (this.transform.position.Equals(_endingPos))
            {
                this.transform.position = _startingPos;
                _transition = false;
            }
        }
    }

    public void Transition()
    {
        SetPositions();
        _transition = true;
    }

    private void ChangeEndPoint()
    {
        _endingPos = _startingPos;
        _endingPos.x += transitionGap;
    }

    public void SetPositions()
    {
        //int currLevel = GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().curr_level;

        //if (currLevel == 0)
        //{
        //    _startingPos = this.transform.position;
        //}
        //else
        //{
        //    _startingPos = GameObject.Find("SpawnPoints").GetComponent<SpawnPoints>().GetTargets()[currLevel - 1].position;
        //}

        _startingPos = this.transform.position;
        _startingPos.x = Camera.main.transform.position.x - gapBeforeCamera;

        _endingPos = _startingPos;
        _endingPos.x += transitionGap;
    }

}
