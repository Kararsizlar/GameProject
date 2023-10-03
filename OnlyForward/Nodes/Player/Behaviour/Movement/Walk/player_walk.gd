extends Node2D

var player
var input = 0

var max_walk_speed

var enabled = false
var max_walk_input = 6 ## multiplicate this with acceleration for actual max speed
var acceleration = 80
var current = 0

func step():
	if enabled == false:
		current = 0
		return
	
	if input != 0:
		current += acceleration * input
		pass
	
	if input == 0 and current != 0:
		current += acceleration * -sign(current)
		pass
	
	current = clamp(current,-max_walk_speed,max_walk_speed)
	player.velocity.x = current
	pass

func initialize(p):
	max_walk_speed = max_walk_input * acceleration
	player = p
	pass

func set_active(value: bool):
	enabled = value
	pass
