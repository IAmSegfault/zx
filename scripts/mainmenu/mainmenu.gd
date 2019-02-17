extends Node

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
signal check_save

func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	get_tree().set_auto_accept_quit(false)

	var dir = Directory.new()
	if not dir.dir_exists("user://save"):
		dir.make_dir("user://save")
		#var manifest = File.new()
		#manifest.open("user://save/manifest.json", File.WRITE)
		#manifest.store_line("foo")
		#manifest.close()
	else:
		emit_signal("check_save")
#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass

func _on_newgamebutton_pressed():
	#var g = get_node("/root/Global")
	get_tree().change_scene("res://scenes/gameworld.tscn")


func _on_quitbutton_pressed():
	get_tree().quit()
	
func _process(delta):
	if Input.is_action_just_pressed("fullscreen"):
		OS.window_fullscreen = not OS.window_fullscreen
