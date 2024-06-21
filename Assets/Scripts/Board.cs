using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    private Row[] rows;
    private int rowIndex;
    private int columnIndex;
    private string[] solutions;
    private string[] validWords;
    private String word;

    [Header("Tiles")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    private void Awake()
    {
        Debug.Log("Awakeeee");
        rows = GetComponentsInChildren<Row>();
    }

    private void Start()
    {
        Debug.Log("Start");
        LoadGame();
        SetRandomWord();

    }

    private void SetRandomWord()
    {
        Debug.Log("SetRandomWord");
        word = solutions[Random.Range(0, solutions.Length)];
        word = word.ToLower().Trim();
    }

    private void LoadGame()
    {
        TextAsset textFile = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFile.text.Split("\n");

        textFile = Resources.Load("official_wordle_all") as TextAsset;
        validWords = textFile.text.Split("\n");
        Debug.Log("LoadGame");
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");
        KeyInputManager.OnKeyPressed += HandleKeyPressed;
        KeyInputManager.OnBackspacePressed += HandleBackspacePressed;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisable");
        KeyInputManager.OnKeyPressed -= HandleKeyPressed;
        KeyInputManager.OnBackspacePressed -= HandleBackspacePressed;
    }

    private void HandleKeyPressed(object sender, KeyInputEventArgs e)
    {
        Debug.Log("HandleKeyPressed");
        Row currentRow = rows[rowIndex];
        if (columnIndex < currentRow.tiles.Length)
        {
            currentRow.tiles[columnIndex].SetLetter(e.KeyChar);
            currentRow.tiles[columnIndex].SetState(occupiedState);
            columnIndex++;
        }

        // Check if the row is complete and submit the row if needed
        if (columnIndex >= currentRow.tiles.Length)
        {
            Debug.Log("Condition to start coroutine met");
            StartCoroutine(SubmitRowCoroutine(currentRow));
        }
        else
        {
            Debug.Log("Condition not met, columnIndex: " + columnIndex + ", tiles.Length: " + currentRow.tiles.Length);
        }
    }

    private void HandleBackspacePressed(object sender, EventArgs e)
    {
        if (columnIndex > 0)
        {
            columnIndex--;
            rows[rowIndex].tiles[columnIndex].SetState(emptyState);
            rows[rowIndex].tiles[columnIndex].SetLetter('\0');

        }
    }

    private bool IsWordGuessed(Row row)
    {
        if (row.tiles.Length != word.Length) return false; // Ensure the lengths match

        for (int i = 0; i < row.tiles.Length; i++)
        {
            if (char.ToLower(row.tiles[i].letter) != char.ToLower(word[i])) // Normalize cases for comparison
                return false;
        }

        return true; // All tiles match the word exactly
    }

    private IEnumerator SubmitRowCoroutine(Row row)
    {
        Debug.Log("SubmitRow");
        for (int i = 0; i < row.tiles.Length; i++)
        {
            Tile tile = row.tiles[i];
            yield return new WaitForSeconds(0.1f); // Delay for effect

            Debug.Log("tile.letter == word: letter " + tile.letter + ", word: " +  word[i]);

            if (tile.letter == word[i])
            {
                tile.SetState(correctState);
                Debug.Log("word is correct ");
            }
            else if (word.Contains(tile.letter))
            {
                tile.SetState(wrongSpotState);
                Debug.Log("wrong spot ");
            }
            else
            {
                tile.SetState(incorrectState);
                Debug.Log("incorrect word");
            }
        }
        rowIndex++;
        columnIndex = 0;

        if (rowIndex >= rows.Length || IsWordGuessed(row))
        {
            Debug.Log("Implement this method to handle end of game");
        }
    }
}