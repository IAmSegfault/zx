extends Node

# class member variables go here, for example:
# var a = 2
# var b = "textvar"

signal load_processor(processor, priority)


func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	var f = File.new()
	f.open("res://data/directive/gameworldprocessors.json", File.READ)
	var text = f.get_as_text()
	f.close()
	var data = parse_json(text)
	#for i in range(len(data["processors"])):
	#	emit_signal("load_processor", data["processors"][i], data["priorities"][i])
	emit_signal("load_processor", data["processors"], data["priorities"])

#func _process(delta):
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass
