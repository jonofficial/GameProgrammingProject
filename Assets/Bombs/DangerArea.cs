using UnityEngine;

public class DangerArea : MonoBehaviour {

    [SerializeField] private protected GameObject bombPreFab;
    [SerializeField] private protected Transform[] areaLimit;

    private protected GameObject playerObject;
    private protected Vector2 playerPostion;

    [SerializeField] private protected float timeToSpawn;
    private protected float timer = 0;

    [SerializeField] private protected float maxSpawnBombDistance;

    void Start() {
        playerObject = GameObject.Find("Player");
    }

    void Update() {
        playerPostion = playerObject.transform.position;

        if (playerPostion.x > areaLimit[0].position.x && playerPostion.x < areaLimit[1].position.x) {
            if (timer >= timeToSpawn) {
                for (int i = 0; i < 1; i++)
                    Instantiate(bombPreFab, new Vector2(Random.Range(playerPostion.x - maxSpawnBombDistance, playerPostion.x + maxSpawnBombDistance), playerPostion.y + 15 + Mathf.Pow(i, i)), transform.rotation);

                timer = 0;
            }
            else timer += Time.deltaTime;
        }
        else timer = 0;
    }
}