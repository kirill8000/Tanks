using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tanks.Model
{
    class PlayingField
    {
        private const int BULLET_FACTOR = 3;
        private Random _random = new Random();
        private int _fire = 0;
        private int _cellSize;
        private Size _size;
        private List<Tank> _tanks = new List<Tank>();
        private List<Wall> _walls = new List<Wall>();
        private List<Bullet> _tanksBulets = new List<Bullet>();
        private List<Bullet> _kolobokBulets = new List<Bullet>();
        private List<Apple> _apples = new List<Apple>();


        private bool[,] _grid;

        public Size Size => _size;
        public IList<Tank> Tanks => _tanks;
        public IReadOnlyCollection<Wall> Wall => _walls;
        public IReadOnlyCollection<Bullet> TanksBullets => _tanksBulets;
        public IReadOnlyCollection<Bullet> KolobokBullets => _kolobokBulets;
        public IReadOnlyCollection<Apple> Apples => _apples;

        public Kolobok Kolobok { get; }
        public event EventHandler GameOver;
        public event EventHandler ScoreUpdated;
        public int UpdateInterval { get; }
        public int Score { get; private set; } = 0;

        private Point CellLocation(int x, int y)
        {
            return new Point(x * _cellSize, y * _cellSize);
        }

        private (int x, int y) GetRandomCell()
        {
            int index = _random.Next(0, _grid.Length);

            while (_grid[index / _grid.GetLength(1), index % _grid.GetLength(1)])
                index = (index + 1) % _grid.Length;

            return (index / _grid.GetLength(1), index % _grid.GetLength(1));
        }

        private void GenerateTanks(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = GetRandomCell();
                var tank = new Tank(_cellSize, CellLocation(pos.x, pos.y), (Directon)_random.Next(0, 4));
                _tanks.Add(tank);
                _grid[pos.x, pos.y] = true;
            }
        }


        private void GenerateWalls()
        {
            string s = File.ReadAllText("Resources\\level.txt");

            for (int i = 0; i < s.Length; i++)
            {
                int x = i / _grid.GetLength(1);
                int y = i % _grid.GetLength(1);

                if (s[i] == 'w')
                {
                    _walls.Add(new Wall(_cellSize, CellLocation(x, y)));
                    _grid[x, y] = true;
                }

            }
        }

        private void GenerateApples(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = GetRandomCell();
                var apple = new Apple(_cellSize, CellLocation(pos.x, pos.y));
                _apples.Add(apple);
                _grid[pos.x, pos.y] = true;
            }
        }

        private int CollidesIndex(Item item, IEnumerable<Item> items)
        {
            int index = 0;
            foreach (var i in items)
            {
                if (i.IntersectsWith(item))
                    return index;

                index++;
            }

            return -1;
        }

        private bool Collides(Item item, IEnumerable<Item> items)
        {
            return CollidesIndex(item, items) >= 0;
        }

        private bool CollidesWithWalls(Enity enity)
        {
            return Collides(enity, _walls);
        }
        private bool CollidesWithBorders(Enity enity)
        {
            if (enity.X < 0 && enity.Directon == Directon.LEFT ||
                enity.Y < 0 && enity.Directon == Directon.UP ||
                enity.X + enity.Size > _size.Width && enity.Directon == Directon.RIGHT ||
                enity.Y + enity.Size > _size.Height && enity.Directon == Directon.DOWN ||
                CollidesWithWalls(enity))
            {
                return true;
            }


            return false;
        }

        private bool CanRotate(Enity enity)
        {
            if (enity.X % _cellSize == 0 && enity.Y % _cellSize == 0)
                return true;

            return false;
        }

        private bool CanFire()
        {
            if (_random.NextDouble() < 0.01)
                return true;

            return false;
        }

        private void UpdateTanks()
        {
            for (int i = 0; i < _tanks.Count; i++)
            {
                _tanks[i].Move();
                if (CollidesWithBorders(_tanks[i]))
                {
                    _tanks[i].MoveBack();
                    _tanks[i].RandomRotate();
                    continue;
                }

                bool needRotate = true;
                for (int j = i + 1; j < _tanks.Count; j++)
                {
                    if (_tanks[i].IntersectsWith(_tanks[j]))
                    {
                        _tanks[i].Rotate180();
                        _tanks[j].Rotate180();
                        needRotate = false;
                        break;
                    }
                }

                if (needRotate && CanRotate(_tanks[i]))
                    _tanks[i].RandomRotate();
                else if (CanFire())
                    _tanksBulets.Add(new Bullet(3, _tanks[i]));
            }
        }

        private void RemoveBullets(List<Bullet> bullets)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                int index = CollidesIndex(bullets[i], _walls);
                if (index >= 0)
                {
                    if (_walls[index].WallType == WallType.Destructible)
                    {
                        _walls.RemoveAt(index);
                    }
                    else if (_walls[index].WallType == WallType.ShotThrough)
                    {
                        continue;
                    }

                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        private void MoveBullets(List<Bullet> bullets)
        {
            foreach (var b in bullets)
            {
                b.Move(BULLET_FACTOR);
            }
        }

        private void UpdateScore()
        {
            Score++;
            ScoreUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void UpdadeBullets()
        {
            RemoveBullets(_tanksBulets);
            RemoveBullets(_kolobokBulets);

            for (int j = 0; j < _kolobokBulets.Count; j++)
            {
                for (int i = 0; i < _tanks.Count; i++)
                {
                    if (_kolobokBulets[j].IntersectsWith(_tanks[i]))
                    {
                        _tanks.RemoveAt(i);
                        _kolobokBulets.RemoveAt(j);
                        j--;
                        UpdateScore();
                        break;
                    }
                }
            }

            MoveBullets(_kolobokBulets);
            MoveBullets(_tanksBulets);
        }

        public void Fire()
        {
            _fire++;
        }

        private void UpdateKolobok()
        {
            if (_fire > 0)
            {
                _kolobokBulets.Add(new Bullet(3, Kolobok));
                _fire--;
            }
            Kolobok.Move();

            if (CollidesWithBorders(Kolobok))
                Kolobok.MoveBack();

            for (int i = 0; i < _apples.Count; i++)
            {
                if (_apples[i].IntersectsWith(Kolobok))
                {
                    UpdateScore();
                    _apples.RemoveAt(i);
                    break;
                }
            }
        }

        private void CheckGameOver()
        {
            if (Collides(Kolobok, _tanksBulets) || Collides(Kolobok, _tanks))
                GameOver?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateApples()
        {
            if (_apples.Count <= 4)
            {
                var cell = GetRandomCell();
                _apples.Add(new Apple(_cellSize, CellLocation(cell.x, cell.y)));
            }
        }

        public void Update()
        {
            CheckGameOver();
            UpdadeBullets();
            UpdateKolobok();
            UpdateTanks();
            UpdateApples();
        }

        private Kolobok CreateKolobok()
        {
            if (Kolobok != null)
                return Kolobok;

            _grid[0, 0] = true;
            _grid[1, 0] = true;
            _grid[0, 1] = true;
            _grid[1, 1] = true;
            return new Kolobok(_cellSize, CellLocation(0, 0), Directon.RIGHT);
        }

        public PlayingField(Size size, int cellSize, uint enitiesSpeed, int tanksCount)
        {
            _cellSize = cellSize;
            _grid = new bool[size.Width, size.Height];
            _size = new Size(size.Width * cellSize, size.Height * cellSize);
            Kolobok = CreateKolobok();
            GenerateWalls();
            GenerateTanks(tanksCount);
            GenerateApples(5);
            UpdateInterval = (int)(1000 / enitiesSpeed);

        }

    }
}
