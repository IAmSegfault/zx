extends PopupMenu

var state = "hidden"
var vlock = false

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