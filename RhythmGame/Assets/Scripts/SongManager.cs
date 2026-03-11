using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
     public AudioSource audioSource;      // AudioSource that plays the song
    public List<AudioClip> songs;        // List of songs to switch between
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
        audioSource.clip = songs[index];
        audioSource.Play();

        // Reset notes for the new song
        if (noteSpawner != null)
            noteSpawner.ResetNotes();
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
