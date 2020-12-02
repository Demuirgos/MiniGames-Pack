#include <iostream>
#include <cstdlib>
#include <ctime>
#include <cstdio>
#include "conio.h"
#define input(s,p) system("cls");char p;cout << s;cin >> p;
#define Clear_screen (cout << "\033[" << 0 << ';' << 0 << 'H' )
using namespace std;
class coord {
	public:
		int x;
		int y;
		coord() = default;
		coord(int x,int y){
			this->x = x;
			this->y = y;
		}
		bool operator==(coord& rhs){
			return rhs.x==this->x && rhs.y==this->y;
		}
};
struct worm {
	coord pos;
	worm operator=(worm& b)
	{
		pos.x = b.pos.x; pos.y = b.pos.y;
		return b;
	}
	bool operator==(worm& b)
	{
		if ((pos.x == b.pos.x) && (pos.y == b.pos.y)) return 1;
		else return 0;
	}
	void prnt()
	{
		cout << "\033[" << pos.y << ';' << pos.x << 'H';
		cout << "\033[38;5;0;48;5;15mo\033[0m";
	}
	void moveup()
	{
		pos.y--;
	}
	void movedown()
	{
		pos.y++;
	}
	void moveleft()
	{
		pos.x--;
	}
	void moveright()
	{
		pos.x++;
	}
	void nomove()
	{

	}
};
class food {
	private:
		coord pos;
	public:
		coord position() {return coord(this->pos.y,this->pos.x);};
		food(int w, int h) {
			relocate(w, h);
		}
		void relocate(int w, int h) {
			this->pos = coord(rand() % (w - 10) + 2,rand() % (h - 10) + 2);
		}
		void prnt()
		{
			cout << "\033[" << pos.x << ';' << pos.y << 'H';
			cout << "\033[;48;5;15;38;5;9mO\033[0m";
		}
};
int main()
{
	input("Do you want to activate ANSI Mode : y for yes ",answer)
	if(answer=='y'){
		system("REG ADD HKCU\CONSOLE /f /v VirtualTerminalLevel /t REG_DWORD /d 1");
	}
	int lm = 240000;
	int l = 0;
	char LstMovemnt = '0';
	srand(time(0));
	char move = '8';
	int h = 20, w = 30;
	void prntscr(int, int);
	int counting(int);
	void prntscore(int, int, int);
	void losemsg();
	char mode;
	cout << "please enter a or A for hard Mode and b or B for easy mode : ";
	cin >> mode; cout << "\033[2J";
	Clear_screen;
	worm* tab = (worm*)malloc(1 * sizeof(worm));
	tab[0].pos = coord(rand() % (w - 2) + 2,rand() % (h - 2) + 2);
	food food_1 = food(w, h);
	int t_1 = time(0);
	while (true)
	{
		int t = time(0);
		/*Drawing the GUI*/{ 
			prntscr(h, w); // print board
			prntscore(1, w + 3, l); // print score
			food_1.prnt(); // print food
			for (int i = 0; i <= l; i++) // drawing worm
			{
				if (i == 0)
				{
					tab[i].prnt();
					if (LstMovemnt == '8') cout << "\b" << "\033[38;5;0;48;5;15mA\033[0m";
					if (LstMovemnt == '4') cout << "\b" << "\033[38;5;0;48;5;15m<\033[0m";
					if (LstMovemnt == '5') cout << "\b" << "\033[38;5;0;48;5;15mV\033[0m";
					if (LstMovemnt == '6') cout << "\b" << "\033[38;5;0;48;5;15m>\033[0m";
				}
				else  tab[i].prnt();
			}
		}
		/*Porcessing Movement*/{
			if (_kbhit()) { // getting movement
				move = _getch();
				if ((LstMovemnt == '6' && move == '4') || (LstMovemnt == '4' && move == '6')
					|| (LstMovemnt == '8' && move == '5') || (LstMovemnt == '5' && move == '8')) {
					move = LstMovemnt;
				}
				else {
					LstMovemnt = move;
				}
			}
			else {
				move = LstMovemnt;
			}
			worm temp = tab[1];
			for (int i = 1; i <= l; i++) { // propagating movement 
				if (i == 1) {
					temp = tab[i];
					tab[i] = tab[i-1];
				}
				else {
					auto localTemp = tab[i];
					tab[i] = temp;
					temp = localTemp;
				}
			}
			if (move == '5')  tab[0].movedown(); // move 
			else if (move == '8')  tab[0].moveup();
			else if (move == '4')  tab[0].moveleft();
			else if (move == '6')  tab[0].moveright();
		}
		/*Checking End States*/{
			if (tab[0].pos.y == h + 3 || tab[0].pos.y == 1 || tab[0].pos.x == w + 3 || tab[0].pos.x == 1)// chck boundaries
			{
				if (mode == 'a' || mode == 'A')
				{
					printf("\033[2J");
					losemsg();
					break;
				}
				else if (mode == 'b' || mode == 'B')
				{
					if (tab[0].pos.y == h + 3)
						tab[0].pos.y = 1;
					else if (tab[0].pos.y == 1)
						tab[0].pos.y = h + 2;
					else if (tab[0].pos.x == w + 3)
						tab[0].pos.x = 1;
					else if (tab[0].pos.x == 1)
						tab[0].pos.x = w + 2;
				}
			}
			for (int i = 1; i <= l; i++) // check if cannibalism
			{
				if (tab[0] == tab[i]) {
					printf("\033[2J");
					losemsg();
					break;
				}
			}
		}
		/*Checking Feeding*/{
			if (tab[0].pos == food_1.position()) // check if eating
			{
				food_1.relocate(w, h);
				tab = (worm*)realloc(tab, (++l + 1) * sizeof(worm));
				tab[l] = tab[0];
				if (l == 300) { losemsg(); break; } //Cap limit
			}
		}
		/*Post Porcessing Loop*/{
			Clear_screen;
			while (true) // delay the loop
			{
				lm -= l + 5;
				counting(lm);
				break;
			}
		}
	}
	free(tab);
}
void prntscr(int h, int w)
{
	for (int i = 0; i <= h + 5; i++)
	{
		for (int j = 0; j <= w + 5; j++)
		{
			if (j == 0 || i == 0 || j == w + 2 || i == h + 2) cout << " ";
			else if (i <= h + 1 && j <= w + 1 && j > 0 && i > 0) cout << "\033[38;5;0;48;5;15m \033[0m";
		}
		cout << endl;
	}
}
void prntscore(int x, int y, int z)
{
	cout << "\033[" << x + 1 << ';' << y + 1 << 'H';
	cout << z;

}
void losemsg()
{
	cout << "_______________________________\n";
	cout << "|*  *    *******      ******* |\n";
	cout << "|*  *          *            * |\n";
	cout << "|****                         |\n";
	cout << "|              *******        |\n";
	cout << "| *             **** *****    |\n";
	cout << "| *       * *  *       *      |\n";
	cout << "| *      *   *  ****   *      |\n";
	cout << "| *      *   *      *  *      |\n";
	cout << "| * * *   * *   ****   *      |\n";
	cout << "|_____________________________|\n";
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