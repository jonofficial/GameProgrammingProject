using UnityEngine;

public class parallaxEffect : MonoBehaviour {
    [SerializeField] private float paraEffect;
    private float length, startPos, speed, distance;
    private Transform camera;

    [SerializeField] private protected byte CloudEffect = 0;

    private void Start() {
        camera = Camera.main.transform;
        length = this.GetComponent<SpriteRenderer>().bounds.size.x;
        startPos = transform.position.x;
    }

    private void Update() {
        float rePos = camera.transform.position.x * (1 - paraEffect);
        switch (CloudEffect) {
            case 0:
                distance = camera.transform.position.x * paraEffect;

                if (rePos > startPos + length) startPos += length;
                else if (rePos < startPos - length) startPos -= length; 
                break;
            case 1:
                distance = speed * paraEffect;        
                speed++;

                if (camera.transform.position.x > transform.position.x + length) startPos += length;
                else if (camera.transform.position.x < transform.position.x - length) startPos -= length;
                break;
        }
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}