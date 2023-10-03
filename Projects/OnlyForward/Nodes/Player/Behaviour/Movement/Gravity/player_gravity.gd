extends Node2D

var player
var enabled = false

var force_per_frame = 60
var max_gravity = 1200

func step():
	if enabled == false:
		return
	
	if player.is_on_floor():
		player.velocity.y = 0
		return
	
	player.velocity.y += force_per_frame
	player.velocity.y = clamp(player.velocity.y,-INF,max_gravity)
	pass

func initialize(p):
	player = p
	pass

func set_active(value: bool):
	enabled = value
	pass
