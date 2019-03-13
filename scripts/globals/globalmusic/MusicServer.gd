extends Node

func _ready():
    pass

signal play_song
signal stop_song
signal stop_all_songs

func play(song_name, looping):
    emit_signal("play_song", song_name, looping)

func stop(song_name):
    emit_signal("stop_song", song_name)

func stop_all_songs():
    emit_signal("stop_all_songs")