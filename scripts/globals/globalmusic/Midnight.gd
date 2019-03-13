extends AudioStreamPlayer

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
var song = "midnight.wav"
func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	pass

func _on_MusicServer_play_song(song_name, looping):
	if song_name == song:
		stream.loop_mode = looping
		play() 

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass


func _on_MusicServer_stop_all_songs():
	stop()


func _on_MusicServer_stop_song(song_name):
	if song_name == song:
		stop()
