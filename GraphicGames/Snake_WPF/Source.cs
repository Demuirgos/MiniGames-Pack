using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    public delegate void hasDiedEventHandler();
    public enum state
    {
        dead, alive
    }
    class game
    {
        private enum difficulty { easy,medium, hard};
        private enum gameState { paused, ended, ongoing }
        public event hasDiedEventHandler gameEnded;
        DispatcherTimer refOfTimer;
        Canvas CurrentBoard;
        Snake snake;
        Food food;
        private gameState Gtate;
        public game(DispatcherTimer Timer,Canvas Board, int Height, int Width)
        {
            CurrentBoard = Board;
            this.refOfTimer = Timer;
            Gtate = gameState.paused;
            this.food = new Food(Height, Width);
            this.snake = new Snake(CurrentBoard, Height, Width);
            snake.currentFood = this.food;
        }
        public void Start()
        {
            Gtate = gameState.ongoing;
        }
        public void Pause()
        {
            Gtate = gameState.paused;
        }
        void getInput()
        {
            if (Keyboard.IsKeyDown(Key.Escape) || Keyboard.IsKeyDown(Key.Space))
            {
                if (Gtate == gameState.ongoing)
                    Pause();
                else
                    Start();
            }
        }
        public void tickTock()
        {
            getInput();
            if (Gtate == gameState.ongoing)
            {
                this.food.TickTock();
                this.food.Show(this.CurrentBoard);
                this.snake.TickTock();
                /*
                    if(snake.Lenght % 10 ==0)
                    this.refOfTimer.Interval = this.refOfTimer.Interval.Subtract(TimeSpan.FromMilliseconds(5));
                */
                if (this.snake.IsAlive)
                {
                    this.snake.TickTock();
                }
                else
                {
                    Gtate = gameState.ended;
                    this.gameEnded();
                }
                //this.snake.Show(this.CurrentBoard);
            }
        }
    }
    class Position
    {
        public double x;
        public double y;
        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Position normalize()
        {
            var dist = System.Math.Sqrt(x * x + y * y);
            return new Position(x / dist, y / dist);
        }
        public static Position operator +(Position a, Position b) => new Position(a.x + b.x, a.y + b.y);
        public static Position operator -(Position a, Position b) => new Position(a.x - b.x, a.y - b.y);
        public static Position operator *(Position a, double b) => new Position(a.x * b, a.y * b);
        public static Boolean operator ==(Position a, Position b) => (a.x == b.x && a.y == b.y);
        public static Boolean operator !=(Position a, Position b) => (a.x != b.x || a.y != b.y);
    }
    class Food
    {
        private Position location;
        private int size;
        public Rectangle bodyShape;
        SnakeGame.state status;
        TranslateTransform translate;
        int height, width;
        int life;
        bool shown;
        public event hasDiedEventHandler reIncarnate;
        public Rect collision;
        public Food(int Height, int Width)
        {
            height = Height;
            width = Width;
            translate = new TranslateTransform();
            this.bodyShape = new Rectangle();
            this.bodyShape.RenderTransform = translate;
            reLocate();
            life = 12345;
            bodyShape.Width = this.size;
            bodyShape.Height = this.size;
            this.reIncarnate += new hasDiedEventHandler(reLocate);
        }
        private void reLocate()
        {
            Random pos = new Random();
            this.location = new Position(pos.Next(15, width - 50), pos.Next(15, height - 50));
            int quantity = pos.Next(0, 10);
            if (quantity == 7)
            {
                this.size = 2;
            }
            else
            {
                this.size = 1;
            }
            status = state.alive;
            translate.X = this.location.x;
            translate.Y = this.location.y;
            collision = new Rect(this.translate.X, this.translate.Y, 20, 20);
        }
        public void Show(Canvas board)
        {

            ImageBrush foodbrush = new ImageBrush();
            if (this.size == 1)
            {
                foodbrush.ImageSource = new BitmapImage(new Uri("Assets/Food.png", UriKind.Relative));
                this.bodyShape.Width = 25;
                this.bodyShape.Height = 25;
            }
            else
            {
                foodbrush.ImageSource = new BitmapImage(new Uri("Assets/BigFood.png", UriKind.Relative));
                this.bodyShape.Width = 50;
                this.bodyShape.Height = 25;
            }
            this.bodyShape.Fill = foodbrush;
            if (!shown)
            {
                board.Children.Add(this.bodyShape);
                this.shown = true;
            }
        }
        public void TickTock()
        {
            life--;
            if (life < 1)
            {
                reLocate();
                life = 12345;
            }
        }
        public Position Location
        {
            get { return location; }
        }
        public int Size
        {
            get { return size; }
        }
        public void kill()
        {
            this.status = state.dead;
            reIncarnate();
        }
    }
    class BodyPart
    {
        public Position position;
        public TranslateTransform translate;
        public Rectangle body;
        public void update()
        {
            translate.X = position.x;
            translate.Y = position.y;
            collision = new Rect(this.translate.X + 2.5, this.translate.Y + 2.5, 10, 10);
        }
        public Rect collision;
        void init()
        {
            this.body.Width = 15;
            this.body.Height = 15;
            this.translate = new TranslateTransform();
            this.body.RenderTransform = translate;
            this.translate.X = position.x;
            this.translate.Y = position.y;
            collision = new Rect(this.translate.X + 2.5, this.translate.Y + 2.5, 10, 10);
        }
        public BodyPart(double x, double y)
        {
            this.body = new Rectangle();
            this.position = new Position(x, y);
            init();
        }
        public BodyPart(Position v)
        {
            this.body = new Rectangle();
            this.position = v;
            this.translate = new TranslateTransform();
            init();
        }
    }
    class Snake
    {
        Canvas board;
        int width, height;
        private enum orientation { up, down, left, right};
        private int lenght = 3;
        private List<BodyPart> Body;
        public Food currentFood;
        private orientation CurrentOrientation;//up = 1;right = 2; down = 3; left = 4;
        SnakeGame.state status;
        public delegate void hasEatenEventHandler();
        public event hasEatenEventHandler hasEaten;
        public delegate void sizeChangedEventHandler(int addedSize);
        public event sizeChangedEventHandler hasGrown;
        //public delegate int hasChangedOrientationEventHandler();
        //public event hasChangedOrientationEventHandler hasChangedOrientation;
        public delegate void onOverlapEventHandler();
        public event onOverlapEventHandler hasOverlapped;
        public event hasDiedEventHandler hasDied;
        bool shown = false;
        private BodyPart Tail
        {
            get { return Body.Last(); }
        }
        private BodyPart Head
        {
            get { return Body.First(); }
        }
        public Boolean IsAlive
        {
            get { return status == state.alive; }
        }

        public int Lenght {
            get { return Body.Count; } 
        }

        public Snake(Canvas board, int Height, int Width)
        {
            this.board = board;
            width = Width; height = Height;
            Random pos = new Random();
            this.Body = new List<BodyPart>();
            for (int i = 1; i <= lenght; i++)
            {
                if (this.Body.Count == 0)
                {
                    this.Body.Add(new BodyPart(pos.Next(5, Width), pos.Next(5, Height)));
                }
                else
                {
                    this.Body.Add(new BodyPart(this.Head.position.x, this.Head.position.y + i * 20));
                }
            }
            status = state.alive;
            CurrentOrientation = orientation.up;
            this.hasEaten += Eating;
            this.hasGrown += Growing;
            this.hasDied += Dying;
            this.hasOverlapped += Overlapping;
        }
        public void update()
        {
            foreach (var bodypart in this.Body)
            {
                bodypart.update();
                if (bodypart.position == this.Head.position)
                {
                    Canvas.SetZIndex(bodypart.body,this.Body.Count+1);
                    ImageBrush headbrush = new ImageBrush();
                    headbrush.ImageSource = new BitmapImage(new Uri("Assets/Head.png", UriKind.Relative));
                    double angle = 0;
                    if (CurrentOrientation == orientation.down)
                    {
                        angle = 180;
                    }
                    else if (CurrentOrientation == orientation.left)
                    {
                        angle = 270;
                    }
                    else if (CurrentOrientation == orientation.right)
                    {
                        angle = 90;
                    }
                    var rotate = new RotateTransform();
                    rotate.Angle = angle;
                    rotate.CenterX = 0.5;
                    rotate.CenterY = 0.5;
                    headbrush.RelativeTransform = rotate;
                    this.Head.body.Fill = headbrush;
                    this.Head.body.Stretch = Stretch.Fill;
                }
                else if (bodypart.position == this.Tail.position)
                {
                    var TailOrientation = getTailOrientation();
                    ImageBrush tailbrush = new ImageBrush();
                    tailbrush.ImageSource = new BitmapImage(new Uri("Assets/Tail.png", UriKind.Relative));
                    double angle = 0;
                    if (TailOrientation == orientation.down)
                    {
                        angle = 180;
                    }
                    else if (TailOrientation == orientation.left)
                    {
                        angle = 90;
                    }
                    else if (TailOrientation == orientation.right)
                    {
                        angle = 270;
                    }
                    var rotate = new RotateTransform();
                    rotate.Angle = angle;
                    rotate.CenterX = 0.5;
                    rotate.CenterY = 0.5;
                    tailbrush.RelativeTransform = rotate;
                    this.Tail.body.Fill = tailbrush;
                    this.Tail.body.Stretch = Stretch.Fill;
                }
                else
                {
                    ImageBrush bodybrush = new ImageBrush();
                    bodybrush.ImageSource = new BitmapImage(new Uri("Assets/Body.png", UriKind.Relative));
                    bodypart.body.Fill = bodybrush;
                    bodypart.body.Stretch = Stretch.Fill;
                }
                if (!board.Children.Contains(bodypart.body))
                {
                    board.Children.Add(bodypart.body);
                }
            }
        }
        public void TickTock()
        {
            if (this.status == state.alive)
            {
                getInput();
                update();
                move();
            }
        }
        public void getInput()
        {
            if (Keyboard.IsKeyDown(Key.Up) && CurrentOrientation != orientation.down)
            {
                CurrentOrientation = orientation.up;
            }
            else if (Keyboard.IsKeyDown(Key.Down) && CurrentOrientation != orientation.up)
            {
                CurrentOrientation = orientation.down;
            }
            else if (Keyboard.IsKeyDown(Key.Left) && CurrentOrientation != orientation.right)
            {
                CurrentOrientation = orientation.left;
            }
            else if (Keyboard.IsKeyDown(Key.Right) && CurrentOrientation != orientation.left)
            {
                CurrentOrientation = orientation.right;
            }
        }
        private void move(double dl = 15)
        {
                Position prev = new Position(this.Head.position.x, this.Head.position.y);
                foreach (var BodyPart in this.Body.ToList())
                {
                    if (BodyPart.position == this.Head.position)
                    {
                        BodyPart.position.x = BodyPart.position.x + (CurrentOrientation == orientation.right ? dl : CurrentOrientation == orientation.left ? -dl : 0);
                        BodyPart.position.y = BodyPart.position.y + (CurrentOrientation == orientation.down ? dl : CurrentOrientation == orientation.up ? -dl : 0);
                    if (BodyPart.translate.X < 0 || BodyPart.translate.Y < 0 || BodyPart.translate.X > width || BodyPart.translate.Y > height)
                    {
                        this.hasDied();
                        return;
                    }
                    }
                    else
                    {
                        Position next = new Position(BodyPart.position.x, BodyPart.position.y);
                        BodyPart.position = new Position(prev.x, prev.y);
                        prev = new Position(next.x, next.y);
                    }
                }
            this.CheckEating();
            this.CheckOverlapping();
        }
        void Eating()
        {
            this.hasGrown(this.currentFood.Size);
        }
        void Overlapping()
        {
            this.hasDied();
        }
        private orientation getOrientation(Position V1, Position V2)
        {
            Position pos = V1 - V2;
            if (pos.x > 0) return orientation.right;
            else if (pos.x < 0) return orientation.left;
            if (pos.y > 0) return orientation.down;
            return orientation.up;
        }
        private orientation getTailOrientation()
        {
            var V1 = this.Body.ElementAt(this.Body.Count - 2).position;
            var V2 = this.Tail.position;
            return getOrientation(V1, V2);
        }
        private void CheckOverlapping()
        {
            int overlapps = 0;
            foreach (var otherPos in this.Body.ToList())
            {
                if (this.Head.collision.IntersectsWith(otherPos.collision))
                {
                    overlapps++;
                }
            }
            if (overlapps > 2)
            {
                this.hasOverlapped();
            }
        }
        private void CheckEating()
        {
            foreach (var pos in this.Body.ToList())
            {
                if (pos.collision.IntersectsWith(currentFood.collision))
                {
                    this.hasEaten();
                    this.currentFood.kill();
                }
            }
        }
        private void Growing(int dsize)
        {
            this.lenght += dsize;
            if (dsize > 0)
            {
                for (int i = 0; i < dsize; i++)
                {
                    var TailOrientation = getTailOrientation();
                    int dx = TailOrientation == orientation.right ? 20 : TailOrientation == orientation.left ? -20 : 0;
                    int dy = TailOrientation == orientation.down ? 20 : TailOrientation == orientation.up ? -20 : 0;
                    Position Loc = this.Tail.position + new Position(dx, dy);
                    this.Body.Add(new BodyPart(Loc));
                }
            }
            else
            {
                if (this.lenght == 0)
                {
                    this.hasDied();
                }
            }

        }
        private void Dying()
        {
            this.Body.Clear();
            this.lenght = 0;
            this.status = state.dead;
        }
    }
}
