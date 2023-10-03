extends Sprite2D

var player
var active = false

var max_speed = 100
var acceleration = 20

var input_data = Vector2(0,0)

func initialize(p):
	player = p
	pass

func set_active(value: bool):
	active = value
	pass

func step():
	if active == false:
		return
	
