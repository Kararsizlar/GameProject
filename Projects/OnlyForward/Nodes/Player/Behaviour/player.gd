extends Node2D

var player
var player_renderer
var player_behaviour

func initialize():
	player = self
	player_behaviour = player.get_child(0)
	player_renderer = player.get_child(1)
	
	player_behaviour.initialize(player)
	player_renderer.initialize(player)
	pass

func _ready():
	initialize()
	pass
