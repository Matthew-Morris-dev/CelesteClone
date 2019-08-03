using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedClones : MonoBehaviour
{
    private SpriteRenderer myColor;

    public Color visible;
    public Color invisible;
    
    // Start is called before the first frame update
    void Start()
    {
        myColor = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        myColor.color = Color.Lerp(myColor.color, invisible, 5 * Time.deltaTime);
    }

    public void SetPosition (Vector3 position, bool facing) {
        myColor.color = visible;
        transform.position = position;
        myColor.flipX = facing;
    }
}
