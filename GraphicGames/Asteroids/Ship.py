from Pawn import Actor
from pygame.math import Vector2
from pygame.transform import rotate
from Helpers import load_sprite
import datetime

class Ship(Actor):
    def __init__(self, position, sprite = None, direction = Vector2(0, -1), size = (23,30), speed = Vector2(0)):
        self.size = size
        self.direction = direction
        super().__init__(position, load_sprite("Ship") if sprite == None else sprite, self.size, speed)


    def accelerate(self, acceleration = 0.25, sameDirection=True):
        self.velocity += self.direction * acceleration * (1 if sameDirection else -1)

    def rotate(self, theta = 3, clockwise=True):
        self.direction.rotate_ip(theta if clockwise else -theta)

    def draw(self, surface):
        angle = self.direction.angle_to(Vector2(0, -1))
        blit_position = self.position - Vector2(self.radius)
        surface.blit(rotate(self.sprite, angle), blit_position)

class Plane(Ship):
    status = False
    def __init__(self, shootingHandler, position):
        self.shootingEvent = shootingHandler
        self.dt = datetime.datetime.now()
        super().__init__(position = position, sprite = load_sprite("Ship"))

    def shoot(self):
        time = datetime.datetime.now() - self.dt
        if time.total_seconds() >= 0.25 or self.status == False:
            self.status = True
            self.dt = datetime.datetime.now()
            self.shootingEvent(Rocket(self,(self.position.x,self.position.y)))


class Rocket(Ship):
    def __init__(self, ship, position):
        self.alive = True
        super().__init__(position = position, sprite = load_sprite("Bullet"), direction = Vector2(ship.direction), size = (7,15), speed = Vector2(ship.direction) * 3)

    def move(self, canvas):
        self.position += self.velocity
        w, h = canvas.get_size()
        if  self.position.x < 0 or self.position.x > w or self.position.y < 0 or self.position.y > h :
            self.alive = False