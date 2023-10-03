extends Node2D

var input_target

func initialize():
	input_target = get_parent()
	pass

func _process(_delta):
	if input_target.enabled == false:
		return
	
	input_target.input = Input.is_action_pressed("jump")
	pass

func _ready():
	initialize()
	pass
