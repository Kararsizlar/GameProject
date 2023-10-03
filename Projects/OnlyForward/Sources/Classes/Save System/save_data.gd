extends Node

@export var save_node : Node2D
@export var save_path : String
@export var save_id   : int
@export var do_save   : bool

var data = {
	"id" = 1,
	"room_name" = "test",
	"has_walk" = true,
	"has_jump" = false,
	"has_dash" = false
}


func load_data(save_id):
	var data_path = "user://Save1.t" #+ str(data["id"]) + ".t"
	print(data_path)
		
	if not FileAccess.file_exists(data_path):
		print("Save file does not exist!")
		return
	
	var txt_file = FileAccess.open(data_path,FileAccess.READ)
	var json_parsed = JSON.parse_string(txt_file.get_line())
	print(json_parsed)
	

func save_data(data):
	var data_path = "user://Save" + str(data["id"]) + ".t"
	var json_data = JSON.stringify(data,"/t",)
	var txt_file = FileAccess.open(data_path,FileAccess.WRITE)
	
	print(data_path)
	txt_file.store_line(json_data)
	txt_file.close()

func _ready():
	await get_tree().create_timer(3).timeout
	
	if do_save:
		save_data(data)
	
	load_data(save_id)
