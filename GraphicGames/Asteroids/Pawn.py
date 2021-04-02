from pygame.math import Vector2
from pygame.transform import scale
from Helpers import wrap_position

class Actor:
    def __init__(self, position, sprite, size, velocity):
        self.position = Vector2(position)
        self.radius = min(size)/2
        self.sprite = scale(sprite,size)
        self.velocity = Vector2(velocity)

    def draw(self, surface):
        blit_position = self.position - Vector2(self.radius)
        surface.blit(self.sprite, blit_position)

    def move(self, surface):
        self.position = wrap_position(self.position + self.velocity,surface)

    def collides_with(self, other):
        distance = self.position.distance_to(other.position)
        return distance < self.radius + other.radius