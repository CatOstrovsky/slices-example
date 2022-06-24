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
    public Score score;

    private Slice currentSlice;

    private void Start()
    {
        GenerateNewSlice();
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
        slice.transform.localScale = Vector3.zero;
        slice.transform.DOScale(Vector3.one, .25f);

        return slice;
    }

    public void OnTapSlicePlace(SlicePlace slicePlace)
    {
        if (slicePlace.Slices[currentSlice.kind] == null)
        {
            slicePlace.Slices[currentSlice.kind] = currentSlice;
            var slicePlaceTransform = slicePlace.transform;
            currentSlice.transform.SetParent(slicePlaceTransform);
            currentSlice.MoveToSlicePlace(slicePlace)
                .OnComplete(() =>
                {
                    CheckIfWin();
                    GenerateNewSlice();
                });
        }
    }

    private void CheckIfWin()
    {
        foreach (var slicePlace in slicePlaces)
        {
            if (slicePlace.IsWin())
            {
                var totalScore = slicePlace.GetTotalScore();
                score.AddScore(totalScore);
                slicePlace.Clear();
            }
        }
    }
}