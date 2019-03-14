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

	var g = get_node("/root/Global")
	if not g.isInit("mainmenu"):
		var ms = get_node("/root/GlobalMusic/")
		ms.play("sunnspot.wav", 1)
	
#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass

func _on_newgamebutton_pressed():
	var g = get_node("/root/Global")
	g.initScene("mainmenu")
	get_tree().change_scene("res://scenes/charactercreation.tscn")
	#var ms = get_node("/root/GlobalMusic/")
	#ms.stop("midnight.wav")

func _on_quitbutton_pressed():
	get_tree().quit()
	
func _input(event):
	pass

func _process(delta):
	pass
