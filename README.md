# Unity-SoundManager
A free SoundManager for 2D Unity projects.

This class will spawn and reuse AudioSources. All you need to do is have it in your scene
and call its static method to play sounds. No hassle!

## How to use
1) Simply attach this script to any GameObject in your scene (e.g the Camera)
2) Create a folder in your Assets root folder called "Sounds" (i.e Assets/Sounds)
3) Add the sound file into said folder and name it appropriately (i.e "CoinSound")
4) In other scripts, call the static method via SoundManager.Play("CoinSound")

