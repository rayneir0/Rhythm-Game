using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteSpawner : MonoBehaviour
{
    public NoteController notePrefab;
    public TextMeshProUGUI feedbackText;
    public CalculateScore score;
    public Transform spawnPoint;
    public Transform hitLine;
    public InputController[] laneInputs; 
    public Transform[] lanePositions;
    
    public float bpm = 120f;
    private float beatTimer;

    public int poolSize = 20;
    private List<GameObject> notePool = new List<GameObject>();
    private Dictionary<int, List<NoteController>> laneNotes = new Dictionary<int, List<NoteController>>();
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            NoteController note = Instantiate(notePrefab, spawnPoint.position, Quaternion.identity);
            note.hitLine = hitLine; // Assign hit line at the start
            note.gameObject.SetActive(false);
            notePool.Add(note.gameObject); // Add ut to the pool
        }
        for (int i = 0; i < laneInputs.Length; i++)
        {
            laneNotes[i] = new List<NoteController>();
        }
    }
    void Update()
    {
        for (int i = 0; i < laneInputs.Length; i++)
        {
            // Remove notes that are inactive
            laneNotes[i].RemoveAll(n => !n.gameObject.activeInHierarchy);

            if (laneInputs[i].getKeyDown())
            {
                NoteController closest = GetClosestNoteInLane(i); // Only check the hit for the closest note
                if (closest != null)
                    closest.CheckHitStatus();
            }
        }
        beatTimer += Time.deltaTime;

        float beatInterval = 60f / bpm; // Spawn the notes at this interval

        if (beatTimer >= beatInterval)
        {
            int laneIndex = Random.Range(0, laneInputs.Length);
            SpawnNote(laneIndex);
            beatTimer -= beatInterval;
        }
        
        // Check input per lane
        for (int i = 0; i < laneInputs.Length; i++)
        {
            if (laneInputs[i].getKeyDown())
            {
                NoteController closest = GetClosestNoteInLane(i);
                if (closest != null)
                    closest.CheckHitStatus();
            }
        }
       
    }
    void SpawnNote(int laneIndex)
    {
        GameObject noteObj = GetNoteFromPool();
        if (noteObj != null)
        {
            noteObj.transform.position = new Vector3(lanePositions[laneIndex].position.x, spawnPoint.position.y, 0f);
            NoteController note = noteObj.GetComponent<NoteController>();

            // Initializations and Assigns
            note.inputController = laneInputs[laneIndex]; // Assign which lane input this note responds to
            note.feedbackText = feedbackText; // Assign feedback text
            note.score = score; // Assign score
            note.hitLine = hitLine; // Assign hit line in case of respawn
            note.isHit = false; // Set is hit to false
            noteObj.SetActive(true); // Activate note object
            laneNotes[laneIndex].Add(note); // Add the note to the lane
            
        }
    }

    GameObject GetNoteFromPool()
    {
        foreach (GameObject note in notePool)
        {
            if (!note.activeInHierarchy)
            {
                return note;
            }
        }

        return null;
    }
    NoteController GetClosestNoteInLane(int laneIndex)
    {
        NoteController closest = null;
        float minDistance = Mathf.Infinity;

        foreach (NoteController note in laneNotes[laneIndex])
        {
            if (!note.gameObject.activeInHierarchy) continue;

            float distance = Mathf.Abs(note.transform.position.y - hitLine.position.y);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = note;
            }
        }
        return closest;
    }
    public void ResetNotes()
{
    foreach (GameObject note in notePool)
    {
        note.SetActive(false);
    }
    beatTimer = 0f;

    foreach (var lane in laneNotes.Keys)
    {
        laneNotes[lane].Clear();
    }
}
}
