extends Node2D

var input_target

func initialize():
	input_target = get_parent()
	pass

func _process(delta):
	if input_target.enabled == false:
		return
	
	input_target.input = Input.get_axis("left","right")
	pass

func _ready():
	initialize()
	pass
