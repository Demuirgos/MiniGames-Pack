from Pawn import Actor
from pygame.math import Vector2
from pygame.transform import rotate
from Helpers import load_sprite
import random

class Asteroid(Actor):
    def __init__(self, position, speed, size = None):
        self.isSplit = False
        self.size = random.randint(50,150) if size == None else size
        super().__init__(position, load_sprite("Asteroid"), [self.size]*2, speed)
    
    def hit(self):
        if self.size > 75:        
            self.isSplit = True
        else:
            self.isSplit = False

class Asteroids :
    def __init__(self, pos, dim, surface, num):
        self.items = []
        while len(self.items) < num :
            w, h = surface.get_size()
            postion = (random.randint(0,w),random.randint(0,h))
            if postion[0] - pos[0] < dim[0] and postion[1] - pos[1] < dim[1] :
                pass
            else:
               self.items.append(Asteroid(postion, self.get_random_velocity(1, 3))) 
    
    def draw(self, screen):
        for e in self.items :
            e.draw(screen)

    def get_random_velocity(self, min_speed, max_speed):
        speed = random.randint(min_speed, max_speed)
        angle = random.randrange(0, 360)
        return Vector2(speed, 0).rotate(angle)
