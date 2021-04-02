from pygame.image import load
from pygame.math import Vector2
from pygame import Color

def load_sprite(name):
    loaded_sprite = load(f"assets/sprites/{name}.png")
    return loaded_sprite.convert_alpha()

def wrap_position(position, canvas):
    x, y = position
    w, h = canvas.get_size()
    return Vector2(x % w, y % h)

def print_text(surface, text, font, color=Color("tomato")):
    text_surface = font.render(text, True, color)

    rect = text_surface.get_rect()
    rect.center = Vector2(surface.get_size()) / 2

    surface.blit(text_surface, rect)