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
    public static List<SongData> songs = new List<SongData>();        // List of songs to switch between
    public static int currentSongIndex = 0;     // Tracks which song is playing
    public NoteSpawner noteSpawner;      // Reference to your NoteSpawner
    public AudioClip stage1;
    public AudioClip stage2;
    //public AudioClip stage3;
    //public AudioClip stage4;
    public static bool isMenu = true;

    void Start()
    {
        if (songs.Count == 0)
        {
            SongData newSong = new SongData();
            newSong.clip = stage1;
            newSong.bpm = 75;
            songs.Add(newSong);
            newSong.clip = stage2;
            newSong.bpm = 130;
            songs.Add(newSong);
            /*
            newSong.clip = stage3;
            newSong.bpm = 150;
            songs.Add(newSong);
            newSong.clip = stage4;
            newSong.bpm = 150;
            songs.Add(newSong);
            */
        }
        if (!isMenu)
        {
            PlaySong(currentSongIndex);
        }
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


    public void setSongIndex(int index)
    {
        currentSongIndex = index;
    }

    public void setIsMenu(bool boo)
    {
        isMenu = boo;
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
