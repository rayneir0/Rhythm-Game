using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongData
{
    public AudioClip clip;
    public float bpm;
}
public class SongManager : MonoBehaviour
{
     public AudioSource audioSource;      // AudioSource that plays the song
    public List<SongData> songs;        // List of songs to switch between
    public int currentSongIndex = 0;     // Tracks which song is playing
    public NoteSpawner noteSpawner;      // Reference to your NoteSpawner

    void Start()
    {
           if (songs.Count > 0)
            PlaySong(currentSongIndex);
    }

    public void PlaySong(int index)
    {
        if (index < 0 || index >= songs.Count) return;

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
    }


    // // Play next song in the list
    // public void NextSong()
    // {
    //     int nextIndex = (currentSongIndex + 1) % songs.Count;
    //     PlaySong(nextIndex);
    // }

    // // Play previous song
    // public void PreviousSong()
    // {
    //     int prevIndex = (currentSongIndex - 1 + songs.Count) % songs.Count;
    //     PlaySong(prevIndex);
    // }
}
