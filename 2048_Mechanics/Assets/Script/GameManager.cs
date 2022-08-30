using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public Sprite[] AllBallSprite;
    [SerializeField] TextMeshProUGUI BallAmount;
    [SerializeField] GameObject[] Balls;
    [SerializeField] GameObject BallShotPlatform;
    [SerializeField] GameObject BallShotPoint;
    int CurrentBall;

    private void Start()
    {
        BallAmount.text = Balls.Length.ToString();
        SetBall();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider!=null)
            {
                if (hit.collider.gameObject.CompareTag("Platform"))
                {
                    Vector2 MosuePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    BallShotPlatform.transform.position = Vector2.MoveTowards(BallShotPlatform.transform.position, new Vector2(MosuePos.x, BallShotPlatform.transform.position.y), 30 * Time.deltaTime);
                }
            }
        }
    }

    void SetBall()
    {
        Balls[CurrentBall].gameObject.transform.SetParent(BallShotPlatform.transform);
        Balls[CurrentBall].transform.position = BallShotPoint.transform.position;
        Balls[CurrentBall].SetActive(true);
    }
}
