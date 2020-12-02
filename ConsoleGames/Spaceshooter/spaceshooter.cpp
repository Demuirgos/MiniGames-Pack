#include <iostream>
#include "conio.h"
#include <cstdlib>
#include <string>
#include <ctime>
#define input(s,p) system("cls");char p;cout << s;cin >> p;
#define enemyPerLine 3
#define line 3
#define Clear_screen (cout << "\033[" << 0 << ';' << 0 << 'H' )
using namespace std;
enum class state {
	alive, dead
};
enum owner {
	ai, player
};
class pawn
{
public:
	int x, y;
	state isAlive;
	pawn() :x(0), y(0), isAlive(state::alive) {};
	void print();
	void fire();
	void kill() {
		this->isAlive = state::dead;
	}
	bool isDead() {
		return this->isAlive != state::alive;
	}
	bool operator==(pawn& other) {
		return other.x == this->x && other.y == this->y;
	}
};
class ship : public pawn
{
public:
	ship(int w, int h) {
		this->x = rand() % (w - 1) + 1;
		this->y = h + 2;
	}
	char move;
	void movleft()
	{
		x--;
	}
	void movright()
	{
		x++;
	}
	void getmove()
	{
		move = _getch();
	}
	bool fire()
	{
		if (move == '5') return 1;
		else return 0;
	}
	void print()
	{
		cout << "\033[" << y << ';' << x-2 << 'H';
		if(x-2 > 0)
			cout << "=/_\\=";
		else 
			cout << "_\\=";
	}
};
class other : public pawn {
public:
	void operator=(pawn& b)
	{
		x = b.x; y = b.y; isAlive = b.isAlive;
	}
};
class enemies :public other
{
public:
	enemies(int _x, int _y) {
		this->x = _x; this->y = _y;
	}
	void print()
	{
		cout << "\033[" << y << ';' << x << 'H';
		cout << "\b[ ]";
	}
	bool fire() {
		return rand()%234==23;
	}
};
class missile :public other
{
public:
	missile() = default;
	missile(int x, int y, owner _sender = owner::ai) {
		this->x = x; this->y = y; sender = _sender;
	}
	owner sender = owner::ai;
	void print()
	{
		cout << "\033[" << y << ';' << x << 'H';
		cout << "0";
	}
	void movedown()
	{
		y++;
	}
	void moveup()
	{
		y--;
	}
	void donothing()
	{}
};
int main()
{
	input("Do you want to activate ANSI Mode : y for yes ",answer)
	if(answer=='y'){
		system("REG ADD HKCU\CONSOLE /f /v VirtualTerminalLevel /t REG_DWORD /d 1");
	}
	int h = 10, w = 31;
	char move;
	int tim = 0;
	void prntscr(int, int);
	void winmsg();
	void losemsg();
	int counting(int);
	int countEnemie = (int)(w / enemyPerLine)*line;
	enemies* enem = (enemies*)malloc(++countEnemie * sizeof(enemies));
	missile* miss = nullptr;
	int countMissiles = 0;
	ship ship_1(w, h);
	for (int j = 0; j < countEnemie; j++)
	{
		int x = (j * enemyPerLine) % w;
		int y = (j * enemyPerLine) / w ;
		enem[j] = enemies(x + 2,y + 2);
	}
	int k = 0;
	while (ship_1.isAlive == state::alive && countEnemie > 0)
	{
		
		//cleaning enemies
		bool EnemiesModified = false;
		for (int j = 0; j < countEnemie; j++)
			if (enem[j].isDead())
			{
				enem[j] = enem[countEnemie -1 ];
				EnemiesModified = true;
				j--; countEnemie--;
			}
		//cleaning missiles
		bool MissilesModified = false;
		for (int j = 0; j < countMissiles; j++)
			if (miss[j].isDead())
			{
				miss[j] = miss[countMissiles -1];
				MissilesModified = true;
				j--; countMissiles--;
			}
		if (MissilesModified) miss = (missile*)realloc(miss, countMissiles * sizeof(missile));
		if (EnemiesModified) enem = (enemies*)realloc(enem, countEnemie * sizeof(enemies));
		Clear_screen;
		//print data 
		prntscr(h, w);
		ship_1.print();
		for (int i = 0; i < countEnemie; i++) {
			enem[i].print();
		}
		for (int i = 0; i < countMissiles; i++) {
			miss[i].print();
		}
		//getting enemies hit
		for (int j = 0; j < countEnemie; j++)
			if (!enem[j].isDead() && enem[j].fire())
			{
				if (countMissiles == 0) {
					miss = (missile*)malloc(++countMissiles * sizeof(missile));
				}
				else {
					miss = (missile*)realloc(miss, ++countMissiles * sizeof(missile));
				}
				miss[countMissiles - 1] = missile(enem[j].x+1, enem[j].y);
			}
		//check for collision
		for (int i = 0; i < countMissiles; i++) {
			if (!miss[i].isDead()) {
				if (miss[i] == ship_1 && miss[i].sender == owner::ai) {
					ship_1.kill();
					miss[i].kill();
				}
				else if (miss[i].sender == owner::player) {
					for (int j = 0; j < countEnemie; j++) {
						if (miss[i] == enem[j]) {
							enem[j].kill();
							miss[i].kill();
						}
					}
				}
				for (int j = 0; j < countMissiles; j++) {
					if (j != i && miss[i] == miss[j]) {
						miss[i].kill(); miss[j].kill();
					}
				}
			}
		}
		if (_kbhit()) ship_1.getmove();
		else ship_1.move = '\0';
		{
			if (ship_1.move == '4')
			{
				if (ship_1.x > 2 )
					ship_1.movleft();
			}
			else if (ship_1.move == '6')
			{
				if (ship_1.x != w+1)
					ship_1.movright();
			}

		}
		{
			if (ship_1.fire())
			{
				miss = (missile*)realloc(miss, ++countMissiles * sizeof(missile));
				miss[countMissiles - 1] = missile(ship_1.x, ship_1.y, owner::player);
			}
		}
		//updating missile 
		for (int j = 0; j < countMissiles; j++)
			if (!miss[j].isDead())
			{
				if (miss[j].sender == owner::ai) {
					miss[j].movedown();
				}
				else if (miss[j].sender == owner::player) {
					miss[j].moveup();
				}
				
				if (miss[j].y < 0 || miss[j].y > h + 2) miss[j].kill();
			}
		Clear_screen;
		while (true)
		{
			counting(100000);
			break;
			if (_kbhit()) break;
		}
	}
	Clear_screen;
	if (ship_1.isDead())
		losemsg();
	else
		winmsg();
	free(miss);
	free(enem);
}
void prntscr(int h, int w)
{
	for (int i = 0; i <= h + 1; i++)
	{
		for (int j = 0; j <= w + 1; j++)
		{
			if (i == 0) cout << "_";
			else if (i <= h && i > 0 && (j == 0 || j == w + 1)) cout << "|";
			else if (i == h + 1 && j == 0) cout << "|_";
			else if (i == h + 1 && j == w + 1) cout << "_|";
			else if (i == h + 1 && (j > 0 && j < w - 1)) cout << "_";
			else if (i <= h && j <= w && j > 0 && i > 0) cout << " ";

		}
		cout << endl;
	}
}
void losemsg()
{
	cout << "_______________________________\n";
	cout << "|*   *    *******      ******* |\n";
	cout << "|*   *          *            * |\n";
	cout << "|*   *                         |\n";
	cout << "|*****            *******      |\n";
	cout << "| *             **** *****     |\n";
	cout << "| *       * *  *       *       |\n";
	cout << "| *      *   *  ****   *       |\n";
	cout << "| *      *   *      *  *       |\n";
	cout << "| * * *   * *   ****   *       |\n";
	cout << "|______________________________|\n";
}
void winmsg()
{
	cout << "_______________________________\n";
	cout << "||   |    \\   /\\    / /\\ |\\  |  |\n";
	cout << "||   |     \\ /  \\  / |  || \\ |  |\n";
	cout << "||   |      \\    \\/   \\/ |  \\|\n";
	cout << "|\\___/                          |\n";
	cout << "|           * *         * *     |\n";
	cout << "|          *   *       *   *    |\n";
	cout << "|                               |\n";
	cout << "|            *            *     |\n";
	cout << "|             ************      |\n";
	cout << "|_______________________________|\n";
}
int counting(int l)
{
	int k = 0;

	int t = time(0);
	while (true)
	{
		k++;
		if (time(0) - t == 1 || k == l) break;
	}
	return k;
}