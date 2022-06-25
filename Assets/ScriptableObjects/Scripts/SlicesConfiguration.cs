using System.Collections;
using System.Collections.Generic;
using Model.Slices;
using UnityEngine;

[CreateAssetMenu(fileName = "Slices Config", menuName = "ScriptableObjects/Create Slices Config", order = 1)]
public class SlicesConfiguration : ScriptableObject
{
    [Header("Slices configuration")]
    [Tooltip("Ключ - тип слайса, значение - на сколько градусов его необходимо развернуть в зависимости от позиции")]
    public List<SliceRule> rotationOnKindConfig = new List<SliceRule>();
}
