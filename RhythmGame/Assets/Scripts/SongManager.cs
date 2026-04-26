using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SongData
{
    public AudioClip clip;
    public float bpm;
    public float startTime = 0f;
    public float endTime = 180f; // Cut off songs at 3 minutes
}
public class SongManager : MonoBehaviour
{
    public AudioSource audioSource;      // AudioSource that plays the song
    public static List<SongData> songs = new List<SongData>();// List of songs to switch between
    public static int currentSongIndex = 0;     // Tracks which song is playing
    public NoteSpawner noteSpawner;      // Reference to your NoteSpawner
    public AudioClip stage1;
    public AudioClip stage2;
    public AudioClip stage3;
    public AudioClip stage4;
    public static bool isMenu = true;
    private bool songEnded = false;

    public CalculateScore calculateScore;

    void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (songs.Count == 0)
        {
            SongData newSong1 = new SongData();
            newSong1.clip = stage1;
            newSong1.bpm = 75;
            songs.Add(newSong1);
            SongData newSong2 = new SongData();
            newSong2.clip = stage2;
            newSong2.bpm = 130;
            songs.Add(newSong2);
            SongData newSong3 = new SongData();
            newSong3.clip = stage3;
            newSong3.bpm = 150;
            songs.Add(newSong3);
            SongData newSong4 = new SongData();
            newSong4.clip = stage4;
            newSong4.bpm = 150;
            songs.Add(newSong4);
        }
        isMenu = SceneManager.GetActiveScene().name == "Main Menu";
        if (!isMenu)
        {
            Debug.Log("Playing song from song manager");
            PlaySong(currentSongIndex);
        }
    }

    public void PlaySong(int index)
    {
        if (index < 0 || index >= songs.Count) return;
        Debug.Log("The playing song's index is " + index);
        
        SongData song = songs[index];
        
        currentSongIndex = index;
        audioSource.Stop();
        audioSource.clip = songs[index].clip;
        audioSource.Play();

        // Reset notes for the new song
        if (noteSpawner != null)
        {
            noteSpawner.SetBPM(songs[index].bpm);
            noteSpawner.ResetNotes();
        }

        if(song.endTime > song.startTime)
        {
            StartCoroutine(StopSongAtTime(song.endTime));
        }
    }


    public void setSongIndex(int index)
    {
        currentSongIndex = index;
        Debug.Log("Song Index Set to " + index);
    }

    public void setIsMenu(bool boo)
    {
        Debug.Log("IsMenuSet");
        isMenu = boo;
    }

    private IEnumerator StopSongAtTime(float endTime)
    {
        Debug.Log("Coroutine Started");
        while(audioSource.time < endTime && audioSource.isPlaying)
        {
            yield return null;
        }

        if(!songEnded)
        {
            Debug.Log("Coroutine Ended");
            songEnded = true;
            audioSource.Stop();
            calculateScore.OnSongEnd();
        }
    }
    


}
