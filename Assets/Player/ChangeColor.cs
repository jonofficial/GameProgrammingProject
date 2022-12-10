using UnityEngine;

public class ChangeColor : MonoBehaviour {
    [SerializeField] private protected Sprite newColor;
    byte collected = 0;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collected == 0) {
            GetComponent<SpriteRenderer>().sprite = newColor;
            collected = 1;
        }
    }
}