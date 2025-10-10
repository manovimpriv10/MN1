#include "binpackingFuncs.h"
#include <iostream>
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

void simplePack(const vector<int>& items, int capacity) {
    vector<int> space;
    vector<vector<int>> bins;

    for (int w : items) {
        bool placed = false;
        for (size_t i = 0; i < space.size(); ++i) {
            if (w <= space[i]) {
                space[i] -= w;
                bins[i].push_back(w);
                placed = true;
                break;
            }
        }
        if (!placed) {
            space.push_back(capacity - w);
            bins.push_back({ w });
        }
    }

    cout << "\nРезультат упаковки:\n";
    for (size_t i = 0; i < bins.size(); ++i) {
        cout << "Контейнер " << i + 1 << " (осталось " << space[i] << "): ";
        for (int w : bins[i]) cout << w << " ";
        cout << "\n";
    }
    cout << "Всего контейнеров: " << bins.size() << "\n";
}