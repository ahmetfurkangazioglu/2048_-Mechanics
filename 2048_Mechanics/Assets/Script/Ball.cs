using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CurrentValueText;
    [SerializeField] int CurrentValue;
    [SerializeField] SpriteRenderer BallSprite;
    [SerializeField] ParticleSystem MergingEffect;
    [SerializeField] bool Operation;
    public  GameManager manager;

    private void Start()
    {
        CurrentValueText.text = CurrentValue.ToString();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CurrentValue.ToString()) && Operation)
        {
            collision.gameObject.SetActive(false);
            MergingEffect.gameObject.SetActive(true);
            CurrentValue *= 2;
            CurrentValueText.text = CurrentValue.ToString();
            SpriteControl(CurrentValue);
            gameObject.tag = CurrentValue.ToString();
            Operation = false;
            StartInvok();
        }
    }

    void ChangeOperationOrder()
    {
        Operation = true;
    }
    void StartInvok()
    {
        Invoke("ChangeOperationOrder", 2f);
    }
    void SpriteControl(int Value)
    {
        switch (Value)
        {
            case 4:
                BallSprite.sprite = manager.AllBallSprite[1];
                break;
            case 8:
                BallSprite.sprite = manager.AllBallSprite[2];
                break;
            case 16:
                BallSprite.sprite = manager.AllBallSprite[3];
                break;
            case 32:
                BallSprite.sprite = manager.AllBallSprite[4];
                break;
            case 64:
                BallSprite.sprite = manager.AllBallSprite[5];
                break;
            case 128:
                BallSprite.sprite = manager.AllBallSprite[6];
                break;
            case 256:
                BallSprite.sprite = manager.AllBallSprite[7];
                break;
            case 512:
                BallSprite.sprite = manager.AllBallSprite[8];
                break;
            case 1024:
                BallSprite.sprite = manager.AllBallSprite[9];
                break;
            case 2048:
                BallSprite.sprite = manager.AllBallSprite[0];
                break;
        }
    }
}
