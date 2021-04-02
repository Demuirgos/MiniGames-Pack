import pygame

from random import randint
from Ship import Plane, Rocket, Ship
from Asteroids import Asteroids, Asteroid
from Helpers import load_sprite, print_text

class ShootingStars:
    def __init__(self, endedGameEvent):
        pygame.init()
        pygame.display.set_caption("Shooting Stars")
        self.clock = pygame.time.Clock()
        self.screen = pygame.display.set_mode((800, 600))
        self.rockets = []
        self.spaceship = Plane(self.rockets.append, (400, 300))
        self.asteroids = Asteroids( self.spaceship.position,
                                    self.spaceship.size,
                                    self.screen,
                                    6)
        self.Message = ""
        self.isRunning = True
        self.EndGameEventHandler = endedGameEvent

    def loop(self):
        while True:
            self._handle_input()
            if self.isRunning : 
                self._process_game_logic()
            self._draw()

    def _get_actors(self) : 
        return [*self.rockets, *self.asteroids.items, self.spaceship]

    def handleGameInput(self, is_key_pressed):
        if self.isRunning : 
            if is_key_pressed[pygame.K_RIGHT]:
                self.spaceship.rotate(clockwise=True)
            elif is_key_pressed[pygame.K_LEFT]:
                self.spaceship.rotate(clockwise=False)
            elif is_key_pressed[pygame.K_UP]:
                self.spaceship.accelerate(sameDirection=True)
            elif is_key_pressed[pygame.K_DOWN]:
                self.spaceship.accelerate(sameDirection=False)
            elif is_key_pressed[pygame.K_SPACE] :
                self.spaceship.shoot()

    def _handle_input(self):
        self.events = pygame.event.get()
        is_key_pressed = pygame.key.get_pressed()
        self.handleGameInput(is_key_pressed)
        if is_key_pressed[pygame.K_ESCAPE]:
            quit()
        elif is_key_pressed[pygame.K_SPACE]:
            if not self.isRunning :
                self.EndGameEventHandler()
        for event in self.events:
            if event.type == pygame.QUIT :
                quit()

    def _process_game_logic(self):
        for e in self._get_actors():
            e.move(self.screen)
        for asteroid in self.asteroids.items : 
            if asteroid.collides_with(self.spaceship):
                self.isRunning = False
                self.Message = "You Lost"
                break
        if len(self.asteroids.items) == 0 : 
            self.isRunning = False
            self.Message = "You Won"
           
        if self.isRunning : 
            for bullet in self.rockets:
                for asteroid in self.asteroids.items:
                    if asteroid.collides_with(bullet):
                        asteroid.hit()
                        if asteroid.isSplit :
                            self.asteroids.items += [Asteroid(asteroid.position + pygame.Vector2( 10,0), asteroid.velocity + pygame.Vector2( randint(0,2),randint(0,5) ), asteroid.size // 2)
                                                    ,Asteroid(asteroid.position + pygame.Vector2(-10,0), asteroid.velocity + pygame.Vector2( randint(0,2),randint(0,5) ), asteroid.size // 2)]
                        self.asteroids.items.remove(asteroid) 
                        self.rockets.remove(bullet)
                        break

    def _draw(self):
        self.screen.fill((0, 0, 0))
        for e in self._get_actors():
            e.draw(self.screen)
            if isinstance(e,Rocket):
                if e.alive == False:
                    self.rockets.remove(e)
        print_text(self.screen,self.Message, pygame.font.Font(None, 64))
        pygame.display.flip()
        self.clock.tick(60)