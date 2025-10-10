#include "binpackingFuncs.h"
using namespace std;

void readInput(vector<int>& items, int& capacity) {
    cout << "Введите количество предметов и вместимость контейнера:\n";
    int n;
    cin >> n >> capacity;
    items.resize(n);
    cout << "Введите веса предметов:\n";
    for (int i = 0; i < n; ++i) cin >> items[i];
}

void printItems(const vector<int>& items) {
    cout << "Предметы: ";
    for (int w : items) cout << w << " ";
    cout << "\n";
}

vector<Container> firstFitPack(const vector<int>& items, int capacity) {
    vector<Container> bins;

    for (int w : items) {
        bool placed = false;
        for (auto& bin : bins) {
            if (w <= bin.remaining) {
                bin.remaining -= w;
                bin.items.push_back(w);
                placed = true;
                break;
            }
        }
        if (!placed) {
            Container c;
            c.remaining = capacity - w;
            c.items.push_back(w);
            bins.push_back(c);
        }
    }
    return bins;
}

void printResult(const vector<Container>& bins) {
    cout << "\nРезультат упаковки:\n";
    for (size_t i = 0; i < bins.size(); ++i) {
        cout << "Контейнер " << i + 1 << " (осталось " << bins[i].remaining << "): ";
        for (int w : bins[i].items) cout << w << " ";
        cout << "\n";
    }
    cout << "Всего контейнеров: " << bins.size() << "\n";
}
