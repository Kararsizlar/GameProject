extends Node2D

var player
var input = false

var enabled = false
var start_velocity = 1100

func step():
	if enabled == false:
		return
	
	if input == true and player.is_on_floor():
		player.velocity.y = start_velocity * -1
		pass

	pass

func initialize(p):
	player = p
	pass

func set_active(value: bool):
	enabled = value
	pass
