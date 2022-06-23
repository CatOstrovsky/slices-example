using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoard : MonoBehaviour
{
    public SlicePlace[] slicePlaces = Array.Empty<SlicePlace>();
    public GameObject slicePrefab;
    public TextMeshProUGUI scoreText;

    private Slice currentSlice;
    private int totalScore;

    private void Start()
    {
        GenerateNewSlice();
        scoreText.text = totalScore.ToString();
    }

    private void GenerateNewSlice()
    {
        currentSlice = CreateSlice();
    }

    private Slice CreateSlice()
    {
        var slice = Instantiate(slicePrefab, transform).GetComponent<Slice>();
        var sliceKind = (SliceKind)Random.Range(0, 6);
        slice.kind = sliceKind;
        slice.score = Random.Range(1, 20);
        slice.ApplyKind();
        slice.transform.position = Vector3.zero;

        return slice;
    }

    public void OnTapSlicePlace(SlicePlace slicePlace)
    {
        if (slicePlace.slices[currentSlice.kind] == null)
        {
            slicePlace.slices[currentSlice.kind] = currentSlice;
            var slicePlaceTransform = slicePlace.transform;
            currentSlice.transform.SetParent(slicePlaceTransform);
            currentSlice.transform.DOMove(slicePlaceTransform.position, 1f)
                .OnComplete(() =>
                {
                    CheckIfWin();
                    GenerateNewSlice();
                });
        }
    }

    private void CheckIfWin()
    {
        for (int i = 0; i < slicePlaces.Length; i++)
        {
            var slicePlace = slicePlaces[i];
            var win = true;
            var score = 0;

            foreach (var sliceKvp in slicePlace.slices)
            {
                if (sliceKvp.Value == null)
                {
                    win = false;
                }
                else
                {
                    score += sliceKvp.Value.score;
                }
            }

            if (win)
            {
                totalScore += score;
                scoreText.text = totalScore.ToString();
                slicePlace.Clear();
            }
        }
    }
}