extends VBoxContainer

var is_on_screen = false
var menu_manager

var save_menu
var option_menu
var quit_menu

func _ready():
	menu_manager = get_parent()
	menu_manager.move_element(self,is_on_screen)
	
	save_menu = menu_manager.get_child(1)
	option_menu = menu_manager.get_child(2)
	quit_menu = menu_manager.get_child(3)
	pass

func on_button_start():
	menu_manager.move_element(self,is_on_screen)
	menu_manager.set_secondary(save_menu,save_menu.is_on_screen)
	pass
