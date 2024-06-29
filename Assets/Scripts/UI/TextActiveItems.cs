using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextActiveItems : MonoBehaviour
{
    [SerializeField] private string _header;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private TMP_Text _label;
    private int _activeCubes;
    private int _activeBombs;

    private void Awake()
    {
        _label = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _cubeSpawner.ChangedActiveItems += ChangeActiveCubes;
        _bombSpawner.ChangedActiveItems += ChangeActiveBombs;
    }

    private void OnDisable()
    {
        _cubeSpawner.ChangedActiveItems -= ChangeActiveCubes;
        _bombSpawner.ChangedActiveItems -= ChangeActiveBombs;
    }

    private void ChangeActiveCubes(int activeCubes)
    {
        _activeCubes = activeCubes;

        ChangeValue();
    }

    private void ChangeActiveBombs(int activeBombs)
    {
        _activeBombs = activeBombs;

        ChangeValue();
    }

    private void ChangeValue()
    {
        _label.text = _header + (_activeCubes + _activeBombs);
    }
}
