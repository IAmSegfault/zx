extends PopupMenu

# class member variables go here, for example:
# var a = 2
# var b = "textvar"
var state = "visible"
var vlock = true
func _ready():
	# Called when the node is added to the scene for the first time.
	# Initialization here
	pass

func _process(delta):
	if not is_visible():
		state = "hidden"
	else:
		state = "visible" 

	if state != "visible" and vlock:
		popup_centered()
#	# Called every frame. Delta is time since last frame.
#	# Update game logic here.
#	pass
